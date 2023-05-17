namespace CarService.MainAPI.Models
{
    public class DiscountModel
    {
        public int Id { get; set; }
        public int Percent { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int? ServiceDataId { get; set; }
        public int? CarBrandId { get; set; }
        public int? CarTypeId { get; set; }
        public string ServiceDataName { get; set; }
        public string CarBrandName { get; set; }
        public string CarTypeName { get; set; }
    }
}
