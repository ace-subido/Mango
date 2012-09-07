using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mango.Tests.Models;

namespace Mango.Tests.Repository
{
    public class DocumentTemplateRepository : MangoRepository<DocumentTemplate>
    {
        public DocumentTemplateRepository()
            : base("TemplateCollection")
        { }
    }
}
