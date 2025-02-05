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
            var enabledTestFlagCreated = await _dataSource.CreateFeatureFlag("e2e_test_enabled", true);
            Assert.That(enabledTestFlagCreated, Is.True);
            
            if (enabledTestFlagCreated)
            {
                _scenarioContext.Get<List<string>>(FlagsCreated).Add("e2e_test_enabled");
            }
            
            var disabledTestFlagCreated = await _dataSource.CreateFeatureFlag("e2e_test_disabled", false);
            Assert.That(disabledTestFlagCreated, Is.True);
            
            if (disabledTestFlagCreated)
            {
                _scenarioContext.Get<List<string>>(FlagsCreated).Add("e2e_test_disabled");
            }
        }
    }

    [When(@"the feature flag enabled endpoint is opened for the (enabled|disabled) test flag")]
    public async Task WhenTheFeatureFlagEnabledEndpointIsOpenedForTheEnabledDisabledTestFlag(string flagType)
    {
        var flagId = await _dataSource.GetUniqueId($"e2e_test_{flagType}");
        
        var response = await _request.GetAsync($"/{flagId}/enabled");
        Assert.That(response.Ok, Is.True);
        
        _scenarioContext[Response] = response;
    }

    [Then(@"the result should be (true|false)")]
    public async Task ThenTheResultShouldBeTrue(string expectedResult)
    {
        var jsonResponse = await _scenarioContext.Get<IAPIResponse>(Response).JsonAsync();
        Assert.That(jsonResponse, Is.Not.Null);
        
        if (expectedResult == "true")
        {
            Assert.That(jsonResponse!.Value.GetBoolean(), Is.True);
            return;
        }
        
        Assert.That(jsonResponse!.Value.GetBoolean(), Is.False);
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