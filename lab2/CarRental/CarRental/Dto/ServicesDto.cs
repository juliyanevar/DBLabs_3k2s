using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Dto
{
    public class ServicesByDateDto
    {
        [DataType(DataType.Date)]
        public DateTime FirstDay { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastDay { get; set; }
    }
}