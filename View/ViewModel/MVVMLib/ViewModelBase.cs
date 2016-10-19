using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using DBO.View;

namespace DBO.ViewModel.MVVMLib
{
    /// <summary>
    /// Баззовый класс для ВьюМоделей
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged<T>(Expression<Func<T>> changedProperty)
        //{
        //    PropertyChangedEventHandler handler = this.PropertyChanged;
        //    if (handler != null)
        //    {
        //        string propName = ((MemberExpression)changedProperty.Body).Member.Name;
        //        handler.Invoke(this, new PropertyChangedEventArgs(propName));
        //    }
        //}

        /// <summary>
        /// Новый OnPropertyChanged которому вообще не нужно передавать ни свойство ни название свойства)))
        /// </summary>
        /// <param name="propertyName"> Заполняется автоматически названием вызываюшего члена!</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Тоже самое 
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));


        /// 
        /// Метод показа ViewModel в окне
        /// 
        /// viewModel">
        protected void Show(ViewModelBase viewModel)
        {
            viewModel._wnd = new DialogView();
            viewModel._wnd.DataContext = viewModel;
            viewModel._wnd.Closed += (sender, e) => Closed();
            viewModel._wnd.Show();
        }

        /// 
        /// Окно в котором показывается текущий ViewModel
        ///
        private DialogView _wnd = null;

        /// 
        /// Заголовок окна
        /// 
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        /// 
        /// Методы вызываемый окном при закрытии
        /// 
        protected virtual void Closed() { }

        /// 
        /// Методы вызываемый для закрытия окна связанного с ViewModel
        /// 
        public bool Close()
        {
            var result = false;
            if (_wnd != null)
            {
                _wnd.Close();
                _wnd = null;
                result = true;
            }
            return result;
        }
    }
}
