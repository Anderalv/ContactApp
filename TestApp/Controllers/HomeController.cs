using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using TestApp.Data;
using TestApp.Data.Interfaces;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllContacts _allContacts;
        private readonly AppDbContent _content;
        public HomeController(IAllContacts iAllContacts, AppDbContent content){
            _allContacts = iAllContacts;
            _content = content;
        }
        public ViewResult Index()
        {
            var contacts = _allContacts.Contacts;
            return View(contacts);
        }
            
        [Route("/HomeController/Delete/{id}")]
        public ViewResult DeleteContact(string id)
        {
            
            var idContact = int.Parse(id.Replace("{", "").Replace("}", ""));
            
            
            var contact = _content.Contacts.First(x => x.Id == idContact);
            _content.Contacts.Remove(contact);
            _content.SaveChanges();
            var contacts = _allContacts.Contacts; 
            return View("Index", contacts);
        }
        
        [HttpPost]
        [Route("/HomeController/EditContact/{id}")]
        public ViewResult EditContact(string name, string phone, string jobTitle, string birthDate, string id)
        {
            var idContact = int.Parse(id.Replace("{", "").Replace("}", ""));
            
            var contact = _content.Contacts.First(x => x.Id == idContact);
            contact.Name = name;
            contact.MobilePhone = phone;
            contact.JobTitle = jobTitle;
            // contact.BirthDate = birthDate;
            _content.Update(contact);
            
            // Contact contact = new Contact()
            // {
            //     Name = name,
            //     MobilePhone = phone,
            //     JobTitle = jobTitle,
            //     BirthDate = DateTime.UtcNow //fix
            // };
            // _content.Contacts.Add(contact);
            _content.SaveChanges();
            var contacts = _allContacts.Contacts;
            return View("Index", contacts);
        }
        
        [HttpPost]
        [Route("/HomeController/AddContact")]
        public ViewResult AddContact(string name, string phone, string jobTitle, string birthDate)
        {
            if (!Validate(name, phone, jobTitle, birthDate)) return View("Error",new object()); ////////////????????????????
            var dates = birthDate.Split('.');
            var day = int.Parse(dates[0]);
            var month = int.Parse(dates[1]);
            var year = int.Parse(dates[2]);
            Contact contact = new Contact()
            {
                Name = name,
                MobilePhone = phone,
                JobTitle = jobTitle,
                BirthDate = new DateTime(year,month,day)
            };
            _content.Contacts.Add(contact);
            _content.SaveChanges();
            var contacts = _allContacts.Contacts;
            return View("Index", contacts);
        }

        public bool Validate(string name, string phone, string jobTitle, string birthDate)
        {
            
            if(CheckName(name))  return false;

            if(CheckPhone(phone)) return false;
            
            if(string.IsNullOrEmpty(jobTitle)) return false;
            
            if(CheckBirthDate(birthDate)) return false;
            
            return true;
        }
        
        public bool CheckPhone(string phone)
        {
            if (phone?[0] == '+' || phone?.Length == 13) return false;
            return true;
        }
	
        public bool CheckName(string name)
        {
            if (name?.Length > 2 || name[0] == char.ToUpper(name[0])) return false;
            return true;
        }
	
        public bool CheckBirthDate(string birthDate)
        {
            if (birthDate?[2] == '.' || birthDate?[5] == '.' || birthDate?.Length == 10) return false;
            return true;
        }
    }
}