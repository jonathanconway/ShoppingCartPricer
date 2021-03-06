﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartPricer.DiscountRules;

namespace ShoppingCartPricer.Tests.DiscountRules
{
    [TestClass]
    public class TenPercentOffInDecemberDiscountRuleTests
    {
        [TestMethod]
        public void CalculateDiscount_WithoutAnyEntries_ReturnsNoDiscount()
        {
            var testRule = new TenPercentOffInDecemberDiscountRule();
            var basketEntries = new List<BasketEntry>();

            var discount = testRule.CalculateDiscount(basketEntries);

            Assert.AreEqual(0M, discount);
        }

        [TestMethod]
        public void CalculateDiscount_WithEntries_ButNotInDecember_ReturnsNoDiscount()
        {
            var testRule = new TenPercentOffInDecemberDiscountRule(new DateTime(2000, 1, 1));
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test1", price: 1), quantity: 1),
                new BasketEntry(new Item(name: "Test2", price: 2), quantity: 1),
            });

            var discount = testRule.CalculateDiscount(basketEntries);

            Assert.AreEqual(0M, discount);
        }

        [TestMethod]
        public void CalculateDiscount_WithEntries_AndInDecember_ReturnsCorrectDiscount()
        {
            var testRule = new TenPercentOffInDecemberDiscountRule(new DateTime(2000, 12, 1));
            var basketEntries = new List<BasketEntry>(new[] {
                new BasketEntry(new Item(name: "Test1", price: 1), quantity: 1),
                new BasketEntry(new Item(name: "Test2", price: 2), quantity: 1),
            });

            var discount = testRule.CalculateDiscount(basketEntries);

            Assert.AreEqual(0.3M, discount);
        }
    }
}
