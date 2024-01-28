Feature: Basic Bank System API Automation

Scenario Outline: A user can create an account
    Given the user provides account details with "<accountName>" and "<initialBalance>"
    When the user sends a "POST" request to the "/accounts/create" endpoint
    Then the account should be created successfully with a success response
    And Verify no error is returned
    And Verify the success message “Account created successfully”
    And Verify the account details are correctly returned in the JSON response with "<accountName>" and "<initialBalance>"

    Examples:
    | accountName    | initialBalance |
    | Dhanashri      | 5000           |
    | Chavan         | 1000           |

Scenario Outline: A user can delete an account
    Given the user provides an account ID "<accountId>"
    When the user sends a "DELETE" request to the "/accounts/delete" endpoint with <accountId>
    Then the account with ID "<accountId>" should be deleted successfully with a response code of <successStatusCode>
    And the delete should fail with an error if the account does not exist with a response code of <failureStatusCode>

    Examples:
    | accountId | successStatusCode | failureStatusCode |
    | 12345     | 204               | 404               |
    | 45678     | 204               | 404               |

Scenario Outline: A user can deposit to an account
    Given the user provides account ID "<accountId>" and deposit amount "<depositAmount>"
    When the user sends a PUT request to the "/accounts/deposit" endpoint
    Then the deposit should be successful with a success response code
    And the deposit should fail with an error if amount "<depositAmount>" is less than or equal to 0
    And the deposit should fail with an error if account not exists

    Examples:
    | accountId | depositAmount |
    | 12345     | 200           |
    | 45678     | 0           |

Scenario Outline: A user can withdraw from an account
    Given the user provides account ID "<accountId>" and withdrawal amount "<withdrawalAmount>"
    When the user sends a PUT request to the "/accounts/withdraw" endpoint for withdraw
    Then the withdrawal should be successful with a success response code if amount "<withdrawalAmount>" is greater than 0
    And the withdrawal should fail with an error if amount "<withdrawalAmount>" is less than or equal to 0
    And the withdrawal should fail with an error if account not exists

    Examples:
    | accountId | withdrawalAmount |
    | 12345     | 100              |
    | 45678     | 0                |
