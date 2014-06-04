using System.Collections.Generic;
using System.Web.Http;

namespace TheProblemSolver.ASPNET.Helpers.WebApi
{
    public abstract class ReadOnlyApiController<TModel, TKey> : ApiController where TModel : class
    {
        public IEnumerable<TModel> Get()
        {
            return GetAll();
        }

        public IHttpActionResult Get(TKey id)
        {
            TModel result = GetSingle(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected abstract IEnumerable<TModel> GetAll();
        protected abstract TModel GetSingle(TKey id);
    }
}