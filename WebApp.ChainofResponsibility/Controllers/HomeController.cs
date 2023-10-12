using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ChainofResponsibility.ChainofResponsibility;
using WebApp.ChainofResponsibility.Context;
using WebApp.ChainofResponsibility.Models;

namespace WebApp.ChainofResponsibility.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> SendEmail(string emailAddress)
    {
        var products = await _context.Products.ToListAsync();

        var excelProcessHandler = new ExcelProcessHandler<Product>();
        var zipFileProcessHandler = new ZipProcessHandler<Product>();
        if (string.IsNullOrEmpty(emailAddress))
        {
            emailAddress = "gidecekolanmailadresi";
        }

        var sendEmailProcessHandler = new EmailProcessHandler("product.zip", emailAddress);

        excelProcessHandler.SetNext(zipFileProcessHandler).SetNext(sendEmailProcessHandler);

        excelProcessHandler.handle(products);

        return View(nameof(Index));
    }


    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}