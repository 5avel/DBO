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
        public AddEditGroupeViewModel()
        {
           
        }

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
                _parent = GroupeParents?.SingleOrDefault(x => x.ID == value?.ID);
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
                            using (var db = new DBODataContext())
                            {
                                CurentGroup.Parent = Parent;
                                db.Groups.Update(CurentGroup);
                                db.SaveChanges();
                                
                            }
                        }
                ));
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
