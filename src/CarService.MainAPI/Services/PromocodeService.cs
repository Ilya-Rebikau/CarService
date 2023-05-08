using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;
using System;

namespace CarService.MainAPI.Services
{
    internal class PromocodeService : BaseService<Promocode>, IPromocodeService
    {
        public PromocodeService(IRepository<Promocode> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }

        public override Task<Promocode> Create(Promocode obj)
        {
            obj.Text = GeneratePromocode();
            obj.WasUsed = false;
            return base.Create(obj);
        }

        public IEnumerable<Promocode> GetAllByUser(string userId)
        {
            return Repository.GetAll().Where(p => p.UserId == userId && p.WasUsed == false && p.DateEnd >= DateTime.Now);
        }

        public Promocode GetPromocodeByText(string text)
        {
            return Repository.GetAll().FirstOrDefault(p => p.Text == text);
        }

        private string GeneratePromocode()
        {
            var promocodes = Repository.GetAll().ToList();
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var text = string.Empty;
            do
            {
                text = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (promocodes.Any(p => p.Text != text));
            return text;
        }
    }
}
