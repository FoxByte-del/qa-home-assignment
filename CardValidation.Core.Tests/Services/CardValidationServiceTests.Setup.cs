using NUnit.Framework;
using CardValidation.Core.Services;

namespace CardValidation.Core.Tests.Services;

[TestFixture]
public partial class CardValidationServiceTests
{
    private CardValidationService _service;

    [SetUp]
    public void Setup()
    {
        _service = new CardValidationService();
    }
}
