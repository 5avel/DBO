using DBO.ViewModel.MVVMLib;
using System.Collections.ObjectModel;
using DBO.Model.DataModel;
using Model.DAL;

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
                OnPropertyChanged(() => IpCameraCollection);
            }
        }

        private string ipCamSource;

        public string IPCameraSource
        {
            get { return ipCamSource; }
            set
            {
                ipCamSource = value;
                OnPropertyChanged(() => IPCameraSource);
            }
        }

        private IpCamera selectedIpCamera;

        public IpCamera SelectedIpCamera
        {
            get { return selectedIpCamera; }
            set
            {
                selectedIpCamera = value;
                string string_source = "rtsp:///" + selectedIpCamera.Cam_Login + ":" + selectedIpCamera.Cam_Password + "@" + selectedIpCamera.Cam_IpAddress + "/" + selectedIpCamera.Name + ".sdp";
                IPCameraSource = string_source;
                OnPropertyChanged(() => SelectedIpCamera);
            }
        }


        #endregion Public Properties

        public IpCameraViewModel()
        {
            IpCameraCollection = new ObservableCollection<IpCamera>(new IpCameraProvider().GetAllIpCamera());
        }

        
      
    }
}