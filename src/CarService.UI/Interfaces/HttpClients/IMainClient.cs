﻿using CarService.UI.Models;
using CarService.UI.Models.Appointment;
using CarService.UI.Models.Feedback;
using CarService.UI.Models.Service;
using Microsoft.AspNetCore.Mvc;
using RestEase;

namespace CarService.UI.Interfaces.HttpClients
{
    public interface IMainClient
    {
        private const string AuthorizationKey = "Authorization";

        [Get("service/getservices")]
        public Task<IEnumerable<ServiceViewModel>> GetServiceViewModels([Header(AuthorizationKey)] string token, [Query] int serviceDataId, CancellationToken cancellationToken = default);

        [Get("service/details/{id}")]
        public Task<ServiceViewModel> GetServiceById([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Post("service/create")]
        public Task CreateService([Header(AuthorizationKey)] string token, [Body] ServiceViewModel model, CancellationToken cancellationToken = default);

        [Put("service/edit/{id}")]
        public Task EditService([Header(AuthorizationKey)] string token, [Path] int id, [Body] ServiceViewModel model, CancellationToken cancellationToken = default);

        [Delete("service/delete/{id}")]
        public Task DeleteService([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("carbrand/getcarbrands")]
        public Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);

        [Get("carbrand/getallcarbrands")]
        public Task<IEnumerable<CarBrandViewModel>> GetCarBrandViewModels([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Post("carbrand/create")]
        public Task CreateCarBrand([Header(AuthorizationKey)] string token, [Body] CarBrandViewModel model, CancellationToken cancellationToken = default);

        [Get("carbrand/edit/{id}")]
        public Task<CarBrandViewModel> GetCarBrandViewModelForEdit([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Put("carbrand/edit/{id}")]
        public Task EditCarBrand([Header(AuthorizationKey)] string token, [Path] int id, [Body] CarBrandViewModel model, CancellationToken cancellationToken = default);

        [Delete("carbrand/delete/{id}")]
        public Task DeleteCarBrand([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("cartype/getcartypes")]
        public Task<IEnumerable<CarTypeViewModel>> GetCarTypes([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Post("cartype/create")]
        public Task CreateCarType([Header(AuthorizationKey)] string token, [Body] CarTypeViewModel model, CancellationToken cancellationToken = default);

        [Get("cartype/edit/{id}")]
        public Task<CarTypeViewModel> GetCarTypeViewModelForEdit([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Put("cartype/edit/{id}")]
        public Task EditCarType([Header(AuthorizationKey)] string token, [Path] int id, [Body] CarTypeViewModel model, CancellationToken cancellationToken = default);

        [Delete("cartype/delete/{id}")]
        public Task DeleteCarType([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("servicedata/getservicedatas")]
        public Task<IEnumerable<ServiceDataViewModel>> GetServiceDatas([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);

        [Get("servicedata/getallservicedatas")]
        public Task<IEnumerable<ServiceDataViewModel>> GetServiceDatas([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Get("servicedata/details/{id}")]
        public Task<ServiceDataViewModel> GetServiceDataById([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);
        
        [Post("servicedata/create")]
        public Task CreateServiceData([Header(AuthorizationKey)] string token, [Body] ServiceDataViewModel model, CancellationToken cancellationToken = default);
        
        [Put("servicedata/edit/{id}")]
        public Task EditServiceData([Header(AuthorizationKey)] string token, [Path] int id, [Body] ServiceDataViewModel model, CancellationToken cancellationToken = default);
        
        [Delete("servicedata/delete/{id}")]
        public Task DeleteServiceData([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("discount/getdiscounts")]
        public Task<IEnumerable<DiscountViewModel>> GetDiscounts([Header(AuthorizationKey)] string token, CancellationToken cancellationToken = default);

        [Get("discount/details/{id}")]
        public Task<DiscountViewModel> GetDiscountById([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);
        
        [Post("discount/create")]
        public Task CreateDiscount([Header(AuthorizationKey)] string token, [Body] DiscountViewModel model, CancellationToken cancellationToken = default);

        [Put("discount/edit/{id}")]
        public Task EditDiscount([Header(AuthorizationKey)] string token, [Path] int id, [Body] DiscountViewModel model, CancellationToken cancellationToken = default);

        [Delete("discount/delete/{id}")]
        public Task DeleteDiscount([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Get("promocode/getpromocodes")]
        public Task<IEnumerable<PromocodeViewModel>> GetPromocodes([Header(AuthorizationKey)] string token, [Query] string userId, CancellationToken cancellationToken = default);

        [Post("promocode/usepromocode")]
        public Task<PromocodeViewModel> UsePromocode([Header(AuthorizationKey)] string token, [Query] string text, CancellationToken cancellationToken = default);

        [Post("promocode/create")]
        public Task CreatePromocode([Header(AuthorizationKey)] string token, [Body] PromocodeViewModel promocode, CancellationToken cancellationToken = default);

        [Get("appointment/getallappointments")]
        public Task<IEnumerable<AppointmentViewModel>> GetAllAppointments([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);

        [Get("appointment/getappointmentsbydateandservice")]
        public Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByDateAndService([Header(AuthorizationKey)] string token, [Query] string stringDateTime, [Query] int serviceId, CancellationToken cancellationToken = default);
        
        [Get("appointment/getappointmentsbyuser")]
        public Task<IEnumerable<AppointmentViewModel>> GetAppointmentsByUser([Header(AuthorizationKey)] string token, [Query] string userId, CancellationToken cancellationToken = default);

        [Post("appointment/create")]
        public Task CreateAppointment([Header(AuthorizationKey)] string token, [Body] AppointmentViewModel model, CancellationToken cancellationToken = default);

        [Put("appointment/finish/{id}")]
        public Task FinishAppointment([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

        [Delete("appointment/delete/{id}")]
        public Task DeleteAppointment([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);
        
        [Get("feedback/getfeedbacks")]
        public Task<IEnumerable<FeedbackViewModel>> GetFeedbacks([Header(AuthorizationKey)] string token, [Query] int pageNumber, CancellationToken cancellationToken = default);
        
        [Post("feedback/create")]
        public Task CreateFeedback([Header(AuthorizationKey)] string token, [Body] FeedbackViewModel model, CancellationToken cancellationToken = default);
        
        [Delete("feedback/delete/{id}")]
        public Task DeleteFeedback([Header(AuthorizationKey)] string token, [Path] int id, CancellationToken cancellationToken = default);

    }
}