using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JLL.DVDCentral.BL.Models;
using JLL.DVDCentral.BL;


namespace JLL.ProgDec.API.Controllers
{
    public class FormatController : ApiController
    {
        // GET: api/Format
        public IEnumerable<Format> Get()
        {
            List<Format> formats = FormatManager.Load();
            return formats;
        }

        // GET: api/Format/5
        public Format Get(int id)
        {
            Format format = FormatManager.LoadById(id);
            return format;
        }

        // POST: api/Format
        public void Post([FromBody]Format format)
        {
            FormatManager.Insert(format);
        }

        // PUT: api/Format/5
        public void Put(int id, [FromBody]Format format)
        {
            FormatManager.Update(format);
        }

        // DELETE: api/Format/5
        public void Delete(int id)
        {
            FormatManager.Delete(id);
        }
    }
}
