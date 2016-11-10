using AutoMapper;
using BabyShop.Common;
using BabyShop.Model.Models;
using BabyShop.Service;
using BabyShop.Web.Infratructure.Core;
using BabyShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BabyShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }

        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(productModel);
            var relatedProduct = _productService.GetReatedProducts(id, 6);

            ViewBag.RelatedProduct = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);
            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productViewModel.MoreImages);
            ViewBag.MoreImages = listImages;

            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(id));

            return View(productViewModel);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            int Totalpage = (int)Math.Ceiling((Double)totalRow / pageSize);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            var pagingnationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("Maxpage")),
                TotalCount = totalRow,
                TotalPages = Totalpage
            };
            return View(pagingnationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);
            return Json(new
            {
                Data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            int Totalpage = (int)Math.Ceiling((Double)totalRow / pageSize);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            ViewBag.keyword = keyword;

            var pagingnationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("Maxpage")),
                TotalCount = totalRow,
                TotalPages = Totalpage
            };
            return View(pagingnationSet);
        }

        public ActionResult ListByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByTag(tagId, page, pageSize, out totalRow);
            int Totalpage = (int)Math.Ceiling((Double)totalRow / pageSize);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTag(tagId));

            var pagingnationSet = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                Page = page,
                MaxPage = int.Parse(ConfigHelper.GetByKey("Maxpage")),
                TotalCount = totalRow,
                TotalPages = Totalpage
            };
            return View(pagingnationSet);
        }
    }
}