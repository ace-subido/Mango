using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mango;
using Mango.Tests.Models;
using Mango.Tests.Repository;

namespace Mango.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //create the repository
            DocumentTemplateRepository repository = new DocumentTemplateRepository();            
            Console.ReadKey();

            //create document template
            DocumentTemplate template = new DocumentTemplate();

            template.Body = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed"
                + "do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, "
                + "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. "
                + "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat "
                + "nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia "
                + "deserunt mollit anim id est laborum";

            template.Title = "Sample";
            template.DateCreated = DateTime.Now;
            
            //simple add method that returns the added object with the mongodb generated Id
            DocumentTemplate templateForUpdate = repository.Add(template);

            //simple update
            templateForUpdate.Body = "Update Test";
            templateForUpdate.Title = "Updated Title";
            template.DateCreated = DateTime.Now;

            repository.Update(templateForUpdate);

            DocumentTemplate templateForDelete = templateForUpdate;

            //simple delete method
            repository.Delete(templateForDelete);            
        }
    }
}
