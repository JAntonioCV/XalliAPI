using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuAPI.Areas.API.Models
{
    public class OrdenWS
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public DateTime fechaorden { get; set; }
        public DateTime tiempoorden  { get; set; }
        public bool estado { get; set; }

    }
}