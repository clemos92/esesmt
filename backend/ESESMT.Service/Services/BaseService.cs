using FluentValidation;
using System;
using ESESMT.Infra.Shared.Notifications;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Interfaces;

namespace ESESMT.Service.Services
{
    public class BaseService<TEntity, TKeyType>
        where TEntity : BaseEntity<TKeyType>
    {
        protected readonly IBaseRepository<TEntity, TKeyType> _baseRepository;
        protected readonly NotificationContext _notificationContext;
        protected readonly AbstractValidator<TEntity> _validator;

        public BaseService(IBaseRepository<TEntity, TKeyType> baseRepository, NotificationContext notificationContext,
            AbstractValidator<TEntity> validator)
        {
            _baseRepository = baseRepository;
            _notificationContext = notificationContext;
            _validator = validator;
        }

        protected virtual void Validate(TEntity obj)
        {
            if (obj == null)
                throw new Exception("Register not found!");

            obj.Validate(obj, _validator);

            _notificationContext.AddNotifications(obj.ValidationResult);
        } 
    }
}