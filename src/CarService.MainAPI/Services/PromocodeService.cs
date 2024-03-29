﻿using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.MainAPI.Infrastructure;
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
            var oldPromocodes = Repository.GetAll().Where(p => p.DateEnd.Date < DateTime.Now.Date).ToList();
            foreach (var oldPromocode in oldPromocodes)
            {
                await Repository.Delete(oldPromocode);
            }

            CheckForRightDate(obj);
            obj.Text = GeneratePromocode();
            return await base.Create(obj);
        }

        public IEnumerable<Promocode> GetAllByUser(string userId)
        {
            return Repository.GetAll().Where(p => p.UserId == userId && p.DateEnd.Date >= DateTime.Now.Date);
        }

        public Promocode GetPromocodeByText(string text)
        {
            return Repository.GetAll().FirstOrDefault(p => p.Text == text);
        }

        private static void CheckForRightDate(Promocode obj)
        {
            if (obj.DateEnd.Date < DateTime.Now.Date)
            {
                throw new MyException("Срок действия нового промокода не должен быть прошедшей датой");
            }
        }

        private string GeneratePromocode()
        {
            var promocodes = Repository.GetAll().ToList();
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var text = string.Empty;
            while (true)
            {
                int count = 0;
                text = new string(Enumerable.Repeat(chars, 10)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
                foreach (var promocode in promocodes)
                {
                    if (promocode.Text != text)
                    {
                        count++;
                    }
                }
                if (count == promocodes.Count)
                {
                    break;
                }
            }

            return text;
        }
    }
}
