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
                OnPropertyChanged(() => ReferenceFrameSource);
            }
        }

        #endregion Public Properties

        public ReferenceViewModel() // КОНСТРУКТОР
        {
            ReferenceFrameSource = "ViewsReference/ViewGroupsGoodsPage.xaml"; // вид при старте
        }


        private ICommand groupsGoodsPage;
        public ICommand GroupsGoodsPage
        {
            get
            {
                return groupsGoodsPage ?? (groupsGoodsPage = new RelayCommand(() =>
                {
                    ReferenceFrameSource = "ViewsReference/ViewGroupsGoodsPage.xaml";
                }));
            }
        }


    }
}
