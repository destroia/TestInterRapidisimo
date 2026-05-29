using Business.Interfaces;
using Dto.BeginSession.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BeginSessionController : ControllerBase
{
    private readonly IBeginSessionService _beginSessionService;

    public BeginSessionController(IBeginSessionService beginSessionService)
    {
        _beginSessionService = beginSessionService;
    }

    [HttpPost]
    public ActionResult<string> GenerateToken(BeginSessionRequestDto request)
    {
        var token = _beginSessionService.GenerateToken(request.AdminId, request.Login);
        return Ok(new { Token = token });
    }
}