using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CabgemininMVC.Models;

namespace CabgemininMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private MyDBContext _defContext;

         public EmployeeController()
        {


            _defContext = new MyDBContext();
        }
        public ActionResult Index()
        {


            //var list = _defContext.employee.ToList();

            //return View(list);

            var model = new EmployeeViewModel();
            var details = _defContext.employee.ToList();
            model.EmployeeList = details.OrderByDescending(r => r.Id).ToList().Select(x =>
            {
                var m = new EmployeeViewModel();
                PrepareModelanswer(m, x);
                //PrepareModelListanswer(m);
                return m;
            }).ToList();
            return View(model);
        }

        [NonAction]

        public virtual void PrepareModelanswer(EmployeeViewModel viewmodel,EmployeeModel employeemodel)
        {
            if (employeemodel != null)
            {
                viewmodel.Id = employeemodel.Id;
                viewmodel.Name = employeemodel.Name;
                viewmodel.Age = employeemodel.Age;
                var branch = _defContext.branch.Where(r => r.Id == employeemodel.Branch_Id).ToList();
                viewmodel.BranchName = branch != null ? branch.FirstOrDefault().BranchNmae : "";

                var department = _defContext.deptt.Where(r => r.Id == employeemodel.Deptt_Id).ToList();
                viewmodel.DepartmentName = department != null ? department.FirstOrDefault().Department : "";

                var section = _defContext.section.Where(r => r.Id == employeemodel.Section_Id).ToList();
                viewmodel.SectionName = section != null ? section.FirstOrDefault().SectionName : "";
                viewmodel.Email = employeemodel.Email;
                
            }
        }
        [NonAction]
        public void Dropdownlist()
        {      
           //model.AvailbleBranch.Add(new SelectListItem { Text = "---Select---", Value = "0" });
           // foreach (var i in _defContext.branch.ToList())
           // model.AvailbleBranch.Add(new SelectListItem { Text = i.BranchNmae, Value = i.Id.ToString() });

            var model = new EmployeeModel();
            ViewBag.branch = new SelectList((from s in _defContext.branch.ToList()
                                               select new
                                               {
                                                   Name = s.BranchNmae,
                                                   Id = s.Id
                                               }), "Id", "Name", model.Branch_Id);

            //model.AvailableDeptt.Add(new SelectListItem { Text = "---Select---", Value = "0" });
            //foreach (var i in _defContext.deptt.ToList())
            //model.AvailableDeptt.Add(new SelectListItem { Text = i.Department, Value = i.Id.ToString() });

            //model.AvailbleSection.Add(new SelectListItem { Text = "---Select---", Value = "0" });
            //foreach (var i in _defContext.section.ToList())
            //model.AvailbleSection.Add(new SelectListItem { Text = i.SectionName, Value = i.Id.ToString() });

            ViewBag.Deptt = new SelectList("");
           ViewBag.section = new SelectList("");

        }

        [NonAction]
        public void DropdownListEdit(EmployeeModel modell)
        {

            ViewBag.branch = new SelectList((from s in _defContext.branch.ToList()
                                               select new
                                               {
                                                   Name = s.BranchNmae,
                                                   Id = s.Id
                                               }), "Id", "Name", modell.Branch_Id);


            ViewBag.Deptt = new SelectList((from s in _defContext.deptt.Where(r => r.Id == modell.Deptt_Id).OrderBy(r => r.Department).ToList()
                                            select new
                                            {
                                                Name = s.Department,
                                                Id = s.Id
                                            }), "Id", "Name", modell.Deptt_Id);
            ViewBag.section = new SelectList((from s in _defContext.section.Where(r=> r.Id == modell.Section_Id).OrderBy(r => r.SectionName).ToList()
                                            select new
                                            {
                                                Name = s.SectionName,
                                                Id = s.Id
                                            }), "Id", "Name", modell.Section_Id);

        }

        //Auto Search
        [HttpPost]  //AJAX POST
        public ActionResult GetAutoSearch()
        {
            var Leveltype = _defContext.employee.ToList();

            var list2 = new List<SelectListItem>();
            foreach (var itm in Leveltype)
            {
                var levelname = _defContext.employee.Where(r => r.Id == itm.Id);
                list2.Add(new SelectListItem() { Text = (levelname != null ? levelname.FirstOrDefault().Name : ""), Value = levelname.FirstOrDefault().Id.ToString() });
            }
            return Json(list2);
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
            foreach(var ii in _defContext.section.Where(r=>r.Deptt_Id == Deptt_Id).OrderBy(r=>r.SectionName).ToList())
            {
                lst.Add(new SelectListItem { Text = ii.SectionName, Value = ii.Id.ToString()});
            }
            return Json(lst,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var model = new EmployeeModel();
            Dropdownlist();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel em)
        {
            var viewmodel = new EmployeeViewModel();

            if(ModelState.IsValid)
            {
                viewmodel.Name = em.Name;
                viewmodel.Email = em.Email;
                viewmodel.Age = em.Age;
                viewmodel.Branch_Id = em.Branch_Id;
                //var branchname = _defContext.branch.Where(r => r.Id == em.Branch_Id).FirstOrDefault().BranchNmae;
                //em.BranchName = branchname;

                var branch = _defContext.branch.Where(r => r.Id == em.Branch_Id).ToList();
                viewmodel.BranchName = branch != null ? branch.FirstOrDefault().BranchNmae : "";

                viewmodel.Deptt_Id = em.Deptt_Id;
                viewmodel.Section_Id = em.Section_Id;
                _defContext.employee.Add(em);
                _defContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(em);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
                     
            //var model = new EmployeeViewModel();
            //var mod = _defContext.employee.Find(id);
            //if (mod == null)
            //{
            //    return RedirectToAction("IndexClass");
            //}
            //PrepareModelanswer(model, mod);
           
            //return View(model);

            var mod = _defContext.employee.Find(id);
            DropdownListEdit(mod);
            if (mod == null)
            {          
                return RedirectToAction("Index");
            }
            return View(mod);
        }

        [HttpPost]
        public ActionResult Edit(EmployeeViewModel evm)
        {
            var model = _defContext.employee.Find(evm.Id);
            Dropdownlist();
            if(ModelState.IsValid)
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

        public ActionResult Delete(EmployeeViewModel evm,int id)
        {
            var model = _defContext.employee.Find(id);
            if(ModelState.IsValid)
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
    }
}