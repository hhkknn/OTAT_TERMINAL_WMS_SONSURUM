using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Abstarct;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib;
using AIF.ObjectsDLL.Utils;
using AIF.WMS.Models;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Handler = AIF.ObjectsDLL.Events.Handler;

namespace AIF.WMS.ClassLayer
{
    public class SevkEdilenUrunler
    {
        [ItemAtt(AIFConn.SevkEdilenUID)]
        public SAPbouiCOM.Form frmSevkEdilen;

        [ItemAtt("Item_2")]
        public SAPbouiCOM.Matrix oMatrix;

        SAPbouiCOM.DataTable oDataTable = null;

        public void LoadForms(string _docEntry)
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.SevkEdilenUrunlerXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.SevkEdilenUrunlerXML));
            Functions.CreateUserOrSystemFormComponent<SevkEdilenUrunler>(AIFConn.SvkUrun);

            docEntry = _docEntry;
            InitForms();
        }

        string docEntry = "";
        public void InitForms()
        {
            try
            {
                frmSevkEdilen.Freeze(true);

                oDataTable = frmSevkEdilen.DataSources.DataTables.Add("DATA");

                string condition = ConstVariables.oCompanyObject.DbServerType == BoDataServerTypes.dst_HANADB ? "IFNULL" : "ISNULL"; 

                string sql = "";

                //sql = "select * from (Select T0.\"U_SiparisNumarasi\" as \"SiparisNumarasi\",T1.\"DocDate\" as \"SiparisTarihi\",T1.\"DocDueDate\" as \"TeslimatTarihi\",T0.\"U_SiparisSatirNo\" as \"SiparisSatirNo\",T2.\"ItemCode\" as \"UrunKodu\",T2.\"Dscription\" as \"UrunTanimi\",T2.\"Quantity\" as \"ToplamSiparisMiktari\", (T2.\"Quantity\" - T2.\"OpenQty\") as \"SevkSipMiktari\",(T2.\"OpenQty\" - T0.\"U_Miktar\") AS \"AcikSiparisMiktari\",T2.\"WhsCode\" as \"SiparisDepoKodu\",T0.\"U_Miktar\" as \"ToplananMiktar\",T0.\"U_PaletNo\" as \"PaletNo\",(T2.\"OpenQty\" - ISNULL(T0.\"U_Miktar\", 0)) as \"PlanlananSiparisMiktari\", (SELECT Count(\"DocEntry\") FROM \"@AIF_WMS_KNTYNR1\" AS T98  WHERE T98.\"U_SiparisNo\" = T0.\"U_SiparisNumarasi\" and T98.\"U_SipSatirNo\" = T0.\"U_SiparisSatirNo\") AS \"KonteynerVarmi\",T0.\"U_TeslimatNo\" as \"TeslimatNo\", T0.\"DocEntry\" AS \"ToplananDocEntry\",T1.\"NumAtCard\" as \"MuhatapReferansNo\",(Select TOP 1 " + condition + "(\"Substitute\",'')  from OSCN as T77 where T77.\"CardCode\" = T1.\"CardCode\" and T77.\"ItemCode\" = T2.\"ItemCode\" and T77.\"IsDefault\" = 'Y') as \"MuhatapKatalogNo\" from \"@AIF_WMS_TOPLANAN\" as T0 INNER JOIN ORDR AS T1 ON T0.\"U_SiparisNumarasi\" = T1.\"DocEntry\" INNER JOIN RDR1 AS T2 ON T1.\"DocEntry\" = T2.\"DocEntry\" and T2.\"LineNum\" = T0.\"U_SiparisSatirNo\" WHERE T0.\"U_BelgeNo\" = '" + docEntry + "' ) as tbl WHERE tbl.\"KonteynerVarmi\" > 0";


                #region cekme no gelmiyorsa paletten tamamlandı
                //sql = "  select * from (Select T0.\"U_SiparisNumarasi\" as \"SiparisNumarasi\",T1.\"DocDate\" as \"SiparisTarihi\",T1.\"DocDueDate\" as \"TeslimatTarihi\",T0.\"U_SiparisSatirNo\" as \"SiparisSatirNo\",T2.\"ItemCode\" as \"UrunKodu\",T2.\"Dscription\" as \"UrunTanimi\",T2.\"Quantity\" as \"ToplamSiparisMiktari\", (T2.\"Quantity\" - T2.\"OpenQty\") as \"SevkSipMiktari\", (T2.\"OpenQty\" - T0.\"U_Miktar\") AS \"AcikSiparisMiktari\",T2.\"WhsCode\" as \"SiparisDepoKodu\",T0.\"U_Miktar\" as \"ToplananMiktar\",T0.\"U_PaletNo\" as \"PaletNo\",(T2.\"OpenQty\" - ISNULL(T0.\"U_Miktar\", 0)) as \"PlanlananSiparisMiktari\", case when T0.\"U_BelgeNo\" = 0 then(select \"U_CekmeNo\" from \"@AIF_WMS_PALET1\" where \"U_PaletNo\" = T0.\"U_PaletNo\") else T0.\"U_BelgeNo\" end as \"CekmeNo\",T0.\"U_TeslimatNo\" as \"TeslimatNo\",T0.\"DocEntry\" AS \"ToplananDocEntry\",T1.\"NumAtCard\" as \"MuhatapReferansNo\",(Select TOP 1 " + condition + "(\"Substitute\",'')  from OSCN as T77 where T77.\"CardCode\" = T1.\"CardCode\" and T77.\"ItemCode\" = T2.\"ItemCode\" and T77.\"IsDefault\" = 'Y') as \"MuhatapKatalogNo\" from \"@AIF_WMS_TOPLANAN\" as T0 INNER JOIN ORDR AS T1 ON T0.\"U_SiparisNumarasi\" = T1.\"DocEntry\" INNER JOIN RDR1 AS T2 ON T1.\"DocEntry\" = T2.\"DocEntry\" and T2.\"LineNum\" = T0.\"U_SiparisSatirNo\" WHERE T0.\"U_BelgeNo\" = '" + docEntry + "' ) as tbl ";

                //sql += "WHERE CONCAT(tbl.\"CekmeNo\",tbl.\"SiparisNumarasi\",\"SiparisSatirNo\") in ((SELECT CONCAT(case when T99.\"U_CekmeNo\" = 0 then(select top 1 \"U_CekmeNo\" from \"@AIF_WMS_PALET1\" where \"U_PaletNo\" = T99.\"U_PaletNo\")  else T99.\"U_CekmeNo\" end ,\"U_SiparisNo\",\"U_SipSatirNo\") FROM \"@AIF_WMS_KNTYNR1\" AS T99)) ";
                #endregion

                #region 24.03.2022
                sql = "SELECT \"SiparisNumarasi\",\"SiparisTarihi\",\"TeslimatTarihi\",\"UrunKodu\",\"SiparisSatirNo\",\"UrunTanimi\",\"ToplamSiparisMiktari\",\"SevkSipMiktari\", \"AcikSiparisMiktari\",\"SiparisDepoKodu\",\"ToplananMiktar\",\"PaletNo\",\"PlanlananSiparisMiktari\", \"CekmeNo\",\"U_TeslimatNo\" as \"TeslimatNo\",\"ToplananDocEntry\",\"MuhatapReferansNo\",\"MuhatapKatalogNo\",\"FrgnName\" as \"YabanciAd\",\"Konteyner\" FROM ";
                sql += " (SELECT T0.\"U_SiparisNumarasi\" AS \"SiparisNumarasi\", T1.\"DocDate\" AS \"SiparisTarihi\", T1.\"DocDueDate\" AS \"TeslimatTarihi\", T0.\"U_SiparisSatirNo\" AS \"SiparisSatirNo\", T2.\"ItemCode\" AS \"UrunKodu\", T2.\"Dscription\" AS \"UrunTanimi\", T2.\"Quantity\" AS \"ToplamSiparisMiktari\", (T2.\"Quantity\" - T2.\"OpenQty\") AS \"SevkSipMiktari\", (T2.\"OpenQty\" - T0.\"U_Miktar\") AS \"AcikSiparisMiktari\", T2.\"WhsCode\" AS \"SiparisDepoKodu\", T0.\"U_Miktar\" AS \"ToplananMiktar\", T0.\"U_PaletNo\" AS \"PaletNo\", (T2.\"OpenQty\" - ISNULL(T0.\"U_Miktar\", 0)) AS \"PlanlananSiparisMiktari\", T0.\"U_BelgeNo\" AS \"CekmeNo\", T0.\"U_TeslimatNo\", T0.\"DocEntry\" AS \"ToplananDocEntry\", T1.\"NumAtCard\" AS \"MuhatapReferansNo\", ";
                sql += "(SELECT TOP 1 ISNULL(\"Substitute\", '') FROM OSCN AS T77 ";
                sql += "WHERE T77.\"CardCode\" = T1.\"CardCode\" AND T77.\"ItemCode\" = T2.\"ItemCode\" AND T77.\"IsDefault\" = 'Y' ) AS \"MuhatapKatalogNo\",T3.FrgnName, T2.Price, t1.NumAtCard, T1.CardCode, t1.CardName, CASE WHEN ISNULL(T0.\"U_Kaynak\",'') = '' THEN (SELECT distinct T5.U_KonteynerNo FROM \"@AIF_WMS_KNTYNR1\" t4 ";
                sql += "LEFT JOIN \"@AIF_WMS_KNTYNR\" t5 ON t5.DocEntry = T4.DocEntry ";
                sql += "where  t4.\"DocEntry\"=Cast(ISNULL(T0.\"U_KntynrNo\",-1) as int)) ELSE  (SELECT distinct T5.U_KonteynerNo FROM \"@AIF_WMS_KNTYNR1\" t4 ";
                sql += "LEFT JOIN \"@AIF_WMS_KNTYNR\" t5 ON t5.DocEntry = T4.DocEntry ";
                sql += "where  T4.\"U_Kaynak\" = T0.\"U_Kaynak\" AND (case when ISNULL(T4.U_PaletNo, '') = '' then cast(T4.U_CekmeNo as nvarchar) else T4.U_PaletNo end) = (case when ISNULL(T0.U_PaletNo,'')= '' then cast(T0.U_BelgeNo as nvarchar) else T0.U_PaletNo end) ) END as \"Konteyner\",T0.\"U_Kaynak\" as \"Kaynak\" FROM \"@AIF_WMS_TOPLANAN\" AS T0 ";
                sql += "INNER JOIN ORDR AS T1 ON T0.\"U_SiparisNumarasi\" = T1.\"DocEntry\" INNER JOIN RDR1 AS T2 ON T1.\"DocEntry\" = T2.\"DocEntry\" AND T2.\"LineNum\" = T0.\"U_SiparisSatirNo\" INNER JOIN OITM AS T3 ON T2.ItemCode = T3.ItemCode ) AS tbl ";
                sql += "inner JOIN \"@AIF_WMS_SIPKAR\" Tb2 ON tb2.DocEntry = tbl.\"CekmeNo\" ";
                sql += "WHERE Concat(CASE WHEN ISNULL(tbl.PaletNo,'') = '' THEN cast(tbl.CekmeNo AS nvarchar) ELSE ISNULL(tbl.PaletNo,'') END,  ISNULL(tbl.Kaynak, CONCAT(tbl.SiparisNumarasi, tbl.SiparisSatirNo))) IN((SELECT DISTINCT Concat(CASE WHEN ISNULL(U_PaletNo,'') = '' THEN cast(U_CekmeNo AS nvarchar) ELSE ISNULL(U_PaletNo,'') END, ISNULL(T99.U_Kaynak, CONCAT(T99.U_SiparisNo, T99.U_SipSatirNo))) FROM \"@AIF_WMS_KNTYNR1\" AS T99)) and tbl.CekmeNo = '" + docEntry + "' ";
                sql += " ORDER BY tbl.CekmeNo,  tb2.U_Aciklama";
                #endregion

                oDataTable.Clear();
                oDataTable.ExecuteQuery(sql);

                oMatrix.Clear();
                oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "SiparisNumarasi");
                oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "SiparisTarihi");
                oMatrix.Columns.Item("Col_2").DataBind.Bind("DATA", "TeslimatTarihi");
                oMatrix.Columns.Item("Col_3").DataBind.Bind("DATA", "SiparisSatirNo");
                oMatrix.Columns.Item("Col_4").DataBind.Bind("DATA", "UrunKodu");
                oMatrix.Columns.Item("Col_5").DataBind.Bind("DATA", "UrunTanimi");
                oMatrix.Columns.Item("Col_6").DataBind.Bind("DATA", "ToplamSiparisMiktari");
                oMatrix.Columns.Item("Col_7").DataBind.Bind("DATA", "SevkSipMiktari");
                oMatrix.Columns.Item("Col_8").DataBind.Bind("DATA", "AcikSiparisMiktari");
                oMatrix.Columns.Item("Col_9").DataBind.Bind("DATA", "PlanlananSiparisMiktari");
                oMatrix.Columns.Item("Col_10").DataBind.Bind("DATA", "SiparisDepoKodu");
                oMatrix.Columns.Item("Col_13").DataBind.Bind("DATA", "ToplananMiktar");
                oMatrix.Columns.Item("Col_15").DataBind.Bind("DATA", "PaletNo");
                oMatrix.Columns.Item("Col_11").DataBind.Bind("DATA", "TeslimatNo");
                oMatrix.Columns.Item("Col_12").DataBind.Bind("DATA", "ToplananDocEntry");
                oMatrix.Columns.Item("Col_16").DataBind.Bind("DATA", "MuhatapReferansNo");
                oMatrix.Columns.Item("Col_17").DataBind.Bind("DATA", "MuhatapKatalogNo");
                oMatrix.Columns.Item("Col_18").DataBind.Bind("DATA", "YabanciAd");
                oMatrix.Columns.Item("Col_19").DataBind.Bind("DATA", "Konteyner");


                oMatrix.LoadFromDataSource();
                oMatrix.AutoResizeColumns();
                oMatrix.Item.AffectsFormMode = false;

            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox(ex.Message);
            }
            finally
            {
                frmSevkEdilen.Freeze(false);
            }
        }

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

        public bool SAP_ItemEvent(string FormUID, ref ItemEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;

            switch (pVal.EventType)
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
                    if (!pVal.BeforeAction && pVal.ItemUID == "Item_4")
                    {
                        string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                        var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                    where (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_14" select new XElement(y.Element("Value"))).First().Value == "Y"
                                    select new _SiparisKarsilama
                                    {
                                        siparisNumarasi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                                        siparisSatirNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value,
                                        siparisDepoKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_10" select new XElement(y.Element("Value"))).First().Value,
                                        toplananMiktar = HelperClass.parseNumber.parservalues<double>((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_13" select new XElement(y.Element("Value"))).First().Value),
                                    }).ToList();

                        var result = rows.GroupBy(l => new { l.siparisNumarasi, l.siparisSatirNo }).Select(y => new _SiparisKarsilama { siparisNumarasi = y.First().siparisNumarasi, siparisSatirNo = y.First().siparisSatirNo, toplananMiktar = y.Sum(c => c.toplananMiktar) });

                        SAPbobsCOM.Documents oDocuments = (SAPbobsCOM.Documents)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

                        oDocuments.DocObjectCode = SAPbobsCOM.BoObjectTypes.oDeliveryNotes;

                        oDocuments.DocDate = DateTime.Now;
                        oDocuments.TaxDate = DateTime.Now;
                        oDocuments.DocDueDate = DateTime.Now;
                        bool satirvar = false;
                        List<_SiparisKarsilama> ilgiliSatirlar = new List<_SiparisKarsilama>();

                        foreach (var item in result.Where(x => x.siparisNumarasi != ""))
                        {
                            satirvar = true;
                            ConstVariables.oRecordset.DoQuery("Select TOP 1 \"CardCode\" from ORDR where \"DocEntry\" = '" + item.siparisNumarasi + "'");

                            oDocuments.CardCode = ConstVariables.oRecordset.Fields.Item(0).Value.ToString();

                            if (item.siparisNumarasi != "" && item.siparisSatirNo != "")
                            {
                                oDocuments.Lines.BaseEntry = Convert.ToInt32(item.siparisNumarasi);
                                oDocuments.Lines.BaseLine = item.siparisSatirNo == "" ? 0 : Convert.ToInt32(item.siparisSatirNo);
                                oDocuments.Lines.BaseType = 17;

                                oDocuments.Lines.Quantity = item.toplananMiktar;

                                ilgiliSatirlar.Add(new _SiparisKarsilama { siparisNumarasi = item.siparisNumarasi, siparisSatirNo = item.siparisSatirNo });

                                oDocuments.Lines.Add();
                            }

                        }


                        if (satirvar)
                        {
                            var resp = oDocuments.Add();


                            if (resp != 0)
                            {
                                Handler.SAPApplication.MessageBox("Teslimat Belgesi Oluşturulurken Hata Oluştu." + ConstVariables.oCompanyObject.GetLastErrorDescription());
                            }
                            else
                            {
                                Handler.SAPApplication.MessageBox("İşlemler tamamlandı.");
                                var rowsSira = (from x in XDocument.Parse(xml).Descendants("Row")
                                                where (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_14" select new XElement(y.Element("Value"))).First().Value == "Y"
                                                select new _SiparisKarsilama
                                                {
                                                    siparisNumarasi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                                                    siparisSatirNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value,
                                                    siraNo = x.ElementsBeforeSelf().Count() + 1
                                                }).ToList();

                                //var satirSayisi = rows.Where(x=>x.siparisNumarasi==)
                                CompanyService oCompService = null;

                                GeneralService oGeneralService;

                                GeneralData oGeneralData;

                                oCompService = ConstVariables.oCompanyObject.GetCompanyService();

                                GeneralDataParams oGeneralParams;
                                foreach (var item in ilgiliSatirlar)
                                {
                                    if (rowsSira.Where(x => x.siparisNumarasi == item.siparisNumarasi && x.siparisSatirNo == item.siparisSatirNo).Count() > 0)
                                    {
                                        var satirSayisi = rowsSira.Where(x => x.siparisNumarasi == item.siparisNumarasi && x.siparisSatirNo == item.siparisSatirNo).ToList();

                                        foreach (var itemx in satirSayisi)
                                        {
                                            ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_11").Cells.Item(itemx.siraNo).Specific).Value = ConstVariables.oCompanyObject.GetNewObjectKey();



                                            //oCompany.StartTransaction();

                                            oGeneralService = oCompService.GetGeneralService("AIF_WMS_TOPLANAN");

                                            oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                                            oGeneralParams = (GeneralDataParams)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams);
                                            oGeneralParams.SetProperty("DocEntry", Convert.ToInt32(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_12").Cells.Item(itemx.siraNo).Specific).Value));
                                            oGeneralData = oGeneralService.GetByParams(oGeneralParams);


                                            oGeneralData.SetProperty("U_TeslimatNo", ConstVariables.oCompanyObject.GetNewObjectKey().ToString());


                                            oGeneralService.Update(oGeneralData);
                                        }
                                    }
                                }
                            }


                        }

                        //                     l.siparisNumarasi && l.siparisSatirNo)
                        //.Select(cl => new ResultLine
                        //{
                        //    ProductName = cl.First().Name,
                        //    Quantity = cl.Count().ToString(),
                        //    Price = cl.Sum(c => c.Price).ToString(),
                        //}).ToList();

                    }
                    else if (pVal.ColUID == "Col_14" && pVal.BeforeAction)
                    {
                        var teslimatNo = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_11").Cells.Item(pVal.Row).Specific).Value.ToString();

                        if (teslimatNo != "")
                        {
                            Handler.SAPApplication.MessageBox("Teslimatı Oluşturulmuş Satır İçin İşaretleme Yapılamaz.");
                            BubbleEvent = false;
                        }
                    }
                    else if (pVal.ItemUID == "Item_1" && !pVal.BeforeAction)
                    {
                        try
                        {
                            frmSevkEdilen.Close();
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

        public void MenuEvent(ref MenuEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;
        }

        public void RightClickEvent(ref ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }

    }
}