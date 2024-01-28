# Basic Bank System API Automation

## API Endpoints
- `/accounts/create`: Create an account
- `/accounts/delete`: Delete an account
- `/accounts/deposit`: Deposit to an account
- `/accounts/withdraw`: Withdraw from an account

## Test Coverage
- Scenario-1: A user can create an account with valid input and verify
	1. account created successfully with a success response
    2. Verify no error is returned
    3. Verify the success message “Account created successfully”
    4. Verify the account details are correctly returned in the JSON response 

- Scenario-2: A user can delete an account with valid input and verify
    1. the account is deleted successfully with a response code of 
    2. the delete should fail with an error if the account does not exist

- Scenario-3: A user can deposit to an account with valid input and verify
    1. the deposit should be successful with a success response code
    2. the deposit should fail with an error if amount is less than or equal to 0
    3. the deposit should fail with an error if account not exists

- Scenario-4: A user can withdraw from an account with valid input and verify
    1. the withdraw should be successful with a success response code
    2. the withdraw should fail with an error if amount is less than or equal to 0
    3. the withdraw should fail with an error if account not exists

## Tech Stacks
- SpecFlow
- RestSharp
- NUnit