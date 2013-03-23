using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAMacroRuntime.MacroGramma
{
    class SRmacro:SCS.Gfc.GfcProductionSet
    {
        public override void DefineProductions()
        {
            this.SetKeywords("MSetInfomation", "MConfigResult");
            this.DefineProduction("1=>[macro]=>MSetInfomation(\"info\");[globalList][macroElist][resultList]").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRmacro_Processing1);
            this.DefineProduction("2=>[globalList]=>MConfigGlobal(\"type\",\"name\");[globalList]").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRmacro_Processing);
            this.DefineProduction("3=>[globalList]=>").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRmacro_Processing6);
            this.DefineProduction("4=>[macroElist]=>[macroE][macroElist]").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRmacro_Processing2);
            this.DefineProduction("5=>[macroElist]=>").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRmacro_Processing3);
            this.DefineProduction("6=>[resultList]=>MConfigResult(\"type\",\"name\");[resultList]").Processing += new SCS.Gfc.GfcProductionProcessingHandler(SRmacro_Processing4);
            this.DefineProduction("7=>[resultList]=>").Processing+=new SCS.Gfc.GfcProductionProcessingHandler(SRmacro_Processing5);
        }

        object SRmacro_Processing(SCS.Parser parser, int posStart, int posEnd, params SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            //MConfigGlobal     (       \"type\"        ,       \"name\"        )       ;   [globalList]"
            var l = (List<FRAGlobalCfg>)rightParts[7].InterminalObject;
            l.Add(new FRAGlobalCfg()
            {
                Type = rightParts[2].Token.Data.Trim('"'),
                Name = rightParts[4].Token.Data.Trim('"'),
                ContextOffset = posStart
            });
            return l;
        }
        object SRmacro_Processing6(SCS.Parser parser, int posStart, int posEnd, params SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            return new List<FRAGlobalCfg>();
        }
        object SRmacro_Processing5(SCS.Parser parser, int posStart, int posEnd, params SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            return new List<FRAResultCfg>();
        }
        object SRmacro_Processing4(SCS.Parser parser, int posStart, int posEnd, params SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            //  MConfigResult    (    \"type\"    ,     \"name\"   )   ;   [resultList]
            List<FRAResultCfg> l = (List<FRAResultCfg>)rightParts[7].InterminalObject;
            l.Add(new FRAResultCfg()
            {
                Type=rightParts[2].Token.Data.Trim('"'),
                Name = rightParts[4].Token.Data.Trim('"'),
                ContextOffset=posStart
            });
            return l;
        }


        object SRmacro_Processing3(SCS.Parser parser, int posStart, int posEnd, SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            return new List<FRAProcessCfg>();
        }

        object SRmacro_Processing2(SCS.Parser parser, int posStart, int posEnd, SCS.Gfc.GfcSemanticRecord[] rightParts)
        {

            List<FRAProcessCfg> l = (List<FRAProcessCfg>)rightParts[1].InterminalObject;
            l.Add((FRAProcessCfg)rightParts[0].InterminalObject);
            return l;
        }

        object SRmacro_Processing1(SCS.Parser parser, int posStart, int posEnd, SCS.Gfc.GfcSemanticRecord[] rightParts)
        {
            //MSetInfomation   (    \"info\"   )   ;    [macroElist]       [resultList]
            FRAMacro m = new FRAMacro();
            m.Infomation = rightParts[2].Token.Data.Trim('"');
            m.ListGlobalData = (List<FRAGlobalCfg>)rightParts[5].InterminalObject;
            m.ListGlobalData.Reverse();
            m.ListProcess = (List<FRAProcessCfg>)rightParts[6].InterminalObject;
            m.ListProcess.Reverse();
            m.ListResult = (List<FRAResultCfg>)rightParts[7].InterminalObject;
            m.ListResult.Reverse();
            return m;
        }

    }
}
