using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Interfaces;
using ESESMT.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESESMT.Application.Controllers
{
    public class ChecklistController : BaseController
    {
        private readonly IChecklistService _service;

        public ChecklistController(IChecklistService service)
        {
            this._service = service;
        }

        [HttpPost(nameof(Save))]
        public IActionResult Save([FromBody] ChecklistRegisterDto checklistDto)
        {
            _service.Save(checklistDto);
            return Ok();
        }

        [HttpPost(nameof(Update))]
        public IActionResult Update([FromBody] ChecklistRegisterDto checklistDto)
        {
            _service.Update(checklistDto);
            return Ok();
        }

        [HttpGet(nameof(GetDetails) + "/{id}")]
        public IActionResult GetDetails([FromRoute] int id)
        {
            var checklists = _service.GetDetails(id);
            return Ok(checklists);
        }

        [HttpPost(nameof(GetPagedByFilter))]
        public IActionResult GetPagedByFilter([FromBody] ChecklistFilter filter)
        {
            var resultPaged = _service.GetPagedByFilter(filter);
            return Ok(resultPaged);
        }
    }
}
