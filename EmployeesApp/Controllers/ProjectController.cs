using EmployeesApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProjectsApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<ProjectController> _logger;

    // It's better to have the service folder in a different project, like a class library. However, I put it here because this is a TEST project.
    private readonly IManageProjectsService _manageProjectService;

    public ProjectController(ILogger<ProjectController> logger,
        IManageProjectsService manageProjectService)
    {
        _manageProjectService = manageProjectService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post(IFormFile formFile)
    {
        if (formFile == null)
            return BadRequest("Missing file!");

        if (!formFile.FileName.EndsWith(".csv"))
            return BadRequest("Invalid file format!");

        var result = await _manageProjectService.GetMaxWorkDaysByPair(formFile);

        return Ok(result);
    }
}

