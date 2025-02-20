using Dusza_WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DuszaProg_IndexOutOfRange
{
    public class SzamitogepConfig : Button
    {
        string eleres;
        int millimag;
        int memoria;
        int maxMemoria;
        int maxMag;
        private List<string> programPeldanyAzonositok = [];

        public SzamitogepConfig() { }
        public SzamitogepConfig(int millimag, int memoria, string eleres, string nev, int maxMemoria, int maxMag)
        {
            this.millimag = millimag;
            this.memoria = memoria;
            this.eleres = eleres;
            Content = nev;
            Width = 100;
            Height = 30;
            FontSize = 15;
            this.maxMemoria = maxMemoria;
            this.maxMag = maxMag;
        }

        public int Millimag { get => millimag; set => millimag = value; }
        public int Memoria { get => memoria; set => memoria = value; }
        public string Eleres { get => eleres; set => eleres = value; }
        public List<string> ProgramPeldanyAzonositok { get => programPeldanyAzonositok; set => programPeldanyAzonositok = value; }
        public int MaxMemoria { get => maxMemoria; set => maxMemoria = value; }
        public int MaxMag { get => maxMag; set => maxMag = value; }

        public string KiIratas() => $"{millimag}\n{memoria}";
        

        
    }
}
