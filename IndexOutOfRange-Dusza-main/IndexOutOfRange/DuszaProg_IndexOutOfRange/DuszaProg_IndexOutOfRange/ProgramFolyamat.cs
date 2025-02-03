using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuszaProg_IndexOutOfRange
{
    internal class ProgramFolyamat
    {
        DateTime inidtasIdeje;
        string isActive;
        int processzorEroforras;
        int memoriaEroforras;
        string fajlNeve;
        public ProgramFolyamat(DateTime inidtasIdeje, string isActive, int processzorEroforras, int memoriaEroforras, string fajlNeve)
        {
            this.inidtasIdeje = inidtasIdeje;
            this.isActive = isActive;
            this.processzorEroforras = processzorEroforras;
            this.memoriaEroforras = memoriaEroforras;
            this.fajlNeve = fajlNeve;
        }

        public DateTime InidtasIdeje { get => inidtasIdeje; set => inidtasIdeje = value; }
        public string IsActive { get => isActive; set => isActive = value; }
        public int ProcesszorEroforras { get => processzorEroforras; set => processzorEroforras = value; }
        public int MemoriaEroforras { get => memoriaEroforras; set => memoriaEroforras = value; }
        public string FajlNeve { get => fajlNeve; set => fajlNeve = value; }
    }
}
