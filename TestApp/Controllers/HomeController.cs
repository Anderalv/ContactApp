using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        [Route("/HomeController/EditContact")]
        public ViewResult EditContact(string name, string phone, string jobTitle, string birthDate, int id)
        {
            if (!Validate(name, phone, jobTitle, birthDate)) return View("Error",new object());
            
            var contact = _content.Contacts.First(x => x.Id == id);
            
            var dates = birthDate.Split('/');
            var day = int.Parse(dates[0]);
            var month = int.Parse(dates[1]);
            var year = int.Parse(dates[2]);
            
            contact.Name = name;
            contact.MobilePhone = phone;
            contact.JobTitle = jobTitle;
            contact.BirthDate = new DateTime(year,month,day);
            _content.Update(contact);
            
            _content.SaveChanges();
            var contacts = _allContacts.Contacts;
            return View("Index", contacts);
        }
        
        [HttpPost]
        [Route("/HomeController/AddContact")]
        public ViewResult AddContact(string name, string phone, string jobTitle, string birthDate)
        {
            if (!Validate(name, phone, jobTitle, birthDate)) return View("Error",new object()); 
            var dates = birthDate.Split('/');
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

        private bool Validate(string name, string phone, string jobTitle, string birthDate)
        {
            if(CheckName(name))  return false;

            if(CheckPhone(phone)) return false;
            
            if(string.IsNullOrEmpty(jobTitle)) return false;
            
            if(CheckBirthDate(birthDate)) return false;
            
            return true;
        }
        
        private bool CheckPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone)) return false;
            if (phone?[0] == '+' || phone?.Length == 13) return false;
            return true;
        }
	
        private bool CheckName(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (name?.Length > 2 || name[0] == char.ToUpper(name[0])) return false;
            return true;
        }
	
        private bool CheckBirthDate(string birthDate)
        {
            if (string.IsNullOrEmpty(birthDate)) return false;
            if (birthDate?[2] == '/' || birthDate?[5] == '/' || birthDate?.Length == 10) return false;
            return true;
        }
    }
}