using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DBO.Model.DAL;
using DBO.ViewModel.MVVMLib;
using DBO.Model.DataModel;
using DBO.Model;

namespace DBO.ViewModel.VMReference.Dialogs
{
    public class AddEditGroupeViewModel : ViewModelDialogBase
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
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand(param => Close()));
            }
        }

    }
}
