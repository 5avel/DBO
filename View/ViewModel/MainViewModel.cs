using System;

using System.Timers;
using System.Windows.Input;
using DBO.ViewModel.MVVMLib;

namespace DBO.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Timer timer;
        public MainViewModel()
        {
            timer = new Timer(500);
            timer.Elapsed += Callback;
            timer.Start();
        }

        private void Callback(object sender, ElapsedEventArgs e)
        {
            UsedMemory = GC.GetTotalMemory(true)/1024;
        }

        private long _usedMemory;
        public long UsedMemory
        {
            get { return _usedMemory; }
            set
            {
                _usedMemory = value;
                OnPropertyChanged();
            }
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
