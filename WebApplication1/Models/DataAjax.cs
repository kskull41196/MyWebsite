using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DataAjax
    {
        public string item { get; set; }
        public string table { get; set; }
        public string field { get; set; }
        public string action { get; set; }
        public string cmd { get; set; }
    }
}