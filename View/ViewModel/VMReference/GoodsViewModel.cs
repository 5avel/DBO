using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DBO.Model.DAL;
using DBO.ViewModel.MVVMLib;
using DBO.ViewModel.ViewDataModel;

namespace DBO.ViewModel
{
    public class GoodsGroupsViewModel : ViewModelBase
    {
        public GoodsGroupsViewModel() // КОНСТРУКТОР
        {
            LoadGroupCommand.ExecuteAsync(null); // вызов комманды загрузки из БД списка групп
        }

        #region Groups Privat Filds

        private GroupVM _selectedGroup; // Выбранная группа
        private List<GroupVM> _parenGroupCollection; // Список груп родителей для редактируемой группы или товара
        private ObservableCollection<GroupVM> _groupCollection; // Коллекция Групп товаров

        private GroupVM _newOrEditingGroupe; // Редактируемая или создаваемая группа
        private GroupVM _newOrEditingGroupeParent; // Родитель редактируемой или создаваемой группы

        private bool _isShowGroupFlayout; // Отображать флайаут для создания или редактирования группы
        private bool _isAddingGroup; // Состояние добавления группы
        private bool _isEditingGroup; // Состояние редактированиея группы

        private IAsyncCommand _loadGroupCommand;
        private RelayCommand<GroupVM> _selectionChangedGroupCommand;
        private ICommand _addingGroupCommand;
        private ICommand _addNewGroupCommand;
        private ICommand _editingGroupCommand;
        private ICommand _updateGroupCommand;

        private RelayCommand<GroupVM> _removeGroupCommand; // Команда для удаления выбранной группы
        private ICommand _removeParentGroupCommand;
        #endregion Groups  Privat Filds

        #region Properties

        #region Groups Properties
        public GroupVM SelectedGroup // Выбранная группа
        {
            get { return _selectedGroup; }
            set { _selectedGroup = value; OnPropertyChanged(); }
        }

