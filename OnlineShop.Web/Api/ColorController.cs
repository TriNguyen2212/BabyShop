using AutoMapper;
using BabyShop.Model.Models;
using BabyShop.Service;
using BabyShop.Web.Infratructure.Core;
using BabyShop.Web.Infratructure.Extensions;
using BabyShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace BabyShop.Web.Api
{
    [RoutePrefix("api/color")]
    //[Authorize]
    public class ColorController : ApiControllerBase
    {
        #region Initialize

        private IColorService _colorService;

        public ColorController(IErrorService errorService, IColorService colorService)
            : base(errorService)
        {
            this._colorService = colorService;
        }

        #endregion Initialize

        [Route("getallpaging")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetAllPaging(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _colorService.GetAll(keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Color>, IEnumerable<ColorViewModel>>(query);

                var paginationSet = new PaginationSet<ColorViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _colorService.GetAll().OrderByDescending(x => x.Name);
                var responseData = Mapper.Map<IEnumerable<Color>, IEnumerable<ColorViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _colorService.GetById(id);
                var responseData = Mapper.Map<Color, ColorViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldcolor = _colorService.Delete(id);
                    _colorService.SaveChanges();

                    var responseData = Mapper.Map<Color, ColorViewModel>(oldcolor);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedcolors)
        {
            return CreateHttpResponse(request, () =>
            {
                var listcolor = new JavaScriptSerializer().Deserialize<List<int>>(checkedcolors);

                foreach (var item in listcolor)
                {
                    _colorService.Delete(item);
                }
                _colorService.SaveChanges();
                var response = request.CreateResponse(HttpStatusCode.OK, listcolor.Count);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ColorViewModel colorVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newcolor = new Color();
                    newcolor.UpdateColor(colorVM);
                    if (_colorService.GetAll().Where(x => x.Name == newcolor.Name.Trim()).Count() > 0)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest);
                        response.Content = new StringContent("Tên color đã tồn tại", Encoding.Unicode);
                    }
                    else
                    {
                        _colorService.Add(newcolor);
                        _colorService.SaveChanges();
                        var responseData = Mapper.Map<Color, ColorViewModel>(newcolor);
                        response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    }
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ColorViewModel colorVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbcolor = _colorService.GetById(colorVM.ID);
                    dbcolor.UpdateColor(colorVM);
                    if (_colorService.GetAll().Where(x => x.ID != dbcolor.ID && x.Name == dbcolor.Name.Trim()).Count() > 0)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest);
                        response.Content = new StringContent("Tên màu sắc đã tồn tại", Encoding.Unicode);
                    }
                    else
                    {
                        _colorService.Update(dbcolor);
                        _colorService.SaveChanges();
                        var responseData = Mapper.Map<Color, ColorViewModel>(dbcolor);
                        response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    }
                }

                return response;
            });
        }
    }
}