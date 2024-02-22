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
    public class DepoSecimi
    {
        [ItemAtt(AIFConn.DepoSecimiUID)]
        public SAPbouiCOM.Form frmDepoSecim;

        [ItemAtt("Item_0")]
        public SAPbouiCOM.Matrix oMatrix;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.Button btnSec;

        [ItemAtt("2")]
        public SAPbouiCOM.Button btnIptal;

        public void LoadForms(string _oncekiSecilenler)
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.DepoSecimiXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.DepoSecimiXML));
            Functions.CreateUserOrSystemFormComponent<DepoSecimi>(AIFConn.DepSecim);

            oncekiSecilenler = _oncekiSecilenler;
            InitForms();
        }


        SAPbouiCOM.DataTable oDT = null;

        string xmlformat = @"<?xml version=""1.0"" encoding=""UTF-8""?><DataTable Uid=""DATA""><Columns><Column Uid=""Secim"" Type=""1"" MaxLength=""1""/><Column Uid=""DepoKodu"" Type=""1"" MaxLength=""100""/><Column Uid=""DepoAdi"" Type=""1"" MaxLength=""150""/></Columns><Rows>{0}</Rows></DataTable>";

        List<_DepoSecim> depoSecimis = new List<_DepoSecim>();
        string oncekiSecilenler = "";
        public void InitForms()
        {
            try
            {
                frmDepoSecim.Freeze(true);

                ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);


                frmDepoSecim.EnableMenu("1283", false);
                frmDepoSecim.EnableMenu("1284", false);
                frmDepoSecim.EnableMenu("1286", false);

                oDT = frmDepoSecim.DataSources.DataTables.Add("DATA");

                string sql = "";
                sql = "Select \"WhsCode\",\"WhsName\" from OWHS where \"InActive\" = 'N' order by Cast(ISNULL(\"WhsCode\",0) as int) ";


                ConstVariables.oRecordset.DoQuery(sql);

                string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                XDocument xDoc = XDocument.Parse(xmll);
                XNamespace ns = "http://www.sap.com/SBO/SDK/DI";
                depoSecimis = new List<_DepoSecim>();
                depoSecimis = (from t in xDoc.Descendants(ns + "Row")
                               select new _DepoSecim
                               {
                                   secim = "N",
                                   depoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsCode" select new XElement(y.Element(ns + "Value"))).First().Value,
                                   depoAdi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsName" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                               }).ToList();

                if (oncekiSecilenler != "")
                {
                    var split = oncekiSecilenler.Split('|');

                    foreach (var item in split)
                    {
                        depoSecimis.Where(x => x.depoKodu == item).ToList().ForEach(x => x.secim = "Y");
                    }
                }

                string xmlRow = @"<Row><Cells><Cell><ColumnUid>Secim</ColumnUid><Value>{0}</Value></Cell><Cell><ColumnUid>DepoKodu</ColumnUid><Value>{1}</Value></Cell><Cell><ColumnUid>DepoAdi</ColumnUid><Value>{2}</Value></Cell></Cells></Row>";

                string rows = string.Join("", depoSecimis.Select(y => string.Format(xmlRow, y.secim, y.depoKodu, y.depoAdi)));

                string data = string.Format(xmlformat, rows);

                oDT.LoadSerializedXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_All, data);

                oMatrix.Columns.Item("Scm").DataBind.Bind("DATA", "Secim");
                oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "DepoKodu");
                oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "DepoAdi");

                oMatrix.LoadFromDataSource();
                oMatrix.AutoResizeColumns();

            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox(ex.Message);
            }
            finally
            {
                frmDepoSecim.Freeze(false);
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
                    if (!pVal.BeforeAction && pVal.ItemUID == "Item_3")
                    {
                        try
                        {
                            frmDepoSecim.Freeze(true);


                            string sql = "";
                            sql = "Select \"WhsCode\",\"WhsName\" from OWHS where \"InActive\" = 'N' order by Cast(ISNULL(\"WhsCode\",0) as int) ";


                            ConstVariables.oRecordset.DoQuery(sql);
                            string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                            XDocument xDoc = XDocument.Parse(xmll);
                            XNamespace ns = "http://www.sap.com/SBO/SDK/DI";
                            depoSecimis = (from t in xDoc.Descendants(ns + "Row")
                                           select new _DepoSecim
                                           {
                                               secim = "Y",
                                               depoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsCode" select new XElement(y.Element(ns + "Value"))).First().Value,
                                               depoAdi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsName" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                                           }).ToList();


                            string xmlRow = @"<Row><Cells><Cell><ColumnUid>Secim</ColumnUid><Value>{0}</Value></Cell><Cell><ColumnUid>DepoKodu</ColumnUid><Value>{1}</Value></Cell><Cell><ColumnUid>DepoAdi</ColumnUid><Value>{2}</Value></Cell></Cells></Row>";

                            string rows = string.Join("", depoSecimis.Select(y => string.Format(xmlRow, y.secim, y.depoKodu, y.depoAdi)));

                            string data = string.Format(xmlformat, rows);

                            oDT.LoadSerializedXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_All, data);

                            oMatrix.Columns.Item("Scm").DataBind.Bind("DATA", "Secim");
                            oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "DepoKodu");
                            oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "DepoAdi");

                            oMatrix.LoadFromDataSource();
                            oMatrix.AutoResizeColumns();
                        }
                        catch (Exception)
                        {

                        }
                        finally
                        {
                            frmDepoSecim.Freeze(false);
                        }

                    }
                    else if (!pVal.BeforeAction && pVal.ItemUID == "Item_4")
                    {
                        try
                        {
                            frmDepoSecim.Freeze(true);


                            string sql = "";
                            sql = "Select \"WhsCode\",\"WhsName\" from OWHS where \"InActive\" = 'N' order by Cast(ISNULL(\"WhsCode\",0) as int) ";


                            ConstVariables.oRecordset.DoQuery(sql);
                            string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                            XDocument xDoc = XDocument.Parse(xmll);
                            XNamespace ns = "http://www.sap.com/SBO/SDK/DI";
                            depoSecimis = (from t in xDoc.Descendants(ns + "Row")
                                           select new _DepoSecim
                                           {
                                               secim = "N",
                                               depoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsCode" select new XElement(y.Element(ns + "Value"))).First().Value,
                                               depoAdi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsName" select new XElement(y.Element(ns + "Value"))).First().Value.Replace("&", "-"),
                                           }).ToList();


                            string xmlRow = @"<Row><Cells><Cell><ColumnUid>Secim</ColumnUid><Value>{0}</Value></Cell><Cell><ColumnUid>DepoKodu</ColumnUid><Value>{1}</Value></Cell><Cell><ColumnUid>DepoAdi</ColumnUid><Value>{2}</Value></Cell></Cells></Row>";

                            string rows = string.Join("", depoSecimis.Select(y => string.Format(xmlRow, y.secim, y.depoKodu, y.depoAdi)));

                            string data = string.Format(xmlformat, rows);

                            oDT.LoadSerializedXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_All, data);

                            oMatrix.Columns.Item("Scm").DataBind.Bind("DATA", "Secim");
                            oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "DepoKodu");
                            oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "DepoAdi");

                            oMatrix.LoadFromDataSource();
                            oMatrix.AutoResizeColumns();
                        }
                        catch (Exception)
                        {

                        }
                        finally
                        {
                            frmDepoSecim.Freeze(false);
                        }
                    }
                    else if (pVal.ItemUID == "Item_1" && !pVal.BeforeAction)
                    {
                        string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                        var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                    where (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Scm" select new XElement(y.Element("Value"))).First().Value == "Y"
                                    select new
                                    {
                                        depolar = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                                    }).ToList();


                        string depolar = "";

                        if (rows.Count > 0)
                        {
                            foreach (var item in rows)
                            {
                                if (depolar != "")
                                {
                                    depolar += "|";
                                    depolar += item.depolar.ToString();
                                }
                                else
                                {
                                    depolar = item.depolar.ToString();
                                }
                            }

                            AIFConn.UrunEkle.depolariYaz(depolar);

                            try
                            {
                                frmDepoSecim.Close();
                            }
                            catch (Exception)
                            {
                            }
                        }
                        else
                        {
                            AIFConn.UrunEkle.depolariYaz(depolar);

                            try
                            {
                                frmDepoSecim.Close();
                            }
                            catch (Exception)
                            {
                            }
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