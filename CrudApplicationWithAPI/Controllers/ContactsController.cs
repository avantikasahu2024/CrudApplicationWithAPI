using CrudApplicationWithAPI.Data;
using CrudApplicationWithAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;

namespace CrudApplicationWithAPI.Controllers
{
    [ApiController]
    //[Route("api/contacts")]  //Controller name
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        public readonly ContactAPIDbContext dbContext;

        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
             
        }

        [HttpGet]
        [Route("{Id:guid}")]
        public async Task<IActionResult> GetContactByID([FromRoute] Guid Id)
        {
            var contact = await dbContext.Contacts.FindAsync(Id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContacts(AddContactsRequest addContactsRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContactsRequest.FullName,
                Email = addContactsRequest.Email,
                Phone = addContactsRequest.Phone,
                Address = addContactsRequest.Address
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("{Id:guid}")]
        public async Task<IActionResult> UpdateContacts([FromRoute] Guid Id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(Id);

            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;
                contact.Address = updateContactRequest.Address;

                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }

            return NotFound();
        }

        

        [HttpDelete]
        [Route("{Id:guid}")]
        public async Task<IActionResult> DeleteContactByID([FromRoute] Guid Id)
        {
            var contact = await dbContext.Contacts.FindAsync(Id);

            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }

    }
}
