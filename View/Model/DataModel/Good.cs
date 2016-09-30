using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBO.Model.DataModel
{
    public class Good : BaseDataModel
    {
        [Required] // Обязательный
        [StringLength(32, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 32 символов")]
        [Display(Name = "Наименование")]
        public string Name { set; get; }

        [Required]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 255 символов")]
        [Display(Name = "Полное Наименование")]
        public string FullName { set; get; }


        [StringLength(32, MinimumLength = 0, ErrorMessage = "Длина строки должна быть от 5 до 32 символов")]
        [Display(Name = "Aртикул")]
        public string Article { set; get; }

        [StringLength(255, MinimumLength = 0, ErrorMessage = "Длина строки должна быть от 5 до 255 символов")]
        [Display(Name = "Описание")]
        public string Description { set; get; }

        [Display(Name = "Цена")]
        [Range(minimum: 0.0, maximum: 100000.00, ErrorMessage = "Введите значени в диапазоне от 0.00 до 100000.00")]
        [Column(TypeName = "REAL")]
        public decimal Price { set; get; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
