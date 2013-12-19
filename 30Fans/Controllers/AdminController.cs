﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dao.Impl;
using _30Fans.Models;
using Domain;
using _30Fans.Misc;
using System.IO;

namespace _30Fans.Controllers{
    [Authorize()]
    public class AdminController : BaseController{
        private CategoryDao _categoryDao;
        private FileSystemService _fileSystemService;
        public AdminController() {
            _categoryDao = new CategoryDao();
            _fileSystemService = new FileSystemService();
        }
        //
        // GET: /Admin/
        public ActionResult Index(){
            var adminViewModel = new AdminModel();
            adminViewModel.Categoies = _categoryDao.GetaAll();
            return View(adminViewModel);
        }

        //
        // GET: /Admin/Create
        public ActionResult CreateCategory() {
            return View();
        } 

        //
        // POST: /Admin/Create
        [HttpPost]
        public ActionResult CreateCategory(Category category) {
            try{
                _fileSystemService.CreateFolder(Path.Combine(Server.MapPath(ImagePathConstants.CATEGORIES), category.CategoryName));
                _categoryDao.Save(category);
                return RedirectToAction("Index");
            }
            catch{
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5 
        public ActionResult Delete(int id){
            return View();
        }

        //
        // POST: /Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection){
            try{
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }// class
}
