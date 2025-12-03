using Microsoft.AspNetCore.Mvc;

namespace Web.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "API OK (local)" });
        }
    }
}
