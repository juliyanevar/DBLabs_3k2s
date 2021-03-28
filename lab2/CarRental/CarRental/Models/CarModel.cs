using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string GovernmentPlate { get; set; }
        public BrandModel Brand { get; set; }
        public int YearOfRelease { get; set; }
        public CarTypeModel CarType { get; set; }
        public decimal CostOf1Day { get; set; }
    }
}