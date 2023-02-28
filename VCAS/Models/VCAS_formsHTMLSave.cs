using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VCAS.Models
{
    public class VCAS_formsHTMLSave
    {
        public int Id { get; set; }

        public string name { get; set; }

        public string desc { get; set; }

        [AllowHtml]
        public string form { get; set; }

        public System.DateTime dateModified { get; set; }

        public int FK_location { get; set; }

        public int FK_REF_userRolesId { get; set; }
    }
}