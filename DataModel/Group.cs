using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace DBO.DataModel
{
    public class Group
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

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged(() => IsExpanded);
            }
        }

        public virtual IList<Good> Goods { get; set; }



        public Group()
        {
            ChildrenGroups = new List<Group>();
            Goods = new List<Good>();
        }

        #region MVVM 
        public static event PropertyChangedEventHandler PropertyChanged;



        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> changedProperty)
        {
            if (PropertyChanged != null)
            {
                string name = ((MemberExpression)changedProperty.Body).Member.Name;
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
    }
}
