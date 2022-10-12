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
    public class CompletedChecklistService : BaseService<CompletedChecklist, int>, ICompletedChecklistService
    {
        private readonly ICompletedChecklistRepository _repository;
        private readonly IMapper _mapper;

        public CompletedChecklistService(ICompletedChecklistRepository repository, NotificationContext notificationContext, IMapper mapper, CompletedChecklistValidator validator)
            : base(repository, notificationContext, validator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void Save(CompletedChecklistRegisterDto obj)
        {
            var entity = _mapper.Map<CompletedChecklist>(obj);
            entity.CreationDate = DateTime.Now;
            this.Validate(entity);
            if (entity.Valid)
                _repository.Insert(entity);
        }

        public CompletedChecklistRegisterDto GetDetails(int id)
        {
            return _mapper.Map<CompletedChecklistRegisterDto>(_repository.GetDetails(id));
        }

        public PagedResponse<List<CompletedChecklistListDto>> GetPagedByFilter(CompletedChecklistFilter filter)
        {
            var listPaged = _repository.SelectPaged(filter);
            var mappedData = _mapper.Map<List<CompletedChecklistListDto>>(listPaged.Data);
            return new PagedResponse<List<CompletedChecklistListDto>>(mappedData, filter.PageIndex, filter.PageSize, listPaged.TotalRecords);
        }
    }
}
