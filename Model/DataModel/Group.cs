using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBO.Model.DataModel
{
    public class Group : BaseDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ID { set; get; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual IList<Group> ChildrenGroups { get; set; }
        

        [Required] // Обязательный
        [StringLength(32, MinimumLength = 5)]
        public string Name { set; get; }
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
