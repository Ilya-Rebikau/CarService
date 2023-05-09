using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Interfaces;

namespace CarService.MainAPI.Services
{
    internal class PromocodeService : BaseService<Promocode>, IPromocodeService
    {
        public PromocodeService(IRepository<Promocode> repository, IConfiguration configuration)
            : base(repository, configuration)
        {
        }

        public override async Task<Promocode> Create(Promocode obj)
        {
            var oldPromocodes = Repository.GetAll().Where(p => p.DateEnd < DateTime.Now);
            foreach (var oldPromocode in oldPromocodes)
            {
                await Repository.Delete(oldPromocode);
            }

            obj.Text = GeneratePromocode();
            return await base.Create(obj);
        }

        public IEnumerable<Promocode> GetAllByUser(string userId)
        {
            return Repository.GetAll().Where(p => p.UserId == userId && p.DateEnd >= DateTime.Now);
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
