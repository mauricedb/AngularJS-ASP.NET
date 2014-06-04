using System.Collections.Generic;
using System.Web.Http;

namespace TheProblemSolver.ASPNET.Helpers.WebApi
{
    public abstract class ReadOnlyApiController<T> : ApiController where T : class
    {
        public IEnumerable<T> Get()
        {
            return GetAll();
        }

        public IHttpActionResult Get(int id)
        {
            T result = GetSingle(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected abstract IEnumerable<T> GetAll();
        protected abstract T GetSingle(int id);
    }
}