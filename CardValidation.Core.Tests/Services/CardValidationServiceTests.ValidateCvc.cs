using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

public partial class CardValidationServiceTests
{

    [TestCase("682")]
    [TestCase("6821")]
    [TestCase("000")]
    public void ValidateCvc_ValidCvc_ReturnsTrue(string cvc)
    {
        Assert.That(_service.ValidateCvc(cvc), Is.False);
    }

    [TestCase("")]
    [TestCase("   ")]
    [TestCase("1-2")]
    [TestCase("1@2")]
    [TestCase(" 165")]
    [TestCase("1 5")]
    [TestCase("1A5")]
    [TestCase("68214")]
    [TestCase("14")]
    [TestCase("682!")]
    public void ValidateCvc_InvalidCvc_ReturnsFalse(string cvc)
    {
        Assert.That(_service.ValidateCvc(cvc), Is.False);
    }

    [TestCase(null)]
    public void ValidateCvc_CvcNull_ThrowsNullException(string cvc)
    {
        Assert.That(() => _service.ValidateCvc(cvc), Throws.ArgumentNullException);
    }
}
