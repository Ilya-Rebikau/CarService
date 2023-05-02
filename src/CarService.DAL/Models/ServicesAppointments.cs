using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    /// <summary>
    /// Модель, связующая услуги и записи на них.
    /// </summary>
    public class ServicesAppointments : IModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int AppointmentId { get; set; }
    }
}
