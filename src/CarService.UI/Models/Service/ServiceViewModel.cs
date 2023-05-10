using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Service
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Цена, BYN")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Цена с учётом скидки, BYN")]
        [DataType(DataType.Currency)]
        public decimal? NewPrice { get; set; }

        [Display(Name = "Примерное время ремонта, мин")]
        public int MinutesSpent { get; set; }
        public int CarBrandId { get; set; }
        public int CarTypeId { get; set; }
        public int ServiceDataId { get; set; }

        [Display(Name = "Тип техники")]
        public string CarTypeName { get; set; }

        [Display(Name = "Марка автомобиля")]
        public string CarBrandName { get; set; }
        public SelectList CarBrandSelectList { get; set; }
        public SelectList CarTypeSelectList { get; set; }
    }
}
