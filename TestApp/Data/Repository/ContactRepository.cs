using System.Collections.Generic;
using System.Linq;
using TestApp.Data.Interfaces;
using TestApp.Models;

namespace TestApp.Data.Repository
{
    public class ContactRepository : IAllContacts
    {
        private readonly AppDbContent _appDbContent;

        public ContactRepository(AppDbContent appDbContent)
        {
            this._appDbContent = appDbContent;
        }

        public IEnumerable<Contact> Contacts => _appDbContent.Contacts;

        public Contact GetContact(int contactId) => Contacts.FirstOrDefault(x => x.Id == contactId);
    }
}