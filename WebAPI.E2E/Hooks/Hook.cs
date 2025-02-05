using System;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace WebAPI.E2E.Hooks
{
    [Binding]
    public class Hooks
    {
        public IAPIRequestContext Request { get; private set; } = null!;

        [BeforeScenario]
        public async Task RegisterSingleInstancePractitioner()
        {
            var playwright = await Playwright.CreateAsync();
            
            var headers = new Dictionary<string, string> { { "Accept", "application/json" } };
            Request = await playwright.APIRequest.NewContextAsync(new()
                {
                    BaseURL = "http://localhost:8181",
                    ExtraHTTPHeaders = headers,
                }
            );
        }
    }
}