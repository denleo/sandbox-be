using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sandbox.Wordbook.API.Controllers;

[Authorize("authenticated")]
[ApiController]
[Route("/api/")]
public class CoreController : ControllerBase
{
}