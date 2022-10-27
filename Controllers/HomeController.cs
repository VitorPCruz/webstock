using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStock.Interfaces;
using WebStock.Models;
using WebStock.Repository;

namespace WebStock.Controllers;

[AllowAnonymous]
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Report> _reportRepository;

    public HomeController(ILogger<HomeController> logger, IRepository<Report> reportRepository)
    {
        _logger = logger;
        _reportRepository = reportRepository;
    }

    public async Task<IActionResult> Index()
    {
        return View("../Reports/Index", await _reportRepository.GetAllEntities());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
