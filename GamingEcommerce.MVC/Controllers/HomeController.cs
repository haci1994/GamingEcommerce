using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GamingEcommerce.MVC.Models;
using GamingEcommerce.BLL.Services.WebsiteServices;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;

namespace GamingEcommerce.MVC.Controllers;

public class HomeController : Controller
{
    private readonly HomeLayoutService _homeLayoutService;

    public HomeController(HomeLayoutService homeLayoutService)
    {
        _homeLayoutService = homeLayoutService;
    }

    public async Task<IActionResult> Index()
    {
        var model = await _homeLayoutService.GetHomeLayoutDataAsync();

        if (model == null) return NotFound();

        return View(model);
    }
}
