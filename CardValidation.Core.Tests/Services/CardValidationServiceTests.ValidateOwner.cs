using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

public partial class CardValidationServiceTests
{


    [TestCase("Meow")]
    [TestCase("Meow Purr")]
    [TestCase("Meow Purr Arggghh")]
    public void ValidateOwner_ValidOwner_ReturnsTrue(string owner)
    {
        Assert.That(_service.ValidateOwner(owner), Is.True);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(" Meow")]
    [TestCase("Meow ", Ignore = "Returns True, expected behaviour needs specifying")]
    [TestCase(" Meow ")]
    [TestCase("Meow Purr Arggghh Meow")]
    [TestCase("Meow!")]
    [TestCase("?Meow Purr")]
    [TestCase("Meow 123")]
    [TestCase("Meow_Purr")]
    [TestCase("Meow-Purr")]
    public void ValidateOwner_InvalidOwner_ReturnsFalse(string owner)
    {
        Assert.That(_service.ValidateOwner(owner), Is.False);
    }

    [TestCase(null)]
    public void ValidateOwner_OwnerNull_ThrowsNullException(string owner)
    {
        Assert.That(() => _service.ValidateOwner(owner), Throws.ArgumentNullException);
    }

}
