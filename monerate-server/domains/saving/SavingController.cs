using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using monerate_server.Data;
using monerate_server.domains.auth.types;
using monerate_server.domains.saving.SavingService;
using monerate_server.Models;
using System.Security.Claims;

namespace monerate_server.domains.saving
{
    [ApiController]
    public class SavingController : ControllerBase
    {
        private readonly ISavingService _savingService;
        public SavingController(ISavingService savingService)
        {
            _savingService = savingService;

        }

        [HttpGet]
        [Route("/savings")]
        [Authorize]
        public async Task<IActionResult> GetSavings()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            var savings = await _savingService.GetSavingsFromUser(userId);
            return Ok(savings);
        }

        [HttpPost]
        [Route("/savings")]
        [Authorize]
        public async Task<IActionResult> SaveCurrentSaving(
            [FromBody] SavingPostDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            var saving = await _savingService.SaveCurrentSaving(dto, userId);
            return Ok(saving);
        }
    }
}
