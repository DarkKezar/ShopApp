using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ILogger _logger;

    public TestController(ILogger<TestController> logger){
        _logger = logger;
    }

    [HttpGet]
    public bool Get(){
        _logger.LogInformation("Test log", DateTime.UtcNow);
        return true;
    }
}
