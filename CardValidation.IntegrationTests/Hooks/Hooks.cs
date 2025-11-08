using Microsoft.AspNetCore.Mvc.Testing;
using Reqnroll;

namespace CardValidation.IntegrationTests.Hooks;

[Binding]
public sealed class Hooks
{
    internal static WebApplicationFactory<Program>? Factory;
    internal static HttpClient? Client;

    [BeforeScenario]
    public void BeforeScenario()
    {
        Factory = new WebApplicationFactory<Program>();
        Client = Factory.CreateClient();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        Client?.Dispose();
        Factory?.Dispose();
    }
}
