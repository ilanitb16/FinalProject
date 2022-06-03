using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class Purchase
    {
        public int UserId { get; set; }
        public int ProductSeriaNumber { get; set; }
        public string ProductType { get; set; }
        public override string ToString()
        {
            return "User Id: " + UserId + " Number of Product: ProductserialNumber: " + ProductSeriaNumber + " Character type: " + ProductType;
        }
    }
}
