using DBO.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBO.ViewModel.MVVMLib
{
    public class ViewModelDialogBase : ViewModelBase
    {
        
        /// 
        /// Метод показа ViewModel в окне
        /// 
        /// viewModel">
        public void Show(bool isModal = false)
        {
            this._wnd = new DialogView();
            this._wnd.DataContext = this;
            this._wnd.Closed += (sender, e) => Closed();

            if (isModal) this._wnd.ShowDialog();
            else this._wnd.Show();
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
