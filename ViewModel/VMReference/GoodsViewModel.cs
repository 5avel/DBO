using System.Windows.Input;
using DBO.ViewModel.MVVMLib;
using System.Collections.ObjectModel;
using DBO.DataModel;
using Model;
using Model.DAL;
using System.Windows;

namespace DBO.ViewModel
{
    public class GoodsViewModel : ViewModelBase
    {
        #region Public Properties
        private ObservableCollection<Good> goodsCollection;  // ТОвары выбранной группы
        public ObservableCollection<Good> GoodsCollection
        {
            get { return goodsCollection; }
            set
            {
                goodsCollection = value;
                OnPropertyChanged("GoodsCollection");
            }
        }


        private Group selectedGroup; // Выбранная группа
        public Group SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                IsSelectedGroupe = selectedGroup != null;
                OnPropertyChanged("SelectedGroup");
            }
        }

        private bool isSelectedGood;
        public bool IsSelectedGood
        {
            get { return isSelectedGood; }
            set
            {
                isSelectedGood = value;
                OnPropertyChanged("IsSelectedGood");
            }
        }

        private Good selectedGood; // Выбранный товар
        public Good SelectedGood
        {
            get { return selectedGood; }
            set
            {
                selectedGood = value;

                if (value == null) IsSelectedGood = false;
                else IsSelectedGood = true;
                //RaisePropertyChanged(() => SelectedGood);
            }
        }

        private ObservableCollection<Group> goodsGroupeCollection; // Группы товаров
        public ObservableCollection<Group> GoodsGroupeCollection
        {
            get { return goodsGroupeCollection; }
            set
            {
                goodsGroupeCollection = value;

            }
        }

        private bool isSelectedGroupe;
        public bool IsSelectedGroupe
        {
            get { return isSelectedGroupe; }
            set { isSelectedGroupe = value;
                OnPropertyChanged("IsSelectedGroupe");

            }
        }



        #endregion Public Properties

        public GoodsViewModel() // КОНСТРУКТОР
        {
            GoodsGroupeCollection = new ObservableCollection<Group>(new GroupsProvider().GetAllGoups());
        }


        private RelayCommand<Group> someCommand;
        public ICommand SomeCommand
        {
            get
            {
                return someCommand ?? (someCommand = new RelayCommand<Group>((param)=> 
                {
                    SelectedGroup = param;
                }));
            }
        }
    }
}
