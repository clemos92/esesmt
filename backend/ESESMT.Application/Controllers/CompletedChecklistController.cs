using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Interfaces;
using ESESMT.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ESESMT.Application.Controllers
{
    public class CompletedChecklistController : BaseController
    {
        private readonly ICompletedChecklistService _service;

        public CompletedChecklistController(ICompletedChecklistService service)
        {
            this._service = service;
        }

        [HttpPost(nameof(Save))]
        public IActionResult Save([FromBody] CompletedChecklistRegisterDto dto)
        {
            _service.Save(dto);
            return Ok();
        }

        [HttpGet(nameof(GetDetails) + "/{id}")]
        public IActionResult GetDetails([FromRoute] int id)
        {
            var checklists = _service.GetDetails(id);
            return Ok(checklists);
        }

        [HttpPost(nameof(GetPagedByFilter))]
        public IActionResult GetPagedByFilter([FromBody] CompletedChecklistFilter filter)
        {
            var resultPaged = _service.GetPagedByFilter(filter);
            return Ok(resultPaged);
        }
    }
}
