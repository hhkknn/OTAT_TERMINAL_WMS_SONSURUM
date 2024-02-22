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
    public class StokTransfer
    {
        [ItemAtt(AIFConn.StokTransferUID)]
        public SAPbouiCOM.Form frmStokTransfer;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.EditText edtKalemKodu;

        [ItemAtt("Item_2")]
        public SAPbouiCOM.Button btnAra;

        [ItemAtt("Item_7")]
        public SAPbouiCOM.ComboBox cmbKaynakDepo;

        [ItemAtt("Item_3")]
        public SAPbouiCOM.Matrix oMatrix;

        [ItemAtt("Item_4")]
        public SAPbouiCOM.Button btnStokNakli;

        [ItemAtt("Item_5")]
        public SAPbouiCOM.Button btnIptal;

        SAPbouiCOM.DataTable oDataTable = null;
        string xmlformat = @"<?xml version=""1.0"" encoding=""UTF-8""?><DataTable Uid=""DATA""><Columns><Column Uid=""Sec"" Type=""1"" MaxLength=""1""/><Column Uid=""CardCode"" Type=""1"" MaxLength=""15""/><Column Uid=""CardName"" Type=""1"" MaxLength=""100""/><Column Uid=""DocEntry"" Type=""2"" MaxLength=""0""/><Column Uid=""LineNum"" Type=""2"" MaxLength=""0""/><Column Uid=""ItemCode"" Type=""1"" MaxLength=""50""/><Column Uid=""Dscription"" Type=""1"" MaxLength=""200""/><Column Uid=""Quantity"" Type=""7"" MaxLength=""0""/><Column Uid=""OpenQty"" Type=""7"" MaxLength=""0""/><Column Uid=""TransferMik"" Type=""7"" MaxLength=""0""/><Column Uid=""FromWhsCod"" Type=""1"" MaxLength=""8""/><Column Uid=""WhsCode"" Type=""1"" MaxLength=""8""/><Column Uid=""U_KaynakDYeri"" Type=""1"" MaxLength=""50""/><Column Uid=""U_HedefDYeri"" Type=""1"" MaxLength=""50""/><Column Uid=""KaynakDepoId"" Type=""1"" MaxLength=""50""/><Column Uid=""HedefDepoId"" Type=""1"" MaxLength=""50""/></Columns><Rows>{0}</Rows></DataTable>";
        public void LoadForms()
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.StokTransferXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.StokTransferXML));
            Functions.CreateUserOrSystemFormComponent<StokTransfer>(AIFConn.StokTrans);

            InitForms();
        }
        public void InitForms()
        {
            try
            {
                frmStokTransfer.Freeze(true);

                oDataTable = frmStokTransfer.DataSources.DataTables.Add("DATA");

                string condition = ConstVariables.oCompanyObject.DbServerType == BoDataServerTypes.dst_HANADB ? "IFNULL" : "ISNULL";

                string sql = "";

                sql = "Select \"WhsCode\",\"WhsName\" from OWHS Where \"Inactive\" = 'N' ";
                ConstVariables.oRecordset.DoQuery(sql);

                if (ConstVariables.oRecordset.RecordCount > 0)
                {
                    while (!ConstVariables.oRecordset.EoF)
                    {
                        cmbKaynakDepo.ValidValues.Add(ConstVariables.oRecordset.Fields.Item("WhsCode").Value.ToString(), ConstVariables.oRecordset.Fields.Item("WhsName").Value.ToString());

                        ConstVariables.oRecordset.MoveNext();
                    }
                }
                cmbKaynakDepo.Select("02", BoSearchKey.psk_ByValue);

                Listele();
            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox(ex.Message);
            }
            finally
            {
                frmStokTransfer.Freeze(false);
            }
        }

        private void Listele()
        {
            try
            {
                frmStokTransfer.Freeze(true);
                string sql = "";

                sql = "SELECT  'N' as \"Sec\", T1.\"CardCode\",T1.\"CardName\", T0.\"DocEntry\", T0.\"LineNum\", T0.\"ItemCode\", T0.\"Dscription\", T0.\"Quantity\",T0.\"OpenQty\",cast(0 as decimal(15,2)) as TransferMik, T0.\"FromWhsCod\", T0.\"WhsCode\",T0.\"U_KaynakDYeri\", T0.\"U_HedefDYeri\", ";
                sql += "(SELECT T3.\"AbsEntry\" FROM \"OBIN\" T3 WHERE T3.\"WhsCode\" = T0.\"FromWhsCod\" AND T3.\"BinCode\" = T0.\"U_KaynakDYeri\"  AND T3.\"Disabled\" = 'N') \"KaynakDepoId\", ";
                sql += "(SELECT T3.\"AbsEntry\" FROM \"OBIN\" T3 WHERE T3.\"WhsCode\" = T0.\"WhsCode\" AND T3.\"BinCode\" = T0.\"U_HedefDYeri\"  AND T3.\"Disabled\" = 'N') \"HedefDepoId\" ";
                sql += "FROM WTQ1 T0 ";
                sql += "INNER JOIN OWTQ T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" WHERE 1=1 ";

                if (cmbKaynakDepo.Value != "")
                {
                    sql += "  AND T0.\"FromWhsCod\" = '" + cmbKaynakDepo.Value.Trim() + "' ";
                }

                sql += " AND T0.\"OpenQty\">0 ORDER BY T0.\"DocEntry\" ";


                oDataTable.Clear();
                oDataTable.ExecuteQuery(sql);

                oMatrix.Clear();

                oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "Sec");
                oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "CardCode");
                oMatrix.Columns.Item("Col_2").DataBind.Bind("DATA", "CardName");
                oMatrix.Columns.Item("Col_3").DataBind.Bind("DATA", "DocEntry");
                oMatrix.Columns.Item("Col_4").DataBind.Bind("DATA", "LineNum");
                oMatrix.Columns.Item("Col_5").DataBind.Bind("DATA", "ItemCode");
                oMatrix.Columns.Item("Col_6").DataBind.Bind("DATA", "Dscription");
                oMatrix.Columns.Item("Col_7").DataBind.Bind("DATA", "Quantity");
                oMatrix.Columns.Item("Col_8").DataBind.Bind("DATA", "OpenQty");
                oMatrix.Columns.Item("Col_9").DataBind.Bind("DATA", "TransferMik");
                oMatrix.Columns.Item("Col_10").DataBind.Bind("DATA", "FromWhsCod");
                oMatrix.Columns.Item("Col_11").DataBind.Bind("DATA", "WhsCode");
                oMatrix.Columns.Item("Col_12").DataBind.Bind("DATA", "U_KaynakDYeri");
                oMatrix.Columns.Item("Col_13").DataBind.Bind("DATA", "U_HedefDYeri");
                oMatrix.Columns.Item("Col_14").DataBind.Bind("DATA", "KaynakDepoId");
                oMatrix.Columns.Item("Col_15").DataBind.Bind("DATA", "HedefDepoId");

                oMatrix.LoadFromDataSource();
                oMatrix.AutoResizeColumns();
                //oMatrix.Item.AffectsFormMode = false;

                SatirKapat();

                string xml = oDataTable.SerializeAsXML(BoDataTableXmlSelect.dxs_All);

            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox("Hata oluştu." + ex.Message);
            }

            finally
            {
                frmStokTransfer.Freeze(false);
            }
        }

        public class MatrisVerisi
        {
            public string sec { get; set; }
            public string muhatapKodu { get; set; }
            public string muhatapAdi { get; set; }
            public int belgeNo { get; set; }
            public int satirNo { get; set; }
            public string kalemKodu { get; set; }
            public string kalemAdi { get; set; }
            public double miktar { get; set; }
            public double acikMiktar { get; set; }
            public double transferMik { get; set; }
            public string kaynakDepo { get; set; }
            public string hedefDepo { get; set; }
            public string kaynakDepoYeri { get; set; }
            public string hedefDepoYeri { get; set; }
            public string kaynakDepoId { get; set; }
            public string hedefDepoId { get; set; }
            public int sira { get; set; }
        }
        private void MatristenSecimYap()
        {
            try
            {
                frmStokTransfer.Freeze(true);
                if (oMatrix.RowCount > 0 && edtKalemKodu.Value != "")
                {
                    string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                    var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                    //where (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_5" select new XElement(y.Element("Value"))).First().Value == edtKalemKodu.Value.ToString()
                                select new MatrisVerisi
                                {
                                    sec = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                                    muhatapKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value,
                                    muhatapAdi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value,
                                    belgeNo = Convert.ToInt32((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value),
                                    satirNo = Convert.ToInt32((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_4" select new XElement(y.Element("Value"))).First().Value),
                                    kalemKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_5" select new XElement(y.Element("Value"))).First().Value,
                                    kalemAdi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_6" select new XElement(y.Element("Value"))).First().Value,
                                    miktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_7" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                    acikMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_8" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                    transferMik = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                    kaynakDepo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_10" select new XElement(y.Element("Value"))).First().Value,
                                    hedefDepo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_11" select new XElement(y.Element("Value"))).First().Value,
                                    kaynakDepoYeri = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_12" select new XElement(y.Element("Value"))).First().Value,
                                    hedefDepoYeri = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_13" select new XElement(y.Element("Value"))).First().Value,
                                    kaynakDepoId = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_14" select new XElement(y.Element("Value"))).First().Value,
                                    hedefDepoId = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_15" select new XElement(y.Element("Value"))).First().Value,
                                }).ToList();

                    if (rows.Where(x => x.kalemKodu == edtKalemKodu.Value.ToString()).Count() > 0)
                    {
                        //foreach (var item in rows)
                        //{
                        //   rows.Where(x => x.kalemKodu == edtKalemKodu.Value.ToString()).ToList().ForEach(y => y.sec = "Y");

                        //}

                        rows.Where(x => x.kalemKodu == edtKalemKodu.Value.ToString()).ToList().ForEach(y => y.sec = "Y");

                        #region kalem kodune eşit olanları filtreler ve seçeer
                        string xmlRow = @"<Row><Cells><Cell><ColumnUid>Sec</ColumnUid><Value>{0}</Value></Cell><Cell><ColumnUid>CardCode</ColumnUid><Value>{1}</Value></Cell><Cell><ColumnUid>CardName</ColumnUid><Value>{2}</Value></Cell><Cell><ColumnUid>DocEntry</ColumnUid><Value>{3}</Value></Cell><Cell><ColumnUid>LineNum</ColumnUid><Value>{4}</Value></Cell><Cell><ColumnUid>ItemCode</ColumnUid><Value>{5}</Value></Cell><Cell><ColumnUid>Dscription</ColumnUid><Value>{6}</Value></Cell><Cell><ColumnUid>Quantity</ColumnUid><Value>{7}</Value></Cell><Cell><ColumnUid>OpenQty</ColumnUid><Value>{8}</Value></Cell><Cell><ColumnUid>TransferMik</ColumnUid><Value>{9}</Value></Cell><Cell><ColumnUid>FromWhsCod</ColumnUid><Value>{10}</Value></Cell><Cell><ColumnUid>WhsCode</ColumnUid><Value>{11}</Value></Cell><Cell><ColumnUid>U_KaynakDYeri</ColumnUid><Value>{12}</Value></Cell><Cell><ColumnUid>U_HedefDYeri</ColumnUid><Value>{13}</Value></Cell><Cell><ColumnUid>KaynakDepoId</ColumnUid><Value>{14}</Value></Cell><Cell><ColumnUid>HedefDepoId</ColumnUid><Value>{15}</Value></Cell></Cells></Row>";

                        //string rows2 = string.Join("", rows.Where(x => x.kalemKodu == edtKalemKodu.Value.ToString()).Select(y => string.Format(xmlRow, y.sec = "Y", y.muhatapKodu, y.muhatapAdi.Replace("&", ""), y.belgeNo, y.satirNo, y.kalemKodu, y.kalemAdi.Replace("&", ""), y.miktar, y.acikMiktar, y.transferMik, y.kaynakDepo, y.hedefDepo)));

                        string rows2 = string.Join("", rows.Select(y => string.Format(xmlRow, y.sec, y.muhatapKodu, y.muhatapAdi.Replace("&", ""), y.belgeNo, y.satirNo, y.kalemKodu, y.kalemAdi.Replace("&", ""), HelperClass.parseNumber_Seperator.setDoubleVal(y.miktar.ToString()), HelperClass.parseNumber_Seperator.setDoubleVal(y.acikMiktar.ToString()), HelperClass.parseNumber_Seperator.setDoubleVal(y.transferMik.ToString()), y.kaynakDepo, y.hedefDepo, y.kaynakDepoYeri, y.hedefDepoYeri, y.kaynakDepoId, y.hedefDepoId)));

                        string data = string.Format(xmlformat, rows2);

                        oDataTable.LoadSerializedXML(SAPbouiCOM.BoDataTableXmlSelect.dxs_All, data);

                        oMatrix.Clear();

                        oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "Sec");
                        oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "CardCode");
                        oMatrix.Columns.Item("Col_2").DataBind.Bind("DATA", "CardName");
                        oMatrix.Columns.Item("Col_3").DataBind.Bind("DATA", "DocEntry");
                        oMatrix.Columns.Item("Col_4").DataBind.Bind("DATA", "LineNum");
                        oMatrix.Columns.Item("Col_5").DataBind.Bind("DATA", "ItemCode");
                        oMatrix.Columns.Item("Col_6").DataBind.Bind("DATA", "Dscription");
                        oMatrix.Columns.Item("Col_7").DataBind.Bind("DATA", "Quantity");
                        oMatrix.Columns.Item("Col_8").DataBind.Bind("DATA", "OpenQty");
                        oMatrix.Columns.Item("Col_9").DataBind.Bind("DATA", "TransferMik");
                        oMatrix.Columns.Item("Col_10").DataBind.Bind("DATA", "FromWhsCod");
                        oMatrix.Columns.Item("Col_11").DataBind.Bind("DATA", "WhsCode");
                        oMatrix.Columns.Item("Col_12").DataBind.Bind("DATA", "U_KaynakDYeri");
                        oMatrix.Columns.Item("Col_13").DataBind.Bind("DATA", "U_HedefDYeri");
                        oMatrix.Columns.Item("Col_14").DataBind.Bind("DATA", "KaynakDepoId");
                        oMatrix.Columns.Item("Col_15").DataBind.Bind("DATA", "HedefDepoId");
                        oMatrix.LoadFromDataSource();
                        #endregion  

                        //oMatrix.SelectRow(pVal.Row, true, true);

                    }

                }
            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox("Hata oluştu." + ex.Message);
            }

            finally
            {
                frmStokTransfer.Freeze(false);

            }
        }

        private void SatirKapat()
        {
            try
            {
                string xml2 = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);

                var rows2 = (from x in XDocument.Parse(xml2).Descendants("Row")
                             select new MatrisVerisi
                             {
                                 acikMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_8" select new XElement(y.Element("Value"))).First().Value),
                                 sira = x.ElementsBeforeSelf().Count() + 1,
                             }).ToList();


                if (rows2.Where(x => x.acikMiktar == 0).Count() > 0)
                {
                    //List<int> list = new List<int>();
                    //list.AddRange(rows.Where(x => x.acikMiktar == 0).Select(x => x.sira).ToList());

                    rows2 = rows2.Where(x => x.acikMiktar == 0).ToList();
                    foreach (var item in rows2)
                    {
                        oMatrix.CommonSetting.SetRowEditable(item.sira, false);
                    }
                }
            }
            catch (Exception)
            {
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
                    if (pVal.ItemUID == "Item_1" && !pVal.BeforeAction)
                    {
                        MatristenSecimYap();
                    }
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
                        Listele();
                    }
                    else if (pVal.ItemUID == "Item_2" && !pVal.BeforeAction)
                    {
                        MatristenSecimYap();
                    }
                    else if (pVal.ItemUID == "Item_4" && !pVal.BeforeAction)
                    {
                        try
                        {
                            string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                            var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                        where (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value == "Y"
                                        select new
                                        {
                                            muhatapKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value,
                                            belgeNo = Convert.ToInt32((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value),
                                            satirNo = Convert.ToInt32((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_4" select new XElement(y.Element("Value"))).First().Value),
                                            kalemKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_5" select new XElement(y.Element("Value"))).First().Value,
                                            transferMik = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            kaynakDepo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_10" select new XElement(y.Element("Value"))).First().Value,
                                            hedefDepo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_11" select new XElement(y.Element("Value"))).First().Value,
                                            kaynakDepoId = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_14" select new XElement(y.Element("Value"))).First().Value,
                                            hedefDepoId = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_15" select new XElement(y.Element("Value"))).First().Value,
                                        }).ToList();

                            if (rows.Count > 0)
                            {
                                #region stok nakli talebini,stok nakline çevirir
                                SAPbobsCOM.StockTransfer oStockTransfer = (SAPbobsCOM.StockTransfer)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oStockTransfer);

                                string msj = "";
                                int i = 0;
                                foreach (var item in rows)
                                {
                                    if (item.transferMik > 0)
                                    {
                                        oStockTransfer.DocDate = DateTime.Now;
                                        //oDocuments.Comments = inventoryGenEntry.Comments;
                                        oStockTransfer.CardCode = item.muhatapKodu;

                                        if (item.belgeNo.ToString() != "" && item.belgeNo != 0)
                                        {
                                            oStockTransfer.Lines.BaseEntry = item.belgeNo;
                                            //oStockTransfer.Lines.BaseType = Convert.ToInt32(1250000001);
                                            oStockTransfer.Lines.BaseType = SAPbobsCOM.InvBaseDocTypeEnum.InventoryTransferRequest;
                                            oStockTransfer.Lines.BaseLine = item.satirNo;
                                        }
                                        //else
                                        //{
                                        //    oDocuments.Lines.ItemCode = item.kalemkodu;
                                        //}

                                        oStockTransfer.Lines.Quantity = item.transferMik;

                                        #region depo yeri tayini için eklendi
                                        i = 0;

                                        if (item.kaynakDepoId != null && item.kaynakDepoId != "") //BinCode_from
                                        {
                                            oStockTransfer.Lines.BinAllocations.SetCurrentLine(i);
                                            oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = i;
                                            oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batFromWarehouse;
                                            oStockTransfer.Lines.BinAllocations.BinAbsEntry = Convert.ToInt32(item.kaynakDepoId);
                                            oStockTransfer.Lines.BinAllocations.Quantity = Convert.ToInt32(item.transferMik);
                                            oStockTransfer.Lines.BinAllocations.Add();
                                        }

                                        //i = 1; //invalid row oluyordu kaldırıldı 02.06.2022 chn

                                        if (item.hedefDepoId != null && item.hedefDepoId != "") //BinCode_to
                                        {
                                            oStockTransfer.Lines.BinAllocations.SetCurrentLine(i);
                                            oStockTransfer.Lines.BinAllocations.SerialAndBatchNumbersBaseLine = i;
                                            oStockTransfer.Lines.BinAllocations.BinActionType = SAPbobsCOM.BinActionTypeEnum.batToWarehouse;
                                            oStockTransfer.Lines.BinAllocations.BinAbsEntry = Convert.ToInt32(item.hedefDepoId);
                                            oStockTransfer.Lines.BinAllocations.Quantity = Convert.ToInt32(item.transferMik);
                                            oStockTransfer.Lines.BinAllocations.Add();
                                        }
                                        #endregion


                                        if (item.kaynakDepo != null && item.kaynakDepo != "")
                                        {
                                            oStockTransfer.Lines.FromWarehouseCode = item.kaynakDepo;
                                        }

                                        if (item.hedefDepo != null && item.hedefDepo != "")
                                        {
                                            oStockTransfer.Lines.WarehouseCode = item.hedefDepo;
                                        }


                                        oStockTransfer.Lines.Add();

                                        i = 0;

                                        int retval = oStockTransfer.Add();

                                        if (retval != 0)
                                        {
                                            //Handler.SAPApplication.MessageBox("Hata oluştu." + ConstVariables.oCompanyObject.GetLastErrorDescription()); 
                                            msj += item.kalemKodu + " kalemi için stok nakli yapılamadı." + ConstVariables.oCompanyObject.GetLastErrorDescription();
                                            msj += Environment.NewLine;
                                        }
                                        else
                                        {
                                            //Handler.SAPApplication.MessageBox("Kayıt başarılı.");  
                                            msj += item.kalemKodu + " kalemi için, " + item.kaynakDepo + " kaynak depodan, " + item.hedefDepo + " hedef depoya " + item.transferMik + " miktar stok nakli gerçekleştirilmiştir.";
                                            msj += Environment.NewLine;

                                            Listele();
                                        }
                                    }
                                    #endregion 
                                }
                                if (msj != "")
                                {
                                    Handler.SAPApplication.MessageBox(msj);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox("Hata oluştu." + ex.Message);
                        }

                        //Listele();

                    }
                    else if (pVal.ItemUID == "Item_5" && !pVal.BeforeAction)
                    {
                        try
                        {
                            frmStokTransfer.Close();
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (pVal.ItemUID == "Item_3" && pVal.BeforeAction)
                    {
                        try
                        {
                            oMatrix.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_Auto;
                            oMatrix.SelectRow(pVal.Row, true, true);
                        }
                        catch (Exception ex)
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
                    if (pVal.ItemUID == "Item_3" && pVal.ColUID == "Col_9" && !pVal.BeforeAction)
                    {
                        try
                        {
                            double acikmiktar =  HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_8").Cells.Item(pVal.Row).Specific).Value);
                            double transfermik = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value);

                            if (transfermik > acikmiktar)
                            {
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = 0.ToString();
                                Handler.SAPApplication.MessageBox("Açık miktardan fazla miktar girişi yapılamaz.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox("Hata oluştu." + ex.Message);
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
                    try
                    {
                        //SAPbouiCOM.Form form = Application.SBO_Application.Forms.Item(formUID);
                        //SAPbouiCOM.Form form = Handler.SAPApplication.Forms.ActiveForm;
                        Handler.SAPApplication.ActivateMenuItem("1300");

                        //Item_0 is the ID of the Grouper
                        //var item = form.Items.Item("Item_3");
                        //item.Width = Relationship Width;
                        //item.Height = Relative Height;

                    }
                    catch (Exception)
                    {
                    }
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
                    if (pVal.ItemUID == "Item_3" && pVal.ColUID == "Col_12" && pVal.BeforeAction)
                    {
                        SAPbouiCOM.IChooseFromListEvent oCFLEvento = default(SAPbouiCOM.IChooseFromListEvent);
                        oCFLEvento = (SAPbouiCOM.IChooseFromListEvent)pVal;

                        SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
                        oCFL = frmStokTransfer.ChooseFromLists.Item(oCFLEvento.ChooseFromListUID);

                        SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);
                        SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                        SAPbouiCOM.Conditions oEmptyConts = new SAPbouiCOM.Conditions();

                        oCFL.SetConditions(oEmptyConts);
                        oCons = oCFL.GetConditions();

                        oCon = oCons.Add();
                        oCon.Alias = "Disabled";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        oCon.CondVal = "N";

                        oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                        oCon = oCons.Add();
                        oCon.Alias = "WhsCode";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;

                        string kaynakdepo = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_10").Cells.Item(pVal.Row).Specific).Value.ToString();
                        oCon.CondVal = kaynakdepo;

                        oCFL.SetConditions(oCons);
                    }
                    else if (pVal.ItemUID == "Item_3" && pVal.ColUID == "Col_12" && !pVal.BeforeAction)
                    {
                        try
                        {
                            SAPbouiCOM.DataTable oDataTable = ((SAPbouiCOM.ChooseFromListEvent)pVal).SelectedObjects;
                            string Val = "";
                            Val = oDataTable.GetValue("AbsEntry", 0).ToString();


                            try
                            {
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_12").Cells.Item(pVal.Row).Specific).Value = Val;
                            }
                            catch (Exception)
                            {
                            }
                            //var asdas = oDataTable.SerializeAsXML(BoDataTableXmlSelect.dxs_All);

                            //Val = oDataTable.GetValue("firstName", 0).ToString();

                            //string val2 = oDataTable.GetValue("lastName", 0).ToString();

                            try
                            {
                                //EdtKullaniciAdi.Value = Val + " " + val2;
                            }
                            catch (Exception)
                            {
                            }

                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (pVal.ItemUID == "Item_3" && pVal.ColUID == "Col_13" && pVal.BeforeAction)
                    {
                        SAPbouiCOM.IChooseFromListEvent oCFLEvento = default(SAPbouiCOM.IChooseFromListEvent);
                        oCFLEvento = (SAPbouiCOM.IChooseFromListEvent)pVal;

                        SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
                        oCFL = frmStokTransfer.ChooseFromLists.Item(oCFLEvento.ChooseFromListUID);

                        SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);
                        SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                        SAPbouiCOM.Conditions oEmptyConts = new SAPbouiCOM.Conditions();

                        oCFL.SetConditions(oEmptyConts);
                        oCons = oCFL.GetConditions();

                        oCon = oCons.Add();
                        oCon.Alias = "Disabled";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        oCon.CondVal = "N";

                        oCon.Relationship = SAPbouiCOM.BoConditionRelationship.cr_AND;

                        oCon = oCons.Add();
                        oCon.Alias = "WhsCode";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;

                        string kaynakdepo = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_11").Cells.Item(pVal.Row).Specific).Value.ToString();
                        oCon.CondVal = kaynakdepo;

                        oCFL.SetConditions(oCons);
                    }
                    else if (pVal.ItemUID == "Item_3" && pVal.ColUID == "Col_13" && !pVal.BeforeAction)
                    {
                        try
                        {
                            SAPbouiCOM.DataTable oDataTable = ((SAPbouiCOM.ChooseFromListEvent)pVal).SelectedObjects;
                            string Val = "";
                            Val = oDataTable.GetValue("AbsEntry", 0).ToString();


                            try
                            {
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_13").Cells.Item(pVal.Row).Specific).Value = Val;
                            }
                            catch (Exception)
                            {
                            }
                            //var asdas = oDataTable.SerializeAsXML(BoDataTableXmlSelect.dxs_All);

                            //Val = oDataTable.GetValue("firstName", 0).ToString();

                            //string val2 = oDataTable.GetValue("lastName", 0).ToString();

                            try
                            {
                                //EdtKullaniciAdi.Value = Val + " " + val2;
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