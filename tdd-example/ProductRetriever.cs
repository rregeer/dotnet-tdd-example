using System;
using Nest;
using entities; 

namespace tddexample
{
	public class ProductRetriever
	{
		private IElasticClient _esClient;

		public ProductRetriever (IElasticClient esClient)
		{
			_esClient = esClient;
		}

		public Product RetrieveProduct(int productId)
		{
			if (!ValidateProductId(productId)) 
			{
				throw new ArgumentException ("Productid is invalid");
			}

			var request = new GetRequest("products", "product", productId.ToString())
			{
				Fields = new PropertyPathMarker[] { "name", "salesPrice", "id" }
			};

			IGetResponse<Product> response;
			try
			{
				response = _esClient.Get<Product> (request);
			}
			catch (Exception error)
			{
				throw new UnExpectedResultException (error.Message);
			}

			if (response != null && !response.Found)
			{
				throw new NotFoundException (String.Format ("Product with id {0} is not found", productId)); 
			}
				
			return response.Source;
		}

		private bool ValidateProductId(int productId) 
		{
			if (productId < 10000)
			{
				return false;
			}

			if (productId > 20000)
			{
				return false;
			}

			return true;
		}
	}
}
