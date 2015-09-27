namespace MailMergeTool
{
    public class DocumentPrinter : IDocumentPrinter
    {
        readonly IDocumentFormatter _documentFormatter;
        readonly IPrinter _printer;

        public DocumentPrinter(IDocumentFormatter documentFormatter, IPrinter printer)
        {
            _documentFormatter = documentFormatter;
            _printer = printer;
        }

        public void Print(MergedDocument document)
        {
            var formatted = _documentFormatter.DocumentToString(document);
            _printer.Print(formatted);
        }
    }
}