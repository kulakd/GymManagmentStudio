using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;

namespace GymManagmentStudio.Pages.Checkins
{
    public class CreateModel : PageModel
    {
        private readonly ICheckinService _checkinService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemberService _memberService;

        public CreateModel(ICheckinService checkinService,
            UserManager<IdentityUser> userManager,
            IMemberService memberService)
        {
            _checkinService = checkinService;
            _userManager = userManager;
            _memberService = memberService;
        }

        public IActionResult OnGet()
        {
            GetMembers();
            return Page();
        }

        private async void GetMembers()
        {
            var members = await _memberService.GetMembers();
            ViewData["MemberId"] = new SelectList(members, "Id", "FirstName");
        }

        [BindProperty]
        public Checkin Checkin { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Checkin == null)
            {
                return Page();
            }

            if (await _memberService.CheckIfExpired(Checkin.MemberId))
            {
                GetMembers();
                ViewData["Message"] = "Membership is expired for this member.";
                return Page();
            }

            if (await _checkinService.AlreadyCheckedIn(Checkin.MemberId))
            {
                GetMembers();
                ViewData["Message"] = "You have already checked in.";
                return Page();
            }
            Checkin.UpdateAt = DateTime.Now;
            Checkin.CreatedAt = DateTime.Now;
            await _checkinService.SaveAsync(Checkin);

            return RedirectToPage("./Index");
        }
    }
}
