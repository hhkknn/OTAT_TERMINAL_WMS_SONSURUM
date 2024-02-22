using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Abstarct;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib;
using AIF.ObjectsDLL.Utils;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Handler = AIF.ObjectsDLL.Events.Handler;

namespace AIF.WMS.ClassLayer
{
    public class SubeTayini
    {
        [ItemAtt(AIFConn.SubeTayiniUID)]
        public SAPbouiCOM.Form frmSubeTayini;

        [ItemAtt("Item_0")]
        public SAPbouiCOM.Matrix oMatrixSubeTayin;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.Button btnKaydet;

        [ItemAtt("Item_2")]
        public SAPbouiCOM.Button btnIptal;
        public SAPbouiCOM.DataTable oDataTable;
        public void LoadForms()
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.frmSubeTayiniXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.frmSubeTayiniXML));
            Functions.CreateUserOrSystemFormComponent<SubeTayini>(AIFConn.SubeTayin);

            //oMatrixSubeTayin = _oMatrixSubeTayin;

            InitForms();
        }

        public void InitForms()
        {
            try
            {
                frmSubeTayini.Freeze(true);

                frmSubeTayini.EnableMenu("1283", false);
                frmSubeTayini.EnableMenu("1284", false);
                frmSubeTayini.EnableMenu("1286", false);

                oDataTable = frmSubeTayini.DataSources.DataTables.Add("DATA");


                string sql = "SELECT T0.BPLId as \"U_SubeKodu\",T0.BPLName as \"U_SubeAdi\",'' as \"U_TayinEdildi\"  FROM OBPL T0 WHERE T0.\"Disabled\" = 'N' order by BPLId";
                ConstVariables.oRecordset.DoQuery(sql);

                if (ConstVariables.oRecordset.RecordCount > 0)
                {
                    oDataTable.Clear();
                    oDataTable.ExecuteQuery(sql);

                    oMatrixSubeTayin.Clear();

                    oMatrixSubeTayin.Columns.Item("Col_0").DataBind.Bind("DATA", "U_SubeKodu");
                    oMatrixSubeTayin.Columns.Item("Col_1").DataBind.Bind("DATA", "U_SubeAdi");
                    oMatrixSubeTayin.Columns.Item("Col_2").DataBind.Bind("DATA", "U_TayinEdildi");

                    oMatrixSubeTayin.LoadFromDataSource();
                }

                var xml = AIFConn.BtnParam.oMatrixSubeTayin.SerializeAsXML(BoMatrixXmlSelect.mxs_All);

                var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                            select new
                            {
                                SubeKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                                SubeAdi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value,
                                TayinEdildi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value,
                                //sira = (x.ElementsBeforeSelf().Count() + 1).ToString()

                            }).ToList();



                for (int i = 1; i <= oMatrixSubeTayin.RowCount; i++)
                {
                    string subekodu = ((SAPbouiCOM.EditText)oMatrixSubeTayin.Columns.Item("Col_0").Cells.Item(i).Specific).Value.ToString();

                    string tayin = rows.Where(x => x.SubeKodu == subekodu).Select(y => y.TayinEdildi).FirstOrDefault();

                    if (tayin != null && tayin == "Y")
                    {
                        ((SAPbouiCOM.CheckBox)oMatrixSubeTayin.Columns.Item("Col_2").Cells.Item(i).Specific).Checked = true;
                    }
                }

                #region old
                //if (AIFConn.BtnParam.oMatrixSubeTayin.RowCount > 0)
                //{
                //    var xml = AIFConn.BtnParam.oMatrixSubeTayin.SerializeAsXML(BoMatrixXmlSelect.mxs_All);

                //    var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                //                select new
                //                {
                //                    SubeKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                //                    SubeAdi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value,
                //                    TayinEdildi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value,
                //                    //sira = (x.ElementsBeforeSelf().Count() + 1).ToString()

                //                }).ToList();

                //    int i = 1;
                //    //oMatrixSubeTayin.Clear();

                //    foreach (var item in rows)
                //    {
                //        //oMatrixSubeTayin.AddRow();
                //        if (item.TayinEdildi == "Y")
                //        {

                //        }
                //        ((SAPbouiCOM.EditText)oMatrixSubeTayin.Columns.Item("Col_0").Cells.Item(i).Specific).Value = item.SubeKodu;
                //        ((SAPbouiCOM.EditText)oMatrixSubeTayin.Columns.Item("Col_1").Cells.Item(i).Specific).Value = item.SubeAdi;
                //        if (item.TayinEdildi == "" || item.TayinEdildi == "N")
                //        {
                //            ((SAPbouiCOM.CheckBox)oMatrixSubeTayin.Columns.Item("Col_2").Cells.Item(i).Specific).Checked = false;

                //        }
                //        else
                //        {
                //            ((SAPbouiCOM.CheckBox)oMatrixSubeTayin.Columns.Item("Col_2").Cells.Item(i).Specific).Checked = true;

                //        }
                //        i++;
                //    }


                //sql = "SELECT T0.BPLId as \"U_SubeKodu\"  FROM OBPL T0 ";
                //ConstVariables.oRecordset.DoQuery(sql);

                //if (ConstVariables.oRecordset.RecordCount > 0)
                //{
                //    for (int j = 0; j <= ConstVariables.oRecordset.RecordCount; j++)
                //    { 
                //        ((SAPbouiCOM.CheckBox)oMatrixSubeTayin.Columns.Item("Col_2").Cells.Item(j).Specific).Checked = true;

                //    }
                //} 
                #endregion

                oMatrixSubeTayin.AutoResizeColumns();
            }
            catch (Exception ex)
            {
            }

            finally
            {
                frmSubeTayini.Freeze(false);
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
                    if (pVal.ItemUID == "Item_1" && !pVal.Before_Action)
                    {
                        //AIFConn.BtnParam.oMatrixSubeTayin = oMatrixSubeTayin;

                        if (oMatrixSubeTayin.RowCount > 0)
                        {
                            var xml = oMatrixSubeTayin.SerializeAsXML(BoMatrixXmlSelect.mxs_All);

                            var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                        select new
                                        {
                                            SubeKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                                            SubeAdi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value,
                                            TayinEdildi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value,
                                            //sira = (x.ElementsBeforeSelf().Count() + 1).ToString()

                                        }).ToList();

                            int i = 1;
                            AIFConn.BtnParam.frmButonParametre.DataSources.DBDataSources.Item("@AIF_WMS_BTN1").Clear();
                            AIFConn.BtnParam.oMatrixSubeTayin.Clear();
                            foreach (var item in rows)
                            {
                                AIFConn.BtnParam.oMatrixSubeTayin.AddRow();
                                ((SAPbouiCOM.EditText)AIFConn.BtnParam.oMatrixSubeTayin.Columns.Item("Col_0").Cells.Item(i).Specific).Value = item.SubeKodu;
                                ((SAPbouiCOM.EditText)AIFConn.BtnParam.oMatrixSubeTayin.Columns.Item("Col_1").Cells.Item(i).Specific).Value = item.SubeAdi;
                                if (item.TayinEdildi == "" || item.TayinEdildi == "N")
                                {
                                    ((SAPbouiCOM.CheckBox)AIFConn.BtnParam.oMatrixSubeTayin.Columns.Item("Col_2").Cells.Item(i).Specific).Checked = false;

                                }
                                else
                                {
                                    ((SAPbouiCOM.CheckBox)AIFConn.BtnParam.oMatrixSubeTayin.Columns.Item("Col_2").Cells.Item(i).Specific).Checked = true;

                                }
                                i++;
                            }
                            if (oMatrixSubeTayin.RowCount > 0)
                            {

                            }
                            //oMatrixSubeTayin.AutoResizeColumns();
                        }

                        try
                        {
                            frmSubeTayini.Close();
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else if (pVal.ItemUID == "Item_2" && !pVal.BeforeAction)
                    {
                        try
                        {
                            frmSubeTayini.Close();
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