using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebbiSkools.QuizManager.DAL.Context;
using WebbiSkools.QuizManager.DAL.Entities.Identity;

[assembly: HostingStartup(typeof(WebbiSkools.QuizManager.Areas.Identity.IdentityHostingStartup))]
namespace WebbiSkools.QuizManager.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}