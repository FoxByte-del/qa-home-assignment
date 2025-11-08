using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

public partial class CardValidationServiceTests
{

    [TestCase("04/28")]
    [TestCase("05/2028")]
    public void ValidateIssueDate_ValidDateFormat_ReturnsTrue(string issueDate)
    {
        Assert.That(_service.ValidateIssueDate(issueDate), Is.True);
    }


    [TestCase("0/28")]
    [TestCase("/28")]
    [TestCase("01/2 8")]
    [TestCase("A/28")]
    [TestCase("!0/28")]
    [TestCase("0528", Ignore = "Returns True, expected behaviour needs specifying")]
    [TestCase("05/20281")]
    [TestCase("05-28")]
    public void ValidateIssueDate_InvalidDateFormat_ReturnsFalse(string issueDate)
    {
        Assert.That(_service.ValidateIssueDate(issueDate), Is.False);
    }

    [TestCase(null)]
    public void ValidateIssueDate_DateNull_ThrowsNullException(string issueDate)
    {
        Assert.That(() => _service.ValidateIssueDate(issueDate), Throws.ArgumentNullException);
    }

    [TestCase("04/24")]
    public void ValidateIssueDate_DateIsSetInThePast_ReturnsFalse(string issueDate)
    {
        Assert.That(_service.ValidateIssueDate(issueDate), Is.False);
    }
}