        public List<GroupVM> ParenGroups
        {
            get { return _parenGroupCollection ?? (_parenGroupCollection = new List<GroupVM>()); }
            set
            {
                _parenGroupCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GroupVM> GoodsGroupeCollection
        {
            get { return _groupCollection ?? (_groupCollection = new ObservableCollection<GroupVM>()); }
            set
            {
                _groupCollection = value;
                OnPropertyChanged();
            }
        }

        public GroupVM NewOrEditingGroupe
        {
            get { return _newOrEditingGroupe ?? (_newOrEditingGroupe = new GroupVM()); }
            set
            {
                _newOrEditingGroupe = value;
                OnPropertyChanged();
            }
        }

        public GroupVM NewOrEditingGroupeParent
        {
            get { return _newOrEditingGroupeParent; }
            set
            {
                _newOrEditingGroupeParent = ParenGroups.SingleOrDefault(x => x.ID == value?.ID);
                OnPropertyChanged();
            }
        }

        public bool IsShowGroupFlayout
        {
            get { return _isShowGroupFlayout; }
            set
            {
                _isShowGroupFlayout = value;
                OnPropertyChanged();
            }
        }
        public bool IsAddingGroup
        {
            get { return _isAddingGroup; }
            private set
            {
                _isAddingGroup = value;
                IsEditingGroup = !_isAddingGroup;
                OnPropertyChanged();
            }
        }
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

        #region Private static Methods

        /// <summary>
        /// Рекурсивный метод, преаьразующий дерево в список с пробелами для отображения вложенности
        /// </summary>
        /// <param name="tree"> List<GroupVM> </param>
        /// <param name="level"> уровень вложенности. для внутренней реализации рекурсии.</param>
        /// <returns> List<GroupVM> </returns>
        private static List<GroupVM> TreeToList(IEnumerable<GroupVM> tree, int idToSkip = 0, string level = "")
        {
            var list = new List<GroupVM>();
            foreach (var item in tree)
            {
                if (item.ID == idToSkip) continue;
                item.NameForList = level + item.Name;
                list.Add(item);
                if (item.ChildrenGroups.Count <= 0) continue;
                list.AddRange(TreeToList(item.ChildrenGroups, idToSkip, level += "    "));
                level = "";
            }
            return list;
        }

        #endregion Private Methods

        #region Commands

            #region Groups Commands
     
        public IAsyncCommand LoadGroupCommand
        {
            get
            {
                return _loadGroupCommand ?? (_loadGroupCommand = AsyncCommand.Create(
                           async () =>
                           {
                               GoodsGroupeCollection.Clear();
                               var groups = await new GroupsProvider().GetAllGoupsAsync();
                               foreach (var item in groups)
                                   GoodsGroupeCollection.Add(GroupVM.CopyTreeChildren(item));
                           }
                       ));
            }
        }

        public ICommand SelectionChangedGroupCommand
        {
            get
            {
                return _selectionChangedGroupCommand ??
                       (_selectionChangedGroupCommand = new RelayCommand<GroupVM>(param => { SelectedGroup = param; },
                           null
                       ));
            }
        }

        public ICommand AddingGroupCommand
        {
            get
            {
                return _addingGroupCommand ?? (_addingGroupCommand = new RelayCommand(param =>
                       {
                           IsAddingGroup = true;
                           IsShowGroupFlayout = true;
                           NewOrEditingGroupe = new GroupVM();
                           ParenGroups = TreeToList(GoodsGroupeCollection);
                       }));
            }
        }

        public ICommand AddNewGroupCommand
        {
            get
            {
                return _addNewGroupCommand ?? (_addNewGroupCommand = new RelayCommand(param =>
                       {
                           var grp = NewOrEditingGroupe.ToGroup();
                           grp.ParentId = NewOrEditingGroupeParent?.ToGroup().ID;
                           new GroupsProvider().AddGoup(grp);
                           IsShowGroupFlayout = false;
                           LoadGroupCommand.Execute(null);
                       }));
            }
        }

        public ICommand EditingGroupCommand
        {
            get
            {
                return _editingGroupCommand ?? (_editingGroupCommand = new RelayCommand(
                           param =>
                           {
                               // Действие комманды
                               IsAddingGroup = false;
                               NewOrEditingGroupe = param as GroupVM;

                               ParenGroups = TreeToList(GoodsGroupeCollection, NewOrEditingGroupe?.ID ?? 0);
                                   // заполняем коллекцию родителей для выбора
                               //ParenGroups.Remove(ParenGroups.SingleOrDefault(x => x.ID == NewOrEditingGroupe.ID));
                                   // удоляем текущий элемент из коллекции родителей

                               // TODO: нужно рекурсивно удолить всех детей из коллекции родителей, для редактируемой группы

                               // TODO:  

                               NewOrEditingGroupeParent = NewOrEditingGroupe?.Parent;
                               IsShowGroupFlayout = true;
                           },
                           param => param != null));
            }
        }

        public ICommand UpdateGroupCommand
        {
            get
            {
                return _updateGroupCommand ?? (_updateGroupCommand = new RelayCommand(param =>
                       {
                           var grp = NewOrEditingGroupe.ToGroup();
                           grp.ParentId = NewOrEditingGroupeParent?.ToGroup().ID;

                           new GroupsProvider().UpdateGoup(grp);

                           IsShowGroupFlayout = false;
                           LoadGroupCommand.Execute(null);
                       }));
            }
        }
        /// <summary>
        /// Команда для удоления выбранной группы
        /// </summary>
        public ICommand RemoveGroupCommand
        {
            get
            {
                return _removeGroupCommand ?? (_removeGroupCommand = new RelayCommand<GroupVM>(
                           async param =>
                           {
                               await new GroupsProvider().RemoveGoupAsync(param.ToGroup());
                               await LoadGroupCommand.ExecuteAsync(null);
                           },
                           // TODO можно удолить только если нет подгруп и товаров!
                           param => param != null));
            }
        }

        public ICommand RemoveParentGroupCommand
        {
            get
            {
                return _removeParentGroupCommand ?? (_removeParentGroupCommand = new RelayCommand(param =>
                       {
                           NewOrEditingGroupeParent = null;
                       },
                           param => NewOrEditingGroupeParent != null));
            }
        }

        #endregion Groups Commands

        #region Goods Commands

        #endregion Goods Commands

        #endregion Commands
    }
}
