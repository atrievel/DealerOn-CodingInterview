using System.Collections.Generic;
using Common;
using Common.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ConsoleReceiptWriterTests
    {
        private IReceiptWriter _uut;

        [TestInitialize]
        public void Setup()
        {
            _uut = new ConsoleReceiptWriter();
        }

        [TestMethod]
        public void GroupLineItems_ReturnsCorrectGrouping_ForOneItem()
        {
            // Arrange
            IList<LineItem> testLineItems = new List<LineItem>();
            LineItem item1 = new() { Name = "test", Cost = 1M, Quantity = 1 };
            testLineItems.Add(item1);

            string expectedKey = $"{item1.Name}-{item1.Cost}";

            // Act
            IDictionary<string, LineItem> actualDictionary = _uut.GroupLineItems(testLineItems);

            // Assert
            Assert.IsTrue(actualDictionary.ContainsKey(expectedKey));
            Assert.AreEqual(1, actualDictionary[expectedKey].Quantity);
        }

        [TestMethod]
        public void GroupLineItems_ReturnsCorrectGrouping_ForMultipleItems()
        {
            // Arrange
            IList<LineItem> testLineItems = new List<LineItem>();
            LineItem item1 = new() { Name = "test", Cost = 1M, Quantity = 1 };
            LineItem item2 = new() { Name = "test", Cost = 1M, Quantity = 1 };
            LineItem item3 = new() { Name = "test2", Cost = 1M, Quantity = 1 };
            testLineItems.Add(item1);
            testLineItems.Add(item2);
            testLineItems.Add(item3);

            string expectedKey = $"{item1.Name}-{item1.Cost}";

            // Act
            IDictionary<string, LineItem> actualDictionary = _uut.GroupLineItems(testLineItems);

            // Assert
            Assert.IsTrue(actualDictionary.ContainsKey(expectedKey));
            Assert.AreEqual(2, actualDictionary[expectedKey].Quantity);
        }
    }
}
