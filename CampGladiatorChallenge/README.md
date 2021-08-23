# CampGladiator Challenge

## Installation
First, you 'll need the appsettings file Nico has been emailed. Place this in the directory "CampGladiatorChallenge". Without this, the necessary credentials for my dynamoDB will not be available. Please don't spam or you'll run out of testing budget :). 

You'll need to build and run the CampGladiatorChallenge solution in the root, this can be done from the command line or from your app of choice. 

https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run


## Usage

To send a trainer: 
I used postman, use the API tool of your choice. 
URL: https://localhost:5001/trainer/upsertTrainer/

Body(Type: form - data): 
-email: 
-phone: (this must be only digits - I'd enforce this clientside and api-side in a real environment)
- first_name: 
-last_name:


To retrieve a trainer: 
https://localhost:5001/trainer/{trainerId}

Things I would do to make this complete:
Validation:
-Implement business rules eg make sure I can't enter the same email address twice
- Make sure that phone number field is scrubbed for only numbers - I've had to work with huge databases of string-based phone numbers too many times, which is why you'll see the return output is formatted after retrieval from DB
- Add security, this should be only accepting authenticated users
- Move keys from appsettings file to commvault or some such security area
