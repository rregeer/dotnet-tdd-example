using NUnit.Framework;
using System;
using entities;
using tddexample;
using Moq;
using Nest;

namespace tddexampletests
{
	[TestFixture]
	public class ProductRetrieverTest
	{
		[Test]
		public void RetrieveProductHappyPath ()
		{
			var expectedResult = new Product() 
			{
				ProductId = 1,
				Name = "Apple Iphone",
				SalesPrice = 500
			};
					
			var fakeResponse = new Mock<IGetResponse<Product>> ();
			fakeResponse.Setup (x => x.Source).Returns (new Product() 
				{
					ProductId = 1,
					Name = "Apple Iphone",
					SalesPrice = 500
				});
			fakeResponse.Setup (x => x.Found).Returns (true);

			var esClientMock = new Mock<IElasticClient> ();
			esClientMock.Setup(x => x.Get<Product>(It.IsAny<IGetRequest>()))
				.Returns(fakeResponse.Object);
		
			var productRetriever = new ProductRetriever (esClientMock.Object);
			var result = productRetriever.RetrieveProduct (10000);

			Assert.AreEqual(result.SalesPrice, expectedResult.SalesPrice);
			Assert.AreEqual(result.ProductId, expectedResult.ProductId);
			Assert.AreEqual (result.Name, expectedResult.Name);
		}

		[Test]
		public void RaiseErrorIfProductNotFound ()
		{
			var fakeResponse = new Mock<IGetResponse<Product>> ();
			fakeResponse.Setup (x => x.Source).Returns (new Product());
			fakeResponse.Setup (x => x.Found).Returns (false);

			var esClientMock = new Mock<IElasticClient> ();
			esClientMock.Setup(x => x.Get<Product>(It.IsAny<IGetRequest>()))
				.Returns(fakeResponse.Object);
			
			var productRetriever = new ProductRetriever (esClientMock.Object);

			Assert.Throws<NotFoundException>(delegate { productRetriever.RetrieveProduct (10001); });
		}

		[Test]
		public void RaiseErrrorIfStorageEngineNotAvailable ()
		{
			var esClientMock = new Mock<IElasticClient> ();
			esClientMock.Setup(x => x.Get<Product>(It.IsAny<IGetRequest>()))
				.Throws<Exception>();

			var productRetriever = new ProductRetriever (esClientMock.Object);

			Assert.Throws<UnExpectedResultException>(delegate { productRetriever.RetrieveProduct (10000); });
		}

		[Test]
		public void InvalidProductId ()
		{
			var esClientMock = new Mock<IElasticClient> ();
		
			var productRetriever = new ProductRetriever (esClientMock.Object);

			Assert.Throws<ArgumentException>(delegate { productRetriever.RetrieveProduct (30000); });
		}
	}
}