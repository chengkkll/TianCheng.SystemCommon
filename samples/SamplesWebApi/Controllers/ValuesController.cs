using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TianCheng.SystemCommon.Services;
using Microsoft.AspNetCore.Hosting;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;

namespace SamplesWebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static ServiceCollection _service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceCollection"></param>
        public ValuesController(ServiceCollection serviceCollection)
        {
            _service = serviceCollection;
        }

        /// <summary>
        ///  GET api/values
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var provider = _service.BuildServiceProvider();
            var depService = provider.GetService<DepartmentService>();

            



            return new string[] { "value1", "value2" };
        }

        /// <summary>
        ///  GET api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        ///  POST api/values
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        /// <summary>
        ///  PUT api/values/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        /// <summary>
        ///  DELETE api/values/5
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
