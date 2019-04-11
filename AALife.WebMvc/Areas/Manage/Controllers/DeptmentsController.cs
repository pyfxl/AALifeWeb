using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class DeptmentsController : BaseAdminController
    {
        // GET: Manage/Deptments
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manage/Deptments
        [AdminAuthorize]
        public ActionResult Index2()
        {
            return View();
        }

        // GET: Manage/Deptments
        [AdminAuthorize]
        public ActionResult Index3()
        {
            return View();
        }

        // GET: Manage/Deptments/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manage/Deptments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Deptments/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/Deptments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Deptments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Manage/Deptments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manage/Deptments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
