using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librar.DAL.Interface
{
    public interface IEntry
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
    }
}
