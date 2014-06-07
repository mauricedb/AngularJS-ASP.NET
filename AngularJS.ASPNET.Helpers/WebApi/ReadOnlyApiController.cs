using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace TheProblemSolver.ASPNET.Helpers.WebApi
{
    public abstract class ReadOnlyApiController<TModel, TKey> : ApiController where TModel : class
    {
        public async Task<IEnumerable<TModel>> Get()
        {
            return await GetAll();
        }

        public async Task<IHttpActionResult> Get(TKey id)
        {
            TModel result = await GetSingle(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected abstract Task<IEnumerable<TModel>> GetAll();
        protected abstract Task<TModel> GetSingle(TKey id);
    }
}