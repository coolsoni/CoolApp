using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CabgemininMVC.Models
{
    [Table("tbl_branch")]
    public class BranchModel
    {
        [Key]
        public int Id { get; set; }
        public string BranchNmae { get; set; }
    }
}