using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackAssembler
{
    static class Parser
    {
        public static string DestPart(string line)
        {
            if(line.Contains("="))
            {
                int pos = line.IndexOf("=");
                return line.Remove(pos);
            }
            else
            {
                // DOES NOT CONTAIN DEST PART
                return "";
            }
        }

        public static string JumpPart(string line)
        {
            if (line.Contains(";"))
            {
                int pos = line.IndexOf(";");
                return line.Substring(pos+1);
            }
            else
            {
                // DOES NOT CONTAIN JUMP PART
                return "";
            }
        }

        public static string CompPart(string line)
        {
            int pos;

            if (line.Contains(";"))
            {
                //REMOVE JUMP PART
                pos = line.IndexOf(";");
                line = line.Remove(pos); 
            }

            if (line.Contains("="))
            {
                //REMOVE DEST PART
                pos = line.IndexOf("=");
                return line = line.Substring(pos+1); 
            }

            // If not catched in if's means it is a PURE COMP instruction (AS-IS)
            return line;
        }

        public static string Parse(string line)
        {
            string outString;

            if(IsInstructionTypeA(line))
            {
                // A-INSTRUCTION
                outString = ParseAInstruction(line);
            }
            else
            {
                // C-INSTRUCTION
                string dest;
                string jump;
                string comp;

                dest = ParseDEST(DestPart(line));
                jump = ParseJUMP(JumpPart(line));
                comp = ParseCOMP(CompPart(line));

                outString = "111" + comp + dest + jump;
            }

            return outString;
        }

        public static string ParseDEST(string dest)
        {
            switch (dest)
            {
                case "M":
                    dest = "001";
                    break;
                case "D":
                    dest = "010";
                    break;
                case "MD":
                    dest = "011";
                    break;
                case "A":
                    dest = "100";
                    break;
                case "AM":
                    dest = "101";
                    break;
                case "AD":
                    dest = "110";
                    break;
                case "AMD":
                    dest = "111";
                    break;
                case "":
                    dest = "000";
                    break;
            }

            return dest;
        }

        public static string ParseJUMP(string jump)
        {
            switch (jump)
            {
                case "":
                    jump = "000";
                    break;
                case "JGT":
                    jump = "001";
                    break;
                case "JEQ":
                    jump = "010";
                    break;
                case "JGE":
                    jump = "011";
                    break;
                case "JLT":
                    jump = "100";
                    break;
                case "JNE":
                    jump = "101";
                    break;
                case "JLE":
                    jump = "110";
                    break;
                case "JMP":
                    jump = "111";
                    break;
            }

            return jump;
        }

        public static string ParseCOMP(string comp)
        {
            switch (comp)
            {
                case "0":
                    comp = "0101010";
                    break;
                case "1":
                    comp = "0111111";
                    break;
                case "-1":
                    comp = "0111010";
                    break;
                case "D":
                    comp = "0001100";
                    break;
                case "A":
                    comp = "0110000";
                    break;
                case "!D":
                    comp = "0001101";
                    break;
                case "!A":
                    comp = "0110001";
                    break;
                case "-D":
                    comp = "0001111";
                    break;
                case "-A":
                    comp = "0110011";
                    break;
                case "D+1":
                    comp = "0011111";
                    break;
                case "A+1":
                    comp = "0110111";
                    break;
                case "D-1":
                    comp = "0001110";
                    break;
                case "A-1":
                    comp = "0110010";
                    break;
                case "D+A":
                    comp = "0000010";
                    break;
                case "D-A":
                    comp = "0010011";
                    break;
                case "A-D":
                    comp = "0000111";
                    break;
                case "D&A":
                    comp = "0000000";
                    break;
                case "D|A":
                    comp = "0010101";
                    break;
                //Involving M
                case "M":
                    comp = "1110000";
                    break;
                case "!M":
                    comp = "1110001";
                    break;
                case "-M":
                    comp = "1110011";
                    break;
                case "M+1":
                    comp = "1110111";
                    break;
                case "M-1":
                    comp = "1110010";
                    break;
                case "D+M":
                    comp = "1000010";
                    break;
                case "D-M":
                    comp = "1010011";
                    break;
                case "M-D":
                    comp = "1000111";
                    break;
                case "D&M":
                    comp = "1000000";
                    break;
                case "D|M":
                    comp = "1010101";
                    break;
            }

            return comp;
        }

        public static bool IsInstructionTypeA(string line)
        {
            return line.StartsWith("@");
        }

        public static string ParseAInstruction(string instruction)
        {
            string outString;
            string symbolCandidate = instruction.Replace("@", "");
            
            int valor = Int32.Parse(symbolCandidate);

            string binary = Convert.ToString(valor, 2);
            binary = binary.PadLeft(15, '0');

            outString = "0"+binary;
            
            return outString;
        }

    }
}
