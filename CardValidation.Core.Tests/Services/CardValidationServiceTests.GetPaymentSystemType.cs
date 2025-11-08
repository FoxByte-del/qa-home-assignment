using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

public partial class CardValidationServiceTests
{

    [TestCase("")]
    public void GetPaymentSystemType_X_ReturnsTrue(string cardNumber)
    {
        Assert.That(_service.GetPaymentSystemType(cardNumber), Is.True);
    }

    [TestCase("")]
    public void GetPaymentSystemType_X_ReturnsFalse(string cardNumber)
    {
        Assert.That(_service.GetPaymentSystemType(cardNumber), Is.False);
    }

    [TestCase(null)]
    public void GetPaymentSystemType_X_ThrowsNullException(string cardNumber)
    {
        Assert.That(() => _service.GetPaymentSystemType(cardNumber), Throws.ArgumentNullException);
    }
}
