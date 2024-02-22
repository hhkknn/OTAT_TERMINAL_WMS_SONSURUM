using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIF.WMS.Models
{
    public class _SiparisKarsilama
    {
        public int siraNo { get; set; }
        public string siparisNumarasi { get; set; }
        public string siparisTarihi { get; set; }
        public string teslimatTarihi { get; set; }
        public string siparisSatirNo { get; set; }
        public string urunKodu { get; set; }
        public string urunTanimi { get; set; }
        public string urunYTanimi { get; set; }
        public double toplamSatisMiktari { get; set; }
        public double sevksiparisMiktari { get; set; }
        public double acikSiparisMiktari { get; set; }
        public double planlananSiparisMiktari { get; set; }
        public string siparisDepoKodu { get; set; }
        public double depoStokMiktari { get; set; }
        public double genelStokMiktari { get; set; }
        public string paletNo { get; set; }
        public double toplananMiktar { get; set; }
        public string muhatapReferansNo { get; set; }
        public string muhatapKatalogNo { get; set; }
        public double birimFiyat { get; set; }
        public double hesaplananSiparisMiktari { get; set; }
        public double toplamSatirTutari { get; set; }
        public string Gorunen { get; set; }
    }
}
