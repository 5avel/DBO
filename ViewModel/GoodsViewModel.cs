using System.Windows.Input;
using DBO.ViewModel.MVVMLib;
using System.Collections.ObjectModel;
using Model.DataModels;
using Model;
using Model.DAL;

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
            set
            {
                isSelectedGroupe = value;
                OnPropertyChanged("IsSelectedGroupe");
            }
        }

        private Group selectedGroup; // Выбранная группа
        public Group SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                //cm.SelectedGroup = value;
                //SetGoodsSelectedGrop(); // Выводит товары выбранной группы
                if (value == null) IsSelectedGroupe = false;
                else IsSelectedGroupe = true;
                OnPropertyChanged("SelectedGroup");
            }
        }

        #endregion Public Properties

        public GoodsViewModel() // КОНСТРУКТОР
        {
            GoodsGroupeCollection = new ObservableCollection<Group>(new GroupsProvider().GetAllGoups());
            
        }

    }
}
