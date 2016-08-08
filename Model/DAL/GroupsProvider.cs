﻿using Microsoft.EntityFrameworkCore;
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
    }
}
