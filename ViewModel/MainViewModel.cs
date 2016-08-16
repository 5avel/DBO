using System.Windows.Input;
using DBO.ViewModel.MVVMLib;

namespace DBO.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            FrameSource = "Reference.xaml"; // вид при старте
        }
        private string frameSource;

        public string FrameSource
        {
            get { return frameSource; }
            set
            {
                frameSource = value;
                OnPropertyChanged(() => FrameSource);
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

        private ICommand _windowsReference;
        public ICommand WindowsReference
        {
            get
            {
                return _windowsReference ?? (_windowsReference = new RelayCommand(() =>
                {
                    FrameSource = "Reference.xaml";
                }));
            }
        }

        private ICommand _windowsIpCameras;

        public ICommand WindowsIpCameras
        {
            get
            {
                return _windowsIpCameras ?? (_windowsIpCameras = new RelayCommand(() =>
                {
                    FrameSource = "IpCameras.xaml";
                }));
            }
        }
    
    }
}
