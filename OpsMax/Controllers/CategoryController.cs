using Microsoft.AspNetCore.Mvc;
using OpsMax.Data;
using OpsMax.Models;

namespace OpsMax.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// Application DB Context Object
        /// </summary>
        /// <returns></returns>

        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Constructor to implement ApplicationDbContext
        /// </summary>
        /// <returns></returns>

        public CategoryController(ApplicationDbContext db)
        {
            _db = db; //Getting the ApplicationDbContext that has already been registered in application
        }

        public IActionResult Index()
        {
            //Access the categories table here via ApplicationDbContext object and categories DbSet
            IEnumerable<Category> objCategoryList = _db.Categories; // Categories is the DbSet defined in ApplicationDbContext

            return View(objCategoryList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        // Called when we post the create category form
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps in preventing cross site request forgery attacks
        public IActionResult Create(Category obj)
        {
            //Custom Validations
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name and Displayorder cannot be same");
            }

            //Validate the object received
            if (ModelState.IsValid)
            {

                //Add the category object to database
                _db.Categories.Add(obj);
                _db.SaveChanges(); // Saved to database

                //Using TempData for alerts
                TempData["success"] = "Category Created Successfully!";
                //After saving data redirect to index action of category
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if(id == null || id==0)
            {
                return NotFound();
            }

            //Find the category in table with the specified id
            //var categoryFromDB = _db.Categories.SingleOrDefault(c => c.Id == id);
            var categoryFromDB = _db.Categories.Find(id);
            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        //Post
        // Called when we post the edit category form
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps in preventing cross site request forgery attacks
        public IActionResult Edit(Category obj)
        {
            //Custom Validations
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name and Displayorder cannot be same");
            }

            //Validate the object received
            if (ModelState.IsValid)
            {

                //Update the category object 
                _db.Categories.Update(obj);
                _db.SaveChanges(); // Saved to database
                                   //Using TempData for alerts
                TempData["success"] = "Category Edited Successfully!";
                //After saving data redirect to index action of category
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

       

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //Find the category in table with the specified id
            //var categoryFromDB = _db.Categories.SingleOrDefault(c => c.Id == id);
            var categoryFromDB = _db.Categories.Find(id);
            if (categoryFromDB == null)
            {
                return NotFound();
            }

            return View(categoryFromDB);
        }

        //Post
        
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken] // Helps in preventing cross site request forgery attacks
        public IActionResult DeletePOST(int ? id)
        {
                var obj = _db.Categories.Find(id);
                if (obj == null)
                {
                    return NotFound();
                }
            

                //Update the category object 
                _db.Categories.Remove(obj);
                _db.SaveChanges(); // Saved to database
                 //Using TempData for alerts
                TempData["success"] = "Category Deleted Successfully!";

                //After saving data redirect to index action of category
                return RedirectToAction("Index");
            
            }
        
    }
}
