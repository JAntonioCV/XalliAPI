﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MenuAPI.Areas.API.Models
{
    public class DetallesDeOrden
    {
        public int id { get; set; }
        public int cantidadorden { get; set; }
        public string notaorden { get; set; }
        public bool estado { get; set; }
        public int menuid { get; set; }
        public int ordenid { get; set; }

    }
}