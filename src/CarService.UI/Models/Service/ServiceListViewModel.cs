using System.ComponentModel.DataAnnotations;

namespace CarService.UI.Models.Service
{
    public class ServiceListViewModel
    {
        public IEnumerable<ServiceViewModel> Services { get; set; }

        [Display(Name = "Название услуги")]
        public string ServiceName { get; set; }
        public byte[] ServiceImageData { get; set; }
        public int ServiceDataId { get; set; }
        public string Desciption { get; set; }
    }
}
