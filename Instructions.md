# INSTRUCTIONS for Running the Web Api service

- Open project solution with Visual Studio 2019
- Attach the MDF DB file provided embedded in the solution items folder with SQL Server Express 2019 LocalDB (recomended SQL Management Studio 2018)
(Application on sturtup created a text file first time it starts with connection string to localDB, if fails you can play/change the connection string of the file in solution folder to connect it to other DB)
- Run it with IIS Express
- Run Unit tests and integration tests and check all tests are in green
- Test the API with the following CURL commands , or swagger(application startup) or either postman :

	Get Rates : 
		
		curl -X GET "https://localhost:{PORT}/api/v1/vueling/all-rates" -H  "accept: */*" -H  "request: isXml,{VALUE}"

	Get Transactions :

		curl -X GET "https://localhost:{PORT}/api/v1/vueling/all-transactions" -H  "accept: */*" -H  "request: isXml,{VALUE}"

	Get Product Transactions :

		curl -X POST "https://localhost:{PORT}/api/v1/vueling/sku" -H  "accept: */*" -H  "Content-Type: application/json" -d "{\"sku\":\"{SKU_VALUE}\"}"
		WHERE SKU-VALUE must exist in the transactions list (empty list any other case is returned).