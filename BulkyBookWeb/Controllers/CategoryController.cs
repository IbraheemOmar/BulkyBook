using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryDbContext _db;

        public CategoryController(CategoryDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CategoryModel> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }


        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        //[ValidateAntiForgeryToken]

        public IActionResult Create(CategoryModel obj)
        {

            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Custom Error","Object Name and Object Display Order cannot match");
            }
            if(obj.DisplayOrder > 100 || obj.DisplayOrder < 1)
            {
                ModelState.AddModelError("Display Order", "Display Order Value must be between 1-100");
            }

            if (ModelState.IsValid)
            {
                 _db.Categories.Add(obj);
                _db.SaveChanges();

                TempData["Success"] = "Category created successfully!";
             return  RedirectToAction("Index", "Category");
            }
            return View();
        }


        //GET 
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(c => c.Id == id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Custom Error", "Object Name and Object Display Order cannot match");
            }
            if (obj.DisplayOrder > 100 || obj.DisplayOrder < 1)
            {
                ModelState.AddModelError("Display Order", "Display Order Value must be between 1-100");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();

                TempData["Success"] = "Changes Saved!";

                return RedirectToAction("Index");
            }
            return View(obj);
        }





        //GET && //Post 
        public IActionResult Delete(int? id)
        {
           //GET

            var categoryFromDb = _db.Categories.Find(id);
          

            //POST 

            _db.Categories.Remove(categoryFromDb);
            _db.SaveChanges();

            TempData["Success"] = "Category Deleted!";


            return RedirectToAction("Index");

        }

      
       
    }
}
