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

        private string ipCamSource;

        public string IPCameraSource
        {
            get { return ipCamSource; }
            set
            {
                ipCamSource = value;
                OnPropertyChanged("IPCameraSource");
            }
        }

        private IpCamera selectedIpCamera;

        public IpCamera SelectedIpCamera
        {
            get { return selectedIpCamera; }
            set
            {
                selectedIpCamera = value;
                IPCameraSource = "rtsp:///admin:1234@192.168.88.172:554/ipcam.sdp";
                OnPropertyChanged("SelectedIpCamera");
            }
        }



        //  


        #endregion Public Properties

        public IpCameraViewModel()
        {
            IpCameraCollection = new ObservableCollection<IpCamera>(new IpCameraProvider().GetAllIpCamera());
        }

        
      
    }
}