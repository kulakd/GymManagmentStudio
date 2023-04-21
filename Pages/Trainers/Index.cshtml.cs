using Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymManagmentStudio.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly ITrainerService _trainerService;

        public IndexModel(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public IList<Trainer> Trainer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Trainer = await _trainerService.GetTrainers();
        }
    }
}
