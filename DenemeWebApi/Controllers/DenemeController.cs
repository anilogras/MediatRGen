using Core.Deneme.Features.IsyeriMediatR.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DenemeWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DenemeController : Controller
    {
        private readonly IMediator _mediator;
        public DenemeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("deneme")]
        public async Task<IActionResult> GetDeneme()
        {
            var res = await _mediator.Send(new CreateIsyeriCommand() { Name = "Deneme" });
            return Ok(res);
        }
    }
}
