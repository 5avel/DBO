using System.Windows.Input;
using DBO.ViewModel.MVVMLib;

namespace DBO.ViewModel
{
    public class OptionsViewModel : ViewModelBase
    {
        #region Public Properties


        private string optionsFrameSource;

        public string OptionsFrameSource
        {
            get { return optionsFrameSource; }
            set
            {
                optionsFrameSource = value;
                OnPropertyChanged();
            }
        }

        #endregion Public Properties

        public OptionsViewModel() // КОНСТРУКТОР
        {
           
        }


        private ICommand _setOptionsFrameSourceCommand;
        public ICommand SetOptionsFrameSourceCommand
        {
            get
            {
                return _setOptionsFrameSourceCommand ?? (_setOptionsFrameSourceCommand = new RelayCommand((param) =>
                {
                    OptionsFrameSource = (string)param;
                }
                //,param => optionsFrameSource != (string)param
                ));
            }
        }


    }
}
