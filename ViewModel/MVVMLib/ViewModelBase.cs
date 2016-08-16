using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace DBO.ViewModel.MVVMLib
{
    /// <summary>
    /// Баззовый класс для ВьюМоделей
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> changedProperty)
        {
            if (PropertyChanged != null)
            {
                string propName = ((MemberExpression)changedProperty.Body).Member.Name;
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
