using System;
using System.Collections.Generic;
using Common;
using Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class CalculatorTests
    {
        private ICalculator _uut;
        private const decimal TAX_RATE = 0.10M;
        private const decimal IMPORT_TAX_RATE = 0.05M;
        private const decimal ROUND_TO = 0.05M;

        [TestInitialize]
        public void Setup()
        {
            _uut = new Calculator(TAX_RATE, IMPORT_TAX_RATE, ROUND_TO);
        }

        [TestMethod]
        public void CalculateLineItemTaxes_ReturnsCorrectTax_ForNonTaxableItems()
        {
            // Arrange
            IList<LineItem> testLineItems = new List<LineItem>();
            LineItem item1 = new() { Quantity = 1, Cost = 10, IsTaxable = false, IsImported = false };
            testLineItems.Add(item1);

            decimal expectedTaxes = 0;

            // Act
            decimal actualTaxes = _uut.CalculateLineItemTaxes(testLineItems)[0].Taxes;

            // Assert
            Assert.AreEqual(expectedTaxes, actualTaxes);
        }

        [TestMethod]
        public void CalculateLineItemTaxes_ReturnsCorrectTax_ForTaxableItems()
        {
            // Arrange
            IList<LineItem> testLineItems = new List<LineItem>();
            LineItem item1 = new() { Quantity = 1, Cost = 14.99M, IsTaxable = true, IsImported = false };
            testLineItems.Add(item1);

            decimal expectedTaxes = 1.50M;

            // Act
            decimal actualTaxes = _uut.CalculateLineItemTaxes(testLineItems)[0].Taxes;

            // Assert
            Assert.AreEqual(expectedTaxes, actualTaxes);
        }

        [TestMethod]
        public void CalculateLineItemTaxes_ReturnsCorrectTax_ForNonTaxableImportedItems()
        {
            // Arrange
            IList<LineItem> testLineItems = new List<LineItem>();
            LineItem item1 = new() { Quantity = 1, Cost = 10M, IsTaxable = false, IsImported = true };
            testLineItems.Add(item1);

            decimal expectedTaxes = 0.50M;

            // Act
            decimal actualTaxes = _uut.CalculateLineItemTaxes(testLineItems)[0].Taxes;

            // Assert
            Assert.AreEqual(expectedTaxes, actualTaxes);
        }

        [TestMethod]
        public void CalculateLineItemTaxes_ReturnsCorrectTax_ForTaxableImportedItems()
        {
            // Arrange
            IList<LineItem> testLineItems = new List<LineItem>();
            LineItem item1 = new() { Quantity = 1, Cost = 47.50M, IsTaxable = true, IsImported = true };
            testLineItems.Add(item1);

            decimal expectedTaxes = 7.15M;

            // Act
            decimal actualTaxes = _uut.CalculateLineItemTaxes(testLineItems)[0].Taxes;

            // Assert
            Assert.AreEqual(expectedTaxes, actualTaxes);
        }

        [TestMethod]
        public void CalculateReceiptTaxes_ReturnsCorrectSum()
        {
            // Arrange
            IList<LineItem> testLineItems = new List<LineItem>();
            LineItem item1 = new() { Taxes = 10.5M };
            LineItem item2 = new() { Taxes = 17.35M };
            testLineItems.Add(item1);
            testLineItems.Add(item2);

            decimal expectedSum = item1.Taxes + item2.Taxes;

            // Act
            decimal actualSum = _uut.CalculateReceiptTaxes(testLineItems);

            // Assert
            Assert.AreEqual(expectedSum, actualSum);
        }

        [TestMethod]
        public void CalculateReceiptTotal_ReturnsCorrectSum()
        {
            // Arrange
            IList<LineItem> testLineItems = new List<LineItem>();
            LineItem item1 = new() { Quantity = 2, Cost = 10M, Taxes = 1.5M };
            LineItem item2 = new() { Quantity = 10, Cost = 9.35M, Taxes = 0M };
            testLineItems.Add(item1);
            testLineItems.Add(item2);

            decimal expectedSum = item1.BaseCost + item2.BaseCost;

            // Act
            decimal actualSum = _uut.CalculateReceiptTotal(testLineItems);

            // Assert
            Assert.AreEqual(expectedSum, actualSum);
        }
    }
}
