﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Models
{
    public class AuthenticationContext : DbContext, IAuthenticationContext
    {
        //public AuthenticationContext() { }
        public AuthenticationContext(DbContextOptions options) : base(options) {
            this.Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
    }
}
