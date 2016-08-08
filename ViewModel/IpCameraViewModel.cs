using System.Windows.Input;
using DBO.ViewModel.MVVMLib;
using System.Collections.ObjectModel;
using Model.DataModels;
using Model;
using Model.DAL;
using System.Windows;

namespace DBO.ViewModel
{
    public class IpCameraViewModel : ViewModelBase
    {
        #region Public Properties
        private ObservableCollection<IpCamera> ipCameraCollection;  // Список камер
        public ObservableCollection<IpCamera> IpCameraCollection
        {
            get { return ipCameraCollection; }
            set
            {
                ipCameraCollection = value;
                OnPropertyChanged("IpCameraCollection");
            }
        }

        private bool isSelectedIpCamera;

        public bool IsSelectedIpCamera
        {
            get { return isSelectedIpCamera; }
            set
            {
                isSelectedIpCamera = value;
                OnPropertyChanged("IsSelectedIpCamera");
            }
        }
        #endregion Public Properties

        public IpCameraViewModel()
        {
            IpCameraCollection = new ObservableCollection<IpCamera>(new IpCameraProvider().GetAllIpCamera());
        }


        private ICommand _getSelectedCamera;

        public ICommand GetSelectedCamera;
        {
            get
            {
                return _getSelectedCamera ?? (_getSelectedCamera = new RelayCommand(() =>
                {
                    Window w1 = new Window();
                }));
            }
        }
    }
}