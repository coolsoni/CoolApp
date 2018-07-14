using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CabgemininMVC.Models;

namespace CabgemininMVC.Controllers
{
    public class StudentController : Controller
    {
        private MyDBContext _defContext;


        public StudentController()
        {
            _defContext = new MyDBContext();
        }

        [NonAction]

        public virtual void PrepareModelanswer(StudentViewModel viewmodel, StudentModel studenymodel)
        {
            if (studenymodel != null)
            {
                viewmodel.Id = studenymodel.Id;
                viewmodel.Name = studenymodel.Name;
                viewmodel.Age = studenymodel.Age;
                viewmodel.Branch_Id = studenymodel.Branch_Id;
                viewmodel.Deptt_Id = studenymodel.Deptt_Id;
                viewmodel.Section_Id = studenymodel.Section_Id;
                viewmodel.Contact = studenymodel.Contact;
                var branch = _defContext.branch.Where(r => r.Id == studenymodel.Branch_Id).ToList();
                viewmodel.BranchName = branch != null ? branch.FirstOrDefault().BranchNmae : "";

                var department = _defContext.deptt.Where(r => r.Id == studenymodel.Deptt_Id).ToList();
                viewmodel.DepartmentName = department != null ? department.FirstOrDefault().Department : "";

                var section = _defContext.section.Where(r => r.Id == studenymodel.Section_Id).ToList();
                viewmodel.SectionName = section != null ? section.FirstOrDefault().SectionName : "";
                viewmodel.Email = studenymodel.Email;
                viewmodel.Contact = studenymodel.Contact;

            }
        }
        [NonAction]
        public void Dropdownlist(StudentViewModel viewmodel)
        {
            /// viewmodel.AvailbleBranch.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var i in _defContext.branch.OrderBy(r => r.BranchNmae).ToList())
             viewmodel.AvailbleBranch.Add(new SelectListItem { Text = i.BranchNmae, Value = i.Id.ToString() });


            viewmodel.AvailableDeptt.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var i in _defContext.deptt.OrderBy(r => r.Department).ToList())
            viewmodel.AvailableDeptt.Add(new SelectListItem { Text = i.Department, Value = i.Id.ToString() });


            viewmodel.AvailbleSection.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var i in _defContext.section.OrderBy(r => r.SectionName).ToList())
                viewmodel.AvailbleSection.Add(new SelectListItem { Text = i.SectionName, Value = i.Id.ToString() });
        }


        [NonAction]
        public void DropdownlistEdit(StudentViewModel viewmodel ,StudentModel student)
        {
            viewmodel.AvailbleBranch.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var i in _defContext.branch.OrderBy(r => r.BranchNmae).ToList())
                viewmodel.AvailbleBranch.Add(new SelectListItem { Text = i.BranchNmae, Value = i.Id.ToString() });


            viewmodel.AvailableDeptt.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var i in _defContext.deptt.OrderBy(r => r.Department).Where(r=>r.Id==student.Deptt_Id).ToList())
                viewmodel.AvailableDeptt.Add(new SelectListItem { Text = i.Department, Value = i.Id.ToString() });


            viewmodel.AvailbleSection.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            foreach (var i in _defContext.section.OrderBy(r => r.SectionName).Where(r=>r.Id==student.Section_Id).ToList())
                viewmodel.AvailbleSection.Add(new SelectListItem { Text = i.SectionName, Value = i.Id.ToString() });
        }
        public ActionResult Index()
        {
            var model = new StudentViewModel();
            var details = _defContext.student.ToList();
            model.studentlist = details.OrderByDescending(r => r.Id).ToList().Select(x =>
            {
                var m = new StudentViewModel();
                PrepareModelanswer(m, x);
                //PrepareModelListanswer(m);
                return m;
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new StudentViewModel();
            Dropdownlist(model);       
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(StudentViewModel model, HttpPostedFileBase file)
        {
            var fileName = "";
            if (file != null)
            {

                var extension = Path.GetExtension(file.FileName);
                string fileNameOnly = Path.GetFileNameWithoutExtension(file.FileName);

                if (extension == ".PNG" || extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {

                    fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                    var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  

                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                    string myfile = name + ext; //appending the name with id  
                                                // store the file inside ~/project folder(Img)  

                    string sss = Server.MapPath("~/Content/images/").Replace("\\", "/");
                    try// if (System.IO.File.Exists(sss))
                    {
                        var path = Path.Combine(Server.MapPath("~/Content/images"), myfile);
                        file.SaveAs(path);
                    }
                    catch (Exception e)
                    {

                        //ViewBag.Result = _alertMessage.messagewarning("Format Is Not Correct", "Warning");
                        return RedirectToAction("Create");
                    }
                }
            }
            Dropdownlist(model);
           
            if (ModelState.IsValid)
            {
               var student = new StudentModel();
                student.Name = model.Name;
                student.Age = model.Age;
                student.Email = model.Email;
                student.Branch_Id = model.Branch_Id;
                student.Deptt_Id = model.Deptt_Id;
                student.Section_Id = model.Section_Id;
                _defContext.student.Add(student);
                _defContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
           
        }

        
        public ActionResult Delete(StudentViewModel evm, int id)
        {
            var model = _defContext.student.Find(id);
            if (ModelState.IsValid)
            {
                model.Id = evm.Id;
                model.Name = evm.Name;
                model.Age = evm.Age;
                model.Email = evm.Email;
                model.Branch_Id = evm.Branch_Id;
                model.Deptt_Id = evm.Deptt_Id;

                _defContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _defContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(StudentViewModel studentviewmodel,int id)
        {
            var model = _defContext.student.Find(id);
            var idmapping = model;
            DropdownlistEdit(studentviewmodel, idmapping);
            studentviewmodel.Name = model.Name;
            studentviewmodel.Age = model.Age;
            studentviewmodel.Email = model.Email;
            studentviewmodel.Contact = model.Contact;
            studentviewmodel.Branch_Id = model.Branch_Id;
            studentviewmodel.Deptt_Id = model.Deptt_Id;
            studentviewmodel.Section_Id = model.Section_Id;
            return View(studentviewmodel);
        }


        [HttpPost]
        public ActionResult GetDepttbyBranchId(int Branch_Id)
        {
            var lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = "--Select Depart--", Value = "0" });

            foreach (var ii in _defContext.deptt.Where(r => r.Branch_Id == Branch_Id).OrderBy(r => r.Department).ToList())
            {
                lst.Add(new SelectListItem { Text = ii.Department, Value = ii.Id.ToString() });
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]

        public ActionResult GetSectionbyDepttId(int Deptt_Id)
        {
            var lst = new List<SelectListItem>();
            lst.Add(new SelectListItem { Text = "---Select Section---", Value = "0" });
            foreach (var ii in _defContext.section.Where(r => r.Deptt_Id == Deptt_Id).OrderBy(r => r.SectionName).ToList())
            {
                lst.Add(new SelectListItem { Text = ii.SectionName, Value = ii.Id.ToString() });
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }


    }
}