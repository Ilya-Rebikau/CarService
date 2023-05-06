using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;
using CarService.UI.Models.CarBrands;
using CarService.UI.Models.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarService.UI.Services
{
    internal class ServiceService : IServiceService
    {
        private readonly IMainClient _mainClient;
        public ServiceService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }
    }
}
