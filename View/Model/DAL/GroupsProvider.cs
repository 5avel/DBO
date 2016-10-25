using DBO.Model.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBO.Model.DAL
{
    public class GroupsProvider
    {
        /// <summary>
        /// Возврасщает список груп
        /// </summary>
        /// <returns></returns>
        public async Task<List<Group>> GetAllGoupsAsync()
        {
            
            using (var db = new DBODataContext())
            {
                db.ChangeTracker.AutoDetectChangesEnabled = true;
               
               // await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
                var groups = await db.Groups
                    .Include(x => x.ChildrenGroups)
                    .ToListAsync()
                    .ConfigureAwait(false);
                return groups.Where(x => x.ParentId == null).ToList();
            }
        }

        public List<Group> GetAllGoupsList()
        {
            using (var db = new DBODataContext())
            {
                // await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
                var groups = db.Groups
                    .Include(x => x.ChildrenGroups)
                    .ToList();
                return groups;
            }
        }

        public Group GetGoupsByID(int? id)
        {
            using (var db = new DBODataContext())
            {
                // await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
                var group = db.Groups
                    .Where(x => x.ID == id)
                    .FirstOrDefault();
                return group;
            }
        }

        public async Task RemoveGoupAsync(Group g)
        {
            using (var db = new DBODataContext())
            {
               // await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
                db.Groups.Remove(g);
                await db.SaveChangesAsync();
            }
        }

        public void UpdateGoup(Group g)
        {
            using (var db = new DBODataContext())
            {
                var temp = db.Groups.FirstOrDefault(x => x.ID == g.ID);
                temp.ParentId = g.ParentId;
                temp.Name = g.Name;
                db.Update(temp);
                db.SaveChanges();
            }
        }

        public void UpdateGoupIsSelectedAndIsExpandedProperty(Group g)
        {
            using (var db = new DBODataContext())
            {
                var temp = db.Groups.FirstOrDefault(x => x.ID == g.ID);
                temp.IsExpanded = g.IsExpanded;
                temp.IsSelected = g.IsSelected;
                db.Update(temp);
                db.SaveChanges();
            }
        }

        public void AddGoup(Group g)
        {
            using (var db = new DBODataContext())
            {
                db.Add(g);
                db.SaveChanges();
            }
        }
    }
}
