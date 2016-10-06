using System.Windows.Input;
using DBO.ViewModel.MVVMLib;

namespace DBO.ViewModel
{
    public class ReferenceViewModel : ViewModelBase
    {
        #region Public Properties


        private string referenceFrameSource;

        public string ReferenceFrameSource
        {
            get { return referenceFrameSource; }
            set
            {
                referenceFrameSource = value;
                OnPropertyChanged();
            }
        }

        #endregion Public Properties

        public ReferenceViewModel() // КОНСТРУКТОР
        {
           
        }


        private ICommand _setReferenceFrameSourceCommand;
        public ICommand SetReferenceFrameSourceCommand
        {
            get
            {
                return _setReferenceFrameSourceCommand ?? (_setReferenceFrameSourceCommand = new RelayCommand((param) =>
                {
                    ReferenceFrameSource = (string)param;
                }));
            }
        }


    }
}
