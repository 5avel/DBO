using System.Windows.Input;
using DBO.ViewModel.MVVMLib;
using System.Collections.ObjectModel;
using DBO.DataModel;
using Model;
using Model.DAL;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DBO.ViewModel
{

   
    public class GoodsGroupsViewModel : ViewModelBase
    {
        #region Properties
        #region Groups Properties

        private Group selectedGroup; // Выбранная группа
        public Group SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                selectedGroup = value;
                IsSelectedGroupe = selectedGroup != null;
                OnPropertyChanged(() => SelectedGroup);
            }
        }


        private ObservableCollection<Group> goodsGroupeCollection; // Коллекция Групп товаров
        public ObservableCollection<Group> GoodsGroupeCollection
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
            GoodsGroupeCollection = new ObservableCollection<Group>(new GroupsProvider().GetAllGoups());
            Group.PropertyChanged += EntityViewModelPropertyChanged;
            SetGroupBtnState();
        }

        private void EntityViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            new GroupsProvider().UpdateGoup(sender as Group);
        }

        #region Commands

        #region Groups Commands

        private RelayCommand<Group> selectionChangedGroupCommand;
        public ICommand SelectionChangedGroupCommand
        {
            get
            {
                return selectionChangedGroupCommand ?? (selectionChangedGroupCommand = new RelayCommand<Group>((param)=> 
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
