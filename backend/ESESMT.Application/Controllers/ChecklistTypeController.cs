using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Interfaces;
using ESESMT.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESESMT.Application.Controllers
{
    public class ChecklistTypeController : BaseController
    {
        private readonly IChecklistTypeService _service;

        public ChecklistTypeController(IChecklistTypeService service)
        {
            this._service = service;
        }


        [HttpPost(nameof(Save))]
        public IActionResult Save([FromBody] ChecklistTypeDto checklistTypeDto)
        {
            _service.Save(checklistTypeDto);
            return Ok();
        }

        [HttpPost(nameof(Update))]
        public IActionResult Update([FromBody] ChecklistTypeDto checklistTypeDto)
        {
            _service.Update(checklistTypeDto);
            return Ok();
        }

        [HttpGet(nameof(GetDetails) + "/{id}")]
        public IActionResult GetDetails([FromRoute] int id)
        {
            var checklistTypes = _service.GetById(id);
            return Ok(checklistTypes);
        }

        [HttpPost(nameof(GetPagedByFilter))]
        public IActionResult GetPagedByFilter([FromBody] ChecklistTypeFilter filter)
        {
            var resultPaged = _service.GetPagedByFilter(filter);
            return Ok(resultPaged);
        }

        [HttpGet(nameof(GetAllActive))]
        public IActionResult GetAllActive()
        {
            var activeEntities = _service.GetAllActive();
            return Ok(activeEntities);
        }
    }
}