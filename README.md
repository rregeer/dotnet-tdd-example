# dotnet-tdd-example
This example contains a product retriever class to retriever a product from a data storage. In the example elasticsearch is used as the as the data storage.

There are a couple of simple test-cases that covers:
- If a product is not found.
- The storage engine is not available.
- The given id of the product is not a valid id.
- Happy path example.

There's no real data in elasticsearch. The client and data is mocked to create the tests.

The product entity contains of the following fields:
- productId (int)
- name (string)
- salesPrice (float)

## Installation
The install and run the test of the code we need a couple of tools:
- Visual Studio or Xamarin Studio
- git

### Installation
If you are not using Visual studio you can install Xamarin Studio. This can be downloaded add: http://www.monodevelop.com/download/
Xamarin studio is using mono and is available for Windows, Mac OS and Linux.
To retrieve the code we need a git client. If the client is installed we can clone the git repository, checkout the code:
```bash
git clone https://github.com/rregeer/dotnet-tdd-example
```
Make sure the Nuget packages are installed. If not run "update NuGet packages" from the project menu. 
The tests can be run using NUnit and can be run from without in IDE.
## Exercise 1: finding bugs.
All the tests are running, but is everything ok?. There can be still bugs in the code we haven't seen and covered.
Take a look at the tests and the code and see if everything is covered and the code is correct.
If you found the bug(s). Correct the code and create a test for it so the bug will never happen again. *Hint* take a good look at the product entity.
## Exercise 2: extending result and refactor code.
Add salesprice object and product information object to the product.
If there´s no salesprice then the salesprice is null.
If there´s no product information then the product information is null.
All the data is in the "product" index the type is "product".

Product:
- productId (int)
- productInformation (object)
- salesPrice (object)

ProductInformation:
- shortName (string)
- fullName (string)
- description (string)

SalesPrice:
- priceExcludingVat (float)
- priceIncludingVat (float)

Example of the elastic-search response in json format:
```js
_source: {
  id: 123456,
  productInformation: {
    shortName: "Apple iPhone 6 GB",
    fullName: "Apple Iphone 6 special edition",
    description: "The Apple Iphone 6 special edition."
    },
    salesPrice: {
      priceExcludingVat: 660.33058,
      priceIncludingVat: 799
    }
  }
}
```
## Exercise 3: refactor code and add new functionality.
Add brand information to the product.
If there´s no brand information then the brand is null.
The brand is in a separate elastic-search index "brand". The type is "brand".
The brand can also be retrieved separately from the product, so create a seperate brandRetriever that can be used in the productRetriever.

Brand:
- brandId (int)
- name (string)

Product:
- brand (object)
- productId (int)
- productInformation (object)
- salesPrice (object)

Example of the product elastic-search response in json format:
```js
_source: {
  id: 123456,
  brandId: 32,
  productInformation: {
    shortName: "Apple iPhone 6 GB",
    fullName: "Apple Iphone 6 special edition",
    description: "The Apple Iphone 6 special edition."
    },
    salesPrice: {
      priceExcludingVat: 660.33058,
      priceIncludingVat: 799
    }
  }
}
```
Example of the brand elastic-search response in json format:
```js
_source: {
  id: 12,
  name: "Apple"
}
```
