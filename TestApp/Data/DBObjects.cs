using System;
using System.Linq;
using TestApp.Models;

namespace TestApp.Data
{
    public class DBObjects
    {
        public static void Initial(AppDbContent content)
        {
            
            // content.Database.EnsureDeleted();
            // content.Database.EnsureCreated();
           
            if (!content.Contacts.Any())
            {
                Contact contact1 = new Contact()
                {
                    Name = "Ivan",
                    MobilePhone = "+375294651232",
                    JobTitle = "Manager",
                    BirthDate =  new DateTime(2000, 7, 20)
                }; 
                
                Contact contact2 = new Contact()
                {
                    Name = "Andrey",
                    MobilePhone = "+375298858575",
                    JobTitle = "Driver",
                    BirthDate =  new DateTime(1990, 4, 7)
                }; 
                
                Contact contact3 = new Contact()
                {
                    Name = "Vasia",
                    MobilePhone = "+375292348991",
                    JobTitle = "Programmer",
                    BirthDate =  new DateTime(1985, 1, 1)
                }; 
                
                Contact contact4 = new Contact()
                {
                    Name = "Dima",
                    MobilePhone = "+375295426912",
                    JobTitle = "Manager",
                    BirthDate =  new DateTime(1995, 3, 5)
                }; 
                
                content.Contacts.AddRange(contact1, contact2, contact3, contact4);
                content.SaveChanges();
            }
        }
    }
}