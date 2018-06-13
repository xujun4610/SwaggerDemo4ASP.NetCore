using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebAPI.SwaggerForASPDotNetCore.BO;

namespace WebAPI.SwaggerForASPDotNetCore.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAllOrigins")] //识别在startup中，名称为“AllowAllOrigins”用户权限方案。  //此方案为允许跨域访问。
    public class DemoController : Controller
    {
        // GET api/values
        [HttpGet]
        [Route("GetValue")]
        public string[] GetValue()
        {
            var r = new Random();
            double randomVal = Convert.ToDouble(MathF.Pow(10, 15)) * r.NextDouble();
            return new string[] { DateTime.Now.ToString(), randomVal.ToString()  };
        }

        // GET api/values/5
        [HttpGet]
        [Route("GetId")]
        public string GetById(int id)
        {
            return id.ToString();
        }

        // POST api/values
        [HttpPost]
        [Route("Post")]
        public string Post([FromBody]string value)
        {
            return "you're executing post method!";
        }

        // PUT api/values/5
        [HttpPut]
        [Route("Put")]
        public string Put([FromBody]User u)
        {
            var jsonStr = JsonConvert.SerializeObject(u);
            return string.Format("you're executing put method!The object string was \n[{0}]", jsonStr);
        }

        // DELETE api/values/5
        [HttpDelete]
        [Route("Delete")]
        public string Delete(int id)
        {
            return "you're executing delete method!";
        }
    }
}
