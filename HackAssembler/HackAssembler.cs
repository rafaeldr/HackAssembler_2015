using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HackAssembler
{
    public partial class HackAssembler : Form
    {
        StreamReader fileASM;  //Arquivo de entrada
        string fileNameASM;
        SymbolTable symbolTable = new SymbolTable();

        public HackAssembler()
        {
            InitializeComponent();
        }

        private void btnASM_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileNameASM = openFileDialog1.FileName;
                fileASM = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                AssemblerLogic(fileASM);
                MessageBox.Show("Conversão Efetuada!");
                btnASM.Enabled = false;
            }
        }
        
        private void AssemblerLogic(StreamReader fileASM)
        {
            StreamReader sr;
            sr = stepZero(fileASM);
            sr = FirstPass(sr);
            sr = SecondPass(sr);
            sr = ConvertPass(sr);
        }

        private StreamReader FirstPass(StreamReader file)
        {
            List<string> lines = new List<string>();

            int lineNumber = 0;  // First line is 0

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();

                if (line.StartsWith("("))
                {
                    //It's a LABEL
                    string symbol = line.Replace("(", "").Replace(")", "");
                    int value = lineNumber; //Refers always to next line and the current line is ignored
                    symbolTable.Add(symbol, value.ToString());
                    //THIS ASSEMBLY LINE CAN NOW BE REMOVED
                }
                else
                {
                    lines.Add(line);
                    lineNumber++; //Update to next line
                }
            }

            string fileOut = Path.GetDirectoryName(fileNameASM) + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(fileNameASM) + ".TEMP1";
            File.WriteAllLines(fileOut, lines);
            file.Close();
            StreamReader fileNew = new StreamReader(fileOut);
            return fileNew;
        }

        private StreamReader SecondPass(StreamReader file)
        {
            List<string> lines = new List<string>();
            int memoryPos = 16;

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();

                // A-INSTRUCTION
                if (line.StartsWith("@"))
                {
                    //It CAN have a symbol
                    string symbolCandidate = line.Replace("@", "");

                    int t;
                    if(!Int32.TryParse(symbolCandidate, out t))
                    {
                        // Not a decimal number
                        if(!symbolTable.SymbolsList.Contains(symbolCandidate))
                        {
                            // NOT IN SYMBOL TABLE
                            symbolTable.Add(symbolCandidate, memoryPos.ToString());
                            line = "@" + memoryPos.ToString();
                            memoryPos++;
                        }
                        else
                        {
                            // IN SYMBOL TABLE
                            foreach(Symbol s in symbolTable.Table)
                            {
                                if(s.SymbolName == symbolCandidate)
                                {
                                    line = "@" + s.Value;
                                }
                            }
                        }
                    }
                }
                
                lines.Add(line);
            }


            string fileOut = Path.GetDirectoryName(fileNameASM) + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(fileNameASM) + ".TEMP2";
            File.WriteAllLines(fileOut, lines);
            file.Close();
            StreamReader fileNew = new StreamReader(fileOut);
            return fileNew;
        }

        private StreamReader ConvertPass(StreamReader file)
        {
            List<string> lines = new List<string>();

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();

                string convertedLine = Parser.Parse(line);

                lines.Add(convertedLine);
            }

            string fileOut = Path.GetDirectoryName(fileNameASM) + Path.DirectorySeparatorChar
                + Path.GetFileNameWithoutExtension(fileNameASM) + ".HACK";
            File.WriteAllLines(fileOut, lines);
            file.Close();
            StreamReader fileNew = new StreamReader(fileOut);
            return fileNew;
        }

        /// <summary>
        /// Clear white spaces from input file
        /// </summary>
        /// <param name="file">Input File</param>
        /// <returns>Pre-Parse File</returns>
        private StreamReader stepZero(StreamReader file)
        {
            List<string> lines = new List<string>();

            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                if (!IsWhite(line))
                {
                    line = line.Trim();

                    //In-line Comments
                    int pos = line.IndexOf("//");
                    if(pos != -1)
                        line = line.Remove(pos);

                    //Remove white-space in-line
                    line = line.Replace(" ", "");
                    line = line.ToUpper();

                    lines.Add(line);
                }
            }
            
            string fileOut = Path.GetDirectoryName(fileNameASM) + Path.DirectorySeparatorChar 
                + Path.GetFileNameWithoutExtension(fileNameASM) + ".TEMP0";
            File.WriteAllLines(fileOut, lines);
            file.Close();
            StreamReader fileNew = new StreamReader(fileOut);
            return fileNew;
        }

        /// <summary>
        /// Test if file fine must be ignored
        /// </summary>
        /// <param name="line">A line in string format</param>
        /// <returns>TRUE: Must be ignored</returns>
        private bool IsWhite(string line)
        {
            if (line.Trim().StartsWith("//"))
            {
                return true;
            }
            //Empty Line
            if (line.Trim() == "")
            {
                return true;
            }
            return false;
        }
    }
}
