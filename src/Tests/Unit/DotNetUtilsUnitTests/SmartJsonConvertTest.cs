﻿using DotNetUtils;
using Newtonsoft.Json;
using NUnit.Framework;

namespace DotNetUtilsUnitTests
{
    [TestFixture]
    class SmartJsonConvertTest
    {
        private static readonly string ExpectedJson = "{" +
                                                      "\"first_name\":\"John\"," +
                                                      "\"LASTNAME\":\"Doe\"," +
                                                      "\"email_address\":\"a@b.com\"," +
                                                      "\"PhOnE_NuMbEr\":\"1234567890\"," +
                                                      "\"zip_code\":\"53217\"," +
                                                      "\"md5\":\"abcdef\"" +
                                                      "}";

        private static readonly Person ExpectedObject = new Person
                                                        {
                                                            FirstName = "John",
                                                            LastName = "Doe",
                                                            EmailAddress = "a@b.com",
                                                            PhoneNumer = "1234567890",
                                                            ZIPCode = "53217",
                                                            MD5 = "abcdef",
                                                        };

        [Test]
        public void TestSerialize()
        {
            var actualJson = SmartJsonConvert.SerializeObject(ExpectedObject);
            Assert.AreEqual(ExpectedJson, actualJson);
        }

        [Test]
        public void TestDeserialize()
        {
            var actualObject = SmartJsonConvert.DeserializeObject<Person>(ExpectedJson);
            Assert.AreEqual(ExpectedObject, actualObject);
        }
    }

    class Person
    {
        public string FirstName;

        [JsonProperty("LASTNAME")]
        public string LastName;

        public string EmailAddress { get; set; }

        [JsonProperty("PhOnE_NuMbEr")]
        public string PhoneNumer { get; set; }

        public string ZIPCode { get; set; }

        public string MD5 { get; set; }

        protected bool Equals(Person other)
        {
            return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && string.Equals(EmailAddress, other.EmailAddress) && string.Equals(PhoneNumer, other.PhoneNumer) && string.Equals(ZIPCode, other.ZIPCode);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Person) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EmailAddress != null ? EmailAddress.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PhoneNumer != null ? PhoneNumer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ZIPCode != null ? ZIPCode.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return string.Format("FirstName: {0}, LastName: {1}, EmailAddress: {2}, PhoneNumer: {3}, ZIPCode: {4}", FirstName, LastName, EmailAddress, PhoneNumer, ZIPCode);
        }
    }
}
