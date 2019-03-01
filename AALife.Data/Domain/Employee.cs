using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Data.Domain
{
    public partial class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Designation { get; set; }

        public int? ReportTo { get; set; }
    }
}
