using System;
using CardValidation.Core.Enums;
using NUnit.Framework;

namespace CardValidation.Core.Tests.Services;

public partial class CardValidationServiceTests
{

    [TestCase("4484070000000000", PaymentSystemType.Visa)]
    [TestCase("5102589999999913", PaymentSystemType.MasterCard)]
    [TestCase("378282246310005", PaymentSystemType.AmericanExpress)]
    public void GetPaymentSystemType_ValidCardNumber_ReturnsSystemType(string cardNumber, PaymentSystemType systemType)
    {
        Assert.That(_service.GetPaymentSystemType(cardNumber), Is.EqualTo(systemType));
    }

    [TestCase("")]
    [TestCase("1484070000000000")]
    public void GetPaymentSystemType_InvalidCardNumber_ThrowsNotImplementedException(string cardNumber)
    {
        Assert.That(() => _service.GetPaymentSystemType(cardNumber),Throws.TypeOf<NotImplementedException>());
    } 

    [TestCase(null)]
    public void GetPaymentSystemType_CardNumberNull_ThrowsNullException(string cardNumber)
    {
        Assert.That(() => _service.GetPaymentSystemType(cardNumber), Throws.ArgumentNullException);
    }
}