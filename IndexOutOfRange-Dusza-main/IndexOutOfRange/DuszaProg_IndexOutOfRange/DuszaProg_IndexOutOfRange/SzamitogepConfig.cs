using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuszaProg_IndexOutOfRange
{
    internal class SzamitogepConfig
    {
        string eleres;
        int millimag;
        int memoria;
        private List<string> programPeldanyAzonositok = [];

        public SzamitogepConfig(int millimag, int memoria, string eleres)
        {
            this.millimag = millimag;
            this.memoria = memoria;
            this.eleres = eleres;
        }

        public int Millimag { get => millimag; set => millimag = value; }
        public int Memoria { get => memoria; set => memoria = value; }
        public string Eleres { get => eleres; set => eleres = value; }
        public List<string> ProgramPeldanyAzonositok { get => programPeldanyAzonositok; set => programPeldanyAzonositok = value; }

        public string KiIratas() => $"{millimag}\n{memoria}";
    }
}
