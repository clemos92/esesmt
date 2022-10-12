using FluentValidation;
using System.Collections.Generic;
using ESESMT.Infra.Shared.Common;
using ESESMT.Domain.Entities;

namespace ESESMT.Domain.Interfaces
{
    public interface IBaseService<TEntity, TKeyType>
        where TEntity : BaseEntity<TKeyType>
    {
        TEntity Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

        void Delete(TKeyType id);

        IList<TEntity> Get();

        TEntity GetById(TKeyType id);

        void Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>;

        PagedResponse<List<TEntity>> SelectPaged<TFilter>(TFilter filter)
            where TFilter : BasePaginationFilter;
    }
}
