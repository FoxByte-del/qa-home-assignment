using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

public partial class CardValidationServiceTests
{

    [TestCase("")]
    public void ValidateCvc_X_ReturnsTrue(string cvc)
    {
        Assert.That(_service.ValidateCvc(cvc), Is.True);
    }

    [TestCase("")]
    public void ValidateCvc_X_ReturnsFalse(string cvc)
    {
        Assert.That(_service.ValidateCvc(cvc), Is.False);
    }

    [TestCase(null)]
    public void ValidateCvc_X_ThrowsNullException(string cvc)
    {
        Assert.That(() => _service.ValidateCvc(cvc), Throws.ArgumentNullException);
    }
}
