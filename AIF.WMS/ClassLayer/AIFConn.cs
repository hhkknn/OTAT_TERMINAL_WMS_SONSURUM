using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIF.WMS.ClassLayer
{
    public class AIFConn
    {
        //Commit.
        public static string FormsViewDefault { get { return "AIF.WMS.FormsView."; } }

        //public static string OrnekFrm = FormsViewDefault + "OrnekFrm.xml";
        //public const string OrnekUID = "Ornek";
        //public static Ornek Ornek { get { return Singleton<Ornek>.Instance; } }



        #region Sistem Ekranları
        public static KalemAnaverileri Sys150 { get { return Singleton<KalemAnaverileri>.Instance; } }
        public const string KalemAnverileri_FormUID = "Sys150";

        public static BelgeTaslak Sys3002 { get { return Singleton<BelgeTaslak>.Instance; } }
        public const string BelgeTaslak_FormUID = "Sys3002";
        #endregion

        #region User Ekranlar


        public static string BtnParamFrmXML = FormsViewDefault + "ButonParametre.xml";
        public const string BtnParamUID = "BtnParam";
        public static ButtonParametre BtnParam { get { return Singleton<ButtonParametre>.Instance; } }

        public static string SirketBilgieriFrmXML = FormsViewDefault + "SirketBilgileri.xml";
        public const string SirketBilgileriUID = "SrktBlg";
        public static SirketBilgileri SrktBlg { get { return Singleton<SirketBilgileri>.Instance; } }

        public static string DepoParametresiXML = FormsViewDefault + "DepoParametresi.xml";
        public const string DepoParametresiUID = "DepoParam";
        public static DepoParametresi DepoParam { get { return Singleton<DepoParametresi>.Instance; } }

        public static string PartiBelirlemeXML = FormsViewDefault + "PartiBelirleme.xml";
        public const string PartiBelirlemeUID = "PartiBelir";
        public static PartiBelirleme PartiBelir { get { return Singleton<PartiBelirleme>.Instance; } }

        public static string SayimListesiXML = FormsViewDefault + "SayimListesi.xml";
        public const string SayimListesiUID = "Sayim";
        public static SayimListesi Sayim { get { return Singleton<SayimListesi>.Instance; } }

        public static string GenelParametrelerXML = FormsViewDefault + "GenelParametreler.xml";
        public const string GenelParametrelerUID = "GenelPrm";
        public static GenelParametreler GenelPrm { get { return Singleton<GenelParametreler>.Instance; } }


        public static string EtiketYazdirmaXML = FormsViewDefault + "EtiketYazdirma.xml";
        public const string EtiketYazdirmaUID = "EtktYzdr";
        public static EtiketYazdirma EtktYzdr { get { return Singleton<EtiketYazdirma>.Instance; } }

        public static string SiparisKarsilamaXML = FormsViewDefault + "SiparisKarsilama.xml";
        public const string SiparisKarsilamaUID = "SipKarsi";
        public static SiparisKarsilama SipKarsi { get { return Singleton<SiparisKarsilama>.Instance; } }

        public static string PaletNumarasiBelirlemeXML = FormsViewDefault + "PaletNumarasiBelirleme.xml";
        public const string PaletNumarasiBelirlemeUID = "PltNoBelir";
        public static PaletNoBelirleme PltNoBelir { get { return Singleton<PaletNoBelirleme>.Instance; } }

        public static string EtiketYazdirmaParametreXML = FormsViewDefault + "EtiketYazdirmaParametre.xml";
        public const string EtiketYazdirmaParametreUID = "EtYazParam";
        public static EtiketYazdirmaParametre EtYazParam { get { return Singleton<EtiketYazdirmaParametre>.Instance; } }


        public static string DepoSecimiXML = FormsViewDefault + "DepoSecimi.xml";
        public const string DepoSecimiUID = "DepSecim";
        public static DepoSecimi DepSecim { get { return Singleton<DepoSecimi>.Instance; } }


        public static string UrunEklemeXML = FormsViewDefault + "UrunEkleme.xml";
        public const string UrunEklemeUID = "UrunEkle";
        public static UrunEkleme UrunEkle { get { return Singleton<UrunEkleme>.Instance; } }


        public static string ToplananUrunlerXML = FormsViewDefault + "ToplananUrunler.xml";
        public const string ToplananUrunlerUID = "TplnUrun";
        public static ToplananUrunler TplnUrun { get { return Singleton<ToplananUrunler>.Instance; } }


        public static string SevkEdilenUrunlerXML = FormsViewDefault + "SevkEdilenUrunler.xml";
        public const string SevkEdilenUID = "SvkUrun";
        public static SevkEdilenUrunler SvkUrun { get { return Singleton<SevkEdilenUrunler>.Instance; } }


        public static string SayimlarXML = FormsViewDefault + "Sayimlar.xml";
        public const string SayimlarUID = "Symlr";
        public static Sayimlar Symlr { get { return Singleton<Sayimlar>.Instance; } }

        public static string StokTransferXML = FormsViewDefault + "StokTransfer.xml";
        public const string StokTransferUID = "StokTrans";
        public static StokTransfer StokTrans { get { return Singleton<StokTransfer>.Instance; } }

        public static string frmYetkiGirisXML = FormsViewDefault + "YetkiGiris.xml";
        public const string YetkiGirisUID = "YetkiGiris";
        public static YetkiGiris YetkiGiris { get { return Singleton<YetkiGiris>.Instance; } }

        public static string frmSubeTayiniXML = FormsViewDefault + "SubeTayini.xml";
        public const string SubeTayiniUID = "SubeTayin";
        public static SubeTayini SubeTayin { get { return Singleton<SubeTayini>.Instance; } }

        public static string frmMalCikisNedeniXML = FormsViewDefault + "MalCikisNedeni.xml";
        public const string MalCikisNedeniUID = "MalCksNdn";
        public static MalCikisNedeni MalCksNdn { get { return Singleton<MalCikisNedeni>.Instance; } }
        #endregion
    }
}