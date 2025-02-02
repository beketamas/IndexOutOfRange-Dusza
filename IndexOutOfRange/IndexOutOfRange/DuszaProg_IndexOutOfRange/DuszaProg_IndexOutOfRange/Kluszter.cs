using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuszaProg_IndexOutOfRange
{
    internal class Kluszter
    {
        string programName;
        int mennyiActive;
        int millimag;
        int memoria;

        public Kluszter()
        {

        }

        public Kluszter(string[] tomb)
        {
            this.programName = tomb[0];
            this.mennyiActive = int.Parse(tomb[1]);
            this.millimag = int.Parse(tomb[2]);
            this.memoria = int.Parse(tomb[3]);
        }

        public string ProgramName { get => programName; set => programName = value; }
        public int MennyiActive { get => mennyiActive; set => mennyiActive = value; }
        public int Millimag { get => millimag; set => millimag = value; }
        public int Memoria { get => memoria; set => memoria = value; }

        public string KiIratas() => $"{programName}\n{MennyiActive}\n{millimag}\n{memoria}";
    }
}
