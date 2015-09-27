namespace UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using MailMergeTool;
    using NSubstitute;
    using Ploeh.AutoFixture;
    using Properties;
    using Xunit;

    public class When_merge_a_document
    {
        MailMerge tool;
        IDocumentPrinter printer;
        IPersonRepository repository;
        Person[] persons;
        List<Person> receivedPersons = new List<Person>(); 
        List<Document> receivedDocuments = new List<Document>();
        Document document;

        public When_merge_a_document()
        {
            // Arrangement
            persons = new Fixture().CreateMany<Person>(5).ToArray();

            repository = Substitute.For<IPersonRepository>();
            repository.Persons().Returns(persons);

            printer = Substitute.For<IDocumentPrinter>();
            printer.When(p => p.Print(Arg.Any<MergedDocument>())).Do(call =>
            {
                receivedPersons.Add(call.Arg<MergedDocument>().Person);
                receivedDocuments.Add(call.Arg<MergedDocument>().Document);
            });

            tool = new MailMerge(repository, printer);

            document = new Document {TokenizedDocument = Resources.TestDocument};

            // Act
            tool.MergeAndPrint(document);
        }

        [Fact]
        public void It_should_use_the_persons_in_the_repository()
        {
            // Assert
            repository.Received().Persons();
        }

        [Fact]
        public void It_should_invoke_Print_method_of_printer()
        {
            // Assert
            printer.ReceivedWithAnyArgs(5).Print(null);
        }

        [Fact]
        public void It_should_pass_person_to_printer()
        {
            // Assert
            receivedPersons.ShouldBeEquivalentTo(persons);
        }

        [Fact]
        public void It_should_pass_document_to_printer()
        {
            // Assert
            receivedDocuments.Distinct().Single().ShouldBeEquivalentTo(document);
        }
    }
}
