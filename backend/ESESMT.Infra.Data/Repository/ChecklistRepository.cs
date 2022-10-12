using ESESMT.Domain.Entities;
using ESESMT.Domain.Interfaces;
using ESESMT.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESESMT.Infra.Data.Repository
{
    public class ChecklistRepository : BaseRepository<Checklist, int>, IChecklistRepository
    {
        public ChecklistRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override IQueryable<Checklist> GetQuery()
        {
            return base.GetQuery().Include(e=>e.ChecklistType);
        }

        public Checklist GetDetails(int id)
        {
            var query = base.GetQuery().Include(e => e.ChecklistItems);
            return query.FirstOrDefault(e => e.Id == id);
        }

        public override void Insert(Checklist obj)
        {
            _dbSet.Add(obj);
            foreach (var item in obj.ChecklistItems)
            {
                var dbSetChecklistItem = _dbContext.Set<ChecklistItem>();
                dbSetChecklistItem.Add(item);
            }

            _dbContext.SaveChanges();
        }

        public override void Update(Checklist obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            foreach(var item in obj.ChecklistItems)
            {
                var dbSetChecklistItem = _dbContext.Set<ChecklistItem>();
                if (item.Id == 0)
                {
                    dbSetChecklistItem.Add(item);
                }
                else
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
            }

            _dbContext.SaveChanges();
 
        }
    }
}
