# CampGladiator Challenge

## Installation
First, you 'll need the appsettings file Nico has been emailed. Place this in the directory "CampGladiatorChallenge". Without this, the necessary credentials for my dynamoDB will not be available. Please don't spam or you'll run out of testing budget :). 

You'll need to build and run the CampGladiatorChallenge solution in the root, this can be done from the command line or from your app of choice. 

https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run
Requires latest dotnet - https://dotnet.microsoft.com/download/dotnet/3.1

## Usage

To send a trainer: 
I used postman, use the API tool of your choice. Turn off SSL verification. 
URL: http://localhost:5000/trainer/upsertTrainer/

Body(Type: form - data): 
-email: 
-phone: (this must be only digits - I'd enforce this clientside and api-side in a real environment)
- first_name: 
-last_name:

The response body should inclide a guid for the trainer - ex da1838b9-2dbe-4893-8aed-3fa4412acf75. 


To retrieve a trainer - use the trainer ID created in your send step or use the example above. 
http://localhost:5000/trainer/{trainerId} - 

Things I would do to make this complete:
Validation:
-Implement business rules eg make sure I can't enter the same email address twice
- Make sure that phone number field is scrubbed for only numbers - I've had to work with huge databases of string-based phone numbers too many times, which is why you'll see the return output is formatted after retrieval from DB
- Add security, this should be only accepting authenticated users
- Move keys from appsettings file to commvault or some such security area
