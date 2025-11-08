using System.Net.Http.Json;
using Reqnroll;

namespace CardValidation.IntegrationTests.Steps;

[Binding]
public class CardValidationSteps
{
    private readonly ScenarioContext _context;

    public CardValidationSteps(ScenarioContext context)
    {
        _context = context;
    }

    [Given("the following card data is inserted:")]
    public void GivenTheFollowingCardDataIsInserted(DataTable table)
    {
        var row = table.Rows[0];

        var payload = new
        {
            Owner = row["Owner"],
            Number = row["CardNumber"],
            Date = row["IssueDate"],
            Cvv = row["CVV"]
        };

        _context["Payload"] = payload;
    }

    [When("the card validation request is sent")]
    public async Task WhenTheCardValidationRequestIsSent()
    {
        var payload = _context["Payload"];
        var client = Hooks.Hooks.Client!;

        var response = await client.PostAsJsonAsync("/CardValidation/card/credit/validate", payload);

        _context["Response"] = response;
        _context["Body"] = await response.Content.ReadAsStringAsync();
    }

    [Then("the response status code should be {string}")]
    public void ThenTheResponseStatusCodeShouldBe(int expectedStatus)
    {
        var response = (HttpResponseMessage)_context["Response"];
        Assert.That((int)response.StatusCode, Is.EqualTo(expectedStatus));
    }

    [Then("the response should contain {string}")]
    public void ThenTheResponseShouldContain(string text)
    {
        var body = (string)_context["Body"];
        Assert.That(body, Does.Contain(text));
    }
}
