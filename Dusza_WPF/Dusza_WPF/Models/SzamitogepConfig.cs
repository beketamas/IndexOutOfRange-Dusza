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
        int jelenlegiMemoria;
        int jelenlegiMag;
        private List<string> programPeldanyAzonositok = [];
        double pozicioX = 0;
        double pozicioY = 0;

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
            this.jelenlegiMemoria = maxMemoria;
            this.jelenlegiMag = maxMag;
        }

        public int Millimag { get => millimag; set => millimag = value; }
        public int Memoria { get => memoria; set => memoria = value; }
        public string Eleres { get => eleres; set => eleres = value; }
        public List<string> ProgramPeldanyAzonositok { get => programPeldanyAzonositok; set => programPeldanyAzonositok = value; }
        public int JelenlegiMemoria { get => jelenlegiMemoria; set => jelenlegiMemoria = value; }
        public int JelenlegiMag { get => jelenlegiMag; set => jelenlegiMag = value; }
        public double PozicioX { get => pozicioX; set => pozicioX = value; }
        public double PozicioY { get => pozicioY; set => pozicioY = value; }

        public string KiIratas() => $"{millimag}\n{memoria}";
        

        
    }
}
