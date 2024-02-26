using DiyorMarket.Domain.DTOs.Dashboard;
using DiyorMarket.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiyorMarket.Controllers;

[Route("api/dashboard")]
[ApiController]
//[Authorize]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public ActionResult<DashboardDto> GetDashboard() => Ok(_dashboardService.GetDashboard());
}
