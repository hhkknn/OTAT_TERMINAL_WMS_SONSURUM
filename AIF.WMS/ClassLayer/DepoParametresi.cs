using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Abstarct;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib;
using AIF.ObjectsDLL.Utils;
using AIF.WMS.Models;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
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
    public class DepoParametresi
    {
        [ItemAtt(AIFConn.DepoParametresiUID)]
        public SAPbouiCOM.Form frmDepoParametresi;

        [ItemAtt("Item_2")]
        public SAPbouiCOM.Matrix oMatrix;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.ComboBox oComboKullaniciKodu;

        [ItemAtt("Item_7")]
        public SAPbouiCOM.EditText EdtKullaniciAdi;

        [ItemAtt("1")]
        public SAPbouiCOM.Button btnAddOrUpdate;

        [ItemAtt("Item_9")]
        public SAPbouiCOM.EditText EdtDocEntry;

        [ItemAtt("Item_4")]
        public SAPbouiCOM.EditText EdtVarsDepoKodu;

        [ItemAtt("Item_5")]
        public SAPbouiCOM.EditText EdtVarsDepoAdi;

        public void LoadForms()
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.DepoParametresiXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.DepoParametresiXML));
            Functions.CreateUserOrSystemFormComponent<DepoParametresi>(AIFConn.DepoParam);

            InitForms();
        }

        private string header = @"<?xml version=""1.0"" encoding=""UTF-16"" ?><dbDataSources uid=""@AIF_WMS_USRWHS1""><rows>{0}</rows></dbDataSources>";

        private string row = "<row><cells><cell><uid>U_DepoKodu</uid><value>{0}</value></cell><cell><uid>U_DepoAdi</uid><value>{1}</value></cell><cell><uid>U_TamYetki</uid><value>{2}</value></cell><cell><uid>U_SipMalGrs</uid><value>{3}</value></cell><cell><uid>U_BlgszMalGrs</uid><value>{4}</value></cell><cell><uid>U_TlpszDepK</uid><value>{5}</value></cell><cell><uid>U_TlpszDepH</uid><value>{6}</value></cell><cell><uid>U_TlpBagDepK</uid><value>{7}</value></cell><cell><uid>U_TlpBagDepH</uid><value>{8}</value></cell><cell><uid>U_TlpKabulK</uid><value>{9}</value></cell><cell><uid>U_TlpKabulH</uid><value>{10}</value></cell><cell><uid>U_BlgszMalC</uid><value>{11}</value></cell><cell><uid>U_SipBagTes</uid><value>{12}</value></cell><cell><uid>U_SprsszTes</uid><value>{13}</value></cell><cell><uid>U_TeslmtIade</uid><value>{14}</value></cell><cell><uid>U_SatisIade</uid><value>{15}</value></cell><cell><uid>U_MagazaIslemleri</uid><value>{16}</value></cell><cell><uid>U_IadeTalep</uid><value>{17}</value></cell><cell><uid>U_Secim</uid><value>{18}</value></cell></cells></row>";

        public void InitForms()
        {
            try
            {
                frmDepoParametresi.Freeze(true);

                #region parametreye göre görünümler
                string sorgu = "Select \"U_DepoCalismaTipi\" from \"@AIF_WMS_GNLPRM\" ";

                ConstVariables.oRecordset.DoQuery(sorgu);

                if (ConstVariables.oRecordset.RecordCount > 0)
                {
                    string depocalismatipi = ConstVariables.oRecordset.Fields.Item(0).Value.ToString();

                    if (depocalismatipi == "1")
                    {
                        frmDepoParametresi.Width = 450;
                        oMatrix.Item.Width = 405;
                        oMatrix.Columns.Item("Col_2").Visible = false;
                        oMatrix.Columns.Item("Col_3").Visible = false;
                        oMatrix.Columns.Item("Col_4").Visible = false;
                        oMatrix.Columns.Item("Col_5").Visible = false;
                        oMatrix.Columns.Item("Col_6").Visible = false;
                        oMatrix.Columns.Item("Col_7").Visible = false;
                        oMatrix.Columns.Item("Col_8").Visible = false;
                        oMatrix.Columns.Item("Col_9").Visible = false;
                        oMatrix.Columns.Item("Col_10").Visible = false;
                        oMatrix.Columns.Item("Col_11").Visible = false;
                        oMatrix.Columns.Item("Col_12").Visible = false;
                        oMatrix.Columns.Item("Col_13").Visible = false;
                        oMatrix.Columns.Item("Col_14").Visible = false;
                        oMatrix.Columns.Item("Col_15").Visible = false;
                        oMatrix.Columns.Item("Col_16").Visible = false;
                        oMatrix.Columns.Item("Col_17").Visible = false;
                    }
                    else if (depocalismatipi == "2")
                    {

                    }
                }
                #endregion

                frmDepoParametresi.EnableMenu("1283", false);
                frmDepoParametresi.EnableMenu("1284", false);
                frmDepoParametresi.EnableMenu("1286", false);

                List<Helper.ValidValue> list;
                //list = Helper.GetValidValuesFromRS("Select \"USER_CODE\" as \"value\", \"U_Name\" as \"description\"  from OUSR");
                list = Helper.GetValidValuesFromRS("Select \"empID\" as \"value\", (\"firstName\" + ' ' + \"lastName\") as \"description\"  from OHEM order by \"empID\"");

                Helper nesne = new Helper();
                nesne.ComboAction(frmDepoParametresi, "Item_1", Helper.ActionCombo.add, list);

                ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                string sql = "Select \"USER_CODE\" from OUSR where \"USERID\" = '" + ConstVariables.oCompanyObject.UserSignature + "' ";

                //ConstVariables.oRecordset.DoQuery(sql);

                //oComboKullaniciKodu.Select(ConstVariables.oCompanyObject.UserSignature.ToString());

                if (oMatrix.RowCount == 0)
                {
                    sql = "Select \"WhsCode\",\"WhsName\" from OWHS where \"InActive\" = 'N' ";// order by Cast(ISNULL(\"WhsCode\",0) as int) "; 
                    ConstVariables.oRecordset.DoQuery(sql);
                    #region MyRegion
                    //,'' as \"TamYetki\",'' as \"SipMalGrs\",'' as \"BlgszMalGrs\",'' as \"TlpszDepK\",'' as \"TlpszDepH\",'' as \"TlpBagDepK\",'' as \"TlpBagDepH\",'' as \"TlpKabul\",'' as \"BlgszMalC\",'' as \"SipBagTes\",'' as \"SprsszTes\",'' as \"TeslmtIade\",'' as \"SatisIade\",'' as \"S\"
                    #endregion
                    string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                    XDocument xDoc = XDocument.Parse(xmll);
                    XNamespace ns = "http://www.sap.com/SBO/SDK/DI";

                    var rows = (from t in xDoc.Descendants(ns + "Row")
                                select new
                                {
                                    WhsCode = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsCode" select new XElement(y.Element(ns + "Value"))).First().Value,
                                    WhsName = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsName" select new XElement(y.Element(ns + "Value"))).First().Value,
                                }).ToList();

                    string data = string.Join("", rows.Select(s => string.Format(row, s.WhsCode, s.WhsName.Replace("&", "-"), "N", "", "N", "N", "N", "N", "N", "N", "N", "N", "N", "N", "N", "N", "N","N","N")));

                    //WriteToFile("InitForm data " + string.Format(header, data));

                    frmDepoParametresi.DataSources.DBDataSources.Item("@AIF_WMS_USRWHS1").LoadFromXML(string.Format(header, data));
                }
                else
                {
                    var xml = frmDepoParametresi.DataSources.DBDataSources.Item(1).GetAsXML();

                    var rows = (from x in XDocument.Parse(xml).Descendants("row")
                                select new
                                {
                                    DepoKodu = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoKodu" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoKodu" select new XElement(y.Element("value"))).First().Value.Replace("&", "-") : "",
                                    DepoAdi = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoAdi" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoAdi" select new XElement(y.Element("value"))).First().Value.Replace("&", "-") : "",
                                    TamYetki = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TamYetki" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TamYetki" select new XElement(y.Element("value"))).First().Value : "",
                                    SipMalGrs = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipMalGrs" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipMalGrs" select new XElement(y.Element("value"))).First().Value : "",
                                    BlgszMalGrs = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalGrs" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalGrs" select new XElement(y.Element("value"))).First().Value : "",
                                    TlpszDepK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepK" select new XElement(y.Element("value"))).First().Value : "",
                                    TlpszDepH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepH" select new XElement(y.Element("value"))).First().Value : "",
                                    TlpBagDepK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepK" select new XElement(y.Element("value"))).First().Value : "",
                                    TlpBagDepH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepH" select new XElement(y.Element("value"))).First().Value : "",
                                    TlpKabulK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulK" select new XElement(y.Element("value"))).First().Value : "",
                                    TlpKabulH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulH" select new XElement(y.Element("value"))).First().Value : "",
                                    BlgszMalC = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalC" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalC" select new XElement(y.Element("value"))).First().Value : "",
                                    SipBagTes = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipBagTes" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipBagTes" select new XElement(y.Element("value"))).First().Value : "",
                                    SprsszTes = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SprsszTes" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SprsszTes" select new XElement(y.Element("value"))).First().Value : "",
                                    TeslmtIade = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TeslmtIade" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TeslmtIade" select new XElement(y.Element("value"))).First().Value : "",
                                    SatisIade = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SatisIade" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SatisIade" select new XElement(y.Element("value"))).First().Value : "", 
                                    MagazaIslem = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_MagazaIslemleri" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_MagazaIslemleri" select new XElement(y.Element("value"))).First().Value : "",
                                    IadeTalep = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_IadeTalep" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_IadeTalep" select new XElement(y.Element("value"))).First().Value : "",
                                    Secim = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_Secim" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_Secim" select new XElement(y.Element("value"))).First().Value : ""
                                }).ToList();

                    sql = "Select \"WhsCode\",\"WhsName\" from OWHS where \"InActive\" = 'N' order by Cast(ISNULL(\"WhsCode\",0) as int) ";

                    ConstVariables.oRecordset.DoQuery(sql);

                    string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                    XDocument xDoc = XDocument.Parse(xmll);
                    XNamespace ns = "http://www.sap.com/SBO/SDK/DI";

                    var SAPDepo = (from t in xDoc.Descendants(ns + "Row")
                                   select new
                                   {
                                       DepoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsCode" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                                       DepoAdi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsName" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                                       TamYetki = "",
                                       SipMalGrs = "",
                                       BlgszMalGrs = "",
                                       TlpszDepK = "",
                                       TlpszDepH = "",
                                       TlpBagDepK = "",
                                       TlpBagDepH = "",
                                       TlpKabulK = "",
                                       TlpKabulH = "",
                                       BlgszMalC = "",
                                       SipBagTes = "",
                                       SprsszTes = "",
                                       TeslmtIade = "",
                                       SatisIade = "",
                                       MagazaIslem = "",
                                       IadeTalep = "", 
                                       Secim = "N"
                                   }).ToList();

                    var s = SAPDepo.Except(rows).ToList();

                    rows.AddRange(s);

                    //string data = string.Join("", rows.Select(sx => string.Format(row, sx.DepoKodu, sx.DepoAdi, sx.Secim)));
                    string data = string.Join("", rows.Select(sx => string.Format(row, sx.DepoKodu, sx.DepoAdi, sx.TamYetki, sx.SipMalGrs, sx.BlgszMalGrs, sx.TlpszDepK, sx.TlpszDepH, sx.TlpBagDepK, sx.TlpBagDepH, sx.TlpKabulK, sx.TlpKabulH, sx.BlgszMalC, sx.SipBagTes, sx.SprsszTes, sx.TeslmtIade, sx.SatisIade,sx.MagazaIslem,sx.IadeTalep, sx.Secim)));

                    //WriteToFile("InitForm data " + string.Format(header, data));

                    frmDepoParametresi.DataSources.DBDataSources.Item("@AIF_WMS_USRWHS1").LoadFromXML(string.Format(header, data));
                }

                oMatrix.AutoResizeColumns();
                oMatrix.Columns.Item("Col_0").TitleObject.Sort(BoGridSortType.gst_Ascending);

            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox(ex.Message);
            }

            finally
            {
                frmDepoParametresi.Freeze(false);
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
                    if (!BusinessObjectInfo.BeforeAction)
                    {
                        try
                        {
                            string sql = "";
                            var xml = frmDepoParametresi.DataSources.DBDataSources.Item(1).GetAsXML();

                            var rows = (from x in XDocument.Parse(xml).Descendants("row")
                                        select new _DepoSecim
                                        {
                                            depoKodu = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoKodu" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoKodu" select new XElement(y.Element("value"))).First().Value.Replace("&", "-") : "",
                                            depoAdi = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoAdi" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoAdi" select new XElement(y.Element("value"))).First().Value.Replace("&", "-") : "",
                                            tamYetki = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TamYetki" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TamYetki" select new XElement(y.Element("value"))).First().Value : "",
                                            sipMalGrs = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipMalGrs" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipMalGrs" select new XElement(y.Element("value"))).First().Value : "",
                                            blgszMalGrs = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalGrs" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalGrs" select new XElement(y.Element("value"))).First().Value : "",
                                            tlpszDepK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepK" select new XElement(y.Element("value"))).First().Value : "",
                                            tlpszDepH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepH" select new XElement(y.Element("value"))).First().Value : "",
                                            tlpBagDepK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepK" select new XElement(y.Element("value"))).First().Value : "",
                                            tlpBagDepH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepH" select new XElement(y.Element("value"))).First().Value : "",
                                            tlpKabulK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulK" select new XElement(y.Element("value"))).First().Value : "",
                                            tlpKabulH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulH" select new XElement(y.Element("value"))).First().Value : "",
                                            blgszMalC = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalC" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalC" select new XElement(y.Element("value"))).First().Value : "",
                                            sipBagTes = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipBagTes" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipBagTes" select new XElement(y.Element("value"))).First().Value : "",
                                            sprsszTes = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SprsszTes" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SprsszTes" select new XElement(y.Element("value"))).First().Value : "",
                                            teslmtIade = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TeslmtIade" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TeslmtIade" select new XElement(y.Element("value"))).First().Value : "",
                                            satisIade = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SatisIade" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SatisIade" select new XElement(y.Element("value"))).First().Value : "", 
                                            magazaIslem = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_MagazaIslemleri" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_MagazaIslemleri" select new XElement(y.Element("value"))).First().Value : "",
                                            IadeTalep = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_IadeTalep" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_IadeTalep" select new XElement(y.Element("value"))).First().Value : "",
                                            secim = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_Secim" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_Secim" select new XElement(y.Element("value"))).First().Value : ""
                                        }).ToList();

                            sql = "Select \"WhsCode\",\"WhsName\" from OWHS where \"InActive\" = 'N' "; // order by Cast(ISNULL(\"WhsCode\",0) as int) ";

                            ConstVariables.oRecordset.DoQuery(sql);

                            string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                            XDocument xDoc = XDocument.Parse(xmll);
                            XNamespace ns = "http://www.sap.com/SBO/SDK/DI";

                            var SAPDepo = (from t in xDoc.Descendants(ns + "Row")
                                           select new _DepoSecim
                                           {
                                               depoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsCode" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                                               depoAdi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsName" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                                               secim = "N"
                                           }).ToList();

                            //var s = SAPDepo.Except(rows).ToList();

                            //rows.AddRange(s);

                            foreach (var item in SAPDepo)
                            {
                                if (rows.Where(x => x.depoKodu == item.depoKodu).Count() == 0)
                                {
                                    rows.Add(new _DepoSecim { depoKodu = item.depoKodu, depoAdi = item.depoAdi, secim = "N" }); //????????????
                                }
                            }


                            //string data = string.Join("", rows.Select(sx => string.Format(row, sx.DepoKodu, sx.DepoAdi, "N")));

                            string data = string.Join("", rows.Select(sx => string.Format(row, sx.depoKodu, sx.depoAdi, sx.tamYetki, sx.sipMalGrs, sx.blgszMalGrs, sx.tlpszDepK, sx.tlpszDepH, sx.tlpBagDepK, sx.tlpBagDepH, sx.tlpKabulK, sx.tlpKabulH, sx.blgszMalC, sx.sipBagTes, sx.sprsszTes, sx.teslmtIade, sx.satisIade,sx.magazaIslem,sx.IadeTalep, sx.secim)));

                            //WriteToFile("Data Load data " + string.Format(header, data));

                            frmDepoParametresi.DataSources.DBDataSources.Item("@AIF_WMS_USRWHS1").LoadFromXML(string.Format(header, data));
                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox(ex.Message);
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

        private string val = "";

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
                    if (pVal.ItemUID == "Item_1" && !pVal.BeforeAction)
                    {
                        if (frmDepoParametresi.Mode == BoFormMode.fm_FIND_MODE)
                        {
                            return false;
                        }

                        ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                        string sql = "Select * from \"@AIF_WMS_USRWHS\" where \"U_KullaniciKodu\" = '" + oComboKullaniciKodu.Value.Trim() + "'";

                        ConstVariables.oRecordset.DoQuery(sql);

                        if (ConstVariables.oRecordset.RecordCount > 0)
                        {
                            frmDepoParametresi.Mode = BoFormMode.fm_FIND_MODE;
                            EdtDocEntry.Item.Enabled = true;
                            EdtDocEntry.Value = ConstVariables.oRecordset.Fields.Item(0).Value.ToString();
                            btnAddOrUpdate.Item.Click();
                            EdtDocEntry.Item.Enabled = false;
                        }
                        else
                        {
                            EdtKullaniciAdi.Value = oComboKullaniciKodu.Selected.Description.ToString();

                            try
                            {
                                var xml = frmDepoParametresi.DataSources.DBDataSources.Item(1).GetAsXML();

                                var rows = (from x in XDocument.Parse(xml).Descendants("row")
                                            select new _DepoSecim
                                            {
                                                depoKodu = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoKodu" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoKodu" select new XElement(y.Element("value"))).First().Value.Replace("&", "-") : "",
                                                depoAdi = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoAdi" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_DepoAdi" select new XElement(y.Element("value"))).First().Value.Replace("&", "-") : "",
                                                tamYetki = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TamYetki" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TamYetki" select new XElement(y.Element("value"))).First().Value : "",
                                                sipMalGrs = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipMalGrs" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipMalGrs" select new XElement(y.Element("value"))).First().Value : "",
                                                blgszMalGrs = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalGrs" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalGrs" select new XElement(y.Element("value"))).First().Value : "",
                                                tlpszDepK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepK" select new XElement(y.Element("value"))).First().Value : "",
                                                tlpszDepH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpszDepH" select new XElement(y.Element("value"))).First().Value : "",
                                                tlpBagDepK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepK" select new XElement(y.Element("value"))).First().Value : "",
                                                tlpBagDepH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpBagDepH" select new XElement(y.Element("value"))).First().Value : "",
                                                tlpKabulK = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulK" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulK" select new XElement(y.Element("value"))).First().Value : "",
                                                tlpKabulH = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulH" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TlpKabulH" select new XElement(y.Element("value"))).First().Value : "",
                                                blgszMalC = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalC" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_BlgszMalC" select new XElement(y.Element("value"))).First().Value : "",
                                                sipBagTes = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipBagTes" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SipBagTes" select new XElement(y.Element("value"))).First().Value : "",
                                                sprsszTes = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SprsszTes" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SprsszTes" select new XElement(y.Element("value"))).First().Value : "",
                                                teslmtIade = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TeslmtIade" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_TeslmtIade" select new XElement(y.Element("value"))).First().Value : "",
                                                satisIade = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SatisIade" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_SatisIade" select new XElement(y.Element("value"))).First().Value : "",
                                                magazaIslem = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_MagazaIslemleri" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_MagazaIslemleri" select new XElement(y.Element("value"))).First().Value : "",
                                                IadeTalep = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_IadeTalep" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_IadeTalep" select new XElement(y.Element("value"))).First().Value : "",
                                                secim = (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_Secim" select new XElement(y.Element("value"))).Any() == true ? (from y in x.Elements("cells").Elements("cell") where y.Element("uid").Value == "U_Secim" select new XElement(y.Element("value"))).First().Value : ""
                                            }).ToList();

                                sql = "Select \"WhsCode\",\"WhsName\" from OWHS where \"InActive\" = 'N' order by \"WhsName\" ";

                                ConstVariables.oRecordset.DoQuery(sql);

                                string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                                XDocument xDoc = XDocument.Parse(xmll);
                                XNamespace ns = "http://www.sap.com/SBO/SDK/DI";

                                var SAPDepo = (from t in xDoc.Descendants(ns + "Row")
                                               select new _DepoSecim
                                               {
                                                   depoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsCode" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                                                   depoAdi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsName" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                                                   secim = "N"
                                               }).ToList();

                                //var s = SAPDepo.Except(rows).ToList();

                                //rows.AddRange(s);

                                foreach (var item in SAPDepo)
                                {
                                    if (rows.Where(x => x.depoKodu == item.depoKodu).Count() == 0)
                                    {
                                        rows.Add(new _DepoSecim { depoKodu = item.depoKodu, depoAdi = item.depoAdi, secim = "N" });
                                    }
                                }

                                //string data = string.Join("", rows.Select(sx => string.Format(row, sx.DepoKodu, sx.DepoAdi, "N")));

                                //string data = string.Join("", rows.Select(sx => string.Format(row, sx.depoKodu, sx.depoAdi, sx.secim)));
                                string data = string.Join("", rows.Select(sx => string.Format(row, sx.depoKodu, sx.depoAdi, sx.tamYetki, sx.sipMalGrs, sx.blgszMalGrs, sx.tlpszDepK, sx.tlpszDepH, sx.tlpBagDepK, sx.tlpBagDepH, sx.tlpKabulK, sx.tlpKabulH, sx.blgszMalC, sx.sipBagTes, sx.sprsszTes, sx.teslmtIade, sx.satisIade,sx.magazaIslem,sx.IadeTalep, sx.secim)));

                                WriteToFile("Combo Select data " + string.Format(header, data));

                                frmDepoParametresi.DataSources.DBDataSources.Item("@AIF_WMS_USRWHS1").LoadFromXML(string.Format(header, data));
                            }
                            catch (Exception ex)
                            {
                                Handler.SAPApplication.MessageBox(ex.Message);
                            }
                        }
                    }
                    break;

                case BoEventTypes.et_CLICK:
                    if (pVal.ItemUID == "Item_2" && pVal.BeforeAction)
                    {
                        try
                        {
                            oMatrix.SelectRow(pVal.Row, true, false);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (pVal.ItemUID == "1" && !pVal.BeforeAction)
                    {
                        string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                        var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                    select new
                                    {
                                        yetki = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value,
                                    }).ToList();

                        if (rows.Where(x => x.yetki != "Y" && x.yetki != "N" && x.yetki != "").Count() > 0)
                        {
                            Handler.SAPApplication.MessageBox("Yetki sütununa yalnızca Y veya N değerleri girilebilir.");
                            return true;
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
                    if (pVal.ItemUID == "Item_2" && pVal.ColUID == "Col_2" && !pVal.BeforeAction)
                    {
                        string veri = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific).Value.ToString();
                        if (veri == "n")
                        {
                            ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific).Value = "N";
                        }
                        else if (veri == "y")
                        {
                            ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific).Value = "Y";
                        }

                        //if (veri != "Y" && veri != "N")
                        //{
                        //    Handler.SAPApplication.MessageBox("Bu alana yalnızca Y veya N girilebilir.");
                        //    //((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_2").Cells.Item(pVal.Row).Specific).Value = "";
                        //}
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
                    if (pVal.ItemUID == "Item_4" && pVal.BeforeAction)
                    {
                        SAPbouiCOM.IChooseFromListEvent oCFLEvento = default(SAPbouiCOM.IChooseFromListEvent);
                        oCFLEvento = (SAPbouiCOM.IChooseFromListEvent)pVal;

                        SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
                        oCFL = frmDepoParametresi.ChooseFromLists.Item(oCFLEvento.ChooseFromListUID);

                        SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);
                        SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                        SAPbouiCOM.Conditions oEmptyConts = new SAPbouiCOM.Conditions();

                        oCFL.SetConditions(oEmptyConts);
                        oCons = oCFL.GetConditions();

                        oCon = oCons.Add();
                        oCon.Alias = "Locked";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        oCon.CondVal = "N";

                        oCFL.SetConditions(oCons);
                    }
                    else if (pVal.ItemUID == "Item_4" && !pVal.BeforeAction)
                    {
                        try
                        {
                            SAPbouiCOM.DataTable oDataTable = ((SAPbouiCOM.ChooseFromListEvent)pVal).SelectedObjects;
                            string Val = "";
                            Val = oDataTable.GetValue("WhsCode", 0).ToString();


                            try
                            {
                                EdtVarsDepoKodu.Value = Val;
                            }
                            catch (Exception)
                            {
                            }

                            Val = "";

                            Val = oDataTable.GetValue("WhsName", 0).ToString();

                            try
                            {
                                EdtVarsDepoAdi.Value = Val;
                            }
                            catch (Exception)
                            {
                            }

                            if (frmDepoParametresi.Mode == BoFormMode.fm_OK_MODE)
                            {
                                frmDepoParametresi.Mode = BoFormMode.fm_UPDATE_MODE;
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

        public void MenuEvent(ref MenuEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;
        }

        public void RightClickEvent(ref ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }

        public static void WriteToFile(string Message)
        {
            return;
            //string path = AppDomain.CurrentDomain.BaseDirectory + "Logs";
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //string filepath = path + "\\Log_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            //if (!File.Exists(filepath))
            //{
            //    using (StreamWriter sw = File.CreateText(filepath))
            //    {
            //        sw.WriteLine(Message);
            //    }
            //}
            //else
            //{
            //    using (StreamWriter sw = File.AppendText(filepath))
            //    {
            //        sw.WriteLine(Message);
            //    }
            //}
        }
    }
}