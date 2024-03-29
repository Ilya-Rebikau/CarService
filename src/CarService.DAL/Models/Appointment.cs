﻿using CarService.DAL.Interfaces;

namespace CarService.DAL.Models
{
    /// <summary>
    /// Запись на прохождение технического обслуживания.
    /// </summary>
    public class Appointment : IModel
    {
        public int Id { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public bool WasFinished { get; set; }
        public int ServiceId { get; set; }
    }
}
