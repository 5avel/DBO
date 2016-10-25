using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DBO.Model.DataModel
{
    public class BaseDataModel : IDataErrorInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int? ID { set; get; }



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



    }
}
