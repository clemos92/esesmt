using AutoMapper;
using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Entities;
using ESESMT.Domain.Interfaces;
using ESESMT.Domain.Models;
using ESESMT.Infra.Shared.Common;
using ESESMT.Infra.Shared.Notifications;
using ESESMT.Service.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Service.Services
{
    public class ChecklistService : BaseService<Checklist, int>, IChecklistService
    {
        private readonly IChecklistRepository _repository;
        private readonly IMapper _mapper;

        public ChecklistService(IChecklistRepository repository, NotificationContext notificationContext, IMapper mapper, ChecklistValidator validator)
            : base(repository, notificationContext, validator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Save(ChecklistRegisterDto obj)
        {
            var entity = _mapper.Map<Checklist>(obj);
            this.Validate(entity);
            if (entity.Valid)
                _repository.Insert(entity);
        }

        public void Update(ChecklistRegisterDto obj)
        {
            var entity = _mapper.Map<Checklist>(obj);
            this.Validate(entity);
            if (entity.Valid)
                _repository.Update(entity);
        }

        public ChecklistRegisterDto GetDetails(int id)
        {
            return _mapper.Map<ChecklistRegisterDto>(_repository.GetDetails(id));
        }

        public PagedResponse<List<ChecklistListDto>> GetPagedByFilter(ChecklistFilter filter)
        {
            var listPaged = _repository.SelectPaged(filter);
            var mappedData = _mapper.Map<List<ChecklistListDto>>(listPaged.Data);
            return new PagedResponse<List<ChecklistListDto>>(mappedData, filter.PageIndex, filter.PageSize, listPaged.TotalRecords);
        }

        public List<DropdownDefaultModel> GetAllActive()
        {
            var activeEntities = _repository.GetAllActive();
            var resultMap = _mapper.Map<List<DropdownDefaultModel>>(activeEntities);

            return resultMap;
        }

        public List<DropdownDefaultModel> GetByIdToDropdown(int id)
        {
            var entity = _repository.GetByIdToDropdown(id);
            var resultMap = _mapper.Map<DropdownDefaultModel>(entity);

            return new List<DropdownDefaultModel>() { resultMap };
        }
        
    }
}
