using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class ParametersController : BaseAdminController
    {
        // GET: Manage/Parameters
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TreeView()
        {
            return View();
        }

        // GET: Manage/Parameters/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manage/Parameters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manage/Parameters/Create
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

        // GET: Manage/Parameters/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Manage/Parameters/Edit/5
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

        // GET: Manage/Parameters/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Manage/Parameters/Delete/5
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
