using DBO.Model.DataModel;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace DBO.Model.DAL
{
    public class GroupsProvider
    {
        /// <summary>
        /// Возврасщает список груп
        /// </summary>
        /// <returns></returns>
        public List<Group> GetAllGoups()
        {
            using (var db = new DBODataContext())
            {
                return db.Groups
                    .Include(x => x.ChildrenGroups)
                    .ToList()
                    .Where(x => x.ParentId == null)
                    .ToList();
            }
        }

        public void UpdateGoup(Group g)
        {
            using (var db = new DBODataContext())
            {
                db.Update(g);
                db.SaveChanges();
            }
        }
    }
}
