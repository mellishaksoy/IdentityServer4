# IdentityServer4

For Get Token Postman Request :
curl -X POST \
  http://localhost:62940/connect/token \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -H 'Postman-Token: a7e5e7d2-2a6d-47ba-9f5c-de9c7494decd' \
  -H 'cache-control: no-cache' \
  -d 'grant_type=password&client_id=Article.API.Client&client_secret=secret&username=melis&password=Password123!&scope=Article.API&undefined='
  
  
  -----------------------------------------------------------------------------------------------------------------------------------------
  
  curl -X POST \
  http://localhost:62940/connect/token \
  -H 'Content-Type: application/x-www-form-urlencoded' \
  -H 'Postman-Token: 80b5e05b-5925-406e-affb-338d43c5f23d' \
  -H 'cache-control: no-cache' \
  -d 'grant_type=password&client_id=Article.API.Client&client_secret=secret&username=admin&password=admin123&scope=Article.API&undefined='
