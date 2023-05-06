﻿using CarService.UI.Interfaces;
using CarService.UI.Interfaces.HttpClients;
using CarService.UI.Models;

namespace CarService.UI.Services
{
    public class CarTypeService : ICarTypeService
    {
        private readonly IMainClient _mainClient;
        public CarTypeService(IMainClient mainClient)
        {
            _mainClient = mainClient;
        }

        public async Task<IEnumerable<CarType>> GetAll(string token)
        {
            return await _mainClient.GetCarTypes(token);
        }
    }
}
