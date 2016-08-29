using System.Windows.Input;
using DBO.ViewModel.MVVMLib;
using System.Collections.ObjectModel;
using DBO.ViewModel.ViewDataModel;
using DBO.Model.DAL;
using DBO.Model.DataModel;

namespace DBO.ViewModel
{
    public class GoodsGroupsViewModel : ViewModelBase
    {
        #region Properties
        #region Groups Properties

        private GroupVM _selectedGroup; // Выбранная группа
        public GroupVM SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<GroupVM> _goodsGroupeCollection; // Коллекция Групп товаров
        public ObservableCollection<GroupVM> GoodsGroupeCollection
        {
            get { return _goodsGroupeCollection ?? (_goodsGroupeCollection = new ObservableCollection<GroupVM>()); }
            set
            {
                _goodsGroupeCollection = value;
                OnPropertyChanged();
            }
        }


        private bool _isShowGroupFlayout; // Состояние добавления группы
        public bool IsShowGroupFlayout
        {
            get { return _isShowGroupFlayout; }
            set
            {
                _isShowGroupFlayout = value;
                OnPropertyChanged();
            }
        }

        #endregion Groups Properties

        #region Goods Properties

        #endregion Goods Properties

        #endregion Properties

        public GoodsGroupsViewModel() // КОНСТРУКТОР
        {
           LoadGroupCommand.ExecuteAsync(null);
        }

        #region Commands

        #region Groups Commands


        private IAsyncCommand _loadGroupCommand;
        public IAsyncCommand LoadGroupCommand
        {
            get
            {
                return _loadGroupCommand ?? (_loadGroupCommand = AsyncCommand.Create(
                    async () =>
                    {
                        GoodsGroupeCollection.Clear();
                        var groups = await new GroupsProvider().GetAllGoupsAsync();
                        foreach (Group item in groups)
                        {
                            GoodsGroupeCollection.Add(GroupVM.CopyTreeChildren(item));
                        }
                    }
                ));
            }
        }

        private RelayCommand<GroupVM> _selectionChangedGroupCommand;
        public ICommand SelectionChangedGroupCommand
        {
            get
            {
                return _selectionChangedGroupCommand ?? (_selectionChangedGroupCommand = new RelayCommand<GroupVM>((param) =>
                {
                    SelectedGroup = param;
                },
                null
                ));
            }
        }

        private ICommand _addingGroupCommand;
        public ICommand AddingGroupCommand
        {
            get
            {
                return _addingGroupCommand ?? (_addingGroupCommand = new RelayCommand((param) =>
                {

                }));
            }
        }

        private ICommand _editingGroupCommand;
        public ICommand EditingGroupCommand
        {
            get
            {
                return _editingGroupCommand ?? (_editingGroupCommand = new RelayCommand(
                    (param) =>
                    { // Действие комманды

                    },
                    (param) => param != null));
            }
        }

        //private IAsyncCommand _removeGroupCommand;
        //public IAsyncCommand RemoveGroupCommand
        //{
        //    get
        //    {
        //        return _removeGroupCommand ?? (_removeGroupCommand = AsyncCommand.Create(
        //             async () =>
        //             {
        //                 await new GroupsProvider().RemoveGoupAsync(SelectedGroup.ToGroup());
        //                 await LoadGroupCommand.ExecuteAsync(null);
        //             }
        //        ));
        //    }
        //}

        private RelayCommand<GroupVM> _removeGroupCommand;
        public ICommand RemoveGroupCommand
        {
            get
            {
                return _removeGroupCommand ?? (_removeGroupCommand = new RelayCommand<GroupVM>(
                     async (param) =>
                     {
                         await new GroupsProvider().RemoveGoupAsync(param.ToGroup());
                         await LoadGroupCommand.ExecuteAsync(null);
                     },
                     (param) => param != null ));
            }
        }

        #endregion Groups Commands

        #region Goods Commands

        #endregion Goods Commands

        #endregion Commands
    }
}
