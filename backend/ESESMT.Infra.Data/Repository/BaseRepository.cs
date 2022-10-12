using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ESESMT.Infra.Shared.Common;
using ESESMT.Infra.Shared.Extensions;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Interfaces;
using ESESMT.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ESESMT.Infra.Data.Repository
{
    public  abstract class BaseRepository<TEntity, TKeyType> : IBaseRepository<TEntity, TKeyType>
        where TEntity : BaseEntity<TKeyType>
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(ApplicationDbContext _dbContext)
        {
            this._dbContext = _dbContext;
            this._dbSet = _dbContext.Set<TEntity>();
        }

        public virtual void Insert(TEntity obj)
        {
            _dbSet.Add(obj);
            _dbContext.SaveChanges();
        }

        public virtual void Update(TEntity obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        //sample of using this method: Update(obj, _dbContext.Entry(obj).Property(prop => prop.Password));
        public virtual void Update<TProperty>(TEntity obj, params PropertyEntry<TEntity, TProperty>[] propsToIgnore)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;

            foreach (var item in propsToIgnore)
                item.IsModified = false;

            _dbContext.SaveChanges();
        }

        public virtual void Delete(TKeyType id)
        {
            _dbSet.Remove(Select(id));
            _dbContext.SaveChanges();
        }

        public virtual IList<TEntity> Select() =>
            _dbSet.ToList();

        public virtual TEntity Select(TKeyType id) =>
            _dbSet.Find(id);

        public virtual IList<TEntity> Select(Expression<Func<TEntity, bool>> predicate) =>
            _dbSet.Where(predicate).ToList();

        public virtual PagedResponse<List<TEntity>> SelectPaged<TFilter>(TFilter filter)
            where TFilter : BasePaginationFilter
        {
            var query = GetQuery().AsNoTracking();

            query = ApplyFilter(filter, query);

            var totalRecords = query.Count();

            query = AppllySort(filter, query);

            var pagedData = query.Skip(filter.PageIndex * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            var pagedReponse = new PagedResponse<List<TEntity>>(pagedData, filter.PageIndex, filter.PageSize, totalRecords);

            return pagedReponse;
        }

        public virtual IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> ApplyFilter<TFilter>(TFilter filter, IQueryable<TEntity> query)
            where TFilter : BasePaginationFilter
        {
            var filterProperties = typeof(TFilter).GetProperties()
                .Where(p=>p.CanWrite && p.CanRead);
            
            var entityProperties = typeof(TEntity).GetProperties()
                .Where(p => p.CanWrite && p.CanRead);
            
            foreach (var propertyInfo in filterProperties)
            {
                var value = propertyInfo.GetValue(filter, null);
                
                if (value != null && !string.IsNullOrEmpty(value.ToString()) && 
                    entityProperties.Any(p=>p.Name == propertyInfo.Name))
                {
                    var parameter = Expression.Parameter(typeof(TEntity), "p");

                    var predicate = Expression.Lambda<Func<TEntity, bool>>(
                        Expression.Equal(Expression.PropertyOrField(parameter, propertyInfo.Name), Expression.Constant(value)),
                        parameter);
                    
                    query = query.Where(predicate);
                }
            }

            return query;
        }

        public virtual IQueryable<TEntity> AppllySort<TFilter>(TFilter filter, IQueryable<TEntity> query)
            where TFilter : BasePaginationFilter
        {
            if (!string.IsNullOrWhiteSpace(filter.SortBy) && !string.IsNullOrWhiteSpace(filter.SortDirection))
            {
                var properties = typeof(TEntity).GetProperties();
                foreach (var prop in properties)
                {
                    if (prop.Name.ToLower() == filter.SortBy.ToLower())
                    {
                        filter.SortBy = prop.Name;
                        break;
                    }
                }

                query = query.OrderBy(filter.SortBy, filter.SortDirection);
            }

            return query;
        }
    }
}