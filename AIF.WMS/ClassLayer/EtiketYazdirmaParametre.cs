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
    public class EtiketYazdirmaParametre
    {
        [ItemAtt(AIFConn.EtiketYazdirmaParametreUID)]
        public SAPbouiCOM.Form frmEtiketYazdirmaParametre;

        [ItemAtt("Item_0")]
        public SAPbouiCOM.Matrix oMatrix;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.Button oBtnSec;

        [ItemAtt("Item_2")]
        public SAPbouiCOM.Button oBtnIptal;

        string kalemKodu = "";
        private SAPbouiCOM.DataTable oDataTable = null;

        public void LoadForms(string _kalemKodu)
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.EtiketYazdirmaParametreXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.EtiketYazdirmaParametreXML));
            Functions.CreateUserOrSystemFormComponent<EtiketYazdirmaParametre>(AIFConn.EtYazParam);
            kalemKodu = _kalemKodu;
            InitForms();
        }

        public void InitForms()
        {
            try
            {
                frmEtiketYazdirmaParametre.Freeze(true);

                frmEtiketYazdirmaParametre.EnableMenu("1283", false);
                frmEtiketYazdirmaParametre.EnableMenu("1284", false);
                frmEtiketYazdirmaParametre.EnableMenu("1286", false);

                oDataTable = frmEtiketYazdirmaParametre.DataSources.DataTables.Add("DATA");

                ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                if (kalemKodu != "")
                {
                    string sql = "";
                    //ConstVariables.oRecordset.DoQuery("SELECT T0.\"CardCode\", T1.\"CardName\", T0.\"Substitute\" FROM OSCN T0  INNER JOIN OCRD T1 ON T0.\"CardCode\" = T1.\"CardCode\" WHERE T0.\"ItemCode\" = '" + kalemKodu + "' ");

                    sql = "SELECT T0.\"CardCode\", T1.\"CardName\", T0.\"Substitute\" FROM OSCN T0  INNER JOIN OCRD T1 ON T0.\"CardCode\" = T1.\"CardCode\" WHERE T0.\"ItemCode\" = '" + kalemKodu + "' ";
                    ConstVariables.oRecordset.DoQuery(sql);
                    if (ConstVariables.oRecordset.RecordCount > 0)
                    {
                        oDataTable.Clear();
                        oDataTable.ExecuteQuery(sql);
                        oMatrix.Clear();

                        oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "CardCode");
                        oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "CardName");
                        oMatrix.Columns.Item("Col_2").DataBind.Bind("DATA", "Substitute");

                        oMatrix.LoadFromDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox("Hata oluştu." + ex.Message);
                return;
            }

            finally
            {
                oMatrix.AutoResizeColumns();
                frmEtiketYazdirmaParametre.Freeze(false);
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
                    if (!pVal.BeforeAction && pVal.ItemUID == "Item_1")
                    {
                        try
                        {
                            int row = oMatrix.GetNextSelectedRow();
                            if (row != -1)
                            {
                                string katalogno = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_2").Cells.Item(row).Specific).Value.ToString();
                                if (katalogno != "")
                                {

                                    AIFConn.EtktYzdr.oEdtMuhKatalogNo.Value = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_2").Cells.Item(row).Specific).Value.ToString();
                                }
                            }
                            else
                            {
                                Handler.SAPApplication.MessageBox("Seçim sırasında hata oluştu.");
                                return false;
                            }


                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox("Hata oluştu." + ex.Message);
                            return false;
                        }
                        finally
                        {
                            frmEtiketYazdirmaParametre.Close();
                        }
                    }
                    else if (!pVal.BeforeAction && pVal.ItemUID == "Item_2")
                    {
                        try
                        {
                            frmEtiketYazdirmaParametre.Close();
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