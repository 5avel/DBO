using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBO.Model.DataModel
{
    public class Group : BaseDataModel
    {
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual IList<Group> ChildrenGroups { get; set; }
        public virtual Group Parent { get; set; }

        [Required] // Обязательный
        [StringLength(32, MinimumLength = 5)]
        [Display(Name = "Название группы")]
        public string Name { set; get; }

        [NotMapped]
        public string NameForList { set; get; }
        public bool IsExpanded { set; get; }
        public bool IsSelected { set; get; }

        public virtual IList<Good> Goods { get; set; }
        public Group()
        {
            ChildrenGroups = new List<Group>();
            Goods = new List<Good>();
        }

    }
}
