using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VCAS.Models;

namespace VCAS.Controllers
{
    public class GlobalSession
    {
        public static string User {get; set;}
        public static string Role {get; set;}
        public static int Location { get; set; }

    }
}