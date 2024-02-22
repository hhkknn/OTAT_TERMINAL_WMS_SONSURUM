using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib;
using AIF.WMS.HelperClass;
using AIF.WMS.Models;
using CrystalDecisions.CrystalReports.Engine;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace AIF.WMS.ClassLayer
{
    public class SiparisKarsilama
    {
        [ItemAtt(AIFConn.SiparisKarsilamaUID)]
        public SAPbouiCOM.Form frmSiparisKarsilama;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.EditText oEdtMusteriKodu;
        [ItemAtt("Item_3")]
        public SAPbouiCOM.EditText oEdtMusteriAdi;
        [ItemAtt("Item_5")]
        public SAPbouiCOM.EditText oEdtSiparisTarihi;
        [ItemAtt("Item_14")]
        public SAPbouiCOM.ComboBox oComboOnayDurumu;
        [ItemAtt("Item_7")]
        public SAPbouiCOM.EditText oEdtSiparisNo;
        [ItemAtt("Item_9")]
        public SAPbouiCOM.Matrix oMatrix;
        [ItemAtt("Item_16")]
        public SAPbouiCOM.EditText oEdtDocEntry;
        [ItemAtt("Item_11")]
        public SAPbouiCOM.CheckBox oChekSifirMiktarlar;
        [ItemAtt("Item_12")]
        public SAPbouiCOM.Button obtnOnayla;
        [ItemAtt("Item_27")]
        public SAPbouiCOM.Button obtnIrsaliyeYazdir;
        [ItemAtt("1")]
        public SAPbouiCOM.Button oBtnEkleGuncelle;

        [ItemAtt("Item_31")]
        public SAPbouiCOM.EditText oEdtSoforAdSoyad;
        [ItemAtt("Item_32")]
        public SAPbouiCOM.EditText oEdtSoforTCKN;
        [ItemAtt("Item_33")]
        public SAPbouiCOM.EditText oEdtAracPlaka;
        [ItemAtt("Item_35")]
        public SAPbouiCOM.CheckBox oChkAcikIsler;
        [ItemAtt("Item_36")]
        public SAPbouiCOM.ComboBox oComboKonteyner;
        public void LoadForms()
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.SiparisKarsilamaXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.SiparisKarsilamaXML));
            Functions.CreateUserOrSystemFormComponent<SiparisKarsilama>(AIFConn.SipKarsi);

            InitForms();
        }


        private string header = @"<?xml version=""1.0"" encoding=""UTF-16"" ?><dbDataSources uid=""@AIF_WMS_SIPKAR1""><rows>{0}</rows></dbDataSources>";

        private string row = "<row>" +
            "<cells>" +
            "<cell><uid>U_SiparisNumarasi</uid><value>{0}</value></cell>" +
            "<cell><uid>U_SiparisTarihi</uid><value>{1}</value></cell>" +
            "<cell><uid>U_TeslimatTarihi</uid><value>{2}</value></cell>" +
            "<cell><uid>U_SipSatirNo</uid><value>{3}</value></cell>" +
            "<cell><uid>U_UrunKodu</uid><value>{4}</value></cell>" +
            "<cell><uid>U_UrunTanimi</uid><value>{5}</value></cell>" +
            "<cell><uid>U_UrunYTanim</uid><value>{6}</value></cell>" +
            "<cell><uid>U_TopSipMik</uid><value>{7}</value></cell>" +
            "<cell><uid>U_SevkSipMik</uid><value>{8}</value></cell>" +
            "<cell><uid>U_AcikSipMik</uid><value>{9}</value></cell>" +
            "<cell><uid>U_PlanSipMik</uid><value>{10}</value></cell>" +
            "<cell><uid>U_SipDepoKodu</uid><value>{11}</value></cell>" +
            "<cell><uid>U_DepoStokMik</uid><value>{12}</value></cell>" +
            "<cell><uid>U_GenelStokMik</uid><value>{13}</value></cell>" +
            "<cell><uid>U_SiraNo</uid><value>{14}</value></cell>" +
            "<cell><uid>U_PaletNo</uid><value>{15}</value></cell>" +
            "<cell><uid>U_ToplananMik</uid><value>{16}</value></cell>" +
            "<cell><uid>U_MuhRefNo</uid><value>{17}</value></cell>" +
            "<cell><uid>U_MuhKatNo</uid><value>{18}</value></cell>" +
            "<cell><uid>U_BirimFiyat</uid><value>{19}</value></cell>" +
            "<cell><uid>U_HesapSipMik</uid><value>{20}</value></cell>" +
            "<cell><uid>U_TopSatirTutar</uid><value>{21}</value></cell>" +
            "<cell><uid>U_Gorunur</uid><value>{22}</value></cell>" +
            "</cells></row>";


        private bool eklemeGuncelleme = false;
        public void InitForms()
        {
            try
            {
                frmSiparisKarsilama.EnableMenu("1283", false);
                frmSiparisKarsilama.EnableMenu("1284", false);
                frmSiparisKarsilama.EnableMenu("1286", false);

                oMatrix.AutoResizeColumns();
                oComboOnayDurumu.Select("T", BoSearchKey.psk_ByValue);
            }
            catch (Exception)
            {

            }

            try
            {
                oMatrix.Columns.Item("Col_21").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
                oMatrix.AutoResizeColumns();
            }
            catch (Exception)
            {
            }
            oComboKonteyner.Item.AffectsFormMode = false;

            //oChkAcikIsler.Item.AffectsFormMode = false;
        }

        private string _docEntry = "";

        public bool SAP_FormDataEvent(ref BusinessObjectInfo BusinessObjectInfo, ref bool BubbleEvent)
        {
            BubbleEvent = true;
            switch (BusinessObjectInfo.EventType)
            {
                case BoEventTypes.et_ALL_EVENTS:
                    break;
                case BoEventTypes.et_ITEM_PRESSED:
                    break;
                case BoEventTypes.et_KEY_DOWN:
                    break;
                case BoEventTypes.et_GOT_FOCUS:
                    break;
                case BoEventTypes.et_LOST_FOCUS:
                    break;
                case BoEventTypes.et_COMBO_SELECT:
                    break;
                case BoEventTypes.et_CLICK:
                    break;
                case BoEventTypes.et_DOUBLE_CLICK:
                    break;
                case BoEventTypes.et_MATRIX_LINK_PRESSED:
                    break;
                case BoEventTypes.et_MATRIX_COLLAPSE_PRESSED:
                    break;
                case BoEventTypes.et_VALIDATE:
                    break;
                case BoEventTypes.et_MATRIX_LOAD:
                    break;
                case BoEventTypes.et_DATASOURCE_LOAD:
                    break;
                case BoEventTypes.et_FORM_LOAD:
                    break;
                case BoEventTypes.et_FORM_UNLOAD:
                    break;
                case BoEventTypes.et_FORM_ACTIVATE:
                    break;
                case BoEventTypes.et_FORM_DEACTIVATE:
                    break;
                case BoEventTypes.et_FORM_CLOSE:
                    break;
                case BoEventTypes.et_FORM_RESIZE:
                    break;
                case BoEventTypes.et_FORM_KEY_DOWN:
                    break;
                case BoEventTypes.et_FORM_MENU_HILIGHT:
                    break;
                case BoEventTypes.et_PRINT:
                    break;
                case BoEventTypes.et_PRINT_DATA:
                    break;
                case BoEventTypes.et_EDIT_REPORT:
                    break;
                case BoEventTypes.et_CHOOSE_FROM_LIST:
                    break;
                case BoEventTypes.et_RIGHT_CLICK:
                    break;
                case BoEventTypes.et_MENU_CLICK:
                    break;
                case BoEventTypes.et_FORM_DATA_ADD:
                    break;
                case BoEventTypes.et_FORM_DATA_UPDATE:
                    //if (BusinessObjectInfo.ActionSuccess)
                    //{
                    //    eklemeGuncelleme = true;
                    //    XmlDocument key = new XmlDocument();
                    //    key.LoadXml(BusinessObjectInfo.ObjectKey);
                    //    _docEntry = key.SelectNodes("AIF_WMS_SIPKAR").Item(0).SelectNodes("DocEntry").Item(0).InnerXml;
                    //}
                    break;
                case BoEventTypes.et_FORM_DATA_DELETE:
                    break;
                case BoEventTypes.et_FORM_DATA_LOAD:
                    if (!BusinessObjectInfo.BeforeAction)
                    {
                        try
                        {
                            if (oComboOnayDurumu.Value == "O")
                            {
                                oComboOnayDurumu.Item.Enabled = false;
                                obtnOnayla.Item.Enabled = false;

                            }
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            oBtnEkleGuncelle.Item.Enabled = true;
                            frmSiparisKarsilama.DataSources.UserDataSources.Item("UD_2").ValueEx = "N";
                        }
                        catch (Exception)
                        {
                        }

                        SatirlariAcKapat();

                        frmSiparisKarsilama.DataSources.UserDataSources.Item("UD_3").ValueEx = "";

                        for (int i = oComboKonteyner.ValidValues.Count - 1; i >= 0; i--)
                        {
                            oComboKonteyner.ValidValues.Remove(0, BoSearchKey.psk_Index);
                        }

                        ConstVariables.oRecordset.DoQuery("SELECT DISTINCT T1.\"U_KonteynerNo\" FROM \"@AIF_WMS_KNTYNR1\" T0 INNER JOIN \"@AIF_WMS_KNTYNR\"  T1 ON  T0.\"DocEntry\" = T1.\"DocEntry\" WHERE T0.\"U_CekmeNo\"  = '" + oEdtDocEntry.Value.ToString() + "'");


                        while (!ConstVariables.oRecordset.EoF)
                        {
                            oComboKonteyner.ValidValues.Add(ConstVariables.oRecordset.Fields.Item(0).Value.ToString(), ConstVariables.oRecordset.Fields.Item(0).Value.ToString());
                            ConstVariables.oRecordset.MoveNext();
                        }

                        try
                        {
                            string docentry = ((SAPbouiCOM.EditText)frmSiparisKarsilama.Items.Item("Item_16").Specific).Value.ToString();

                            string sorgu = "update \"@AIF_WMS_SIPKAR1\" set \"U_DepoStokMik\" = A.\"DepoStokMiktari\",    \"U_GenelStokMik\" = A.\"GenelStokMiktari\" from(    Select(      Select SUM(T99.\"OnHand\")              from OITW as T99    where T99.\"ItemCode\" = T1.\"U_UrunKodu\"       and T99.\"WhsCode\" = T1.\"U_SipDepoKodu\"    ) - (   select SUM(k1.\"U_PlanSipMik\") - SUM(k1.\"U_ToplananMik\")                    from \"@AIF_WMS_SIPKAR1\" k1     inner join \"@AIF_WMS_SIPKAR\" k0 on k0.\"DocEntry\" = k1.\"DocEntry\"      where K1.\"DocEntry\" = T1.\"DocEntry\"          and K1.\"U_UrunKodu\" = t1.\"U_UrunKodu\"       and K1.\"U_SipDepoKodu\" = t1.\"U_SipDepoKodu\"          and K0.\"U_MusteriKodu\" = T0.\"U_MusteriKodu\"      ) as \"DepoStokMiktari\",     (   Select SUM(T99.\"OnHand\")                from OITW as T99                where T99.\"ItemCode\" = T1.\"U_UrunKodu\"                    and T99.\"WhsCode\" != T1.\"U_SipDepoKodu\"            ) as \"GenelStokMiktari\",            T0.\"DocEntry\",            T1.\"LineId\"        from \"@AIF_WMS_SIPKAR1\" t1            inner join \"@AIF_WMS_SIPKAR\" t0 on t0.\"DocEntry\" = t1.\"DocEntry\"    ) A " +
                                " where A.\"DocEntry\" = " + docentry + "    and A.\"LineId\" = \"@AIF_WMS_SIPKAR1\".\"LineId\"    and A.\"DocEntry\" = \"@AIF_WMS_SIPKAR1\".\"DocEntry\"";

                            ConstVariables.oRecordset.DoQuery(sorgu);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    break;
                case BoEventTypes.et_PICKER_CLICKED:
                    break;
                case BoEventTypes.et_GRID_SORT:
                    break;
                case BoEventTypes.et_Drag:
                    break;
                case BoEventTypes.et_FORM_DRAW:
                    break;
                case BoEventTypes.et_UDO_FORM_BUILD:
                    break;
                case BoEventTypes.et_UDO_FORM_OPEN:
                    break;
                case BoEventTypes.et_B1I_SERVICE_COMPLETE:
                    break;
                case BoEventTypes.et_FORMAT_SEARCH_COMPLETED:
                    break;
                case BoEventTypes.et_PRINT_LAYOUT_KEY:
                    break;
                case BoEventTypes.et_FORM_VISIBLE:
                    break;
                case BoEventTypes.et_ITEM_WEBMESSAGE:
                    break;
                default:
                    break;
            }
            return BubbleEvent;
        }
        private void SatirlariAcKapat()
        {

            try
            {
                int colnum = 0;

                for (int i = 0; i <= oMatrix.Columns.Count; i++)
                {
                    if (oMatrix.Columns.Item(i).UniqueID == "Col_9")
                    {
                        colnum = i;
                        break;
                    }
                }




                string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);

                var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                            select new
                            {
                                durum = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_22" select new XElement(y.Element("Value"))).First().Value,
                                sira = x.ElementsBeforeSelf().Count() + 1,
                            }).ToList();


                foreach (var item in rows)
                {
                    if (item.durum != "Y")
                    {
                        oMatrix.CommonSetting.SetRowEditable(item.sira, false);
                    }
                    else
                    {
                        oMatrix.CommonSetting.SetCellEditable(item.sira, colnum, true);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public bool SAP_ItemEvent(string FormUID, ref ItemEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;
            switch (pVal.EventType)
            {
                case BoEventTypes.et_ALL_EVENTS:
                    break;
                case BoEventTypes.et_ITEM_PRESSED:
                    if (pVal.ItemUID == "Item_12" && !pVal.BeforeAction)
                    {
                        if (obtnOnayla.Item.Enabled == false)
                        {
                            return false;
                        }

                        if (frmSiparisKarsilama.Mode == BoFormMode.fm_ADD_MODE)
                        {
                            Handler.SAPApplication.MessageBox("Eklenmemiş belge üzerinde onaylama işlemi yapılamaz.");

                            return false;
                        }
                        #region Onaylama İşlemi
                        SAPbobsCOM.GeneralService oGeneralService;

                        SAPbobsCOM.GeneralData oGeneralData;

                        SAPbobsCOM.GeneralDataParams oGeneralParams;

                        SAPbobsCOM.CompanyService oCompService = ConstVariables.oCompanyObject.GetCompanyService();

                        oGeneralService = oCompService.GetGeneralService("AIF_WMS_SIPKAR");

                        oGeneralParams = ((SAPbobsCOM.GeneralDataParams)(oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)));

                        oGeneralParams.SetProperty("DocEntry", oEdtDocEntry.Value);

                        oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                        oGeneralData.SetProperty("U_OnayDurumu", "O");

                        try
                        {
                            oGeneralService.Update(oGeneralData);

                            Handler.SAPApplication.ActivateMenuItem("1304");
                        }
                        catch (Exception)
                        {
                        }

                        #endregion
                    }
                    else if (pVal.ItemUID == "Item_11" && !pVal.BeforeAction)
                    {
                        try
                        {
                            frmSiparisKarsilama.Freeze(true);

                            //if (frmSiparisKarsilama.Mode == BoFormMode.fm_OK_MODE || frmSiparisKarsilama.Mode == BoFormMode.fm_UPDATE_MODE)
                            //{
                            //    Handler.SAPApplication.MessageBox("Ekli olan belge için filtreleme tekrar yapılamaz.");
                            //    BubbleEvent = false;
                            //    return false;
                            //}

                            //if (frmSiparisKarsilama.Mode == BoFormMode.fm_FIND_MODE)
                            //{
                            //    return false;
                            //}

                            //listele(oChekSifirMiktarlar.Checked); 
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            frmSiparisKarsilama.Freeze(false);
                        }
                    }
                    else if (pVal.ItemUID == "1" && !pVal.BeforeAction)
                    {
                        oEdtSoforAdSoyad.Item.Click();
                        oEdtDocEntry.Item.Enabled = false;

                        try
                        {
                            Handler.SAPApplication.ActivateMenuItem("1304");
                            Handler.SAPApplication.ActivateMenuItem("1304");
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (pVal.ItemUID == "Item_35" && !pVal.BeforeAction)
                    {
                        if (frmSiparisKarsilama.Mode == BoFormMode.fm_UPDATE_MODE)
                        {
                            if (oChkAcikIsler.Checked)
                            {
                                var ret = Handler.SAPApplication.MessageBox("Yapılan tüm değişikler kaybolacaktır. Devam etmek istiyor musunuz?", 1, "Evet", "Hayır");

                                if (ret != 1)
                                {
                                    frmSiparisKarsilama.DataSources.UserDataSources.Item("UD_2").ValueEx = "N";
                                    BubbleEvent = false;
                                    return false;
                                }
                            }
                        }

                        if (oChkAcikIsler.Checked)
                        {
                            #region Görünür işlemi

                            //Görünür işlemi için çalışmaktadır. Veritabanındaki Görünür Y işaretli olan dataları getirir.
                            SAPbouiCOM.Conditions oConditions;

                            SAPbouiCOM.Condition oCondition;

                            oConditions = new SAPbouiCOM.Conditions();
                            oCondition = oConditions.Add();
                            oCondition.Alias = "U_Gorunur";
                            oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCondition.CondVal = "Y";


                            oCondition.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                            oCondition = oConditions.Add();
                            oCondition.Alias = "DocEntry";
                            oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCondition.CondVal = oEdtDocEntry.Value.ToString();

                            frmSiparisKarsilama.DataSources.DBDataSources.Item("@AIF_WMS_SIPKAR1").Query(oConditions);

                            oMatrix.LoadFromDataSource();

                            frmSiparisKarsilama.Mode = BoFormMode.fm_OK_MODE;
                            oBtnEkleGuncelle.Item.Enabled = false;
                            SatirlariAcKapat();
                            #endregion 
                        }
                        else
                        {
                            SAPbouiCOM.Conditions oConditions;

                            SAPbouiCOM.Condition oCondition;

                            oConditions = new SAPbouiCOM.Conditions();
                            oCondition = oConditions.Add();
                            oCondition.Alias = "DocEntry";
                            oCondition.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                            oCondition.CondVal = oEdtDocEntry.Value.ToString();
                            frmSiparisKarsilama.DataSources.DBDataSources.Item("@AIF_WMS_SIPKAR1").Query(oConditions);

                            oMatrix.LoadFromDataSource();
                            oBtnEkleGuncelle.Item.Enabled = true;
                            SatirlariAcKapat();
                        }
                    }
                    break;
                case BoEventTypes.et_KEY_DOWN:
                    break;
                case BoEventTypes.et_GOT_FOCUS:
                    break;
                case BoEventTypes.et_LOST_FOCUS:
                    break;
                case BoEventTypes.et_COMBO_SELECT:
                    break;
                case BoEventTypes.et_CLICK:
                    if (pVal.ItemUID == "Item_8" && !pVal.BeforeAction)
                    {
                        try
                        {
                            frmSiparisKarsilama.Freeze(true);

                            //if (frmSiparisKarsilama.Mode == BoFormMode.fm_OK_MODE || frmSiparisKarsilama.Mode == BoFormMode.fm_UPDATE_MODE)
                            //{
                            //    Handler.SAPApplication.MessageBox("Ekli olan belge için filtreleme tekrar yapılamaz.");
                            //    BubbleEvent = false;
                            //    return false;
                            //}

                            //if (frmSiparisKarsilama.Mode == BoFormMode.fm_FIND_MODE)
                            //{
                            //    return false;
                            //}

                            if (oEdtMusteriKodu.Value == "")
                            {
                                Handler.SAPApplication.MessageBox("Müşteri kodu seçilmeden ürün ekleme yapılamaz.");
                                BubbleEvent = false;
                                return false;
                            }

                            string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                            var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                        select new _SiparisKarsilama
                                        {
                                            //siraNo = Convert.ToInt32((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_14" select new XElement(y.Element("Value"))).First().Value),
                                            siparisNumarasi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                                            siparisTarihi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value.Contains("/") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value.Contains(".") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : "",
                                            teslimatTarihi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value.Contains("/") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value.Contains(".") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : "",
                                            siparisSatirNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value,
                                            urunKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_4" select new XElement(y.Element("Value"))).First().Value,
                                            urunTanimi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_5" select new XElement(y.Element("Value"))).First().Value,
                                            urunYTanimi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_23" select new XElement(y.Element("Value"))).First().Value,
                                            toplamSatisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_6" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            sevksiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_7" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            acikSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_8" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            planlananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            siparisDepoKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_10" select new XElement(y.Element("Value"))).First().Value,
                                            depoStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_11" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            genelStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_12" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            paletNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_15" select new XElement(y.Element("Value"))).First().Value,
                                            toplananMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_13" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                        }).ToList();


                            AIFConn.UrunEkle.LoadForms(oEdtMusteriKodu.Value, rows);
                        }
                        catch (Exception ex)
                        {
                        }
                        finally
                        {
                            frmSiparisKarsilama.Freeze(false);
                        }

                    }
                    else if (pVal.ItemUID == "Item_9" && pVal.Row > -1)
                    {
                        try
                        {
                            oMatrix.SelectRow(pVal.Row, true, false);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (pVal.ItemUID == "1")
                    {
                        if (oBtnEkleGuncelle.Item.Enabled == false)
                        {
                            BubbleEvent = false;
                            return false;
                        }
                        string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                        var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                    select new
                                    {
                                        PlanlananMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                        kalanMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()) - HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_13" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                        index = x.ElementsBeforeSelf().Count() + 1
                                    }).ToList();


                        //foreach (var item in rows.OrderByDescending(x => x.index).Where(x => x.kalanMiktar == 0))
                        //{
                        //    oMatrix.DeleteRow(item.index);
                        //}

                        for (int i = 1; i <= oMatrix.RowCount; i++)
                        {
                            ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_14").Cells.Item(i).Specific).Value = i.ToString();
                        }
                    }
                    else if (pVal.ItemUID == "Item_18" && !pVal.BeforeAction)
                    {
                        //try
                        //{
                        //    AIFConn.DepSecim.LoadForms();

                        //}
                        //catch (Exception ex)
                        //{

                        //}
                    }
                    else if (pVal.ItemUID == "Item_23" && !pVal.BeforeAction)
                    {
                        try
                        {
                            AIFConn.TplnUrun.LoadForms(oEdtDocEntry.Value.ToString());

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (pVal.ItemUID == "Item_24" && !pVal.BeforeAction)
                    {
                        try
                        {
                            AIFConn.SvkUrun.LoadForms(oEdtDocEntry.Value.ToString());

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (pVal.ItemUID == "Item_27" && !pVal.BeforeAction)
                    {
                        try
                        {
                            var htmlText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\AnatolyaIrsaliye.html");

                            SAPbobsCOM.BusinessPartners oBP = (SAPbobsCOM.BusinessPartners)ConstVariables.oCompanyObject.GetBusinessObject(BoObjectTypes.oBusinessPartners);

                            var ret = oBP.GetByKey(oEdtMusteriKodu.Value);


                            if (ret)
                            {
                                string irsaliyeNo = "";
                                int sonIrsaliyeNo = 0;

                                try
                                {
                                    //ConstVariables.oRecordset.DoQuery("Select \"U_Seri\",\"U_EndNumber\",\"U_Year\" from \"@DON_EINV2\" where \"U_isDefault\" = 'Y' and \"U_InvoiceIndicator\" = 'R' ");

                                    ConstVariables.oRecordset.DoQuery("Select \"U_Seri\",\"U_EndNumber\",\"U_Year\" from \"@DON_EINV2\" where \"U_Seri\" = 'EXP' and \"U_InvoiceIndicator\" = 'R' ");

                                    if (ConstVariables.oRecordset.RecordCount > 0)
                                    {
                                        irsaliyeNo = ConstVariables.oRecordset.Fields.Item("U_Seri").Value.ToString() + ConstVariables.oRecordset.Fields.Item("U_Year").Value.ToString();



                                        sonIrsaliyeNo = ConstVariables.oRecordset.Fields.Item("U_EndNumber").Value.ToString() == "" ? 1 : Convert.ToInt32(ConstVariables.oRecordset.Fields.Item("U_EndNumber").Value);// + 1;


                                        irsaliyeNo += sonIrsaliyeNo.ToString().PadLeft(9, '0');

                                        htmlText = htmlText.Replace("{IrsaliyeNo}", irsaliyeNo);

                                    }
                                }
                                catch (Exception)
                                {

                                }


                                htmlText = htmlText.Replace("{MusteriAdi}", oBP.CardName);
                                htmlText = htmlText.Replace("{MusteriAdresi}", oBP.Address);
                                htmlText = htmlText.Replace("{MusteriVergiDairesi}", oBP.AdditionalID.ToString());
                                htmlText = htmlText.Replace("{MusteriVKN}", oBP.UnifiedFederalTaxID.ToString());
                                htmlText = htmlText.Replace("{Tarih}", DateTime.Now.ToString("dd-MM-yyyy"));
                                htmlText = htmlText.Replace("{Saat}", DateTime.Now.ToString("HH:mm:ss"));
                                htmlText = htmlText.Replace("{Tarih1}", DateTime.Now.ToString("dd-MM-yyyy"));
                                htmlText = htmlText.Replace("{Saat1}", DateTime.Now.ToString("HH:mm:ss"));
                                htmlText = htmlText.Replace("{AracPlaka}", oEdtAracPlaka.Value);
                                htmlText = htmlText.Replace("{SoforAdSoyad}", oEdtSoforAdSoyad.Value);
                                htmlText = htmlText.Replace("{SoforTCKN}", oEdtSoforTCKN.Value);
                                string etttno = Guid.NewGuid().ToString();
                                htmlText = htmlText.Replace("{ETTNNo}", etttno);

                                string satirlar = "";
                                string urunKodu = "";
                                string urunTanimi = "";
                                string urunYTanimi = "";
                                double adet = 0;


                                string sql = "";
                                sql += "SELECT \"UrunKodu\",\"UrunTanimi\",SUM(\"ToplananMiktar\") as \"ToplananMiktar\" FROM  (SELECT T2.\"ItemCode\" AS \"UrunKodu\", T2.\"Dscription\" AS \"UrunTanimi\",,T0.\"FrgnName\" as \"UrunYabanciTanim\", T0.\"U_Miktar\" AS \"ToplananMiktar\", T0.\"U_BelgeNo\" AS \"CekmeNo\", CASE WHEN ISNULL(T0.\"U_Kaynak\", '') = '' THEN (SELECT DISTINCT T5.U_KonteynerNo FROM \"@AIF_WMS_KNTYNR1\" t4 LEFT JOIN \"@AIF_WMS_KNTYNR\" t5 ON t5.DocEntry = T4.DocEntry WHERE t4.\"DocEntry\" = Cast(ISNULL(T0.\"U_KntynrNo\", -1) AS int)) ELSE (SELECT DISTINCT T5.U_KonteynerNo FROM \"@AIF_WMS_KNTYNR1\" t4 LEFT JOIN \"@AIF_WMS_KNTYNR\" t5 ON t5.DocEntry = T4.DocEntry WHERE T4.\"U_Kaynak\" = T0.\"U_Kaynak\" AND(CASE WHEN ISNULL(T4.U_PaletNo, '') = '' THEN cast(T4.U_CekmeNo AS nvarchar) ELSE T4.U_PaletNo END) = (CASE WHEN ISNULL(T0.U_PaletNo, '') = '' THEN cast(T0.U_BelgeNo AS nvarchar) ELSE T0.U_PaletNo END)) END AS \"Konteyner\" FROM \"@AIF_WMS_TOPLANAN\" AS T0 INNER JOIN ORDR AS T1 ON T0.\"U_SiparisNumarasi\" = T1.\"DocEntry\" INNER JOIN RDR1 AS T2 ON T1.\"DocEntry\" = T2.\"DocEntry\" AND T2.\"LineNum\" = T0.\"U_SiparisSatirNo\" INNER JOIN OITM AS T3 ON T2.ItemCode = T3.ItemCode) AS tbl INNER JOIN \"@AIF_WMS_SIPKAR\" Tb2 ON tb2.DocEntry = tbl.\"CekmeNo\" WHERE tbl.CekmeNo = '" + oEdtDocEntry.Value + "' AND tbl.\"Konteyner\" = '" + oComboKonteyner.Value.Trim() + "' group by \"UrunKodu\", \"UrunTanimi\", \"Konteyner\" ";

                                ConstVariables.oRecordset.DoQuery(sql);

                                //for (int i = 1; i <= oMatrix.RowCount; i++)
                                //{
                                //    urunTanimi = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_5").Cells.Item(i).Specific).Value.ToString();
                                //    urunKodu = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_4").Cells.Item(i).Specific).Value.ToString();
                                //    adet = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(i).Specific).Value.ToString() == "" ? 0 : HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(i).Specific).Value.ToString());
                                //    satirlar += "<tr>";
                                //    satirlar += "<td style=\"text-align: center;\">" + i + " </td> ";
                                //    satirlar += "<td class=\"wrap\" style=\"text-align: center;\">" + urunTanimi + "</td>";
                                //    satirlar += "<td class=\"wrap\" style=\"text-align: center;\"><span>" + urunKodu + "</span></td> ";
                                //    satirlar += "<td style=\"text-align: center;\">" + adet + " Adet</td> ";
                                //    satirlar += "</tr>";
                                //}

                                int i = 1;
                                while (!ConstVariables.oRecordset.EoF)
                                {
                                    urunTanimi = ConstVariables.oRecordset.Fields.Item("UrunTanimi").Value.ToString();
                                    urunKodu = ConstVariables.oRecordset.Fields.Item("UrunKodu").Value.ToString();
                                    urunYTanimi = ConstVariables.oRecordset.Fields.Item("UrunYTanim").Value.ToString();
                                    adet = HelperClass.parseNumber_Seperator.ConvertToDouble(ConstVariables.oRecordset.Fields.Item("ToplananMiktar").Value.ToString());
                                    satirlar += "<tr>";
                                    satirlar += "<td style=\"text-align: center;\">" + i + " </td> ";
                                    satirlar += "<td class=\"wrap\" style=\"text-align: center;\">" + urunTanimi + "</td>";
                                    satirlar += "<td class=\"wrap\" style=\"text-align: center;\"><span>" + urunKodu + "</span></td> ";
                                    satirlar += "<td style=\"text-align: center;\">" + adet + " Adet</td> ";
                                    satirlar += "</tr>";
                                    i++;
                                    ConstVariables.oRecordset.MoveNext();
                                }




                                htmlText = htmlText.Replace("{Satirlar}", satirlar);


                                string final = barkodGoster(etttno);

                                htmlText = htmlText.Replace("{Ettnbase64}", final);

                                File.WriteAllText(System.IO.Path.GetTempPath() + "\\AnatolyaIrsaliye-" + etttno + ".html", htmlText, Encoding.UTF8);

                                Process.Start(System.IO.Path.GetTempPath() + "\\AnatolyaIrsaliye-" + etttno + ".html");
                            }

                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                case BoEventTypes.et_DOUBLE_CLICK:
                    break;
                case BoEventTypes.et_MATRIX_LINK_PRESSED:
                    break;
                case BoEventTypes.et_MATRIX_COLLAPSE_PRESSED:
                    break;
                case BoEventTypes.et_VALIDATE:
                    if (pVal.ColUID == "Col_9" && pVal.ItemUID == "Item_9" && pVal.ItemChanged && !pVal.BeforeAction)
                    {
                        if (girdi)
                        {
                            girdi = false;
                            return false;
                        }
                        try
                        {
                            var toplanan = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_13").Cells.Item(pVal.Row).Specific).Value.ToString());
                            var planlanan = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value.ToString());

                            if (planlanan < toplanan)
                            {
                                Handler.SAPApplication.MessageBox("Planlanan Miktar Toplanan Miktardan Düşük Olamaz.");
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = HelperClass.parseNumber_Seperator.setDoubleVal(toplanan.ToString()).ToString();
                            }
                        }
                        catch (Exception)
                        {

                        }

                        girdi = true;
                        var depoStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_11").Cells.Item(pVal.Row).Specific).Value.ToString());
                        var planlananMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value.ToString());
                        var genelStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_12").Cells.Item(pVal.Row).Specific).Value.ToString());
                        var acikMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_8").Cells.Item(pVal.Row).Specific).Value.ToString());
                        var urunKodu = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_4").Cells.Item(pVal.Row).Specific).Value.ToString();


                        if (planlananMiktar == 0)
                        {
                            return false;
                        }
                        if (genelStokMiktari >= planlananMiktar)
                        {
                            if (planlananMiktar > depoStokMiktari)
                            {
                                Handler.SAPApplication.MessageBox("İlgili Ürün Sevkiyat Deposunda Bulunmamaktadır. Lütfen Diğer Depoları Kontrol Ediniz.");

                                BubbleEvent = false;
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = "0";
                                return false;
                            }
                        }
                        else
                        {
                            if (planlananMiktar > depoStokMiktari)
                            {
                                Handler.SAPApplication.MessageBox("Planlanan Miktar Depo Stok Miktarından Büyük Olamaz. Maksimum Miktar Otomatik Olarak Atanmıştır.");

                                BubbleEvent = false;
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = HelperClass.parseNumber_Seperator.setDoubleVal(depoStokMiktari.ToString()).ToString();
                                return false;
                            }
                        }

                        string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                        var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                    where (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_4" select new XElement(y.Element("Value"))).First().Value == urunKodu
                                    select new _SiparisKarsilama
                                    {
                                        planlananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                    }).ToList();

                        if (rows.Count > 0)
                        {
                            if (rows.Sum(x => x.planlananSiparisMiktari) > depoStokMiktari)
                            {
                                Handler.SAPApplication.MessageBox("Belge Üzerinde Seçilen İlgili Ürün Miktarı Sevkiyat Depo Miktarından Büyük Girilemez.");
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = "0";
                                return false;
                            }
                        }


                        double YuzdeOnFazla = acikMiktar + ((acikMiktar * 10) / 100);
                        double YuzdeOnAz = acikMiktar - ((acikMiktar * 10) / 100);

                        if (planlananMiktar > YuzdeOnFazla)
                        {
                            var resp = Handler.SAPApplication.MessageBox("Açık Miktardan %10 Miktar Fazlasını Girdiniz. Devam Etmek İstiyor Musunuz?", 1, "Evet", "Hayır");
                            if (resp == 2)
                            {
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = "0";
                            }
                        }

                        if (planlananMiktar < YuzdeOnAz)
                        {
                            var resp = Handler.SAPApplication.MessageBox("Açık Miktardan %10 Az Miktarını Girdiniz. Devam Etmek İstiyor Musunuz?", 1, "Evet", "Hayır");
                            if (resp == 2)
                            {
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = "0";
                            }
                        }

                    }
                    else if (pVal.ColUID == "Col_20" && pVal.ItemUID == "Item_9" && pVal.ItemChanged && !pVal.BeforeAction)
                    {
                        try
                        {
                            double toplamSatirTutari = 0;
                            double birimFiyat = 0;
                            double hesaplananMik = 0;

                            hesaplananMik = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_20").Cells.Item(pVal.Row).Specific).Value.ToString());

                            birimFiyat = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_19").Cells.Item(pVal.Row).Specific).Value.ToString());


                            toplamSatirTutari = birimFiyat * hesaplananMik;


                            try
                            {
                                frmSiparisKarsilama.Freeze(true);
                                oMatrix.Columns.Item("Col_21").Editable = true;
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_21").Cells.Item(pVal.Row).Specific).Value = HelperClass.parseNumber_Seperator.setDoubleVal(toplamSatirTutari.ToString()).ToString();
                                oMatrix.Columns.Item("Col_21").Editable = false;
                            }
                            catch (Exception)
                            {

                            }

                            finally
                            {
                                oMatrix.AutoResizeColumns();
                                frmSiparisKarsilama.Freeze(false);

                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                case BoEventTypes.et_MATRIX_LOAD:
                    break;
                case BoEventTypes.et_DATASOURCE_LOAD:
                    break;
                case BoEventTypes.et_FORM_LOAD:
                    break;
                case BoEventTypes.et_FORM_UNLOAD:
                    break;
                case BoEventTypes.et_FORM_ACTIVATE:
                    break;
                case BoEventTypes.et_FORM_DEACTIVATE:
                    break;
                case BoEventTypes.et_FORM_CLOSE:
                    break;
                case BoEventTypes.et_FORM_RESIZE:
                    break;
                case BoEventTypes.et_FORM_KEY_DOWN:
                    break;
                case BoEventTypes.et_FORM_MENU_HILIGHT:
                    break;
                case BoEventTypes.et_PRINT:
                    break;
                case BoEventTypes.et_PRINT_DATA:
                    break;
                case BoEventTypes.et_EDIT_REPORT:
                    break;
                case BoEventTypes.et_CHOOSE_FROM_LIST:
                    if (pVal.ItemUID == "Item_1" && pVal.BeforeAction)
                    {
                        SAPbouiCOM.IChooseFromListEvent oCFLEvento = default(SAPbouiCOM.IChooseFromListEvent);
                        oCFLEvento = (SAPbouiCOM.IChooseFromListEvent)pVal;

                        SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
                        oCFL = frmSiparisKarsilama.ChooseFromLists.Item(oCFLEvento.ChooseFromListUID);

                        SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);
                        SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                        SAPbouiCOM.Conditions oEmptyConts = new SAPbouiCOM.Conditions();

                        oCFL.SetConditions(oEmptyConts);
                        oCons = oCFL.GetConditions();

                        oCon = oCons.Add();
                        oCon.Alias = "validFor";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        oCon.CondVal = "Y";

                        oCFL.SetConditions(oCons);
                    }
                    else if (pVal.ItemUID == "Item_1" && !pVal.BeforeAction)
                    {
                        try
                        {
                            SAPbouiCOM.DataTable oDataTable = ((SAPbouiCOM.ChooseFromListEvent)pVal).SelectedObjects;
                            string Val = "";
                            Val = oDataTable.GetValue("CardCode", 0).ToString();

                            try
                            {
                                oEdtMusteriKodu.Value = Val;
                            }
                            catch (Exception)
                            {
                            }

                            Val = oDataTable.GetValue("CardName", 0).ToString();

                            try
                            {
                                oEdtMusteriAdi.Value = Val;
                            }
                            catch (Exception)
                            {
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;
                case BoEventTypes.et_RIGHT_CLICK:
                    break;
                case BoEventTypes.et_MENU_CLICK:
                    break;
                case BoEventTypes.et_FORM_DATA_ADD:
                    break;
                case BoEventTypes.et_FORM_DATA_UPDATE:
                    break;
                case BoEventTypes.et_FORM_DATA_DELETE:
                    break;
                case BoEventTypes.et_FORM_DATA_LOAD:
                    break;
                case BoEventTypes.et_PICKER_CLICKED:
                    break;
                case BoEventTypes.et_GRID_SORT:
                    break;
                case BoEventTypes.et_Drag:
                    break;
                case BoEventTypes.et_FORM_DRAW:
                    break;
                case BoEventTypes.et_UDO_FORM_BUILD:
                    break;
                case BoEventTypes.et_UDO_FORM_OPEN:
                    break;
                case BoEventTypes.et_B1I_SERVICE_COMPLETE:
                    break;
                case BoEventTypes.et_FORMAT_SEARCH_COMPLETED:
                    break;
                case BoEventTypes.et_PRINT_LAYOUT_KEY:
                    break;
                case BoEventTypes.et_FORM_VISIBLE:
                    break;
                case BoEventTypes.et_ITEM_WEBMESSAGE:
                    break;
                default:
                    break;
            }
            return BubbleEvent;
        }

        bool girdi = false;
        public void MenuEvent(ref MenuEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;
            if (pVal.MenuUID == "1282" && !pVal.BeforeAction)
            {
                obtnOnayla.Item.Enabled = true;
                oComboOnayDurumu.Select("T", BoSearchKey.psk_ByValue);
                oBtnEkleGuncelle.Item.Enabled = true;
                frmSiparisKarsilama.DataSources.UserDataSources.Item("UD_2").ValueEx = "N";
            }
            else if (pVal.MenuUID == "1281" && !pVal.BeforeAction)
            {
                try
                {
                    oEdtDocEntry.Item.Enabled = true;
                }
                catch (Exception)
                {
                }
                try
                {
                    oBtnEkleGuncelle.Item.Enabled = true;

                    frmSiparisKarsilama.DataSources.UserDataSources.Item("UD_2").ValueEx = "N";
                }
                catch (Exception)
                {
                }
            }
        }

        private bool comboseciliyor = false;

        public void RightClickEvent(ref ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }

        List<int> kapananEskiSatirlar = new List<int>();
        private void SatirEditKapat(List<int> _list)
        {
            string rowxml = @"<CommonSetting><Rows>{0}</Rows></CommonSetting>";

            string rowtemplate = "";
            rowtemplate = @"<Row rowNum=""{0}"" backColor =""-1"" editable=""false""><Cells /></Row>";


            //else
            //{
            //    rowtemplate = @"<Row rowNum=""{0}"" backColor =""-1"" editable=""true""><Cells /></Row>";
            //}

            string data = "";
            string final = "";

            if (_list.Count > 0)
            {
                data = string.Join("", _list.Select(s => string.Format(rowtemplate, s.ToString())));

                final = string.Format(rowxml, data);

                oMatrix.CommonSetting.UpdateFromXML(final);

                kapananEskiSatirlar = _list;
            }

            if (_list.Count == 0)
            {
                rowtemplate = @"<Row rowNum=""{0}"" backColor =""-1"" editable=""true""><Cells /></Row>";

                data = string.Join("", _list.Select(s => string.Format(rowtemplate, s.ToString())));

                final = string.Format(rowxml, data);
                oMatrix.CommonSetting.UpdateFromXML(final);
            }
        }

        private void listele(bool sifirMiktarlariGoster)
        {
            string condition = ConstVariables.oCompanyObject.DbServerType == BoDataServerTypes.dst_HANADB ? "IFNULL" : "ISNULL";

            string sql = "Select * from (Select T0.\"DocEntry\" as \"SiparisNumarasi\",T0.\"DocDate\" as \"SiparisTarihi\",T0.\"DocDueDate\" as \"TeslimatTarihi\",T1.\"LineNum\" as \"SiparisSatirNo\",T1.\"ItemCode\" as \"UrunKodu\",T1.\"Dscription\" as \"UrunTanimi\",T1.\"Quantity\" as \"ToplamSiparisMiktari\",(T1.\"Quantity\" -  T1.\"OpenQty\") as \"SevkSiparisMiktari\",T1.\"OpenQty\" - " + condition + "((Select SUM(T98.\"U_PlanSipMik\") from \"@AIF_WMS_SIPKAR1\" as T98 where T98.\"U_SiparisNumarasi\" = T0.\"DocEntry\" and T98.\"U_SipSatirNo\" = T1.\"LineNum\"),0) as \"AcikSiparisMiktari\", 0 as \"PlanlananSiparisMiktari\",T1.\"WhsCode\" as \"SiparisDepoKodu\", (Select SUM(T99.\"OnHand\") from OITW as T99 where T99.\"ItemCode\" = T1.\"ItemCode\" and T99.\"WhsCode\" = T1.\"WhsCode\") as \"DepoStokMiktari\", (Select SUM(T99.\"OnHand\") from OITW as  T99 where T99.\"ItemCode\" = T1.\"ItemCode\" and T99.\"WhsCode\" != T1.\"WhsCode\") as \"GenelStokMiktari\" from ORDR as T0 INNER JOIN RDR1 AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"CardCode\" = '" + oEdtMusteriKodu.Value + "' and T1.\"LineStatus\" = 'O' ";

            if (oEdtSiparisTarihi.Value != "")
            {
                sql += " and T0.\"DocDueDate\" <='" + oEdtSiparisTarihi.Value + "'";
            }


            if (oEdtSiparisNo.Value != "")
            {
                sql += " and T0.\"DocEntry\" <='" + oEdtSiparisNo.Value + '"';
            }

            sql += " ) as tbl where tbl.\"AcikSiparisMiktari\">0 ";

            if (!sifirMiktarlariGoster)
            {
                sql += " and (tbl.\"DepoStokMiktari\">0 or tbl.\"GenelStokMiktari\">0)";

            }
            ConstVariables.oRecordset.DoQuery(sql);

            string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
            XDocument xDoc = XDocument.Parse(xmll);
            XNamespace ns = "http://www.sap.com/SBO/SDK/DI";
            var rows = (from t in xDoc.Descendants(ns + "Row")
                        select new _SiparisKarsilama()
                        {
                            siparisNumarasi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisNumarasi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            siparisTarihi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisTarihi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            teslimatTarihi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "TeslimatTarihi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            siparisSatirNo = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisSatirNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            urunKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "UrunKodu" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            urunTanimi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "UrunTanimi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString().Replace("&", "-"),
                            toplamSatisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "ToplamSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            sevksiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SevkSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            acikSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "AcikSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            planlananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "PlanlananSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            siparisDepoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisDepoKodu" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            depoStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "DepoStokMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            genelStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "GenelStokMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            siraNo = t.ElementsBeforeSelf().Count() + 1
                        }).ToList();

            rows.RemoveAll(x => x.siparisNumarasi == "0");



            string data2 = string.Join("", rows.Select(s => string.Format(row, s.siparisNumarasi, s.siparisTarihi, s.teslimatTarihi, s.siparisSatirNo, s.urunKodu, s.urunTanimi, HelperClass.parseNumber_Seperator.setDoubleVal(s.toplamSatisMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.sevksiparisMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.acikSiparisMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.planlananSiparisMiktari.ToString()).ToString(), s.siparisDepoKodu, HelperClass.parseNumber_Seperator.setDoubleVal(s.depoStokMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.genelStokMiktari.ToString()).ToString(), s.siraNo)));

            frmSiparisKarsilama.DataSources.DBDataSources.Item("@AIF_WMS_SIPKAR1").LoadFromXML(string.Format(header, data2));

            oMatrix.AutoResizeColumns();
        }

        public void UrunEkle(List<_SiparisKarsilama> _SiparisKarsilamas)
        {

            string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
            var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                        select new _SiparisKarsilama
                        {
                            siraNo = 0,
                            siparisNumarasi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                            siparisTarihi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value.Contains("/") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value.Contains(".") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : "",
                            teslimatTarihi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value.Contains("/") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value.Contains(".") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : "",
                            siparisSatirNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value,
                            urunKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_4" select new XElement(y.Element("Value"))).First().Value,
                            urunTanimi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_5" select new XElement(y.Element("Value"))).First().Value,
                            urunYTanimi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_23" select new XElement(y.Element("Value"))).First().Value,
                            toplamSatisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_6" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            sevksiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_7" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            acikSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_8" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            planlananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            siparisDepoKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_10" select new XElement(y.Element("Value"))).First().Value,
                            depoStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_11" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            genelStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_12" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            paletNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_15" select new XElement(y.Element("Value"))).First().Value,
                            toplananMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_13" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            muhatapReferansNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_17" select new XElement(y.Element("Value"))).First().Value,
                            muhatapKatalogNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_18" select new XElement(y.Element("Value"))).First().Value,
                            birimFiyat = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_19" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            hesaplananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_20" select new XElement(y.Element("Value"))).First().Value.ToString()),
                            toplamSatirTutari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_21" select new XElement(y.Element("Value"))).First().Value),
                        }).ToList();

            foreach (var item in _SiparisKarsilamas)
            {
                if (rows.Where(x => x.siparisNumarasi == item.siparisNumarasi && x.siparisSatirNo == item.siparisSatirNo).Count() > 0)
                {
                    rows.Where(x => x.siparisNumarasi == item.siparisNumarasi && x.siparisSatirNo == item.siparisSatirNo).ToList().ForEach(y => y.planlananSiparisMiktari = y.planlananSiparisMiktari + item.planlananSiparisMiktari);
                    rows.Where(x => x.siparisNumarasi == item.siparisNumarasi && x.siparisSatirNo == item.siparisSatirNo).ToList().ForEach(y => y.hesaplananSiparisMiktari = y.hesaplananSiparisMiktari + item.hesaplananSiparisMiktari);
                }
                else
                {
                    rows.AddRange(_SiparisKarsilamas.Where(x => x.siparisNumarasi == item.siparisNumarasi && x.siparisSatirNo == item.siparisSatirNo).ToList());
                }
            }



            rows.RemoveAll(x => x.siparisNumarasi == "0");

            rows.ToList().ForEach(x => x.Gorunen = "Y");

            rows.Where(x => x.planlananSiparisMiktari - x.toplananMiktar == 0).ToList().ForEach(y => y.Gorunen = "N");

            //rows.ToList().ForEach(y => y.siraNo = y.siraNo + 1);


            string data2 = string.Join("", rows.Select(s => string.Format(row, s.siparisNumarasi, s.siparisTarihi, s.teslimatTarihi, s.siparisSatirNo, s.urunKodu, s.urunTanimi,s.urunYTanimi, HelperClass.parseNumber_Seperator.setDoubleVal(s.toplamSatisMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.sevksiparisMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.acikSiparisMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.planlananSiparisMiktari.ToString()).ToString(), s.siparisDepoKodu, HelperClass.parseNumber_Seperator.setDoubleVal(s.depoStokMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.genelStokMiktari.ToString()).ToString(), "", s.paletNo, HelperClass.parseNumber_Seperator.setDoubleVal(s.toplananMiktar.ToString()).ToString(), s.muhatapReferansNo, s.muhatapKatalogNo, HelperClass.parseNumber_Seperator.setDoubleVal(s.birimFiyat.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal(s.hesaplananSiparisMiktari.ToString()).ToString(), HelperClass.parseNumber_Seperator.setDoubleVal((s.birimFiyat * s.hesaplananSiparisMiktari).ToString()), s.Gorunen)));

            frmSiparisKarsilama.DataSources.DBDataSources.Item("@AIF_WMS_SIPKAR1").LoadFromXML(string.Format(header, data2));

            oMatrix.AutoResizeColumns();

            //if (frmSiparisKarsilama.Mode != BoFormMode.fm_ADD_MODE)
            //{
            //    frmSiparisKarsilama.Mode = BoFormMode.fm_UPDATE_MODE;
            //}

            try
            {
                if (oMatrix.RowCount > 0)
                {
                    oMatrix.Columns.Item("Col_21").Editable = true;
                    oMatrix.Columns.Item("Col_21").Cells.Item(1).Click();
                    Handler.SAPApplication.SendKeys("^{TAB}");
                    //oMatrix.Columns.Item("Col_21").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
                    oMatrix.AutoResizeColumns();
                    oMatrix.Columns.Item("Col_21").Editable = false;
                }
            }
            catch (Exception)
            {
            }

        }

        public void modDegistir()
        {
            if (frmSiparisKarsilama.Mode != BoFormMode.fm_ADD_MODE)
            {
                frmSiparisKarsilama.Mode = BoFormMode.fm_UPDATE_MODE;
            }
        }

        public string barkodGoster(string barkodText)
        {
            StreamReader barcode = new StreamReader(ServiceControllerGET("https://econnect.hizliteknoloji.com.tr/HizliApi/EConnectApi/GetBarcodeString?Text=" + barkodText).GetResponseStream());
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseMessageEIrsaliye>(barcode.ReadToEnd()); barcode.Close();
            string qrcode = model.dataSet.Tables[0].Rows[0]["QrCode"].ToString();
            return qrcode;
        }
        public class ResponseMessageEIrsaliye
        {
            public bool IsSucceeded { get; set; }
            public string MessageCode { get; set; }
            public string Message { get; set; }
            public DataSet dataSet { get; set; }
        }
        public static HttpWebResponse ServiceControllerGET(string URL)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Headers.Add("Username", "hizli");
            request.Headers.Add("Password", "rWBDkoA6");
            request.Headers.Add("Entegrator", "4"); //sabit
            request.Timeout = 600000;//10dk 1000 = 1sn 600 sn = 10dk
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
    }
}
