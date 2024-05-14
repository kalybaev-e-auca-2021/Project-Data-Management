using Application.Interfaces;
using Application.ProjectCommands.Commands;
using Application.ProjectExtensions;
using AutoMapper;
using Infrastructure;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Common;

namespace Tests.UnitTests
{
    [Collection("QueryCollection")]
    public class GetProjectListQueryHandlerTest
    {
        private readonly ApplicationDbContext Context;
        private readonly IMapper Mapper;

        public GetProjectListQueryHandlerTest(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async void GetProjectListQueryHandler_Success()
        {
            //Arrange
            var handler = new GetProjectListQueryHandler(Context, Mapper);

            //Act
            var result = await handler.Handle(
                new GetProjectListQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<ProjectListDto>();
            result.Projects.Count.ShouldBe(4);
        }

    }
}