using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBO.DataModel
{
    public class Group :BaseDataModel
    {
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual IList<Group> ChildrenGroups { get; set; }
        

        [Required] // Обязательный
        [StringLength(32, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 32 символов")]
        [Display(Name = "Наименование")]
        public string Name { set; get; }

        public virtual IList<Good> Goods { get; set; }
        public Group()
        {
            ChildrenGroups = new List<Group>();
            Goods = new List<Good>();

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
