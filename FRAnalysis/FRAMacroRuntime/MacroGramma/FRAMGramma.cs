using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAMacroRuntime.MacroGramma
{
    class FRAMGramma:SCS.Gfc.GfcGrammaBasic
    {
        public override string Include(List<SCS.Gfc.GfcProductionSet> grammas)
        {
            grammas.Add(new SRmacro());
            grammas.Add(new SRProcess());
            return "[macro]";
        }
    }
}
