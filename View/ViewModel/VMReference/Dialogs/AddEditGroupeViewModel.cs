﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DBO.Model.DAL;
using DBO.ViewModel.MVVMLib;
using DBO.Model.DataModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DBO.ViewModel.VMReference.Dialogs
{
    public class AddEditGroupeViewModel : ViewModelDialogBase, IDataErrorInfo
    {
        public AddEditGroupeViewModel(IList<Group> Parents, string windowTitle = "", Group grp = null )
        {
            isAdding = grp == null;
            CurentGroup = grp ?? new Group();
            Parent = CurentGroup.Parent;
            GroupeParents = Parents;
        }

        private bool isAdding = false;

        private Group _groupe;
        public Group CurentGroup
        {
            get { return _groupe; }
            set
            {
                _groupe = value;
                OnPropertyChanged();
            }
        }

        private Group _parent;
        public Group Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                OnPropertyChanged();
            }
        }


        private IList<Group> _groupeParents;
        public IList<Group> GroupeParents
        {
            get { return _groupeParents; }
            set
            {
                _groupeParents = value;
                Parent = GroupeParents?.SingleOrDefault(x => x.ID == CurentGroup?.ParentId);
                OnPropertyChanged();
            }
        }

        private ICommand _okCommand;
        public ICommand OkCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new RelayCommand(
                    param =>
                        {
                            CurentGroup.ParentId = Parent?.ID;
                            if(isAdding) new GroupsProvider().AddGoup(CurentGroup); 
                            else new GroupsProvider().UpdateGoup(CurentGroup);

                            Close();
                        }
                ));
            }
        }

        private ICommand _removeParentGroupCommand;
        public ICommand RemoveParentGroupCommand
        {
            get
            {
                return _removeParentGroupCommand ?? (_removeParentGroupCommand = new RelayCommand(param =>Parent = null));
            }
        }

        private ICommand _closeCommand;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand(param => Close()));
            }
        }

        #region IDataErrorInfo implementation 

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                return OnValidate(propertyName);
            }
        }

        protected virtual string OnValidate(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new ArgumentException("Property may not be null or empty", propertyName);

            string error = string.Empty;

            var value = this.GetType().GetProperty(propertyName).GetValue(this, null);
            var results = new List<ValidationResult>();

            var context = new ValidationContext(this, null, null) { MemberName = propertyName };

            var result = Validator.TryValidateProperty(value, context, results);

            if (!result)
            {
                var validationResult = results.First();
                error = validationResult.ErrorMessage;
            }
            return error;
        }

        #endregion IDataErrorInfo implementation

    }
}
