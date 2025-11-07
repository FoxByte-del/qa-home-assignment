using CardValidation.Core.Services;
using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

[TestFixture]
public class CardValidationServiceTests
{
    private CardValidationService _service;

    [SetUp]
    public void Setup()
    {
        _service = new CardValidationService();
    }

    [TestCase("Meow Purr")]
    public void ValidateOwner_ValidOwner_ReturnsTrue(string owner)
    {
        Assert.That(_service.ValidateOwner(owner), Is.True);
    }

    [TestCase("")]
    public void ValidateOwner_OwnerEmpty_ReturnsFalse(string owner)
    {
        Assert.That(_service.ValidateOwner(owner), Is.False);
    }
}
