using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebbiSkools.QuizManager.BRL.ViewModels.Roles;
using WebbiSkools.QuizManager.DAL.Entities.Identity;

namespace WebbiSkools.QuizManager.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class UpdateModel : PageModel
    {
        [FromQuery]
        public string Id { get; set; }
        [BindProperty]
        public RoleEdit EditModel { get; set; }
        [BindProperty]
        public RoleModification ModificationModel { get; set; }
        public string Message { get; set; }

        private UserManager<QuizManagerUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public UpdateModel(UserManager<QuizManagerUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGet()
        {
            await PopulateModel(Id);
        }

        public async Task<JsonResult> OnPostUpdate(RoleModification model)
        {
            IdentityResult result;
            var addBuilder = new StringBuilder();
            var removeBuilder = new StringBuilder();
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);
                        if (result.Succeeded)
                        {
                            addBuilder.Append($"Successfully added {user.UserName} to {model.RoleName}. ");
                        }
                        else
                        {
                            addBuilder.Append(result.Errors.Select(x => x.Description).Aggregate(((concat, str) => $"{concat}, {str}")));
                        }
                        //Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (result.Succeeded)
                        {
                            removeBuilder.Append($"Successfully removed {user.UserName} from {model.RoleName}. ");
                        }
                        else
                        {
                            removeBuilder.Append(result.Errors.Select(x => x.Description).Aggregate(((concat, str) => $"{concat}, {str}")));
                        }
                        //Errors(result);
                    }
                }
            }
            return new JsonResult(new
            {
                RoleId = model.RoleId,
                AddMessage = addBuilder.ToString(),
                RemoveMessage = removeBuilder.ToString()
            });
        }

        public async Task<IActionResult> OnGetRefreshMembershipTable(string id)
        {
            await PopulateModel(id);
            return Partial("_MembershipTables", EditModel);
        }

        private async Task PopulateModel(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            List<QuizManagerUser> members = new List<QuizManagerUser>();
            List<QuizManagerUser> nonMembers = new List<QuizManagerUser>();
            foreach (QuizManagerUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            EditModel = new RoleEdit()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
        }
    }
}