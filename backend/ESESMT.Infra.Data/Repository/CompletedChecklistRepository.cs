using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Interfaces;
using ESESMT.Infra.Data.Context;
using ESESMT.Infra.Shared.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ESESMT.Infra.Data.Repository
{
    public class CompletedChecklistRepository : BaseRepository<CompletedChecklist, int>, ICompletedChecklistRepository
    {
        public CompletedChecklistRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public override IQueryable<CompletedChecklist> GetQuery()
        {
            return base.GetQuery()
                .Include(e => e.Checklist)
                .ThenInclude(x=>x.ChecklistType);
        }

        public override IQueryable<CompletedChecklist> ApplyFilter<TFilter>(TFilter filter, IQueryable<CompletedChecklist> query)
        {
            var completedChecklistFilter = filter as CompletedChecklistFilter;

            if (completedChecklistFilter.ChecklistTypeId.HasValue)
            {
                query = query.Where(x=>x.Checklist.ChecklistTypeId == completedChecklistFilter.ChecklistTypeId.Value);
            }

            if (!string.IsNullOrWhiteSpace(completedChecklistFilter.Description))
            {
                query = query.Where(x => x.Checklist.Description.StartsWith(completedChecklistFilter.Description));
            }

            if (completedChecklistFilter.StartDate.HasValue)
            {
                query = query.Where(x => x.CreationDate >= completedChecklistFilter.StartDate);
            }

            if (completedChecklistFilter.EndDate.HasValue)
            {
                query = query.Where(x => x.CreationDate <= completedChecklistFilter.EndDate);
            }

            return query;
        }

        public override IQueryable<CompletedChecklist> AppllySort<TFilter>(TFilter filter, IQueryable<CompletedChecklist> query)
        {
            if (!string.IsNullOrWhiteSpace(filter.SortBy) && !string.IsNullOrWhiteSpace(filter.SortDirection))
            {
                switch (filter.SortBy)
                {
                    case "checklistDescription":
                        query = SortBy(filter, x => x.Checklist.Description, query);
                        break;
                    case "checklistChecklistTypeName":
                        query = SortBy(filter, x => x.Checklist.ChecklistType.Name, query);
                        break;
                    default:
                        query = base.AppllySort(filter, query);
                        break;
                }
            }

            return query;
        }

        private IOrderedQueryable<CompletedChecklist> SortBy<TFilter, TOrderBy>(TFilter filter, Expression<Func<CompletedChecklist, TOrderBy>> prop, IQueryable<CompletedChecklist> query)
            where TFilter : BasePaginationFilter
        {
            return filter.SortDirection == "asc" ? query.OrderBy(prop) : query.OrderByDescending(prop);
        }

        public CompletedChecklist GetDetails(int id)
        {
            var query = base.GetQuery().Include(e => e.CompletedChecklistItems);
            return query.FirstOrDefault(e => e.Id == id);
        }

        public override void Insert(CompletedChecklist obj)
        {
            _dbSet.Add(obj);
            foreach (var item in obj.CompletedChecklistItems)
            {
                var dbSetCompletedChecklistItem = _dbContext.Set<CompletedChecklistItem>();
                dbSetCompletedChecklistItem.Add(item);
            }

            _dbContext.SaveChanges();
        }
    }
}
