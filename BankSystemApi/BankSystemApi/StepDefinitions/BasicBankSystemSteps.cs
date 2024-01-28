using NUnit.Framework;
using TechTalk.SpecFlow;
using BankSystemApi.Model;
using BankSystemApi.Utils;
using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

[Binding]
public class BasicBankSystemSteps
{
    private RestResponse _response;
    private dynamic _jsonResponse;

    [Given(@"the user provides account details with (.*) and (.*)")]
    [Obsolete]
    public void GivenTheUserProvidesAccountDetailsWithAnd(string accountName, decimal initialBalance)
    {
        // Create an instance of AccountModel and set properties
        var account = new AccountModel { AccountName = accountName, InitialBalance = initialBalance };
        ScenarioContext.Current["requestBody"] = account;
    }

    [When(@"the user sends a ""(.*)"" request to the ""(.*)"" endpoint")]
    [Obsolete]
    public void WhenTheUserSendsARequestToTheEndpoint(string method, string endpoint)
    {
        var requestBody = ScenarioContext.Current["requestBody"];
        _response = Helper.SendRequest(endpoint, Enum.Parse<Method>(method), requestBody);
        _jsonResponse = JsonConvert.DeserializeObject(_response.Content);
    }

    [Then(@"the account should be created successfully with a success response)")]
    public void ThenTheAccountShouldBeCreatedSuccessfullyWithAResponseCodeOf()
    {
        Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode); 
    }

    [Then(@"Verify no error is returned")]
    public void ThenVerifyNoErrorIsReturned()
    {
        Assert.IsFalse(_jsonResponse.ContainsKey("error"));
    }

    [Then(@"Verify the success message ""(.*)""")]
    public void ThenVerifyTheSuccessMessage(string expectedMessage)
    {
        Assert.AreEqual(expectedMessage, _jsonResponse.message.ToString());
    }

    [Then(@"Verify the account details are correctly returned in the JSON response")]
    public void ThenVerifyTheAccountDetailsAreCorrectlyReturnedInTheJSONResponse(string accountName, decimal initialBalance)
    {
        Assert.AreEqual(initialBalance, _jsonResponse.account_details.initial_balance);
        Assert.AreEqual(accountName, _jsonResponse.account_details.account_name);
    }

    [Given(@"the user provides an account ID ""(.*)""")]
    [Obsolete]
    public void GivenTheUserProvidesAnAccountID(string accountId)
    {
        var account = new AccountModel { AccountId = accountId};
        ScenarioContext.Current["requestBody"] = account;
    }
    
    [When(@"the user sends a (.*) request to the (.*) endpoint with accountnumber (.*)")]
    [Obsolete]
    public void WhenTheUserSendsADELETERequestToTheEndpoint(string method, string endpoint)
    {
        var requestBody = ScenarioContext.Current["requestBody"];
        _response = Helper.SendRequest(endpoint, Enum.Parse<Method>(method), requestBody);       
    }

    [Then(@"the account with ID ""(.*)"" should be deleted successfully with a response code of (.*)")]
    public void ThenTheAccountWithIdShouldBeDeletedSuccessfullyWithAResponseCodeOf(string accountId, int successStatusCode)
    {
        var actualStatusCode = (int)_response.StatusCode;
        Assert.AreEqual(successStatusCode, actualStatusCode);
    }

    [Then(@"the delete should fail with an error if the account does not exist with a response code of (.*)")]
    public void AndTheDeleteShouldFailWithErrorIfTheAccountDoesNotExistWithAResponseCodeOf(int failureStatusCode)
    {
        var actualStatusCode = (int)_response.StatusCode;
        Assert.AreEqual(failureStatusCode, actualStatusCode);
    }

    [Given(@"the user provides account ID (.*) and deposit amount (.*)")]
    [Obsolete]
    public void GivenTheUserProvidesAccountIDAndDepositAmount(string accountId, int depositAmount)
    {
        var account = new AccountModel { AccountId = accountId, DepositAmount = depositAmount };
        ScenarioContext.Current["requestBody"] = account;
    }

    [When(@"the user sends a PUT request to the ""/accounts/deposit"" endpoint")]
    [Obsolete]
    public void WhenTheUserSendsAPUTRequestToTheEndpoint(string method, string endpoint)
    {
        var requestBody = ScenarioContext.Current["requestBody"];        
        _response = Helper.SendRequest(endpoint, Enum.Parse<Method>(method), requestBody);
    }

    [Then(@"the deposit should be successful with a success response code if amount ""(.*)"" is greater than 0")]
    public void ThenTheDepositShouldBeSuccessfulWithASuccessResponseCodeIfAmountIsGreaterThanZero(int depositAmount)
    {
        if (depositAmount > 0)
        {
            Assert.AreEqual(System.Net.HttpStatusCode.OK, _response.StatusCode);
        }
    }

    [Then(@"the deposit should fail with an error if amount ""(.*)"" is less than or equal to 0")]
    public void ThenTheDepositShouldFailWithAnErrorIfAmountIsLessThanOrEqualToZero(int depositAmount)
    {
        if (depositAmount <= 0)
        {
            Assert.AreNotEqual(System.Net.HttpStatusCode.OK, _response.StatusCode);
        }
    }

    [Then(@"the deposit should fail with an error if account not exists")]
    public void ThenTheDepositShouldFailWithAnErrorIfAccountNotExists()
    {
        if (_response.StatusCode != System.Net.HttpStatusCode.NotFound)
        {
            Assert.Fail("Expected a 404 Not Found status code indicating that the account does not exist.");
        }
    }

    [Given(@"the user provides account ID (.*) and withdrawal amount (.*)")]
    [Obsolete]
    public void GivenTheUserProvidesAccountIDAndWithdrawalAmount(string accountId, int withdrawalAmount)
    {
        var account = new AccountModel { AccountId = accountId, WithdrawalAmount = withdrawalAmount };
        ScenarioContext.Current["requestBody"] = account;
    }

    [When(@"the user sends a PUT request to the ""/accounts/withdraw"" endpoint for withdraw")]
    [Obsolete]
    public void WhenTheUserSendsAPUTRequestToTheEndpointForWithdraw(string method, string endpoint)
    {
        var requestBody = ScenarioContext.Current["requestBody"];
        _response = Helper.SendRequest(endpoint, Enum.Parse<Method>(method), requestBody);
    }

    [Then(@"the withdrawal should be successful with a response code of (.*) if amount is greater than 0")]
    public void ThenTheWithdrawalShouldBeSuccessfulWithAResponseCodeOfIfAmountIsGreaterThanZero(int withdrawalAmount)
    {
        if (withdrawalAmount > 0)
        {
            Assert.AreEqual(HttpStatusCode.OK, _response.StatusCode);
        }
    }

    [Then(@"the withdrawal should fail with an error if amount is less than or equal to 0")]
    public void ThenTheWithdrawalShouldFailWithAnErrorIfAmountIsLessThanOrEqualToZero(int withdrawalAmount)
    {
        if (withdrawalAmount <= 0)
        {
            Assert.AreNotEqual(200, (int)_response.StatusCode);
        }
    }

    [Then(@"the withdrawal should fail with an error if account not exists")]
    public void ThenTheWithdrawalShouldFailWithAnErrorIfAccountNotExists()
    {
        if (_response.StatusCode != System.Net.HttpStatusCode.NotFound)
        {
            Assert.Fail("Expected a 404 Not Found status code indicating that the account does not exist.");
        }
    }
}
