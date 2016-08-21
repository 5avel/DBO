using System.Windows.Input;
using DBO.ViewModel.MVVMLib;
using System.Collections.ObjectModel;
using DBO.ViewModel.ViewDataModel;
using DBO.Model.DAL;
using DBO.Model.DataModel;
using System.Threading.Tasks;
using System.Linq;

namespace DBO.ViewModel
{
    public class GoodsGroupsViewModel : ViewModelBase
    {
        #region Properties
        #region Groups Properties

        private GroupVM selectedGroup; // Выбранная группа
        public GroupVM SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                IsSelectedGroupe = selectedGroup != null;
                OnPropertyChanged(() => SelectedGroup);
            }
        }


        private NotifyTaskCompletion<ObservableCollection<GroupVM>> goodsGroupeCollection; // Коллекция Групп товаров
        public NotifyTaskCompletion<ObservableCollection<GroupVM>> GoodsGroupeCollection
        {
            get { return goodsGroupeCollection; }
            set
            {
                goodsGroupeCollection = value;
            }
        }

        private bool isSelectedGroupe; // Флаг указывающий выбрана ли группа товаров в дереве
        public bool IsSelectedGroupe
        {
            get { return isSelectedGroupe; }
            set
            {
                isSelectedGroupe = value;
                OnPropertyChanged(() => IsSelectedGroupe);
            }
        }

        private bool isShowGroupFlayout; // Состояние добавления группы
        public bool IsShowGroupFlayout
        {
            get { return isShowGroupFlayout; }
            set
            {
                isShowGroupFlayout = value;
                OnPropertyChanged(() => IsShowGroupFlayout);
                if(!isShowGroupFlayout)
                {
                    isAddingGroup = isEditingGroup = false;
                }
                SetGroupBtnState();
            }
        }

        private bool isAddingGroup; // Состояние добавления группы
        public bool IsAddingGroup
        {
            get { return isAddingGroup; }
            set
            {
                isAddingGroup = value;
                OnPropertyChanged(() => IsAddingGroup);
                IsShowGroupFlayout = isAddingGroup;
            }
        }

        private bool isEditingGroup; // Состояние изменения группы
        public bool IsEditingGroup
        {
            get { return isEditingGroup; }
            set
            {
                isEditingGroup = value;
                OnPropertyChanged(() => IsEditingGroup);
                IsShowGroupFlayout = isEditingGroup;
            }
        }


        private bool isBtnAddEnabled; // Флаг указывающий активность кнопки добавления группы товаров
        public bool IsBtnAddEnabled
        {
            get { return isBtnAddEnabled; }
            set
            {
                isBtnAddEnabled = value;
                OnPropertyChanged(() => IsBtnAddEnabled);
            }
        }

        private bool isBtnEditEnabled; // Флаг указывающий активность кнопки редактирования группы товаров
        public bool IsBtnEditEnabled
        {
            get { return isBtnEditEnabled; }
            set
            {
                isBtnEditEnabled = value;
                OnPropertyChanged(() => IsBtnEditEnabled);
            }
        }

        private bool isBtnRemoveEnabled; // Флаг указывающий активность кнопки удаления группы товаров
        public bool IsBtnRemoveEnabled
        {
            get { return isBtnRemoveEnabled; }
            set
            {
                isBtnRemoveEnabled = value;
                OnPropertyChanged(() => IsBtnRemoveEnabled);
            }
        }



        #endregion Groups Properties

        #region Goods Properties

        private ObservableCollection<Good> goodsCollection;  // ТОвары выбранной группы
        public ObservableCollection<Good> GoodsCollection
        {
            get { return goodsCollection; }
            set
            {
                goodsCollection = value;
                OnPropertyChanged(() => GoodsCollection);
            }
        }

        private bool isSelectedGood;
        public bool IsSelectedGood
        {
            get { return isSelectedGood; }
            set
            {
                isSelectedGood = value;
                OnPropertyChanged(() => IsSelectedGood);
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

        #endregion Goods Properties

        #endregion Properties

        public GoodsGroupsViewModel() // КОНСТРУКТОР
        {
            SetGroupBtnState();
            GoodsGroupeCollection = new NotifyTaskCompletion<ObservableCollection<GroupVM>>(GetAllGoupsAsync());
        }

        private async Task<ObservableCollection<GroupVM>> GetAllGoupsAsync()
        {
            ObservableCollection<GroupVM> res = new ObservableCollection<GroupVM>();
            var groups = await new GroupsProvider().GetAllGoupsAsync();
           
            foreach (Group item in groups)
            {
                res.Add(GroupVM.CopyTreeChildren(item));
            }
            return res;
        }

        #region Commands

        #region Groups Commands

        private RelayCommand<GroupVM> selectionChangedGroupCommand;
        public ICommand SelectionChangedGroupCommand
        {
            get
            {
                return selectionChangedGroupCommand ?? (selectionChangedGroupCommand = new RelayCommand<GroupVM>((param)=> 
                {
                    SelectedGroup = param;
                    SetGroupBtnState();
                }));
            }
        }

        private ICommand addingGroupCommand;
        public ICommand AddingGroupCommand
        {
            get
            {
                return addingGroupCommand ?? (addingGroupCommand = new RelayCommand(() =>
                {
                    IsAddingGroup = true;
                    isEditingGroup = false;
                    SetGroupBtnState();
                }));
            }
        }

        private ICommand editingGroupCommand;
        public ICommand EditingGroupCommand
        {
            get
            {
                return editingGroupCommand ?? (editingGroupCommand = new RelayCommand(() =>
                {
                    IsAddingGroup = false;
                    IsEditingGroup = true;
                    SetGroupBtnState();
                }));
            }
        }

        private ICommand removeGroupCommand;
        public ICommand RemoveGroupCommand
        {
            get
            {
                return removeGroupCommand ?? (removeGroupCommand = new RelayCommand(() =>
                {
                    SelectedGroup = null;
                    SetGroupBtnState();
                    AddingGroupCommand.CanExecute(false);
                }));
            }
        }

        private void SetGroupBtnState()
        {
            IsBtnAddEnabled = !isAddingGroup && !isEditingGroup;
            IsBtnEditEnabled = !isAddingGroup && !isEditingGroup && isSelectedGroupe;
            IsBtnRemoveEnabled= !isAddingGroup && !isEditingGroup && isSelectedGroupe;
        }

        #endregion Groups Commands

        #region Goods Commands

        #endregion Goods Commands

        #endregion Commands
    }
}
