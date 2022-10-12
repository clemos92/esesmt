using ESESMT.Application.AutoMapper;
using ESESMT.Application.Controllers;
using ESESMT.Domain.CustomFilters;
using ESESMT.Domain.Models;
using ESESMT.Infra.Data.Context;
using ESESMT.Infra.Data.Repository;
using ESESMT.Infra.Shared.Common;
using ESESMT.Infra.Shared.Notifications;
using ESESMT.Service.Services;
using ESESMT.Service.Validators;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ESESMT.Application.UnitTests
{
    public class ChecklistTypeControllerUnitTest
    {
        private ChecklistTypeController GetController(ApplicationDbContext dbContext)
        {
            var repository = new ChecklistTypeRepository(dbContext);
            var notificationContext = new NotificationContext();
            var mapper = AutoMapperConfiguration.RegisterMappings().CreateMapper();
            var validator = new ChecklistTypeValidator();

            var service = new ChecklistTypeService(repository, notificationContext, mapper, validator);

            return new ChecklistTypeController(service);
        }

        [Fact]
        public void TestGetPagedByFilter()
        {
            // Arrange
            var dbContext = DbContextMocker.GetApplicationDbContext(nameof(TestGetPagedByFilter));
            var controller = GetController(dbContext);

            // Act
            var filter = new ChecklistTypeFilter() { PageIndex=0, PageSize= 2};
            var response = controller.GetPagedByFilter(filter);
            var pagedResponse = (response as OkObjectResult).Value as PagedResponse<List<ChecklistTypeDto>>;

            // Assert
            Assert.True(pagedResponse.Data.Count == 2);
        }

        [Fact]
        public void TestGetDetailsById()
        {
            // Arrange
            var dbContext = DbContextMocker.GetApplicationDbContext(nameof(TestGetDetailsById));
            var controller = GetController(dbContext);

            // Act
            var response = controller.GetDetails(2);
            var details = (response as OkObjectResult).Value as ChecklistTypeDto;

            // Assert
            Assert.True(details.Id == 2);
            Assert.True(details.Name == "Tipo 2");
        }

        //[Fact]
        //public void TestFindAllInAnalysisOrInAttendancePaged()
        //{
        //    // Arrange
        //    var dbContext = DbContextMocker.GetApplicationDbContext(nameof(TestFindAllInAnalysisOrInAttendancePaged));
        //    var controller = GetController(dbContext);

        //    // Act
        //    var filter = new ManifestationHistoryFilter() { PageIndex = 0, PageSize = 5, Cpf = "333.333.333-33", Email = "teste3@teste3.com" };
        //    var response = controller.FindAllInAnalysisOrInAttendancePaged(filter);
        //    var pagedResponse = (response as OkObjectResult).Value as PagedResponse<List<ManifestationHistoryModel>>;

        //    // Assert
        //    Assert.True(pagedResponse.Data.Count == 1);
        //    Assert.True(pagedResponse.Data.First().Id == 3);
        //}

        //[Fact]
        //public void TestFindAllAnsweredAttendedOrDeclinedPaged()
        //{
        //    // Arrange
        //    var dbContext = DbContextMocker.GetApplicationDbContext(nameof(TestFindAllAnsweredAttendedOrDeclinedPaged));
        //    var controller = GetController(dbContext);

        //    // Act
        //    var filter = new ManifestationHistoryFilter() { PageIndex = 0, PageSize = 5, Name = "Teste", StartDate = DateTime.Now.AddDays(-44) };
        //    var response = controller.FindAllAnsweredAttendedOrDeclinedPaged(filter);
        //    var pagedResponse = (response as OkObjectResult).Value as PagedResponse<List<ManifestationHistoryModel>>;

        //    // Assert
        //    Assert.True(pagedResponse.Data.Count == 2);
        //    Assert.True(pagedResponse.Data[0].Id == 1);
        //    Assert.True(pagedResponse.Data[1].Id == 4);
        //}
    }
}
