using ExCSS;
using projekt_Jan_Machalski;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt_Jan_Machalski
{
    public interface IReportable
    {
        string Accept(IMedia media);
    }
}
