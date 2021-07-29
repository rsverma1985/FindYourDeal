using Companies.data;
using Companies.Repo;
using Companies.Service;
using NUnit.Framework;
using System;

namespace FindBestdeal.Test
{
    public class TestYourBestDeal
    {
       private  IServiceRequest _bestDealService;
        private  IRepository<ShippingRequestModel> _repo;
        [SetUp]
        public void Setup()
        {
            _repo = new Repository<ShippingRequestModel>();
            _bestDealService = new ServiceRequest(_repo);
        }


        [Test]
        public void Shipping_detail_Missing()
        {
            var bestPrice = Assert.Throws<ArgumentNullException>(() => _bestDealService.FetchBestDeal(null));
            Assert.That(bestPrice.Message.Contains("(Parameter 'Request')"));
        }

        [Test]
        public void Shipping_cost_Missing()
        {
            var minShippingCost = _repo.ExtractMinShippingCost(null);
            Assert.That(minShippingCost, Is.Null);
        }

        /// <summary>
        /// Test Case to ensure we, are getting the correct min amount
        /// </summary>
        [Test]
        public void Best_deal_finder()
        {
            var expectedValue = Convert.ToDecimal(8.2);

            var sampleData = new[]
            {
                new ShippingCostResponse() {Amount = Convert.ToDecimal(10.2)},
                new ShippingCostResponse() {Amount = Convert.ToDecimal(13.2)},
                new ShippingCostResponse() {Amount = expectedValue}
            };
            var minShippingCost = _repo.ExtractMinShippingCost(sampleData);
            Assert.That(minShippingCost.Amount, Is.EqualTo(expectedValue));
        }

        [TearDown]
        public void TearDown()
        {
            _repo = null;
        }
    }
}