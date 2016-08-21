﻿using DBO.Model.DataModel;
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
                await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
                var groups = await db.Groups
                    .Include(x => x.ChildrenGroups)
                    .ToListAsync()
                    .ConfigureAwait(false);
                return groups.Where(x => x.ParentId == null).ToList();
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
