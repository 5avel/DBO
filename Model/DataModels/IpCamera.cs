using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModels
{
    public class IpCamera : BaseDataModel
    {
        [Required] // Обязательный
        [StringLength(32, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 32 символов")]
        [Display(Name = "Название торговой точки")]
        public string Name { set; get; }

        [Required]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 255 символов")]
        [Display(Name = "IP адресс камеры")]
        public string Cam_IpAddress { set; get; }


        [StringLength(32, MinimumLength = 0, ErrorMessage = "Длина строки должна быть от 5 до 32 символов")]
        [Display(Name = "Логин")]
        public string Cam_Login { set; get; }

        [StringLength(255, MinimumLength = 0, ErrorMessage = "Длина строки должна быть от 5 до 255 символов")]
        [Display(Name = "Пароль")]
        public string Cam_Password { set; get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
