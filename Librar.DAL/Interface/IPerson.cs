using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librar.DAL.Interface
{
    public interface IPerson : IName
    {
        string? LastName { get; set; }
        string? SecondName { get; set; }
        DateTime? Birthday { get; set; }
    }
}
