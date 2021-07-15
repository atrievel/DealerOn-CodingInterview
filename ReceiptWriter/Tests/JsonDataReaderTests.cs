using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class JsonDataReaderTests
    {
        private IDataReader _uut;
        private const string BAD_FILE_PATH = "not-a-real-file";

        [TestInitialize]
        public void Setup()
        {
            _uut = new JsonDataReader(BAD_FILE_PATH);
        }

        [TestMethod]
        public async Task GroupLineItems_ReturnsCorrectGrouping_ForOneItemAsync()
        {
            // Arrange
            // Act
           int actualLineItemsCount = (await _uut.ReadDataAsync()).Count;

            // Assert
            Assert.AreEqual(0, actualLineItemsCount);
        }

    }
}
