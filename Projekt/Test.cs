using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Test
    { Test(List<Pytanie> pytania)
        { this.pytania = pytania; }
        List<Pytanie> pytania { get; set; }
    }
}
