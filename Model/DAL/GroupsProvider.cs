using Microsoft.EntityFrameworkCore;
using Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAL
{
    public class GroupsProvider
    {
        public List<Group> GetAllGoups()
        {
            using (var db = new DBODataContext())
            {
                List<Group> groups = db.Groups.Include(x => x.ChildrenGroups).ToList();

                return groups.Where(x => x.ParentId == null).ToList();


                //// ыаываыва
            }
        }
    }
}
