using System.Windows.Input;
using DBO.ViewModel.MVVMLib;

namespace DBO.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            
        }

        private string _frameSource;
        public string FrameSource
        {
            get { return _frameSource; }
            set
            {
                _frameSource = value;
                OnPropertyChanged();
            }
        }

        private ICommand _setMainFrameSourceCommand;
        public ICommand SetMainFrameSourceCommand
        {
            get
            {
                return _setMainFrameSourceCommand ?? (_setMainFrameSourceCommand = new RelayCommand((param) =>
                {
                    FrameSource = (string)param;
                }));
            }
        }
    
    }
}
