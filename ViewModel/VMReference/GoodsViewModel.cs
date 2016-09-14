using System.Collections.Generic;
using System.Windows.Input;
using DBO.ViewModel.MVVMLib;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
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

        private List<GroupVM> _parenGroups;
        public List<GroupVM> ParenGroups
        {
            get { return _parenGroups ?? (_parenGroups = new List<GroupVM>()); }
            set
            {
                _parenGroups = value;
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

        private GroupVM _curentGroupe;

        public GroupVM CurentGroupe
        {
            get { return _curentGroupe ?? (_curentGroupe = new GroupVM()); }
            set
            {
                _curentGroupe = value;
                OnPropertyChanged();
            }
        }

        private GroupVM _curentGroupeParent;

        public GroupVM CurentGroupeParent
        {
            get { return _curentGroupeParent; }
            set
            {
                _curentGroupeParent = ParenGroups.Where((x => x.ID == value?.ID )).SingleOrDefault();
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

        private bool _isAddingGroup; // Состояние добавления группы
        public bool IsAddingGroup
        {
            get { return _isAddingGroup; }
            private set
            {
                _isAddingGroup = value;
                IsEditingGroup =  !_isAddingGroup;
                OnPropertyChanged();
            }
        }

        private bool _isEditingGroup; // Состояние добавления группы
        public bool IsEditingGroup
        {
            get { return _isEditingGroup; }
            private set
            {
                _isEditingGroup = value;
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
                    IsAddingGroup = true;
                    IsShowGroupFlayout = true;
                    ParenGroups = TreeToList(GoodsGroupeCollection);
                    CurentGroupe = new GroupVM();
                }));
            }
        }

        public List<GroupVM> TreeToList(IList<GroupVM> tree, string level = "")
        {
            List<GroupVM> list = new List<GroupVM>();
            foreach (GroupVM item in tree)
            {
                item.NameForList = level + item.Name;
                list.Add(item);
                if (item.ChildrenGroups.Count > 0)
                {
                    list.AddRange(TreeToList(item.ChildrenGroups, level += "    "));
                    level = "";
                }
            }
            return list;
        }

        private ICommand _addNewGroupCommand;
        public ICommand AddNewGroupCommand
        {
            get
            {
                return _addNewGroupCommand ?? (_addNewGroupCommand = new RelayCommand((param) =>
                       {
                           Group grp = CurentGroupe.ToGroup();
                           grp.ParentId = CurentGroupeParent?.ToGroup().ID;
                           new GroupsProvider().AddGoup(grp);
                           IsShowGroupFlayout = false;
                           LoadGroupCommand.Execute(null);
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
                        
                        IsAddingGroup = false;
                        ParenGroups = TreeToList(GoodsGroupeCollection); // заполняем коллекцию родителей для выбора
                        
                        CurentGroupe = param as GroupVM;
                        ParenGroups.Remove(ParenGroups.SingleOrDefault(x => x.ID == CurentGroupe.ID)); // удоляем текущий элемент из коллекции родителей
                        CurentGroupeParent = CurentGroupe.Parent;
                        IsShowGroupFlayout = true;
                    },
                    (param) => param != null));
            }
        }

        private ICommand _updateGroupCommand;
        public ICommand UpdateGroupCommand
        {
            get
            {
                return _updateGroupCommand ?? (_updateGroupCommand = new RelayCommand((param) =>
                {
                    Group grp = CurentGroupe.ToGroup();
                    grp.ParentId = CurentGroupeParent?.ToGroup().ID;
                    
                    new GroupsProvider().UpdateGoup(grp);

                    IsShowGroupFlayout = false;
                    LoadGroupCommand.Execute(null);
                }));
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
