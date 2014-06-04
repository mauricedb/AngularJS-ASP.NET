using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheProblemSolver.ASPNET.Helpers.WebApi;

namespace AngularJS.ASPNET.Helpers.Specs.WebApi
{
    [TestClass]
    public class ReadOnlyApiControllerOfModel
    {
        [TestMethod]
        public void GetShouldReturnAllModels()
        {
            // Arrange
            var controller = new ObjectApiController();

            // Act
            IEnumerable<Model> result = controller.Get();

            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [TestMethod]
        public void GetOneShouldReturnTheModel()
        {
            // Arrange
            var controller = new ObjectApiController();

            // Act
            var result = controller.Get(1) as OkNegotiatedContentResult<Model>;

            // Assert
            result.Should().NotBeNull();
            //result.Content.Should().NotBeNull();
            result.Content.ShouldBeEquivalentTo(new Model {Id = 1});
        }

        [TestMethod]
        public void GetThreeShouldReturnNotFound()
        {
            // Arrange
            var controller = new ObjectApiController();

            // Act
            var result = controller.Get(3) as NotFoundResult;

            // Assert
            result.Should().NotBeNull();
        }

        private class Model
        {
            public int Id { get; set; }
        }

        private class ObjectApiController : ReadOnlyApiController<Model>
        {
            private readonly List<Model> _list;

            public ObjectApiController()
            {
                _list = new List<Model>
                {
                    new Model {Id = 1},
                    new Model {Id = 2},
                };
            }

            protected override IEnumerable<Model> GetAll()
            {
                return _list;
            }

            protected override Model GetSingle(int id)
            {
                return _list.SingleOrDefault(m => m.Id == id);
            }
        }
    }
}