using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArchiLibrary.Data;
using ArchiAPI.Models;
// gestion de notre ORM est présent sur cette classe 
// classe mère de tout les db context
// classe mère défini par la syntaxe base 
// comparativement à c# qui est base
// injection de dépendances : toujours avoir un dbContext à notre disposition 
// afin de pouvoir gagner en performance et utilise le principe du singleton sans l'utiliser
namespace ArchiAPI.Data
{
    public class ArchiDbContext : BaseDbContext
    {
        public ArchiDbContext (DbContextOptions options): base(options)
        {
            // dire au système d'utiliser ce dbcontext pour par la suite utiliser la bdd

        }
        // une table customers créer ainsi qu'une table Pizzas créer
        // context : classe qui représente une session avec la base de données sous jacente (table) 
        // avec laquelle on peut effectuer des opération CRUD (créer, lire, mettre à jour et supprimer)
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }

        internal object Entry(Customer customer)
        {
            throw new NotImplementedException();
        }

        internal Task SaveChangeAsync()
        {
            throw new NotImplementedException();
        }

    }
}
