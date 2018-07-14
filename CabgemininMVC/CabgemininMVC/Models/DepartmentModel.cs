using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CabgemininMVC.Models
{
    [Table("tblDeptt")]
    public class DepartmentModel
    {
        [Key]
        public int Id { get; set; }
        public string Department { get; set; }

        public int Branch_Id { get; set; }
    }
}