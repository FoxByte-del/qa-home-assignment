using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

public partial class CardValidationServiceTests
{

    [TestCase("")]
    public void ValidateNumber_X_ReturnsTrue(string cardNumber)
    {
        Assert.That(_service.ValidateNumber(cardNumber), Is.True);
    }

    [TestCase("")]
    public void ValidateNumber_X_ReturnsFalse(string cardNumber)
    {
        Assert.That(_service.ValidateNumber(cardNumber), Is.False);
    }

    [TestCase(null)]
    public void ValidateNumber_X_ThrowsNullException(string cardNumber)
    {
        Assert.That(() => _service.ValidateNumber(cardNumber), Throws.ArgumentNullException);
    }
}
