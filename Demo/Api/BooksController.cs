using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RestDemo.Models;
using TheProblemSolver.ASPNET.Helpers.WebApi;

namespace Demo.Api
{
    public class BooksController : ReadOnlyApiController<Book, int>
    {
        private readonly IBooksRepository _repository;

        public BooksController():this(new BooksRepository())
        {
        }

        public BooksController(IBooksRepository repository)
        {
            _repository = repository;
        }

        protected override Task<IEnumerable<Book>> GetAll()
        {
            return Task.FromResult(_repository.GetBooks().AsEnumerable());
        }

        protected override Task<Book> GetSingle(int id)
        {
            return Task.FromResult(_repository.GetBook(id));
        }
    }
}
