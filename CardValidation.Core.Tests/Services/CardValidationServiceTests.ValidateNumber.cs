using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

public partial class CardValidationServiceTests
{

    [TestCase("4917300800000000")] // Visa
    [TestCase("5102589999999913")] // Mastercard
    [TestCase("378282246310005")]  // American Express
    public void ValidateNumber_ValidCardNumber_ReturnsTrue(string cardNumber)
    {
        Assert.That(_service.ValidateNumber(cardNumber), Is.True);
    }

    [TestCase("")]
    [TestCase("   ")]
    [TestCase("4917 3008 0000 0000")]
    [TestCase(" 4917300800000000")]
    [TestCase("5102589999999913 ")]
    [TestCase("51025899A9999913")]
    [TestCase("378282246310005!")]
    [TestCase("49173008000000000")]
    [TestCase("491730080000000")]
    public void ValidateNumber_InvalidCardNumber_ReturnsFalse(string cardNumber)
    {
        Assert.That(_service.ValidateNumber(cardNumber), Is.False);
    }

    [TestCase(null)]
    public void ValidateNumber_CardNumberNull_ThrowsNullException(string cardNumber)
    {
        Assert.That(() => _service.ValidateNumber(cardNumber), Throws.ArgumentNullException);
    }
}
