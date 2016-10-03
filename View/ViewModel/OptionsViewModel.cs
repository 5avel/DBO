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
            OptionsFrameSource = "ViewsOptions/ViewInterfesPage.xaml"; // вид при старте
        }


        private ICommand _optionsPage;
        public ICommand OptionsPage
        {
            get
            {
                return _optionsPage ?? (_optionsPage = new RelayCommand((param) =>
                {
                    OptionsFrameSource = "ViewsOptions/ViewInterfesPage.xaml";
                }));
            }
        }


    }
}
