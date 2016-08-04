using System.Windows.Input;
using DBO.ViewModel.MVVMLib;

namespace DBO.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            FrameSource = "ViewGoodsPage.xaml"; // вид при старте
        }
        private string frameSource;

        public string FrameSource
        {
            get { return frameSource; }
            set
            {
                frameSource = value;
                OnPropertyChanged("FrameSource");
            }
        }


        private ICommand _windowsSales;
        public ICommand WindowsSales
        {
            get
            {
                return _windowsSales ?? (_windowsSales = new RelayCommand(() =>
                {
                    FrameSource = "ViewOperationsPage.xaml";
                }));
            }
        }

        private ICommand _windowsGoods;
        public ICommand WindowsGoods
        {
            get
            {
                return _windowsGoods ?? (_windowsGoods = new RelayCommand(() =>
                {
                    FrameSource = "ViewGoodsPage.xaml";
                }));
            }
        }
    
    }
}
