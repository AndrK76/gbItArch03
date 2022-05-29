using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AndrK.ZavPostav.DomainModel;

namespace AndrK.ZavPostav.REST.Models
{
    public class SubjectContent
    {
        public string SubjectType { get; set; }

        public BaseSubject Subject { get; set; }
    }
}