namespace MailMergeTool
{
    public class DocumentFormatter : IDocumentFormatter
    {
        private readonly IAddressFormatter _addressFormatter;
        private readonly IPersonFormatter _personFormatter;

        public DocumentFormatter(IAddressFormatter addressFormatter, IPersonFormatter personFormatter)
        {
            _addressFormatter = addressFormatter;
            _personFormatter = personFormatter;
        }

        public string DocumentToString(MergedDocument document)
        {
            var person = _personFormatter.PersonToString(document.Person);
            var address = _addressFormatter.AddressToString(document.Person.Address);

            var tokenizedDocument = document.Document.TokenizedDocument;
            tokenizedDocument = tokenizedDocument.Replace(document.Document.GreetingToken, person);
            tokenizedDocument = tokenizedDocument.Replace(document.Document.AdressToken, address);

            return tokenizedDocument;
        }
    }
}