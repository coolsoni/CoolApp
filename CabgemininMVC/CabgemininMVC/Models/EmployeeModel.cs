using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CabgemininMVC.Models
{
    [Table("Employeee")]
    public class EmployeeModel
    {
      [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public int Branch_Id { get; set; }
        public int Deptt_Id { get; set; }
        public int Section_Id { get; set; }

    }
    
}