using Microsoft.AspNetCore.Mvc;
using monerate_server.domains.auth.types;
using monerate_server.Models;
using System.Net;
using System.Web.Http;

namespace monerate_server.domains.saving.SavingService
{
    public interface ISavingService
    {

        public Task<List<CurrentSaving>> GetSavingsFromUser(int userId);
        public Task<CurrentSaving> SaveCurrentSaving(SavingPostDto dto, int userId);

        public  Task<CurrentSaving> _CreateCurrentSaving(SavingPostDto dto, int userId);


        public  Task<CurrentSaving> _UpdateCurrentSaving(SavingPostDto dto, int userId);
      
    }
}