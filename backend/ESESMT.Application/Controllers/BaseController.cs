using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ESESMT.Infra.Shared.Common;

namespace ESESMT.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase { }
}
