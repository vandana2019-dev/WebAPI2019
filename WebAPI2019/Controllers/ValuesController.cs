using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI2019.Controllers
{
    public class ValuesController : ApiController
    {
        static List<string> lstStrings = new List<string>()
        {
           "value0", "value1", "value2"
        };

        // GET api/values
        public IEnumerable<string> Get()
        {
            //return new string[] { "value1", "value2" };
            return lstStrings;
        }

        // GET api/values/5
        public string Get(int id)
        {
            //return "value";
            return lstStrings[id];
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
            lstStrings.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
            lstStrings[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            lstStrings.RemoveAt(id);
        }
    }
}
