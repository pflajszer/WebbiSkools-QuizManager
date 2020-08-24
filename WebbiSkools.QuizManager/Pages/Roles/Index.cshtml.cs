using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebbiSkools.QuizManager.BRL.ViewModels.Roles;

namespace WebbiSkools.QuizManager.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public string Message { get; private set; }
        public bool Success { get; private set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        [BindProperty]
        public RoleCreate RoleCreate { get; set; }


        private RoleManager<IdentityRole> _roleManager;
        public IndexModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            Roles = _roleManager.Roles;
        }
        public void OnGet()
        {
        }

        public async Task OnPostDelete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    Message = "Successfully deleted the role.";
                }
                else
                {
                    ModelState.AddModelError("sth went wrung key", "sth went wrung");
                }
            }
        }

        public async Task<JsonResult> OnPostCreate()
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                var x = Request.Query["name"];
                try
                {
                    result = await _roleManager.CreateAsync(new IdentityRole(RoleCreate.Name));
                }
                catch (Exception)
                {
                    throw;
                }
                if (result.Succeeded)
                {
                    Message = "Success! Role created!";
                    Success = true;

                }
                else
                {
                    var error = result.Errors.FirstOrDefault();
                    Message = $"Error. {error.Code}: {error.Description}";
                    Success = false;
                }
            }
            else
            {
                Message = "Error has occurred during posting the role. Please review the suggestions below.";
                Success = false;
            }
            return new JsonResult(new
            {
                Success,
                Message
            }); ;
        }

        public IActionResult OnGetRefreshRoleTable()
        {
            Roles = _roleManager.Roles;
            return Partial("_RolesTable", Roles);
        }

    }
}