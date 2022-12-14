using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using monerate_server.Data;
using monerate_server.domains.auth.types;
using monerate_server.domains.user;
using monerate_server.Models;
using System.Net;
using System.Web.Http;

namespace monerate_server.domains.saving.SavingService
{
    public class SavingService : ISavingService
    {
        private readonly MyDbContext db;
        public SavingService(MyDbContext db)
        {
            this.db = db;
        }

        public async Task<List<CurrentSaving> > GetSavingsFromUser(int userId)
        {
            var savings = await this.db.
                CurrentSaving
                .Where(s => s.UserId == userId)
                .ToListAsync();

            return savings;
        }

        public Task<CurrentSaving> SaveCurrentSaving(SavingPostDto dto, int userId)
        {
            if (dto.Id == null) return this._CreateCurrentSaving(dto, userId);

            return this._UpdateCurrentSaving(dto, userId);
        }

        public async Task<CurrentSaving> _CreateCurrentSaving(SavingPostDto dto, int userId)
        {
            var saving = new CurrentSaving()
            {
                UserId = userId,
                Value = dto.Value,
                DateTime = dto.DateTime
            };

            await this.db.CurrentSaving.AddAsync(saving);
            await this.db.SaveChangesAsync();
            return saving;
        }

        public async Task<CurrentSaving> _UpdateCurrentSaving(SavingPostDto dto, int userId)
        {
            var saving =  await this.db.CurrentSaving.FirstOrDefaultAsync(saving => saving.Id == dto.Id && saving.UserId == userId);
            if (saving == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    ReasonPhrase = "Saving not found."
                };
                throw new HttpResponseException(resp);
            }

            saving.Value = dto.Value;
            saving.DateTime = dto.DateTime;

            await this.db.SaveChangesAsync();

            return saving;
        }
    }
}
