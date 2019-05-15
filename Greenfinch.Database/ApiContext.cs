using Greenfinch.Core.Models.Newsletter;
using Greenfinch.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Greenfinch.Database
{
    public class ApiContext : DbContext, IApiContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Subscription> Subscriptions { get; set; }

    }
}
