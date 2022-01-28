using ArchiAPI.Data;
using ArchiAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archi.Test.MockDbContext
{
    public class MockDbContexte : ArchiDbContext
    {
        public MockDbContexte(DbContextOptions options) : base(options)
        {

        }

        public static MockDbContexte GetDbContexte (bool withData = true)
        {
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase("dbtest").Options;
            var db = new MockDbContexte(options);

            if(withData)
            {
                db.Add(new Customer { Lastname = "steven", Firstname = "azar", Email = "steve.azark@outlook.fr", Phone = "0606060606" });
                db.Add(new Customer { Lastname = "steven", Firstname = "azar", Email = "steve.azark@outlook.fr", Phone = "0606060606" });
                db.Add(new Customer { Lastname = "steven", Firstname = "azar", Email = "steve.azark@outlook.fr", Phone = "0606060606" });
                db.Add(new Customer { Lastname = "steven", Firstname = " azar", Email = "steven.azark@outlook.fr", Phone = "0606060606" });
                db.Add(new Customer { Lastname = "jean ", Firstname = "david", Email = "jean.david@outlook.fr", Phone = "0706060606" });

                db.SaveChanges();

            }

            return db;
        }

    }
}
