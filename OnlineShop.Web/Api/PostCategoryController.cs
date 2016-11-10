using AutoMapper;
using BabyShop.Model.Models;
using BabyShop.Service;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BabyShop.Web.Infratructure.Extensions;
using BabyShop.Web.Infratructure.Core;
using BabyShop.Web.Models;

namespace BabyShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        private IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) :
            base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var ListCategory = _postCategoryService.GetAll();
                var listPostCategoryVM = Mapper.Map<List<PostCategoryViewModel>>(ListCategory);
                HttpResponseMessage response= request.CreateResponse(HttpStatusCode.OK, listPostCategoryVM);
                return response;
            });

        }
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
             {
                 HttpResponseMessage response = null;

                 if (ModelState.IsValid)
                 {
                     request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                 }
                 else
                 {
                     PostCategory newPostCategory = new PostCategory();
                     newPostCategory.UpdatePostCategory(postCategoryVM);
                     var Category = _postCategoryService.Add(newPostCategory);
                     _postCategoryService.Save();

                     response = request.CreateResponse(HttpStatusCode.Created, Category);
                 }

                 return response;
             });
        }
        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, PostCategoryViewModel postCategoryVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var postCategoryDB = _postCategoryService.GetById(postCategoryVM.ID);
                    postCategoryDB.UpdatePostCategory(postCategoryVM);
                    _postCategoryService.Update(postCategoryDB);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (ModelState.IsValid)
                {
                    request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }
    }
}