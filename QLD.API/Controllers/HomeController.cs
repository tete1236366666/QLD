using QLD.SERVICE.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QLD.API.Controllers
{
    [RoutePrefix("api/home")]
    [Route("{id?}")]
    public class HomeController : ApiController
    {
        private readonly ISinhVienService _service;
        public HomeController(ISinhVienService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("getall")]
        public IHttpActionResult GetAll()
        {
            var lstData = _service.Queryable().ToList();
            return Ok(lstData);
        }
    }
}
