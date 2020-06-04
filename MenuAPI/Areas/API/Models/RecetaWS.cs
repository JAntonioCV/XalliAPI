using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuAPI.Areas.API.Models
{
    public class RecetaWS
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public string tiempoEstimado { get; set; }
        public bool estado { get; set; }
        public string ingrediente { get; set; }
    }
}