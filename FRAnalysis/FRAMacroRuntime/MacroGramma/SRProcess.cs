using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAMacroRuntime.MacroGramma
{
    class SRProcess:SCS.Gfc.GfcProductionSet
    {
        public override void DefineProductions()
        {
            this.SetKeywords("MConfigProcess","input","output","MConfigArgument");
            this.DefineProduction("1=>[macroE]=>MConfigProcess(\"type\",\"name\"){input{[mInputList]}output{[mOutputList]}}").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRProcess_Processing1);
            this.DefineProduction("2=>[mInputList]=>[mIOConfig][mInputList]").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRProcess_Processing2);
            this.DefineProduction("3=>[mInputList]=>").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRProcess_Processing3);
            this.DefineProduction("2=>[mOutputList]=>[mIOConfig][mOutputList]").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRProcess_Processing2);
            this.DefineProduction("3=>[mOutputList]=>").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRProcess_Processing3);
            this.DefineProduction("4=>[mIOConfig]=>MConfigArgument(\"type\",\"name\",\"target\");").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRProcess_Processing4);
        }

        object SRProcess_Processing4(SCS.Parser parser,int posStart,int posEnd, SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            //MConfigIO  (  \"type\"  ,  \"name\"  ,   \"target\"  )  ;
            return new FRAProcessIOCfg()
            {
                Type=rightParts[2].Token.Data.Trim('"'),
                Ident=rightParts[4].Token.Data.Trim('"'),
                Target=rightParts[6].Token.Data.Trim('"'),
                ContextOffset=posStart
            };
        }
        object SRProcess_Processing3(SCS.Parser parser, int posStart, int posEnd, SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            return new List<FRAProcessIOCfg>();
        }
        object SRProcess_Processing2(SCS.Parser parser, int posStart, int posEnd, SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            List<FRAProcessIOCfg> l = (List<FRAProcessIOCfg>)rightParts[1].InterminalObject;
            l.Add((FRAProcessIOCfg)rightParts[0].InterminalObject);
            return l;
        }

        object SRProcess_Processing1(SCS.Parser parser, int posStart, int posEnd, SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            //MConfigProcess   (   \"type\"  ,  \"name\"  ) {  input { [mInputList] }  output  {  [mOutputList]  }  }
            FRAProcessCfg c = new FRAProcessCfg();
            c.Type = rightParts[2].Token.Data.Trim('"');
            c.Name = rightParts[4].Token.Data.Trim('"');
            c.CfgInputs = (List<FRAProcessIOCfg>)rightParts[9].InterminalObject;
            c.CfgInputs.Reverse();
            c.CfgOutputs = (List<FRAProcessIOCfg>)rightParts[13].InterminalObject;
            c.CfgOutputs.Reverse();
            c.ContextOffset = posStart;
            return c;
        }
    }
}
