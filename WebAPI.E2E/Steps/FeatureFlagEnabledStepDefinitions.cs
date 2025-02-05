using System.Diagnostics;
using Microsoft.Playwright;
using NUnit.Framework;
using WebAPI.E2E.DataSources;
using WebAPI.E2E.Drivers;
using System.Text.Json;

namespace WebAPI.E2E.Steps;

[Binding]
public sealed class FeatureFlagEnabledStepDefinitions
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;
    private readonly ConsoleDriver _consoleDriver;
    private readonly FeatureFlagDataSource _dataSource;
    private readonly IAPIRequestContext _request;

    private const string FlagsCreated = "flagsCreated";
    private const string Response = "response";

    public FeatureFlagEnabledStepDefinitions(ScenarioContext scenarioContext, Hooks.Hooks hooks)
    {
        _scenarioContext = scenarioContext;
        _scenarioContext[FlagsCreated] = new List<string>();
        _consoleDriver = new ConsoleDriver();
        _dataSource = new FeatureFlagDataSource(_consoleDriver);
        _request = hooks.Request;
    }

    [Given(@"the testing feature flags exist")]
    public async Task GivenTheTestingFeatureFlagsExist()
    {
        if (await _consoleDriver.ConfigShowDataSource() == "Database")
        {
            var testFlagCreated = await _dataSource.CreateFeatureFlag("e2e_test_enabled", true);
            Assert.That(testFlagCreated, Is.True);
            
            if (testFlagCreated)
            {
                _scenarioContext.Get<List<string>>(FlagsCreated).Add("e2e_test_enabled");
            }
        }
    }

    [When(@"the feature flag enabled endpoint is opened for the enabled test flag")]
    public async Task WhenTheFeatureFlagEnabledEndpointIsOpenedForTheEnabledTestFlag()
    {
        var flagId = await _dataSource.GetUniqueId("e2e_test_enabled");
        
        var response = await _request.GetAsync($"/{flagId}/enabled");
        Assert.That(response.Ok, Is.True);
        
        _scenarioContext[Response] = response;
    }

    [Then(@"the result should be true")]
    public async Task ThenTheResultShouldBeTrue()
    {
        var jsonResponse = await _scenarioContext.Get<IAPIResponse>(Response).JsonAsync();
        Assert.That(jsonResponse, Is.Not.Null);
        Assert.That(jsonResponse!.Value.GetBoolean(), Is.True);
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        foreach (var flagId in _scenarioContext.Get<List<string>>(FlagsCreated))
        {
            await _dataSource.DeleteFeatureFlag("e2e_test_enabled");
        }
    }
}