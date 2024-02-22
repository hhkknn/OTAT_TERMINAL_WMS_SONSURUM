using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIF.WMS.DataTables
{
    public class CreateTables
    {
        public static void CreateAndCheckFields()
        {
            //return;
            Dictionary<string, string> fields = new Dictionary<string, string>();

            List<ComboList> sevkTipi = new List<ComboList>();
            sevkTipi.Add(new ComboList { Value = "1", Desc = "Komple Sevk" });
            sevkTipi.Add(new ComboList { Value = "2", Desc = "Parsiyel Sevk" });

            List<ComboList> onayDurumu = new List<ComboList>();
            onayDurumu.Add(new ComboList { Value = "T", Desc = "Taslak" });
            onayDurumu.Add(new ComboList { Value = "O", Desc = "Onaylandı" });
            onayDurumu.Add(new ComboList { Value = "D", Desc = "Depo Onayı" });

            List<ComboList> paletDurumu = new List<ComboList>();
            paletDurumu.Add(new ComboList { Value = "A", Desc = "Aktif" });
            paletDurumu.Add(new ComboList { Value = "P", Desc = "Pasif" });
            paletDurumu.Add(new ComboList { Value = "Y", Desc = "Yüklendi" });

            List<ComboList> acikKapali = new List<ComboList>();
            acikKapali.Add(new ComboList { Value = "Y", Desc = "Açık" });
            acikKapali.Add(new ComboList { Value = "N", Desc = "Kapalı" });

            List<ComboList> depoCalismaTipi = new List<ComboList>();
            depoCalismaTipi.Add(new ComboList { Value = "1", Desc = "Versiyon1" });
            depoCalismaTipi.Add(new ComboList { Value = "2", Desc = "Versiyon2" });

            //if (!TableCreation.TableExists("AIF_WMS_BTN"))
            //{
            //    TableCreation.CreateTable("AIF_WMS_BTN", "Buton Parametreleri", SAPbobsCOM.BoUTBTableType.bott_Document);

            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "UserCode", "Kullanıcı Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 30, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "UserName", "Kullanıcı Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "SprsliMalGrs", "Siparişli Mal Girişi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "SprsszMalGrs", "Siparişsiz Mal Girişi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "BlgszMalGrs", "Belgesiz Mal Girişi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "TlpszDepoNak", "Talepsiz Depo Nakli", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "TlbbgliDepoNak", "Talepli Depo Nakli", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "DepoSayimi", "Depo Sayımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "BlgszMalCks", "Belgesiz Mal Çıkışı", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "SprsbagliTes", "Siparişe Bağlı Tes.", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "SiparissizTes", "Siparişsiz Teslimat", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "BarkodOlustur", "Barkod Oluştur", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "UrtmMalCikis", "Üretime Mal Çıkış", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "UrtmdnMalGiris", "Üretimden Mal Girişi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "MusteriFatIade", "Müşteri Fatura Iadesi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "TeslimatIade", "Teslimat Iadesi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "Raporlar", "Raporlar", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "TalepKabul", "Talep Kabul", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "SatistanIade", "Satıştan İade", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);

            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "CekmeListesi", "Çekme Listesi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "PaletYapma", "Palet Yapma", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BTN", "KonteynerYapma", "Konteyner Yapma", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);


            //}
            //TableCreation.CreateTable("AIF_WMS_BTN1", "Şube Tayini", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

            //TableCreation.CreateUserFields("@AIF_WMS_BTN1", "SubeKodu", "Şube Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_BTN1", "SubeAdi", "Şube Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_BTN1", "TayinEdildi", "Tayin Edildi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);

            //TableCreation.CreateUserFields("@AIF_WMS_BTN", "MagazaIslemleri", "Mağazacılık İşlemleri", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_BTN", "IadeTalep", "İade Talepleri", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);

            //if (!UdoCreation.UDOExists("AIF_WMS_BTN"))
            //{
            //    fields.Clear();
            //    fields.Add("DocEntry", "Kod");
            //    fields.Add("U_UserCode", "Kullanıcı Kodu");
            //    fields.Add("U_UserName", "Kullanıcı Adı");
            //    fields.Add("U_SprsliMalGrs", "Siparişli Mal Girişi");
            //    fields.Add("U_SprsszMalGrs", "Siparişsiz Mal Girişi");
            //    fields.Add("U_BlgszMalGrs", "Belgesiz Mal Girişi");
            //    fields.Add("U_TlpszDepoNak", "Talepsiz Depo Nakli");
            //    fields.Add("U_TlbbgliDepoNak", "Talepli Depo Nakli");
            //    fields.Add("U_DepoSayimi", "Depo Sayımı");
            //    fields.Add("U_BlgszMalCks", "Belgesiz Mal Çıkışı");
            //    fields.Add("U_SprsbagliTes", "Siparişe Bağlı Tes.");
            //    fields.Add("U_SiparissizTes", "Siparişsiz Teslimat");
            //    fields.Add("U_BarkodOlustur", "Barkod Oluştur");
            //    fields.Add("U_UrtmMalCikis", "Üretime Mal Çıkış");
            //    fields.Add("U_UrtmdnMalGiris", "Üretimden Mal Girişi");
            //    fields.Add("U_MusteriFatIade", "Müşteri Fatura Iadesi");
            //    fields.Add("U_TeslimatIade", "Teslimat Iadesi");
            //    fields.Add("U_Raporlar", "Raporlar");
            //    fields.Add("U_TalepKabul", "Talep Kabul");
            //    fields.Add("U_SatistanIade", "Satıştan İade");
            //    fields.Add("U_CekmeListesi", "Çekme Listesi");
            //    fields.Add("U_PaletYapma", "Palet Yapma");
            //    fields.Add("U_KonteynerYapma", "Konteyner Yapma");
            //    fields.Add("U_MagazaIslemleri", "Mağazacılık İşlemleri");
            //    fields.Add("U_IadeTalep", "İade Talepleri");

            //    List<FormColumn> fc = new List<FormColumn>();
            //    List<ChildTable> chList = new List<ChildTable>();

            //    ChildTable ch = new ChildTable();
            //    ch.TableName = "AIF_WMS_BTN1";
            //    fc = new List<FormColumn>();

            //    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
            //    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

            //    fc.Add(new FormColumn { FormColumnAlias = "U_SubeKodu", FormColumnDescription = "Şube Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_SubeAdi", FormColumnDescription = "Şube Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_TayinEdildi", FormColumnDescription = "Tayin Edildi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });


            //    ch.FormColumn = fc;
            //    chList.Add(ch);

            //    //UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_BTN", "AIF_WMS_BTN", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_BTN", "");
            //    //UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_BTN", "AIF_WMS_BTN", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_BTN", "",chList:chList);

            //    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_BTN", "AIF_WMS_BTN", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_BTN", "", chList: chList);
            //}

            //if (!TableCreation.TableExists("AIF_WMS_USRWHS"))
            //{
            //    TableCreation.CreateTable("AIF_WMS_USRWHS", "Depo Parametresi", SAPbobsCOM.BoUTBTableType.bott_Document);
            //    TableCreation.CreateTable("AIF_WMS_USRWHS1", "Depo Parametresi Detay", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

            //    TableCreation.CreateUserFields("@AIF_WMS_USRWHS", "KullaniciKodu", "Kullanıcı Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_USRWHS", "KullaniciAdi", "Kullanıcı Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);

            //    TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "DepoKodu", "Depo Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "DepoAdi", "Depo Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "Secim", "Seçili", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);


            //}

            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "TamYetki", "Tam Yetki", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "SipMalGrs", "Siparişli Mal Girişi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "BlgszMalGrs", "Belgesiz Mal Girişi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "TlpszDepK", "Talepsiz Dep.Nak. Kaynak", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "TlpszDepH", "Talepsiz Dep.Nak. Hedef", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "TlpBagDepK", "Talebe Bağlı Dep.Nak. Kaynak", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "TlpBagDepH", "Talebe Bağlı Dep.Nak. Hedef", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "TlpKabulK", "Talep Kabul Kaynak", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "TlpKabulH", "Talep Kabul Hedef", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "BlgszMalC", "Belgesiz Mal Çıkışı", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "SipBagTes", "Siparişe Bağlı Teslimat", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "SprsszTes", "Siparişsiz Teslimat", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "TeslmtIade", "Teslimat İadesi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "SatisIade", "Satıştan İade", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "MagazaIslemleri", "Mağazacılık İşlemleri", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None); 
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS1", "IadeTalep", "İade Talepleri", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);

            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS", "VarsDepoKodu", "Varsayılan Depo Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_USRWHS", "VarsDepoAdi", "Varsayılan Depo Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

            //if (!UdoCreation.UDOExists("AIF_WMS_USRWHS"))
            //{
            //    fields.Clear();
            //    fields.Add("DocEntry", "Kod");
            //    fields.Add("U_KullaniciKodu", "Kullanıcı Kodu");
            //    fields.Add("U_KullaniciAdi", "Kullanıcı Adı");
            //    fields.Add("U_VarsDepoKodu", "Varsayılan Depo Kodu");
            //    fields.Add("U_VarsDepoAdi", "Varsayılan Depo Adı");

            //    List<FormColumn> fc = new List<FormColumn>();
            //    List<ChildTable> chList = new List<ChildTable>();

            //    ChildTable ch = new ChildTable();
            //    ch.TableName = "AIF_WMS_USRWHS1";
            //    fc = new List<FormColumn>();

            //    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
            //    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoKodu", FormColumnDescription = "Depo Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoAdi", FormColumnDescription = "Depo Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_Secim", FormColumnDescription = "Seçili", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

            //    fc.Add(new FormColumn { FormColumnAlias = "U_TamYetki", FormColumnDescription = "Tam Yetki", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_SipMalGrs", FormColumnDescription = "Siparişli Mal Girişi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_BlgszMalGrs", FormColumnDescription = "Belgesiz Mal Girişi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_TlpszDepK", FormColumnDescription = "Talepsiz Dep.Nak. Kaynak", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_TlpszDepH", FormColumnDescription = "Talepsiz Dep.Nak. Hedef", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_TlpBagDepK", FormColumnDescription = "Talebe Bağlı Dep.Nak. Kaynak", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_TlpBagDepH", FormColumnDescription = "Talebe Bağlı Dep.Nak. Hedef", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_TlpKabulK", FormColumnDescription = "Talep Kabul Kaynak", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_TlpKabulH", FormColumnDescription = "Talep Kabul Hedef", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_BlgszMalC", FormColumnDescription = "Belgesiz Mal Çıkışı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_SipBagTes", FormColumnDescription = "Siparişe Bağlı Teslimat", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_SprsszTes", FormColumnDescription = "Siparişsiz Teslimat", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_TeslmtIade", FormColumnDescription = "Teslimat İadesi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_SatisIade", FormColumnDescription = "Satıştan İade", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_MagazaIslemleri", FormColumnDescription = "Mağazacılık İşlemleri", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_IadeTalep", FormColumnDescription = "İade Talepleri", Editable = SAPbobsCOM.BoYesNoEnum.tYES });


            //    ch.FormColumn = fc;
            //    chList.Add(ch);

            //    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_USRWHS", "AIF_WMS_USRWHS", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_USRWHS", "", chList: chList);
            //}

            //if (!TableCreation.TableExists("AIF_WMS_BATCH"))
            //{
            //    TableCreation.CreateTable("AIF_WMS_BATCH", "Parti Belirleme", SAPbobsCOM.BoUTBTableType.bott_Document);

            //    TableCreation.CreateUserFields("@AIF_WMS_BATCH", "BatchPrefix", "Parti Ön Ek", SAPbobsCOM.BoFieldTypes.db_Alpha, 30, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BATCH", "StartNumber", "Başlangıç Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_BATCH", "NextNumber", "Sıradaki Numara", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
            //}

            //if (!UdoCreation.UDOExists("AIF_WMS_BATCH"))
            //{
            //    fields.Clear();
            //    fields.Add("DocEntry", "Kod");
            //    fields.Add("U_BatchPrefix", "Parti Ön Ek");
            //    fields.Add("U_StartNumber", "Başlangıç Numarası");
            //    fields.Add("U_NextNumber", "Sıradaki Numara");

            //    UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_BATCH", "AIF_WMS_BATCH", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_BATCH", "");
            //}

            //if (!TableCreation.TableExists("AIF_WMS_WHSCOUNT"))
            //{
            //    TableCreation.CreateTable("AIF_WMS_WHSCOUNT", "Depo Sayım", SAPbobsCOM.BoUTBTableType.bott_Document);
            //    TableCreation.CreateTable("AIF_WMS_WHSCOUNT1", "Depo Sayım Detay", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);
            //    TableCreation.CreateTable("AIF_WMS_WHSCOUNT2", "Depo Sayım Partiler", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT", "SayimNumarasi", "Sayım Numarası", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT", "SayimTarihi", "Sayım Tarihi", SAPbobsCOM.BoFieldTypes.db_Date);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT", "KullaniciId", "Kullanıcı Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT", "KullaniciAdi", "Kullanıcı Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT", "Aciklama", "Açıklama", SAPbobsCOM.BoFieldTypes.db_Alpha, 254, SAPbobsCOM.BoFldSubTypes.st_None);

            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "Barkod", "Barkod", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "KalemTanimi", "Kalem Tanımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "DepoKodu", "Depo Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "DepoAdi", "Depo Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "Miktar", "Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "OlcuBirimi", "Ölçü Birimi", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "DepoYeriId", "Depo Yeri Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "DepoYeriAdi", "Depo Yeri Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "DepoKodu", "Depo Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "PartiNo", "Parti No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "Miktar", "Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);

            //}

            //TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "PaletNo", "Palet No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);

            //TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT1", "DetaySatirNo", "Detay Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None); 

            //TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "DepoAdi", "Depo Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None); 
            //TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "DepoYeriId", "Depo Yeri Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "DepoYeriAdi", "Depo Yeri Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None); 
            //TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "PartiSatirNo", "Parti Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "PaletMi", "Palet mi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            //TableCreation.CreateUserFields("@AIF_WMS_WHSCOUNT2", "PaletNo", "Palet Numarası", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);


            //if (!UdoCreation.UDOExists("AIF_WMS_WHSCOUNT"))
            //{
            //    fields.Clear();
            //    fields.Add("DocEntry", "Kod");
            //    fields.Add("U_SayimTarihi", "Sayım Tarihi");
            //    fields.Add("U_SayimNumarasi", "Sayım Numarası");
            //    fields.Add("U_KullaniciId", "Kullanıcı Id");
            //    fields.Add("U_KullaniciAdi", "Kullanıcı Adı");
            //    fields.Add("U_Aciklama", "Açıklama");

            //    List<FormColumn> fc = new List<FormColumn>();
            //    List<ChildTable> chList = new List<ChildTable>();

            //    ChildTable ch = new ChildTable();
            //    ch.TableName = "AIF_WMS_WHSCOUNT1";
            //    fc = new List<FormColumn>();

            //    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
            //    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

            //    fc.Add(new FormColumn { FormColumnAlias = "U_Barkod", FormColumnDescription = "Barkod", Editable = SAPbobsCOM.BoYesNoEnum.tYES }); 
            //    fc.Add(new FormColumn { FormColumnAlias = "U_KalemKodu", FormColumnDescription = "Kalem Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_KalemTanimi", FormColumnDescription = "Kalem Tanımı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoKodu", FormColumnDescription = "Depo Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoAdi", FormColumnDescription = "Depo Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_Miktar", FormColumnDescription = "Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_OlcuBirimi", FormColumnDescription = "Ölçü Birimi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoYeriId", FormColumnDescription = "Depo Yeri Id", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoYeriAdi", FormColumnDescription = "Depo Yeri Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_PaletNo", FormColumnDescription = "Palet No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DetaySatirNo", FormColumnDescription = "Detay Satır No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

            //    ch.FormColumn = fc;
            //    chList.Add(ch);

            //    ch = new ChildTable();
            //    ch.TableName = "AIF_WMS_WHSCOUNT2";
            //    fc = new List<FormColumn>();

            //    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
            //    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

            //    fc.Add(new FormColumn { FormColumnAlias = "U_KalemKodu", FormColumnDescription = "Kalem Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoKodu", FormColumnDescription = "Depo Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_PartiNo", FormColumnDescription = "Parti No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_Miktar", FormColumnDescription = "Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoAdi", FormColumnDescription = "Depo Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoYeriId", FormColumnDescription = "Depo Yeri Id", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
            //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoYeriAdi", FormColumnDescription = "Depo Yeri Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES }); 
            //    fc.Add(new FormColumn { FormColumnAlias = "U_PartiSatirNo", FormColumnDescription = "Parti Satır No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

            //    ch.FormColumn = fc;
            //    chList.Add(ch);

            //    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_WHSCOUNT", "AIF_WMS_WHSCOUNT", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_WHSCOUNT", "", chList: chList);
            //}

            if (!TableCreation.TableExists("AIF_WMS_GNLPRM"))
            {
                TableCreation.CreateTable("AIF_WMS_GNLPRM", "Genel Parametreler", SAPbobsCOM.BoUTBTableType.bott_Document);

                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "DepoYeriCalisir", "Depo Yeri Çalışır", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "TlpBglOrj", "Talebe bağlıda orjinal belge", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "TlpszTslk", "Talepsiz Taslak Belge", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "TurkceArama", "Türkçe Karakter Arama", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "CrystalKullan", "Barkod için Crystal Kullan", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "CkmFzlMktr", "Çekmede Fazla Miktar Girer", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "CkmGrpla", "Çekmeleri Grupla", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "BarkodYolu", "Barkod Yolu", SAPbobsCOM.BoFieldTypes.db_Alpha, 254, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "YaziciTipi", "Yazıcı Tipi", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "SayimMikOto", "Sayım Miktarı Otomatik Açılsın", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None); //Ekranda ürün seçildikten sonra miktar alanı otomatik bir şekilde açılması için kullanılır.
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "SayimBtnOto", "Sayım Butonu Otomatik Basılsın", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);//Ekranda ürün miktarı girişi yapıldıktan sonra otomatik olarak SAY butonuna basması için kullanılır.
                TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "SayimCoklu", "Depo Sayımında Çoklu Kişi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);//Depo sayımı eklenirken tekil mi çoğul mu sayılacağı konusu için kullanılır.


            }
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "OndalikMiktar", "Virgülden Sonra Ondalıklı Miktar", SAPbobsCOM.BoFieldTypes.db_Numeric, 1, SAPbobsCOM.BoFldSubTypes.st_Quantity);
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "DepoCalismaTipi", "Depo Çalışma Tipi", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None, clist: depoCalismaTipi);
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "BarkodKalemOku", "Barkod-Kalem Birleşik Oku", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "TarihBazParti", "Tarih Bazlı Parti Oluşturma", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);

            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "YetkiSifre", "Uygulama Yetki Şifresi", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "UrnSrguFiyat", "Ürün Sorgulamada Fiyat Göster", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "UrnSrguFytList", "Ürün Sorgulamada Fiyat Listesi", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "SubeSecimi", "Şube Seçimi Yapılsın", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "PltYapDepSec", "Palet Yapmada Depo Seçimini Aktifleştir", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
            TableCreation.CreateUserFields("@AIF_WMS_GNLPRM", "MalGirCikZorunlu", "Mal Giriş-Çıkış Nedeni Zorunlu", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);

            if (!UdoCreation.UDOExists("AIF_WMS_GNLPRM"))
            {
                fields.Clear();
                fields.Add("DocEntry", "Kod");
                fields.Add("U_DepoYeriCalisir", "Depo Yeri Çalışır");
                fields.Add("U_TlpBglOrj", "Talebe bağlıda orjinal belge");
                fields.Add("U_TurkceArama", "Türkçe Karakter Arama");
                fields.Add("U_CrystalKullan", "Barkod için Crystal Kullan");
                fields.Add("U_CkmFzlMktr", "Çekmede Fazla Miktar Girer");
                fields.Add("U_CkmGrpla", "Çekmeleri Grupla");
                fields.Add("U_SayimMikOto", "Sayım Miktarı Otomatik Açılsın");
                fields.Add("U_SayimBtnOto", "Sayım Butonu Otomatik Basılsın");
                fields.Add("U_SayimCoklu", "Depo Sayımında Çoklu Kişi");
                fields.Add("U_OndalikMiktar", "Virgülden Sonra Ondalıklı Miktar");
                fields.Add("U_DepoCalismaTipi", "Depo Çalışma Tipi");
                fields.Add("U_BarkodKalemOku", "Barkod-Kalem Birleşik Oku");
                fields.Add("U_TarihBazParti", "Tarih Bazlı Parti Oluşturma");
                fields.Add("U_YetkiSifre", "Uygulama Yetki Şifresi");
                fields.Add("U_UrnSrguFiyat", "Ürün Sorgulamada Fiyat Göster");
                fields.Add("U_UrnSrguFytList", "Ürün Sorgulamada Fiyat Listesi");
                fields.Add("U_SubeSecimi", "Şube Seçimi Yapılsın");
                fields.Add("U_PltYapDepSec", "Palet Yapmada Depo Seçimini Aktifleştir");
                fields.Add("U_MalGirCikZorunlu", "Mal Giriş-Çıkış Nedeni Zorunlu");


                UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_GNLPRM", "AIF_WMS_GNLPRM", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_GNLPRM", "");
            }

            #region ORTAK SİSTEM TABLOSU ALANLARI
            //TableCreation.CreateUserFields("ORDR", "T_KullaniciId", "Terminal Kullanıcı ID", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
            #endregion


            //if (Program.mKod == "10TRMN" || Program.mKod == "20TRMN")
            //{
            //    TableCreation.CreateUserFields("OITM", "Kdv", "KDV", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);

            //    TableCreation.CreateUserFields("ORDR", "AracPlakasi", "Araç Plakası", SAPbobsCOM.BoFieldTypes.db_Alpha, 30, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("ORDR", "SoforAdi", "Şoför Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
            //    TableCreation.CreateUserFields("ORDR", "AracSicakligi", "Araç Sıcaklığı", SAPbobsCOM.BoFieldTypes.db_Float, 3, SAPbobsCOM.BoFldSubTypes.st_Price);
            //    TableCreation.CreateUserFields("ORDR", "GonderimTipi", "Gönderim Tipi", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None, clist: sevkTipi);
            //    TableCreation.CreateUserFields("DRF1", "AcikMiktar", "Açık Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);


            //}

            if (Program.mKod == "10TRMN")
            {
                //if (!TableCreation.TableExists("AIF_WMS_PALET"))
                //{
                //    TableCreation.CreateTable("AIF_WMS_PALET", "Palet Yapma", SAPbobsCOM.BoUTBTableType.bott_Document);
                //    TableCreation.CreateTable("AIF_WMS_PALET1", "Palet Yapma Detay", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

                //    TableCreation.CreateUserFields("@AIF_WMS_PALET", "PaletNo", "Palet No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET", "Durum", "Durum", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None, clist: paletDurumu);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET", "ToplamKap", "Toplam Kap", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET", "NetKilo", "Net Kilo", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET", "BrutKilo", "Brut Kilo", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);

                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "Barkod", "Barkod", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "MuhKatalogNo", "Muhatap Katalog No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "Tanim", "Kalem Tanımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "Miktar", "Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 3, SAPbobsCOM.BoFldSubTypes.st_Price);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "SiparisNo", "Sipariş No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "SipSatirNo", "Sipariş Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "CekmeNo", "Çekme Listesi Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "Kaynak", "Kaynak Belgeler", SAPbobsCOM.BoFieldTypes.db_Alpha, 254, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "DepoKodu", "Depo Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "DepoAdi", "Depo Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "DepoYeriId", "Depo Yeri Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "DepoYeriAdi", "Depo Yeri Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                //} 

                //TableCreation.CreateTable("AIF_WMS_PALET2", "Palet Yapma Partiler", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "Barkod", "Barkod", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "Tanim", "Kalem Tanımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "PartiNo", "Parti Numarası", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "Miktar", "Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 3, SAPbobsCOM.BoFldSubTypes.st_Price);

                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "DepoKodu", "Depo Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "DepoAdi", "Depo Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "DepoYeriId", "Depo Yeri Id", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "DepoYeriAdi", "Depo Yeri Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

                //TableCreation.CreateUserFields("@AIF_WMS_PALET1", "DetaySatirNo", "Detay Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                //TableCreation.CreateUserFields("@AIF_WMS_PALET2", "PartiSatirNo", "Parti Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);


                //if (!UdoCreation.UDOExists("AIF_WMS_PALET"))
                //{
                //    fields.Clear();
                //    fields.Add("DocEntry", "Kod");
                //    fields.Add("U_PaletNo", "Palet No");
                //    fields.Add("U_Durum", "Durum");
                //    fields.Add("U_ToplamKap", "Toplam Kap");
                //    fields.Add("U_NetKilo", "Net Kilo");
                //    fields.Add("U_BrutKilo", "Brut Kilo"); 

                //    List<FormColumn> fc = new List<FormColumn>();
                //    List<ChildTable> chList = new List<ChildTable>();

                //    ChildTable ch = new ChildTable();
                //    ch.TableName = "AIF_WMS_PALET1";
                //    fc = new List<FormColumn>();

                //    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
                //    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

                //    fc.Add(new FormColumn { FormColumnAlias = "U_Barkod", FormColumnDescription = "Barkod", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_MuhKatalogNo", FormColumnDescription = "Muhatap Katalog No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_KalemKodu", FormColumnDescription = "Kalem Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_Tanim", FormColumnDescription = "Kalem Tanımı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_Miktar", FormColumnDescription = "Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_DetaySatirNo", FormColumnDescription = "Detay Satır No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                //    ch.FormColumn = fc;
                //    chList.Add(ch);

                //    ch = new ChildTable();
                //    ch.TableName = "AIF_WMS_PALET2";
                //    fc = new List<FormColumn>();

                //    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
                //    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

                //    fc.Add(new FormColumn { FormColumnAlias = "U_Barkod", FormColumnDescription = "Barkod", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_KalemKodu", FormColumnDescription = "Kalem Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_Tanim", FormColumnDescription = "Kalem Tanımı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_PartiNo", FormColumnDescription = "Parti Numarası", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_Miktar", FormColumnDescription = "Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoKodu", FormColumnDescription = "Depo Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoAdi", FormColumnDescription = "Depo Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoYeriId", FormColumnDescription = "Depo Yeri Id", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_DepoYeriAdi", FormColumnDescription = "Depo Yeri Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                //    fc.Add(new FormColumn { FormColumnAlias = "U_PartiSatirNo", FormColumnDescription = "Parti Satır No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                //    ch.FormColumn = fc;
                //    chList.Add(ch);

                //    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_PALET", "AIF_WMS_PALET", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_PALET", "", chList: chList);
                //}

                //if (!TableCreation.TableExists("AIF_WMS_PLTNO"))
                //{
                //    TableCreation.CreateTable("AIF_WMS_PLTNO", "Palet No Belirleme", SAPbobsCOM.BoUTBTableType.bott_Document);

                //    TableCreation.CreateUserFields("@AIF_WMS_PLTNO", "BaslangicNo", "Başlangıç Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                //    TableCreation.CreateUserFields("@AIF_WMS_PLTNO", "SiradakiNo", "Sıradaki Numara", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                //}

                //if (!UdoCreation.UDOExists("AIF_WMS_PLTNO"))
                //{
                //    fields.Clear();
                //    fields.Add("DocEntry", "Kod");
                //    fields.Add("U_BaslangicNo", "Başlangıç Numarası");
                //    fields.Add("U_SiradakiNo", "Sıradaki Numara");

                //    UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_PLTNO", "AIF_WMS_PLTNO", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_PLTNO", "");
                //}

                if (!TableCreation.TableExists("AIF_WMS_MALCIKISNDN"))
                {
                    TableCreation.CreateTable("AIF_WMS_MALCIKISNDN", "Mal Cikis Nedeni", SAPbobsCOM.BoUTBTableType.bott_Document);

                    TableCreation.CreateUserFields("@AIF_WMS_MALCIKISNDN", "TransferKodu", "Transfer Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_MALCIKISNDN", "TransferTipi", "Transfer Tipi", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_MALCIKISNDN", "HesapKodu", "Hesap Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                }

                if (!UdoCreation.UDOExists("AIF_WMS_MALCIKISNDN"))
                {
                    fields.Clear();
                    fields.Add("DocEntry", "Kod");
                    fields.Add("U_TransferKodu", "Transfer Kodu");
                    fields.Add("U_TransferTipi", "TransferTipi");
                    fields.Add("U_HesapKodu", "Hesap Kodu");

                    UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_MALCIKISNDN", "Mal Cikis Nedeni", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_MALCIKISNDN", "");
                }
            }

            if (Program.mKod == "30TRMN")
            {
                if (!TableCreation.TableExists("AIF_WMS_SIPKAR"))
                {
                    TableCreation.CreateTable("AIF_WMS_SIPKAR", "Sipariş Karşılama", SAPbobsCOM.BoUTBTableType.bott_Document);
                    TableCreation.CreateTable("AIF_WMS_SIPKAR1", "Sipariş Karşılama Detay", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "MusteriKodu", "Müşteri Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "MusteriAdi", "Müşteri Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "SiparisTarihi", "Sipariş Tarihi", SAPbobsCOM.BoFieldTypes.db_Date);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "OnayDurumu", "Onay Durumu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None, clist: onayDurumu);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "TerminalGizle", "Terminal için gizle", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "Aciklama", "Açıklama", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);

                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "SiparisTarihi", "Sipariş Tarihi", SAPbobsCOM.BoFieldTypes.db_Date);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "TeslimatTarihi", "Teslimat Tarihi", SAPbobsCOM.BoFieldTypes.db_Date);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "UrunKodu", "Ürün Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "UrunTanimi", "Ürün Tanımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "UrunYTanim", "Ürün Yabancı Tanımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 250, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "TopSipMik", "Toplam Sipariş Miktarı", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "SevkSipMik", "Sevk Sipariş Miktarı", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "AcikSipMik", "Açık Sipariş Miktarı", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "PlanSipMik", "Planlanan Sipariş Miktarı", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "SipDepoKodu", "Sipariş Depo Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 30, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "DepoStokMik", "Depo Stok Miktarı", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "GenelStokMik", "Genel Stok Miktarı", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "ToplananMik", "Toplanan Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "PaletNo", "Palet No", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "SiraNo", "Sıra No", SAPbobsCOM.BoFieldTypes.db_Alpha, 10, SAPbobsCOM.BoFldSubTypes.st_None);

                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "SiparisNumarasi", "Sipariş Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "SipSatirNo", "Sipariş Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "SiparisNumarasi", "Sipariş Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "MuhRefNo", "Muhatap Referans No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "MuhKatNo", "Muhatap Katalog No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "MuhKatGoster", "Muhatap Katalog No Göster", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None); //Eğer bu alan ekran üzerinde işaretli ise terminalde kalem kodu sütununda müşterinin muhatap katalog numarası değil ise kalem kodu gözükmesi için yapıldı.

                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "BirimFiyat", "Birim Fiyat", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "HesapSipMik", "Hesaplanan Sipariş Miktarı", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "TopSatirTutar", "Toplam Satır Tutarı", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);

                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "SoforAdSoyad", "Şoför Ad/Soyad", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "SoforTCKN", "Şoför TCKN", SAPbobsCOM.BoFieldTypes.db_Alpha, 11, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR", "AracPlaka", "Araç Plakası", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_SIPKAR1", "Gorunur", "Görünür", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None, clist: acikKapali);
                }

                if (!UdoCreation.UDOExists("AIF_WMS_SIPKAR"))
                {
                    fields.Clear();
                    fields.Add("DocEntry", "Kod");
                    fields.Add("U_MusteriKodu", "Müşteri Kodu");
                    fields.Add("U_MusteriAdi", "Müşteri Adı");
                    fields.Add("U_SiparisTarihi", "Sipariş Tarihi");
                    fields.Add("U_SiparisNumarasi", "Sipariş Numarası");
                    fields.Add("U_OnayDurumu", "Onay Durumu");
                    fields.Add("U_TerminalGizle", "Terminal İçin Gizle");
                    fields.Add("U_MuhKatGoster", "Muhatap Katalog No Göster");
                    fields.Add("U_Aciklama", "Açıklama");
                    fields.Add("U_SoforAdSoyad", "Şoför Ad/Soyad");
                    fields.Add("U_SoforTCKN", "Şoför TCKN");
                    fields.Add("U_AracPlaka", "Araç Plakası");

                    List<FormColumn> fc = new List<FormColumn>();
                    List<ChildTable> chList = new List<ChildTable>();

                    ChildTable ch = new ChildTable();
                    ch.TableName = "AIF_WMS_SIPKAR1";
                    fc = new List<FormColumn>();


                    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
                    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

                    fc.Add(new FormColumn { FormColumnAlias = "U_SiparisNumarasi", FormColumnDescription = "Sipariş Numarası", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_SiparisTarihi", FormColumnDescription = "Sipariş Tarihi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_TeslimatTarihi", FormColumnDescription = "Teslimat Tarihi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_SipSatirNo", FormColumnDescription = "Sipariş Satır No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_UrunKodu", FormColumnDescription = "Ürün Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_UrunTanimi", FormColumnDescription = "Ürün Tanımı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_UrunYTanim", FormColumnDescription= "Ürün Yabancı Tanımı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_TopSipMik", FormColumnDescription = "Toplam Sipariş Miktarı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_SevkSipMik", FormColumnDescription = "Sevk Sipariş Miktarı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_AcikSipMik", FormColumnDescription = "Açık Sipariş Miktarı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_PlanSipMik", FormColumnDescription = "Planlanan Sipariş Miktarı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_SipDepoKodu", FormColumnDescription = "Sipariş Depo Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_DepoStokMik", FormColumnDescription = "Depo Stok Miktarı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_GenelStokMik", FormColumnDescription = "Genel Stok Miktarı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_ToplananMik", FormColumnDescription = "Toplanan Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_PaletNo", FormColumnDescription = "Palet No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_SiraNo", FormColumnDescription = "Sıra No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                    fc.Add(new FormColumn { FormColumnAlias = "U_MuhRefNo", FormColumnDescription = "Muhatap Referans No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_MuhKatNo", FormColumnDescription = "Muhatap Katalog No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                    fc.Add(new FormColumn { FormColumnAlias = "U_BirimFiyat", FormColumnDescription = "Birim Fiyat", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_HesapSipMik", FormColumnDescription = "Hesaplanan Sipariş Miktarı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_TopSatirTutar", FormColumnDescription = "Toplam Satır Tutarı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_Gorunur", FormColumnDescription = "Görünür", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                    ch.FormColumn = fc;
                    chList.Add(ch);

                    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_SIPKAR", "AIF_WMS_SIPKAR", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_SIPKAR", "", chList: chList);
                }

                if (!TableCreation.TableExists("AIF_WMS_PALET"))
                {
                    TableCreation.CreateTable("AIF_WMS_PALET", "Palet Yapma", SAPbobsCOM.BoUTBTableType.bott_Document);
                    TableCreation.CreateTable("AIF_WMS_PALET1", "Palet Yapma Detay", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);
                    TableCreation.CreateTable("AIF_WMS_PALET2", "Palet Yapma Partiler", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

                    TableCreation.CreateUserFields("@AIF_WMS_PALET", "PaletNo", "Palet No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET", "Durum", "Durum", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None, clist: paletDurumu);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET", "ToplamKap", "Toplam Kap", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET", "NetKilo", "Net Kilo", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET", "BrutKilo", "Brut Kilo", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET", "Depo", "Depo", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "Barkod", "Barkod", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "MuhKatalogNo", "Muhatap Katalog No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "Tanim", "Kalem Tanımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "Miktar", "Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 3, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "SiparisNo", "Sipariş No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "SipSatirNo", "Sipariş Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "CekmeNo", "Çekme Listesi Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PALET1", "Kaynak", "Kaynak Belgeler", SAPbobsCOM.BoFieldTypes.db_Alpha, 254, SAPbobsCOM.BoFldSubTypes.st_None);
                }

                TableCreation.CreateUserFields("@AIF_WMS_PALET2", "Barkod", "Barkod", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);  
                TableCreation.CreateUserFields("@AIF_WMS_PALET2", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_PALET2", "Tanim", "Kalem Tanımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_PALET2", "PartiNo", "Parti Numarası", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                TableCreation.CreateUserFields("@AIF_WMS_PALET2", "Miktar", "Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 3, SAPbobsCOM.BoFldSubTypes.st_Price);

                if (!UdoCreation.UDOExists("AIF_WMS_PALET"))
                {
                    fields.Clear();
                    fields.Add("DocEntry", "Kod");
                    fields.Add("U_PaletNo", "Palet No");
                    fields.Add("U_Durum", "Durum");
                    fields.Add("U_ToplamKap", "Toplam Kap");
                    fields.Add("U_NetKilo", "Net Kilo");
                    fields.Add("U_BrutKilo", "Brut Kilo");
                    fields.Add("U_Depo", "Depo");

                    List<FormColumn> fc = new List<FormColumn>();
                    List<ChildTable> chList = new List<ChildTable>();

                    ChildTable ch = new ChildTable();
                    ch.TableName = "AIF_WMS_PALET1";
                    fc = new List<FormColumn>();
                     
                    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
                    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

                    fc.Add(new FormColumn { FormColumnAlias = "U_Barkod", FormColumnDescription = "Barkod", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_MuhKatalogNo", FormColumnDescription = "Muhatap Katalog No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_KalemKodu", FormColumnDescription = "Kalem Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_Tanim", FormColumnDescription = "Kalem Tanımı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_Miktar", FormColumnDescription = "Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                    ch.FormColumn = fc;
                    chList.Add(ch);

                    ch = new ChildTable();
                    ch.TableName = "AIF_WMS_PALET2";
                    fc = new List<FormColumn>();

                    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
                    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

                    fc.Add(new FormColumn { FormColumnAlias = "U_Barkod", FormColumnDescription = "Barkod", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_KalemKodu", FormColumnDescription = "Kalem Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_Tanim", FormColumnDescription = "Kalem Tanımı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_PartiNo", FormColumnDescription = "Parti Numarası", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_Miktar", FormColumnDescription = "Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_PaletMi", FormColumnDescription = "Palet mi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                    ch.FormColumn = fc;
                    chList.Add(ch);

                    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_PALET", "AIF_WMS_PALET", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_PALET", "", chList: chList);
                }

                if (!TableCreation.TableExists("AIF_WMS_PLTNO"))
                {
                    TableCreation.CreateTable("AIF_WMS_PLTNO", "Palet No Belirleme", SAPbobsCOM.BoUTBTableType.bott_Document);

                    TableCreation.CreateUserFields("@AIF_WMS_PLTNO", "BaslangicNo", "Başlangıç Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_PLTNO", "SiradakiNo", "Sıradaki Numara", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                }

                if (!UdoCreation.UDOExists("AIF_WMS_PLTNO"))
                {
                    fields.Clear();
                    fields.Add("DocEntry", "Kod");
                    fields.Add("U_BaslangicNo", "Başlangıç Numarası");
                    fields.Add("U_SiradakiNo", "Sıradaki Numara");

                    UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_PLTNO", "AIF_WMS_PLTNO", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_PLTNO", "");
                }

                if (!TableCreation.TableExists("AIF_WMS_KNTYNR"))
                {
                    TableCreation.CreateTable("AIF_WMS_KNTYNR", "Konteyner Yapma", SAPbobsCOM.BoUTBTableType.bott_Document);
                    TableCreation.CreateTable("AIF_WMS_KNTYNR1", "Konteyner Yapma Detay", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR", "KonteynerNo", "Konteyner No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR", "MuhatapKodu", "Muhatap Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR", "MuhatapAdi", "Muhatap Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "PaletNo", "Palet No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "Barkod", "Barkod", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "MuhKatalogNo", "Muhatap Katalog No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "Tanim", "Kalem Tanımı", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "Miktar", "Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 3, SAPbobsCOM.BoFldSubTypes.st_Price);

                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "SiparisNo", "Sipariş No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "SipSatirNo", "Sipariş Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "CekmeNo", "Çekme Listesi Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "KoliMiktari", "Koli Miktarı", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "NetKilo", "Net Kilo", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "BrutKilo", "Brüt Kilo", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_KNTYNR1", "Kaynak", "Kaynak", SAPbobsCOM.BoFieldTypes.db_Alpha, 254, SAPbobsCOM.BoFldSubTypes.st_None);

                }

                if (!UdoCreation.UDOExists("AIF_WMS_KNTYNR"))
                {
                    fields.Clear();
                    fields.Add("DocEntry", "Kod");
                    fields.Add("U_KonteynerNo", "Konteyner No");
                    fields.Add("U_MuhatapKodu", "Muhatap Kodu");
                    fields.Add("U_MuhatapAdi", "Muhatap Adı");

                    List<FormColumn> fc = new List<FormColumn>();
                    List<ChildTable> chList = new List<ChildTable>();

                    ChildTable ch = new ChildTable();
                    ch.TableName = "AIF_WMS_KNTYNR1";
                    fc = new List<FormColumn>();


                    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
                    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

                    fc.Add(new FormColumn { FormColumnAlias = "U_PaletNo", FormColumnDescription = "Palet No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_Barkod", FormColumnDescription = "Barkod", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_MuhKatalogNo", FormColumnDescription = "Muhatap Katalog No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_KalemKodu", FormColumnDescription = "Kalem Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_Tanim", FormColumnDescription = "Kalem Tanımı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_Miktar", FormColumnDescription = "Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_SiparisNo", FormColumnDescription = "Sipariş No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_SipSatirNo", FormColumnDescription = "Sipariş Satır No", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                    ch.FormColumn = fc;
                    chList.Add(ch);

                    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_KNTYNR", "AIF_WMS_KNTYNR", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_KNTYNR", "", chList: chList);
                }
                 
                if (!TableCreation.TableExists("AIF_WMS_KOLIDTY"))
                {
                    TableCreation.CreateTable("AIF_WMS_KOLIDTY", "Koli Detay", SAPbobsCOM.BoUTBTableType.bott_Document);
                    TableCreation.CreateTable("AIF_WMS_KOLIDTY1", "Koli Detay 1", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY", "BelgeNo", "Belge Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);

                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY1", "KoliAdedi", "Koli Adedi", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY1", "KoliIciAdedi", "Koli İçi Adedi", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY1", "ToplamMiktar", "Toplam Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);

                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY1", "SatirNo", "Satır Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY1", "SiparisNo", "Sipariş Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);

                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY1", "PaletNo", "Palet Numarası", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY1", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

                    TableCreation.CreateUserFields("@AIF_WMS_KOLIDTY1", "Kaynak", "Kaynak Belgeler", SAPbobsCOM.BoFieldTypes.db_Alpha, 254, SAPbobsCOM.BoFldSubTypes.st_None);
                }

                if (!UdoCreation.UDOExists("AIF_WMS_KOLIDTY"))
                {
                    fields.Clear();
                    fields.Add("DocEntry", "Kod");
                    fields.Add("U_BelgeNo", "Belge Numarası");

                    List<FormColumn> fc = new List<FormColumn>();
                    List<ChildTable> chList = new List<ChildTable>();

                    ChildTable ch = new ChildTable();
                    ch.TableName = "AIF_WMS_KOLIDTY1";
                    fc = new List<FormColumn>();


                    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
                    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

                    fc.Add(new FormColumn { FormColumnAlias = "U_SatirNo", FormColumnDescription = "Satır Numarası", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_KoliAdedi", FormColumnDescription = "Koli Adedi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_KoliIciAdedi", FormColumnDescription = "Koli İçi Adedi", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_ToplamMiktar", FormColumnDescription = "Toplam Miktar", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_PaletNo", FormColumnDescription = "Palet Numarası", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_KalemKodu", FormColumnDescription = "Kalem Kodu", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                    ch.FormColumn = fc;
                    chList.Add(ch);

                    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_KOLIDTY", "AIF_WMS_KOLIDTY", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_KOLIDTY", "", chList: chList);
                }

                if (!TableCreation.TableExists("AIF_WMS_TOPLANAN"))
                {
                    TableCreation.CreateTable("AIF_WMS_TOPLANAN", "Toplanan Siparişler", SAPbobsCOM.BoUTBTableType.bott_Document);

                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "SiparisNumarasi", "Sipariş Numarası", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "Miktar", "Miktar", SAPbobsCOM.BoFieldTypes.db_Float, 10, SAPbobsCOM.BoFldSubTypes.st_Price);
                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "PaletNo", "Palet No", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);

                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "SiparisSatirNo", "Sipariş Satır No", SAPbobsCOM.BoFieldTypes.db_Numeric, 10, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "BelgeNo", "Belge No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "TeslimatNo", "Teslimat No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "KntynrNo", "Konteyner No", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "KalemKodu", "Kalem Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "KalemAdi", "Kalem Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_TOPLANAN", "Kaynak", "Kaynak Belgeler", SAPbobsCOM.BoFieldTypes.db_Alpha, 254, SAPbobsCOM.BoFldSubTypes.st_None);
                }

                if (!UdoCreation.UDOExists("AIF_WMS_TOPLANAN"))
                {
                    fields.Clear();
                    fields.Add("DocEntry", "Kod");
                    fields.Add("U_SiparisNumarasi", "Sipariş Numarası");
                    fields.Add("U_SiparisSatirNo", "Sipariş Satır No");
                    fields.Add("U_Miktar", "Miktar");
                    fields.Add("U_PaletNo", "Palet No");
                    fields.Add("U_BelgeNo", "Belge No");
                    fields.Add("U_TeslimatNo", "Teslimat No");

                    UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_TOPLANAN", "AIF_WMS_TOPLANAN", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_TOPLANAN", "");
                }
            }

        }
    }
}