using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DBO.Model.DAL;
using DBO.ViewModel.MVVMLib;
using DBO.ViewModel.VMReference.Dialogs;
using DBO.Model.DataModel;
using System.Windows;

namespace DBO.ViewModel
{
    public class GoodsGroupsViewModel : ViewModelBase
    {
        public GoodsGroupsViewModel() // КОНСТРУКТОР
        {
            LoadGroupCommand.ExecuteAsync(null); // вызов комманды загрузки из БД списка групп
        }

        #region Filds

        private Group _selectedGroup; // Выбранная группа
        private List<Group> _parenGroupCollection; // Список груп родителей для редактируемой группы или товара
        private ObservableCollection<Group> _groupCollection; // Коллекция Групп товаров


        private IAsyncCommand _loadGroupCommand; // Асинхронная загрузка Груп товаров
        private RelayCommand<Group> _selectionChangedGroupCommand; // Команда для изменения выбранной группы
        private ICommand _addingGroupCommand; // Начало Добавления новой группы 
        private ICommand _addNewGroupCommand; // Добавление новой группы
        private ICommand _editingGroupCommand; // Начало Редактирование выбранной группы
        private ICommand _updateGroupCommand; // Сохранение после Редактирования выбранной группы

        private RelayCommand<Group> _removeGroupCommand; // Команда для удаления выбранной группы
        private ICommand _removeParentGroupCommand; // Убрать родителей у выбранной группы

        #endregion Filds

        #region Properties

        public Group SelectedGroup // Выбранная группа
        {
            get { return _selectedGroup; }
            set { _selectedGroup = value; OnPropertyChanged(); }
        }

        public List<Group> ParenGroups
        {
            get { return _parenGroupCollection ?? (_parenGroupCollection = new List<Group>()); }
            set
            {
                _parenGroupCollection = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Group> GoodsGroupeCollection
        {
            get { return _groupCollection ?? (_groupCollection = new ObservableCollection<Group>()); }
            set
            {
                _groupCollection = value;
                OnPropertyChanged();
            }
        }

        #endregion Properties

        #region Private static Methods

        /// <summary>
        /// Рекурсивный метод, преаьразующий дерево в список с пробелами для отображения вложенности
        /// </summary>
        /// <param name="tree"> List<GroupVM> </param>
        /// <param name="level"> уровень вложенности. для внутренней реализации рекурсии.</param>
        /// <returns> List<GroupVM> </returns>
        private static List<Group> TreeToList(IEnumerable<Group> tree, int idToSkip = 0, string level = "")
        {
            var list = new List<Group>();
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
     
        public IAsyncCommand LoadGroupCommand
        {
            get
            {
                return _loadGroupCommand ?? (_loadGroupCommand = AsyncCommand.Create(
                           async () =>
                           {
                               var groups = await new GroupsProvider().GetAllGoupsAsync();
                               GoodsGroupeCollection = new ObservableCollection<Group>(groups);
                           }
                       ));
            }
        }

        public ICommand SelectionChangedGroupCommand
        {
            get
            {
                return _selectionChangedGroupCommand ??
                       (_selectionChangedGroupCommand = new RelayCommand<Group>(param => SelectedGroup = param, null));
            }
        }

        public ICommand AddingGroupCommand
        {
            get
            {
                return _addingGroupCommand ?? (_addingGroupCommand = new RelayCommand(param =>
                       {
                           //IsAddingGroup = true;
                           //IsShowGroupFlayout = true;
                           //NewOrEditingGroupe = new GroupVM();
                           ParenGroups = TreeToList(GoodsGroupeCollection);
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
                               var item = param as Group;
                               if (item == null) return;

                               var child = new AddEditGroupeViewModel(TreeToList(GoodsGroupeCollection, item.ID ?? 0), "Редактирование Группы товаров", item);
                               
                               child.DialogWindowClosingEvent += () => LoadGroupCommand.Execute(null);
                               child.Show(true);                               
                           }, param => param != null));
            }
        }

        /// <summary>
        /// Команда для удоления выбранной группы
        /// </summary>
        public ICommand RemoveGroupCommand
        {
            get
            {
                return _removeGroupCommand ?? (_removeGroupCommand = new RelayCommand<Group>(
                           async param =>
                           {
                               await new GroupsProvider().RemoveGoupAsync(param);
                               await LoadGroupCommand.ExecuteAsync(null);
                           },
                           // TODO можно удолить только если нет подгруп и товаров!
                           param => param != null && param.ChildrenGroups.Count == 0 && param.Goods.Count == 0));
            }
        }

        #endregion Commands
    }
}
