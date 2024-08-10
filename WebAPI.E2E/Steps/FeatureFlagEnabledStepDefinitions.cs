using TechTalk.SpecFlow.Assist;
using WebAPI.E2E.DataSources;
using WebAPI.E2E.Drivers;
using WebAPI.E2E.Models;

namespace WebAPI.E2E.Steps;

[Binding]
public sealed class FeatureFlagEnabledStepDefinitions
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly ScenarioContext _scenarioContext;
    private readonly ConsoleDriver _consoleDriver;
    private readonly FeatureFlagDataSource _dataSource;

    public FeatureFlagEnabledStepDefinitions(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
        _consoleDriver = new ConsoleDriver();
        _dataSource = new FeatureFlagDataSource(_consoleDriver);
    }

    [Given(@"the feature flags exist")]
    public async Task GivenTheFeatureFlagsExist(Table table)
    {
        var featureFlags = table.CreateSet<FeatureFlag>();
        foreach (var featureFlag in featureFlags)
        {
            Console.WriteLine(await _consoleDriver.CreateFeatureFlag(featureFlag.Id, featureFlag.Enabled));
        }
    }

    [When(@"the feature flag enabled endpoint is opened for the e2e_test_enabled flag")]
    public void WhenTheFeatureFlagEnabledEndpointIsOpenedForTheEeTestEnabledFlag()
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the result should be true")]
    public void ThenTheResultShouldBeTrue()
    {
        ScenarioContext.StepIsPending();
    }
}