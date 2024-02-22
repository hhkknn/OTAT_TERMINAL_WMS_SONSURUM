using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIF.WMS.Models
{
    public class _DepoSecim
    {
        public string secim { get; set; }
        public string depoKodu { get; set; }
        public string depoAdi { get; set; }
        public string tamYetki { get; set; }
        public string sipMalGrs { get; set; }
        public string blgszMalGrs { get; set; }
        public string tlpszDepK { get; set; }
        public string tlpszDepH { get; set; }
        public string tlpBagDepK { get; set; }
        public string tlpBagDepH { get; set; }
        public string tlpKabulK { get; set; }
        public string tlpKabulH { get; set; }
        public string blgszMalC { get; set; }
        public string sipBagTes { get; set; }
        public string sprsszTes { get; set; }
        public string teslmtIade { get; set; }
        public string satisIade { get; set; } 
        public string magazaIslem { get; set; } 
        public string IadeTalep { get; set; }
    }
}
