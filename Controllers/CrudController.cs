using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

using Microsoft.AspNetCore.Http;  //This is where session comes from
namespace CRUDelicious.Controllers     //be sure to use your own project's namespace!
{
    public class CrudController : Controller   //remember inheritance??
    {
        private MyContext _context;

        // here we can "inject" our context service into the constructor
        public CrudController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("")]      // Both lines can be written in one line
        public ViewResult Index()
        {
            List<Crud> DishList = _context.Cruds.ToList();
            ViewBag.DishList = DishList;

            return View("Index");
        }
        [HttpGet("new")]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpGet("/home")]

        public IActionResult GoBackHome()
        {
            return RedirectToAction("Index");
        }



        [HttpPost("crud/create")]
        public IActionResult NewRecord(Crud fromForm)
        {
            if (ModelState.IsValid)
            {
                // HttpContext.Session.SetString("Name",fromForm.Name);
                // HttpContext.Session.SetString("Chef",fromForm.Chef);
                // HttpContext.Session.SetInt32("Tastiness",(int)fromForm.Tastiness);
                // HttpContext.Session.SetInt32("Calories",(int)fromForm.Calories);
                // HttpContext.Session.SetString("Description",fromForm.Description);

                _context.Add(fromForm);
                _context.SaveChanges();
                return RedirectToAction("RecordInfo", new { dishId = fromForm.DishId });
            }
            else
            {
                return View("Create");
            }
        }
        //  Get record from dishid 
        [HttpGet("{dishId}")]
        public IActionResult RecordInfo(int dishId)
        {
            //pull a single data from dtabase
            Crud toRender = _context.Cruds.FirstOrDefault(Crud => Crud.DishId == dishId);
            return View(toRender);
        }

        [HttpGet("crud/delete/{dishId}")]

        public RedirectToActionResult DeleteFromSession(int dishId)
        {
            //HttpContext.Session.Clear();
            Crud toDelete = _context.Cruds.FirstOrDefault(Crud => Crud.DishId == dishId);

            _context.Cruds.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("crud/edit/{dishId}")]
        public IActionResult Edit(int dishId)
        {
            Crud edit = _context.Cruds.FirstOrDefault(Crud => Crud.DishId == dishId);
            if(edit == null)
            {
               return RedirectToAction("Index");
            }
            return View("Edit",edit);
        }
        [HttpPost("crud/edit/{dishId}")]
        public IActionResult UpdateRecord(int dishId,Crud fromForm)
        {
            if(ModelState.IsValid)
            {
            Crud inDb = _context.Cruds.FirstOrDefault(crud => crud.DishId == dishId);
            inDb.Name = fromForm.Name;
            inDb.Chef = fromForm.Chef;
            inDb.Tastiness=fromForm.Tastiness;
            inDb.Calories = fromForm.Calories;
            inDb.Description = fromForm.Description;
            inDb.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            
            return RedirectToAction("RecordInfo",new { dishId = dishId});
            }
            else{
                return Edit(dishId);
            }
        }


    }


}




