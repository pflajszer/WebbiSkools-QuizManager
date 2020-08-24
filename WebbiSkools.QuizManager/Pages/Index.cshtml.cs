using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WebbiSkools.QuizManager.DAL.Entities.Identity;

namespace WebbiSkools.QuizManager.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly SignInManager<QuizManagerUser> _signInManager;

		public IndexModel(ILogger<IndexModel> logger, SignInManager<QuizManagerUser> signInManager)
		{
			_logger = logger;
			_signInManager = signInManager;
		}

		public IActionResult OnGet()
		{
			var isUserSignedIn = _signInManager.IsSignedIn(User);
			if (!isUserSignedIn)
			{
				return RedirectToPage("/Account/Login", new { area = "Identity"});
			}
			else
			{
				return Page();
			}
		}
	}
}
