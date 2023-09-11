using Librar.DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Library
{
    public class ReaderInformationUpdate
    {
        [MenuSubmenu("Full name", 1, "Here you can change information about Readers Full name")]
        public FullNameChange FullNameChange { get; set; }
        
        [MenuSubmenu("Documents", 2, "Here you can change information about Readers Documents")]
        public DocumentChange DocumentsChange { get; set; }
    }
}