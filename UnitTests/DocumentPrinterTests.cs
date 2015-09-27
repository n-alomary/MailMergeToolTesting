namespace UnitTests
{
    using MailMergeTool;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Xunit;

    public class When_I_call_document_printer
    {
        IDocumentFormatter documentFormatter;
        IPrinter printer;
        DocumentPrinter documentPrinter;
        MergedDocument document;
        string formattedDocument;

        public When_I_call_document_printer()
        {
            var fixture = new Fixture();
            document = fixture.Create<MergedDocument>();
            formattedDocument = fixture.Create<string>();

            documentFormatter = Substitute.For<IDocumentFormatter>();
            documentFormatter.DocumentToString(document).Returns(formattedDocument);

            printer = Substitute.For<IPrinter>();

            documentPrinter = new DocumentPrinter(documentFormatter, printer);
            
            // Act
            documentPrinter.Print(document);
        }

        [Fact]
        public void It_should_call_DocumentToString()
        {
            documentFormatter.Received().DocumentToString(document);
        }

        [Fact]
        public void It_should_call_Print_on_printer()
        {
            printer.Received().Print(formattedDocument);
        }
    }
}
