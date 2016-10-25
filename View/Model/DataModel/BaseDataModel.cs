using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DBO.Model.DataModel
{
    public class BaseDataModel : IDataErrorInfo, INotifyPropertyChanged
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int? ID { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;


        // check for general model error
        public string Error { get { return null; } }

        // check for property errors
        public string this[string columnName]
        {
            get
            {
                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                        GetType().GetProperty(columnName).GetValue(this)
                        , new ValidationContext(this)
                        {
                            MemberName = columnName
                        }
                        , validationResults))
                    return null;

                return validationResults.First().ErrorMessage;
            }
        }

        /// <summary>
        /// Новый OnPropertyChanged которому вообще не нужно передавать ни свойство ни название свойства)))
        /// </summary>
        /// <param name="propertyName"> Заполняется автоматически названием вызываюшего члена!</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
