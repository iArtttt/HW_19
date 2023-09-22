using Librar.DAL.Models;

namespace Library
{
    public class DocumentChange
    {
        [MenuAction("Type", 1, "You will need to change Document Number then")]
        public void DocumentTypeChange(Reader reader)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change name press ( Enter )");
            Info.Inform($"Readers DocumentType is: {reader.DocumentType}");

            var newDocumentType = Console.ReadLine();

            if (!string.IsNullOrEmpty(newDocumentType))
            {
                reader.DocumentType = newDocumentType;
                Info.SuccedKey("Document Type was changed");
            }
            else
                Info.ErrorKey("Incorrect");

        }
        
        [MenuAction("Number", 2, "To change Document Number")]
        public void DocumentNumberChange(Reader reader)
        {
            Console.Clear();
            Info.Inform($"If you do`t want to change Document Number press ( Enter )");
            Info.Inform($"Readers Document Number is: {reader.DocumentNumber}");

            var newDocumentNumber = Console.ReadLine();

            if (!string.IsNullOrEmpty(newDocumentNumber))
            {
                reader.DocumentNumber = int.Parse(newDocumentNumber);
                Info.SuccedKey("Document Number was changed");
            }
            else
                Info.ErrorKey("Incorrect");
        }
    }
}