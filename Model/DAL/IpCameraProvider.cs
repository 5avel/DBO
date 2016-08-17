using DBO.Model;
using DBO.Model.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace Model.DAL
{
    public class IpCameraProvider
    {
        public List<IpCamera> GetAllIpCamera()
        {
            using (var db = new DBODataContext())
            {
                return db.IpCameras.ToList();
            }
        }
    }
}
