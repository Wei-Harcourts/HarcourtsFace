using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harcourts.Face.Website.Models
{
    /// <summary>
    /// Data returned when error occurs.
    /// </summary>
    public class ErrorModel
    {
        public int Status { get; set; }

        public string StatusDescription { get; set; }
    }
}
