using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{
    public class DBFixture
    {
        public MyShop215736745Context Context { get; private set; }

        public DBFixture()
        { 
            var options = new DbContextOptionsBuilder<MyShop215736745Context>()
                .UseSqlServer("Server=DESKTOP-E0FAPSB\\SQLEXPRESS;Database=Test;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            Context = new MyShop215736745Context(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}
