using System.Collections.Generic;
using ESESMT.Infra.Shared.Common;
using ESESMT.Domain.Entities;

namespace ESESMT.Domain.Interfaces
{
    public interface IBaseRepository<TEntity, TKeyType> 
        where TEntity : BaseEntity<TKeyType>
    {
        void Insert(TEntity obj);

        void Update(TEntity obj);

        void Delete(TKeyType id);

        IList<TEntity> Select();

        TEntity Select(TKeyType id);

        PagedResponse<List<TEntity>> SelectPaged<TFilter>(TFilter filter)
            where TFilter : BasePaginationFilter;
    }
}
