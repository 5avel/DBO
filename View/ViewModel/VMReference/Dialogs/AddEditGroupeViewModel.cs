using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DBO.Model.DAL;
using DBO.ViewModel.MVVMLib;
using DBO.ViewModel.ViewDataModel;

namespace DBO.ViewModel.VMReference.Dialogs
{
    public class AddEditGroupeViewModel : ViewModelBase
    {
        private GroupVM _groupe;
        public GroupVM Groupe
        {
            get { return _groupe; }
            set
            {
                _groupe = value;
                OnPropertyChanged();
            }
        }

            
        private IList<GroupVM> _groupeParents;
        public IList<GroupVM> GroupeParents
        {
            get { return _groupeParents; }
            set
            {
                _groupeParents = value;
                OnPropertyChanged();
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
