using Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
