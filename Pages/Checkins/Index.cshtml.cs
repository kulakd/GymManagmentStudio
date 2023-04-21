using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services.Interfaces;

namespace GymManagmentStudio.Pages.Checkins
{
    public class IndexModel : PageModel
    {
        private readonly ICheckinService _checkinService;

        public IndexModel(ICheckinService checkinService)
        {
            _checkinService = checkinService;
        }

        public IList<Checkin> Checkin { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Checkin = await _checkinService.GetCheckins();
        }
    }
}
