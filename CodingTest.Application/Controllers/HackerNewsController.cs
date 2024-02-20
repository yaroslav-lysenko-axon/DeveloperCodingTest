using CodingTest.Application.Models.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CodingTest.Application.Controllers;

[ApiController]
[Route("[controller]")]
public class HackerNewsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await mediator.Send(new GetHackerNewsCommand());
        
        return Ok(response);
    }
}