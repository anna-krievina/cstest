using CSTest.Controllers;
using System.Security.Principal;

namespace CSTest.Test
{
    [TestClass]
    public class PVNTest
    {
        [TestMethod]
        public void PVNTestMethod()
        {
            int amount = 100;
            double price = 2.0;
            double pvn = 1;
            
            double pvnResult = ProductsController.CalculatePVN(pvn, amount, price);
            Assert.AreEqual(400, pvnResult);
        }
    }
}