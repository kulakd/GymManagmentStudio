using Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace GymManagmentStudio.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly IMemberService _memberService;

        public IndexModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IList<Member> Member { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Member = await _memberService.GetMembers();
        }
    }
}
