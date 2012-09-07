using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mango.Tests.Models
{
    public class DocumentTemplate : MangoModel
    {
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
