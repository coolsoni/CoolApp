using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CabgemininMVC.Models
{
    [Table("tblSection")]
    public class SectionModel
    {
        [Key]
        public int Id { get; set; }
        public string SectionName { get; set; }

        public int Deptt_Id { get; set; }
    }
}