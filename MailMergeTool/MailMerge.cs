namespace MailMergeTool
{
    using System.Linq;

    public class MailMerge
    {
        readonly IPersonRepository _repository;
        readonly IDocumentPrinter _documentPrinter;

        public MailMerge(IPersonRepository repository, IDocumentPrinter documentPrinter)
        {
            _repository = repository;
            _documentPrinter = documentPrinter;
        }

        public void MergeAndPrint(Document document)
        {
            foreach (var merged in _repository.Persons().Select(person => new MergedDocument { Document = document, Person = person }))
            {
                _documentPrinter.Print(merged);
            }
        }
    }
}