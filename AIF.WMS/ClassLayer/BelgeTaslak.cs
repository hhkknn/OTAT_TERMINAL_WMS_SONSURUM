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
    public class BelgeTaslak
    {
        [ItemAtt(AIFConn.BelgeTaslak_FormUID)]
        public SAPbouiCOM.Form frmBelgeTaslak;

        [ItemAtt("3")]
        public SAPbouiCOM.Matrix oMatrix;

        [ItemAtt("1")]
        public SAPbouiCOM.Button oButton;

        private static string formuid = "";
        public void LoadForms()
        {
            Functions.CreateUserOrSystemFormComponent<BelgeTaslak>(AIFConn.Sys3002, true, formuid);

            System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
            System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AIF.WMS.FormsView.BelgeTaslak.xml");

            System.IO.StreamReader streamreader = new System.IO.StreamReader(stream, true);
            xmldoc.LoadXml(string.Format(streamreader.ReadToEnd(), formuid));
            Handler.SAPApplication.LoadBatchActions(xmldoc.InnerXml);

            streamreader.Close();

            var cml = frmBelgeTaslak.GetAsXML();
            InitForms();
        }

        public void InitForms()
        {

            //((SAPbouiCOM.Button)frmKalemAnaverileri.Items.Item("Item_0").Specific).Item.Top = ((SAPbouiCOM.Button)frmKalemAnaverileri.Items.Item("2").Specific).Item.Top;
            //((SAPbouiCOM.Button)frmKalemAnaverileri.Items.Item("Item_0").Specific).Item.Width = ((SAPbouiCOM.Button)frmKalemAnaverileri.Items.Item("2").Specific).Item.Width;
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



        private List<loadDocEnrty> LoadDocentryList;//= new List<loadDocEnrty>();

        private class loadDocEnrty
        {
            public string Docentry { get; set; }


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

                    //if (pVal.ItemUID == "1" && !pVal.BeforeAction)
                    //{


                    //    //string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                    //    //var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                    //    //            select new
                    //    //            {
                    //    //                LoadDocEnt = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "1" select new XElement(y.Element("Value"))).First().Value,
                    //    //            }).ToList();


                    //    //var s = rows.Except(LoadDocentryList).ToList();



                    //}

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
                    if (pVal.BeforeAction)
                    {
                        frmBelgeTaslak = Handler.SAPApplication.Forms.Item(pVal.FormUID);
                        formuid = pVal.FormUID;
                        AIFConn.Sys3002.LoadForms();
                    }
                    if (!pVal.BeforeAction)
                    {
                        //LoadDocentryList = new List<loadDocEnrty>();

                        //string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                        //var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                        //            select new loadDocEnrty
                        //            {
                        //                Docentry = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "1" select new XElement(y.Element("Value"))).First().Value,
                        //            }).ToList();
                    }

                    #region MyRegion
                    //else if (!pVal.BeforeAction)
                    //{

                    //    if (Program.mKod == "10TRMN")
                    //    {
                    //        #region KDV COMBOBOX KAPATILDI.KTA DAN KULLANILIYOR
                    //        //try
                    //        //{
                    //        //    SAPbouiCOM.Form oform = Handler.SAPApplication.Forms.Item(pVal.FormUID);

                    //        //    oform.Items.Add("cmbKdv", SAPbouiCOM.BoFormItemTypes.it_COMBO_BOX);
                    //        //    oform.Items.Item("cmbKdv").Left = oform.Items.Item("2").Left + 285;
                    //        //    oform.Items.Item("cmbKdv").Top = oform.Items.Item("2").Top;
                    //        //    oform.Items.Item("cmbKdv").Width = oform.Items.Item("2").Width + 20;
                    //        //    oform.Items.Item("cmbKdv").Enabled = true;
                    //        //    SAPbouiCOM.ComboBox oCmbKdv = (SAPbouiCOM.ComboBox)oform.Items.Item("cmbKdv").Specific;

                    //        //    //oCmb.ca.Caption = "Etiket Yazdır";

                    //        //    oCmbKdv.DataBind.SetBound(true, "OITM", "U_Kdv");
                    //        //    oCmbKdv.ExpandType = SAPbouiCOM.BoExpandType.et_DescriptionOnly;
                    //        //    oCmbKdv.Item.DisplayDesc = true;
                    //        //    //oCmbKdv.Item.Enabled = false;

                    //        //    string sql = "SELECT  T0.\"Code\",T0.\"Name\" FROM OVTG T0 WHERE T0.\"Inactive\" = 'N' ";

                    //        //    oCmbKdv.ValidValues.Add("", "");

                    //        //    ConstVariables.oRecordset.DoQuery(sql);

                    //        //    if (ConstVariables.oRecordset.RecordCount > 0)
                    //        //    {
                    //        //        while (!ConstVariables.oRecordset.EoF)
                    //        //        {
                    //        //            try
                    //        //            {
                    //        //                oCmbKdv.ValidValues.Add(ConstVariables.oRecordset.Fields.Item(0).Value.ToString(), ConstVariables.oRecordset.Fields.Item(1).Value.ToString());
                    //        //            }
                    //        //            catch (Exception)
                    //        //            {
                    //        //            }

                    //        //            ConstVariables.oRecordset.MoveNext();
                    //        //        }
                    //        //    }
                    //        //}
                    //        //catch (Exception ex)
                    //        //{
                    //        //    Handler.SAPApplication.MessageBox("KDV yüklenirken hata oluştu" + ex.Message);
                    //        //    return false;
                    //        //}
                    //    }
                    //    #endregion

                    //    if (Program.mKod == "30TRMN")
                    //    {
                    //        try
                    //        {
                    //            SAPbouiCOM.Form oform = Handler.SAPApplication.Forms.Item(pVal.FormUID);

                    //            oform.Items.Add("btnEtktYaz", SAPbouiCOM.BoFormItemTypes.it_BUTTON);
                    //            oform.Items.Item("btnEtktYaz").Left = oform.Items.Item("2").Left + 85;
                    //            oform.Items.Item("btnEtktYaz").Top = oform.Items.Item("2").Top;
                    //            oform.Items.Item("btnEtktYaz").Width = oform.Items.Item("2").Width + 20;
                    //            oform.Items.Item("btnEtktYaz").Height = oform.Items.Item("2").Height;
                    //            oform.Items.Item("btnEtktYaz").Enabled = true;
                    //            SAPbouiCOM.Button oBtn = (SAPbouiCOM.Button)oform.Items.Item("btnEtktYaz").Specific;

                    //            oBtn.Caption = "Etiket Yazdır";
                    //        }
                    //        catch (Exception)
                    //        {
                    //        }
                    //    }
                    //} 
                    #endregion
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

            //if (pVal.MenuUID == "1283")
            //{
            //    int row = oMatrix.GetNextSelectedRow();
            //    string DocEntry = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("1").Cells.Item(row).Specific).Value.ToString();
            //    taslakDocEnrty.Add(DocEntry);
            //}
        }

        public void RightClickEvent(ref ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }
    }
}