using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SCDT52CW2.Areas.Asset.Controllers;
using SCDT52CW2Data;
using SCDT52CW2Models;

namespace AssetControllerTests
{
    [TestClass]
    public class AssetControllerTest
    {
        private readonly ApplicationDbContext _context;

        public AssetControllerTest(ApplicationDbContext context)
        {
            //Injecting Database dependecy
            _context = context;
        }

        [TestMethod]
        public void UpsertTest()
        {
            //Arrange
            var controller = new AssetController(_context);

            var Id = 7;
            var AssetID = "AssetUnitTest001";
            var Desc = "Unit Test Asset";
            var Location = "UnitTestRoom1";

            //Act
            ActionResult<Assets> = controller.Upsert(Id, AssetID, Desc, Location);

            //Assert
            OkObjectResult result = Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
