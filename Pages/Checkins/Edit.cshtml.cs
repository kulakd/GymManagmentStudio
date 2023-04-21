using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;

namespace GymManagmentStudio.Pages.Checkins
{
    public class EditModel : PageModel
    {
        private readonly ICheckinService _checkinService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMemberService _memberService;
        private readonly IPlanService _planService;

        public EditModel(ICheckinService checkinService,
            UserManager<IdentityUser> userManager,
            IMemberService memberService,
            IPlanService planService)
        {
            _checkinService = checkinService;
            _userManager = userManager;
            _memberService = memberService;
            _planService = planService;
        }

        [BindProperty]
        public Checkin Checkin { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var checkin = await _checkinService.GetById(id);
            if (checkin == null)
            {
                return NotFound();
            }
            Checkin = checkin;
            var members = await _memberService.GetMembers();
            var plans = await _planService.GetPlans();
            ViewData["MemberId"] = new SelectList(members, "Id", "Address");
            ViewData["PlanId"] = new SelectList(plans, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Page();
            }
            Checkin.UpdateAt = DateTime.Now;
            Checkin.CreatedAt = DateTime.Now;
            Checkin.CreatedBy = loggedInUser?.UserName;
            await _checkinService.SaveAsync(Checkin);

            return RedirectToPage("./Index");
        }
    }
}
