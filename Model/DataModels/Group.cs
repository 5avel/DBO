using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DataModels
{
    public class Group :BaseDataModel
    {
        [Key]
        public virtual Group Parent { set; get; }

        [Required] // Обязательный
        [StringLength(32, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 32 символов")]
        [Display(Name = "Наименование")]
        public string Name { set; get; }

        public ICollection<Good> Goods { get; set; }
        public Group()
        {
            Goods = new List<Good>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
