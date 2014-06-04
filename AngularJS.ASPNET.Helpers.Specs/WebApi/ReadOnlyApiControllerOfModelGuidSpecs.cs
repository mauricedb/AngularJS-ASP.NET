using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheProblemSolver.ASPNET.Helpers.WebApi;

namespace AngularJS.ASPNET.Helpers.Specs.WebApi
{
    [TestClass]
    public class ReadOnlyApiControllerOfModelGuidSpecs
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
            var result = controller.Get(ObjectApiController.Id1) as OkNegotiatedContentResult<Model>;

            // Assert
            result.Should().NotBeNull();
            result.Content.ShouldBeEquivalentTo(new Model {Id = ObjectApiController.Id1});
        }

        [TestMethod]
        public void GetThreeShouldReturnNotFound()
        {
            // Arrange
            var controller = new ObjectApiController();

            // Act
            var result = controller.Get(Guid.NewGuid()) as NotFoundResult;

            // Assert
            result.Should().NotBeNull();
        }

        private class Model
        {
            public Guid Id { get; set; }
        }

        private class ObjectApiController : ReadOnlyApiController<Model, Guid>
        {
            public static readonly Guid Id1 = Guid.Parse("40490564-6C97-4A21-8B8F-AF783D0BE6EE");
            private static readonly Guid Id2 = Guid.Parse("8E6B72E9-6D1A-4DBB-9BFC-9FF20EFBDB9F");

            private readonly List<Model> _list;

            public ObjectApiController()
            {
                _list = new List<Model>
                {
                    new Model {Id = Id1},
                    new Model {Id = Id2},
                };
            }

            protected override IEnumerable<Model> GetAll()
            {
                return _list;
            }

            protected override Model GetSingle(Guid id)
            {
                return _list.SingleOrDefault(m => m.Id == id);
            }
        }
    }
}