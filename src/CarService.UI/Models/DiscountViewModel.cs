using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models
{
    public class DiscountViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Размер скидки, %")]
        public int Percent { get; set; }

        [Display(Name = "От")]
        public DateTime DateStart { get; set; }

        [Display(Name = "До, включительно")]
        public DateTime DateEnd { get; set; }
        public int? ServiceDataId { get; set; }
        public int? CarBrandId { get; set; }
        public int? CarTypeId { get; set; }

        [Display(Name = "Тип услуги")]
        public string ServiceDataName { get; set; }

        [Display(Name = "Марка авто")]
        public string CarBrandName { get; set; }

        [Display(Name = "Тип техники")]
        public string CarTypeName { get; set; }
        public SelectList CarBrandSelectList { get; set; }
        public SelectList CarTypeSelectList { get; set; }
        public SelectList ServiceDataSelectList { get; set; }
    }
}
