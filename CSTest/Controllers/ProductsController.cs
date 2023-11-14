using CSTest.Db.Models;
using CSTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;

namespace CSTest.Controllers
{
    // with proper authorization, this should be here
    // [Authorize]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger; 
        private readonly IConfiguration _configuration;

        public ProductsController(ILogger<ProductsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            Product model = new Product();
            return View("Create", model);
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Product model = new Product();
            using (var context = new CstestdbContext())
            {
                var entity = context.Products.Where(i => i.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    model = entity;
                }
            }
            return View("Create", model);
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            using (var context = new CstestdbContext())
            {
                var entity = context.Products.Where(i => i.Id == id).FirstOrDefault();
                if (entity != null)
                {
                    context.Products.Remove(entity);
                    context.SaveChanges();
                }
            }
            Audit("delete");
            return RedirectToAction(nameof(List));
        }

        public ActionResult List()
        {
            List<ProductModel> model = new List<ProductModel>();
            List<Product> productList = new List<Product>();
            using (var context = new CstestdbContext())
            {
                productList = context.Products.ToList();
            }
            foreach (var item in productList)
            {
                ProductModel productModel = new()
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Amount = item.Amount,
                    Price = item.Price
                };
                double pvn = 0;
                string pvnstring = _configuration.GetValue<string>("pvn");
                double.TryParse(pvnstring, NumberStyles.Any, CultureInfo.InvariantCulture, out pvn);

                productModel.pvnPrice = CalculatePVN(pvn, productModel.Amount, productModel.Price).ToString();
                model.Add(productModel);
            }
            // I would use roles with ViewBag which I would then use in list.cshtml to hide the buttons
            return View("List", model);
        }

        public static double CalculatePVN(double pvn, int amount, double price)
        {
            double pvnResult = (price * amount) * (1 + pvn);
            return Math.Round(pvnResult, 2);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult Create(Product model)
        {
            if (ModelState.IsValid && ValidateProduct(model))
            {
                try
                {
                    string action = "";
                    using (var context = new CstestdbContext())
                    {
                        if (model.Id == 0)
                        {
                            action = "create";
                            context.Products.Add(model);
                            context.SaveChanges();
                        }
                        else
                        {
                            action = "edit";
                            var entity = context.Products.Where(i => i.Id == model.Id).FirstOrDefault();
                            if (entity != null)
                            {
                                context.Entry(entity).CurrentValues.SetValues(model);
                                context.SaveChanges();
                            }
                        }
                    }
                    Audit(action);
                    return RedirectToAction(nameof(List));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(model);
        }

        private bool ValidateProduct(Product product)
        {
            bool isValid = true;
            if (product.ProductName == null)
            {
                ModelState.AddModelError("ProductName", "Product name is required");
                isValid = false;
            }
            return isValid;
        }

        private void Audit(string action)
        {
            using (var context = new CstestdbContext())
            {
                Audit audit = new Audit();
                audit.Action = action;
                audit.Date = DateTime.Now;
                audit.Username = "test";
                context.Audits.Add(audit);
                context.SaveChanges();
            }
        }
    }
}
