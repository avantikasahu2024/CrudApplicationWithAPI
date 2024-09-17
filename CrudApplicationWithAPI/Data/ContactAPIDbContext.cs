using CrudApplicationWithAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApplicationWithAPI.Data
{
    public class ContactAPIDbContext: DbContext
    {
        public ContactAPIDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
