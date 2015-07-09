using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackAssembler
{
    class SymbolTable
    {
        private List<Symbol> table;

        private List<string> symbolsList;

        public List<string> SymbolsList
        {
            get { return symbolsList; }
        }

        internal List<Symbol> Table
        {
            get { return table; }
            set { table = value; }
        }

        public SymbolTable()
        {
            table = new List<Symbol>();
            symbolsList = new List<string>();

            this.populateSymbolTable();

        }

        public void Add(string symbol, string value)
        {
            Symbol s = new Symbol(symbol, value);
            table.Add(s);

            this.symbolsList.Add(symbol);
        }

        private void populateSymbolTable()
        {
            // Carrega Símbolos Pré-Definidos
            //23 pre-defined symbols 

            #region R0 to R15

            //R0
            this.Add("R0", "0");
            //R1
            this.Add("R1", "1");
            //R2
            this.Add("R2", "2");
            //R3
            this.Add("R3", "3");
            //R4
            this.Add("R4", "4");
            //R5
            this.Add("R5", "5");
            //R6
            this.Add("R6", "6");
            //R7
            this.Add("R7", "7");
            //R8
            this.Add("R8", "8");
            //R9
            this.Add("R9", "9");
            //R10
            this.Add("R10", "10");
            //R11
            this.Add("R11", "11");
            //R12
            this.Add("R12", "12");
            //R13
            this.Add("R13", "13");
            //R14
            this.Add("R14", "14");
            //R15
            this.Add("R15", "15");

            #endregion

            //SCREEN
            this.Add("SCREEN", "16384");
            //KBD
            this.Add("KBD", "24576");
            //SP
            this.Add("SP", "0");
            //LCL
            this.Add("LCL", "1");
            //ARG
            this.Add("ARG", "2");
            //THIS
            this.Add("THIS", "3");
            //THAT
            this.Add("THAT", "4");
        }
    }
}
