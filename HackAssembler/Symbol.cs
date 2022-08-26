using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackAssembler
{
    class Symbol
    {
        private string symbolName;

        public string SymbolName
        {
            get { return symbolName; }
            set { symbolName = value; }
        }

        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public Symbol(string symbol, string value)
        {
            this.SymbolName = symbol;
            this.Value = value;
        }
    }
}
