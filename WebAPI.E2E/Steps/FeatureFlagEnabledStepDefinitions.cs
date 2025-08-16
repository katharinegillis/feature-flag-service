using System.Text.Json;
using Microsoft.Playwright;
using NUnit.Framework;
using WebAPI.Common;
using WebAPI.E2E.DataSources;
using WebAPI.E2E.Drivers;

namespace WebAPI.E2E.Steps;

[Binding]
public sealed class FeatureFlagEnabledStepDefinitions
{
    private const string FlagsCreated = "flagsCreated";
    private const string Response = "response";
    private readonly ConsoleDriver _consoleDriver;
    private readonly FeatureFlagDataSource _dataSource;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    private readonly IAPIRequestContext _request;
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;

    public FeatureFlagEnabledStepDefinitions(ScenarioContext scenarioContext, Hooks.Hooks hooks)
    {
        _scenarioContext = scenarioContext;
        _scenarioContext[FlagsCreated] = new List<string>();
        _consoleDriver = new ConsoleDriver();
        _dataSource = new FeatureFlagDataSource(_consoleDriver);
        _request = hooks.Request;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Given("the following feature flags exist")]
    public async Task GivenTheFollowingFeatureFlagsExist(Table table)
    {
        if (await _consoleDriver.ConfigShowDataSource() == "Database")
            foreach (var row in table.Rows)
            {
                var id = row[0];
                var enabled = row[1] == "true";

                var created = await _dataSource.CreateFeatureFlag(id, enabled);
                Assert.That(created, Is.True);

                if (created) _scenarioContext.Get<List<string>>(FlagsCreated).Add(id);
            }
    }

    [When(@"the (v1) feature flag enabled endpoint is opened for the (\w+) feature flag")]
    public async Task WhenTheVFeatureFlagEnabledEndpointIsOpenedForTheWFeatureFlag(string version, string id)
    {
        var flagId = await _dataSource.GetUniqueId(id);

        var response = await _request.GetAsync($"api/{version}/featureflags/{flagId}/enabled");
        Assert.That(response.Ok, Is.True);

        _scenarioContext[Response] = response;
    }

    [Then("the result should be successful and (true|false)")]
    public async Task ThenTheResultShouldBeSuccessfulAndTrueFalse(string expectedResult)
    {
        var response = await _scenarioContext.Get<IAPIResponse>(Response).JsonAsync<ApiResponse<bool?>>(_jsonSerializerOptions);
        Assert.That(response, Is.Not.Null);
        
        Assert.That(response!.Successful, Is.True);

        if (expectedResult == "true")
        {
            Assert.That(response.Data, Is.True);
            return;
        }

        Assert.That(response.Data, Is.False);
    }

    [Then("the result should be unsuccessful with the following errors")]
    public async Task ThenTheResultShouldBeUnsuccessfulWithTheFollowingErrors(Table table)
    {
        var response = await _scenarioContext.Get<IAPIResponse>(Response)
            .JsonAsync<ApiResponse<bool?>>(_jsonSerializerOptions);
        Assert.That(response, Is.Not.Null);
        
        Assert.That(response!.Successful, Is.False);

        foreach (var row in table.Rows)
        {
            Assert.That(response.Errors, Contains.Item(row[0]));
        }
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        foreach (var id in _scenarioContext.Get<List<string>>(FlagsCreated)) await _dataSource.DeleteFeatureFlag(id);
    }
}