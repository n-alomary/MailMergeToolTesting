namespace UnitTests
{
    using FluentAssertions;
    using MailMergeTool;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;

    public class When_I_convert_a_document_to_string
    {
        private DocumentFormatter sut;
        private IPersonFormatter personFormatter;
        private IAddressFormatter addressFormatter;
        private MergedDocument document;
        private string actual;

        public When_I_convert_a_document_to_string()
        {
            // Arrangement
            document = new Fixture().Create<MergedDocument>();
            document.Document.TokenizedDocument = "<<x>> <<y>> World";
            document.Document.GreetingToken = "<<x>>";
            document.Document.AdressToken = "<<y>>";

            personFormatter = Substitute.For<IPersonFormatter>();
            personFormatter.PersonToString(document.Person).Returns("Hello");

            addressFormatter = Substitute.For<IAddressFormatter>();
            addressFormatter.AddressToString(document.Person.Address).Returns("Wide");

            sut = new DocumentFormatter(addressFormatter, personFormatter);
            
            // Act
            actual = sut.DocumentToString(document);
        }

        // Assertions
        [Fact]
        public void It_should_call_PersonToString()
        {
            personFormatter.Received().PersonToString(document.Person);
        }

        [Fact]
        public void It_should_call_AddressToString()
        {
            addressFormatter.Received().AddressToString(document.Person.Address);
        }

        [Fact]
        public void It_should_generate_expected_output()
        {
            const string expected = "Hello Wide World";
            actual.Should().Be(expected);
        }
    }
}
