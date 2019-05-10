using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class ApiContext : DbContext, IApiContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

    }
}
