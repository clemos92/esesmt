using AutoMapper;
using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Interfaces;
using ESESMT.Domain.Models;
using ESESMT.Infra.Shared.Common;
using ESESMT.Infra.Shared.Notifications;
using ESESMT.Service.Validators;
using System.Collections.Generic;

namespace ESESMT.Service.Services
{
    public class ChecklistTypeService : BaseService<ChecklistType, int>, IChecklistTypeService
    {
        private readonly IChecklistTypeRepository _repository;
        private readonly IMapper _mapper;

        public ChecklistTypeService(IChecklistTypeRepository repository, NotificationContext notificationContext, IMapper mapper, ChecklistTypeValidator validator)
            : base(repository, notificationContext, validator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<DropdownDefaultModel> GetAllActive()
        {
            var activeEntities = _repository.GetAllActive();
            var resultMap = _mapper.Map<List<DropdownDefaultModel>>(activeEntities);

            return resultMap;
        }

        public void Save(ChecklistTypeDto obj)
        {
            var entity = _mapper.Map<ChecklistType>(obj);
            this.Validate(entity);
            if (entity.Valid)
                _repository.Insert(entity);
        }

        public void Update(ChecklistTypeDto obj)
        {
            var entity = _mapper.Map<ChecklistType>(obj);
            this.Validate(entity);
            if (entity.Valid)
                _repository.Update(entity);
        }

        public ChecklistTypeDto GetById(int id)
        {
            return _mapper.Map<ChecklistTypeDto>(_repository.Select(id));
        }

        public PagedResponse<List<ChecklistTypeDto>> GetPagedByFilter(ChecklistTypeFilter filter)
        {
            var listPaged = _repository.SelectPaged(filter);
            var mappedData = _mapper.Map<List<ChecklistTypeDto>>(listPaged.Data);
            return new PagedResponse<List<ChecklistTypeDto>>(mappedData, filter.PageIndex, filter.PageSize, listPaged.TotalRecords);
        }
    }
}