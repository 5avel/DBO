using DBO.ViewModel.MVVMLib;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DBO.Model.DAL;
using DBO.Model.DataModel;

namespace DBO.ViewModel.ViewDataModel
{
    public class GroupVM : ObservableObject
    {
        public virtual int ID { set; get; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual IList<GroupVM> ChildrenGroups { get; set; }


        [Required] // Обязательный
        [StringLength(32, MinimumLength = 5)]
        public string Name { set; get; }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged(() => IsExpanded);
                new GroupsProvider().UpdateGoup(this.ToGroup());
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                OnPropertyChanged(() => IsSelected);
                new GroupsProvider().UpdateGoup(this.ToGroup());
            }
        }

        public Group ToGroup()
        {
            Group g = new Group();
            g.ID = ID;
            g.ParentId = ParentId;
            g.Name = Name;
            g.IsExpanded = IsExpanded;
            g.IsSelected = IsSelected;
            return g;
        }

        public virtual IList<GoodVM> Goods { get; set; }



        public GroupVM()
        {
            ChildrenGroups = new List<GroupVM>();
            Goods = new List<GoodVM>();
        }

        public GroupVM(Group group)
        {
            ID = group.ID;
            ParentId = group.ParentId;

            Name = group.Name;
            isExpanded = group.IsExpanded;
            isSelected = group.IsSelected;
            ChildrenGroups = new List<GroupVM>();
            Goods = new List<GoodVM>();
        }

        public static GroupVM CopyTreeChildren(Group group)
        {
            GroupVM gvm = new GroupVM(group);

            foreach (Group item in group.ChildrenGroups)
            {
                if(item.ChildrenGroups.Count > 0)
                {
                    gvm.ChildrenGroups.Add(CopyTreeChildren(item));
                }
                else
                {
                    gvm.ChildrenGroups.Add(new GroupVM(item));
                }
            }
            return gvm;
        }
    }
}
