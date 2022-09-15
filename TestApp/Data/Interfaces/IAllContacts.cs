using System.Collections.Generic;
using TestApp.Models;

namespace TestApp.Data.Interfaces
{
    public interface IAllContacts
    {
        IEnumerable<Contact> Contacts { get; }
    }
}