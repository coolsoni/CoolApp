using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CabgemininMVC.Models
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            AvailbleBranch = new List<SelectListItem>();
            AvailableDeptt = new List<SelectListItem>();
            AvailbleSection = new List<SelectListItem>();
            studentlist = new List<StudentViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public int Branch_Id { get; set; }

        public string BranchName { get; set; }
        public int Deptt_Id { get; set; }
        public string DepartmentName { get; set; }
        public int Section_Id { get; set; }

        public string Contact { get; set; }

        public string SectionName { get; set; }
        public IList<SelectListItem> AvailableDeptt { get; set; }
        public IList<SelectListItem> AvailbleBranch { get; set; }

        public IList<SelectListItem> AvailbleSection { get; set; }

        public IList<StudentViewModel> studentlist { get; set; }
    }
}