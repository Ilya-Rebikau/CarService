using CarService.UI.Infrastructure;
using CarService.UI.Interfaces;
using CarService.UI.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarService.UI.Controllers
{
    [ResponseCache(CacheProfileName = "Caching")]
    [ExceptionFilter]
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;
        public ServiceController(IServiceService service)
        {
            _service = service;
        }
    }
}
