using System;
using System.Collections.Generic;
using System.Text;
using ArchiLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;




namespace ArchiLibrary.Data
{
    public class BaseDbContext : DbContext
    {
        // constructeur 
        // externaliser notre bdd afin que l'on puisse référencer plusieurs projet sur cette base de données

        public BaseDbContext(DbContextOptions options):base(options)
        {

        }
        public override int SaveChanges()
        {
            ChangeDeletedState();
            ChangeCreatedState();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeDeletedState();
            ChangeCreatedState();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ChangeDeletedState()
        {
            var delEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Deleted);
            foreach (var item in delEntities)
            {
                if (item.Entity is BaseModel model)
                {
                    model.Active = false;
                    model.DeletedAt = DateTime.Now;
                    item.State = EntityState.Modified;
                }
            }
        }
        
        
        private void ChangeCreatedState()
        {
            
            var delEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var item in delEntities)
            {
                if (item.Entity is BaseModel model)
                {
                    model.Active = true;
                  
                }
            }
        }


    }
}
