using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class RentalModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public CarModel Car { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfIssue { get; set; }
        public int CountOfDays { get; set; }
        public decimal Amount { get; set; }
    }
}