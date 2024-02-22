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
    public class MalCikisNedeni
    {
        [ItemAtt(AIFConn.MalCikisNedeniUID)]
        public SAPbouiCOM.Form frmMalCikisNedeni;

        [ItemAtt("Item_0")]
        public SAPbouiCOM.Matrix oMatrix; 

        public void LoadForms()
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.frmMalCikisNedeniXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.frmMalCikisNedeniXML));
            Functions.CreateUserOrSystemFormComponent<MalCikisNedeni>(AIFConn.MalCksNdn);

            InitForms();
        }
         
        public void InitForms()
        {
            try
            {
                frmMalCikisNedeni.Freeze(true);
                 
                frmMalCikisNedeni.EnableMenu("1283", false);
                frmMalCikisNedeni.EnableMenu("1284", false);
                frmMalCikisNedeni.EnableMenu("1286", false);

                KayitGetir(); 

            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox(ex.Message);
            }

            finally
            {
                frmMalCikisNedeni.Freeze(false);
            }
        }
        private void KayitGetir()
        {
            try
            {
                frmMalCikisNedeni.Freeze(true);

                string sql = "Select T0.\"DocEntry\" from \"@AIF_WMS_MALCIKISNDN\" as T0 ";

                ConstVariables.oRecordset.DoQuery(sql);

                if (ConstVariables.oRecordset.RecordCount == 0)
                {
                    oMatrix.Clear();
                    oMatrix.AddRow();
                }

                if (ConstVariables.oRecordset.RecordCount > 0)
                {
                    frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Query();
                    oMatrix.LoadFromDataSource();

                    if (oMatrix.RowCount > 0)
                    {
                        frmMalCikisNedeni.Mode = BoFormMode.fm_OK_MODE;
                         
                        frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Clear();
                        oMatrix.Item.AffectsFormMode = false;
                        oMatrix.AddRow();
                        oMatrix.Item.AffectsFormMode = true;
                    }

                }

                oMatrix.AutoResizeColumns();
            }
            catch (Exception)
            {
            }
            finally
            {
                frmMalCikisNedeni.Freeze(false);
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
                    if (BusinessObjectInfo.BeforeAction)
                    {
                        string sonsatir1 = frmMalCikisNedeni.DataSources.DBDataSources.Item(0).GetValue("U_TransferKodu", frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Size - 1);

                        if (sonsatir1 == "")
                        {
                            frmMalCikisNedeni.DataSources.DBDataSources.Item(0).RemoveRecord(frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Size - 1);
                        }

                    }
                    break;

                case BoEventTypes.et_FORM_DATA_UPDATE:
                    if (BusinessObjectInfo.BeforeAction)
                    {
                        string sonsatir1 = frmMalCikisNedeni.DataSources.DBDataSources.Item(0).GetValue("U_TransferKodu", frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Size - 1);

                        if (sonsatir1 == "")
                        {
                            frmMalCikisNedeni.DataSources.DBDataSources.Item(0).RemoveRecord(frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Size - 1);
                        }

                    }
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

        private string val = "";

        public bool SAP_ItemEvent(string FormUID, ref ItemEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;

            switch (pVal.EventType)
            {
                case BoEventTypes.et_ALL_EVENTS:
                    break;

                case BoEventTypes.et_ITEM_PRESSED:
                    if (pVal.ItemUID == "1" && !pVal.BeforeAction)
                    {
                        if (silinecekler.Count > 0)
                        {
                            foreach (var item in silinecekler)
                            {
                                if (item != "")
                                {
                                    ConstVariables.oRecordset.DoQuery("Delete from \"@AIF_WMS_MALCIKISNDN\" where \"DocEntry\" = '" + item + "'");
                                }
                            }

                            #region general data sile silme - docentry ararak devem ediyordu
                            //SAPbobsCOM.GeneralService oGeneralService;

                            //SAPbobsCOM.GeneralData oGeneralData;

                            //SAPbobsCOM.CompanyService sCmp = null;

                            //SAPbobsCOM.GeneralDataParams oGeneralParams = null;

                            //sCmp = ConstVariables.oCompanyObject.GetCompanyService();

                            //oGeneralService = sCmp.GetGeneralService("AIF_WMS_MALCIKISNDN");
                            //oGeneralData = (SAPbobsCOM.GeneralData)oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                            //oGeneralParams = ((SAPbobsCOM.GeneralDataParams)(oGeneralService.GetDataInterface(SAPbobsCOM.GeneralServiceDataInterfaces.gsGeneralDataParams)));

                            //foreach (var item in silinecekler)
                            //{
                            //    if (item != "")
                            //    {
                            //        oGeneralParams.SetProperty("DocEntry", item.ToString());

                            //        //oGeneralData = oGeneralService.GetByParams(oGeneralParams);

                            //        oGeneralService.Delete(oGeneralParams);
                            //    }
                            //} 
                            #endregion
                        }

                        silinecekler = new List<string>();
                        KayitGetir();
                    }
                    break;

                case BoEventTypes.et_KEY_DOWN:
                    //if (pVal.ColUID == "Col_1" && pVal.CharPressed == 9 && !pVal.BeforeAction)
                    //{
                    //    try
                    //    {
                    //        string sonsatir = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_1").Cells.Item(oMatrix.RowCount).Specific).Value.ToString();

                    //        if (sonsatir != "")
                    //        {
                    //            frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Clear();
                    //            oMatrix.AddRow();
                    //            oMatrix.AutoResizeColumns();
                    //            oMatrix.Columns.Item("Col_1").Cells.Item(oMatrix.RowCount).Click();
                    //        }
                    //    }
                    //    catch (Exception)
                    //    {
                    //    }
                    //}
                    break;

                case BoEventTypes.et_GOT_FOCUS:
                    break;

                case BoEventTypes.et_LOST_FOCUS:
                    if (pVal.ColUID == "Col_1")
                    {
                        try
                        {
                            if (((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_1").Cells.Item(oMatrix.RowCount).Specific).Value != "")
                            {
                                frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Clear();
                                oMatrix.AddRow();
                                //oMatrix.Columns.Item("Col_0").Cells.Item(oMatrix.RowCount - 1).Click();
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                    break;

                case BoEventTypes.et_COMBO_SELECT: 
                    break;

                case BoEventTypes.et_CLICK:
                    if (pVal.ItemUID == "1" && !pVal.BeforeAction)
                    {
                        if (oMatrix.RowCount == 0)
                        {
                            BubbleEvent = false;
                        }
                        ConstVariables.oRecordset.DoQuery("Select MAX(\"DocEntry\") from \"@AIF_WMS_MALCIKISNDN\"");
                        int maxDocEntry = Convert.ToInt32(ConstVariables.oRecordset.Fields.Item(0).Value);
                        maxDocEntry++;
                        var xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);

                        for (int i = 1; i <= oMatrix.RowCount; i++)
                        {
                            if (((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_0").Cells.Item(i).Specific).Value.ToString() == "")
                            { 
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_0").Cells.Item(i).Specific).Value = maxDocEntry.ToString();
                                maxDocEntry++;
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
                    if (pVal.ItemUID == "Item_0" && pVal.ColUID =="Col_3" && pVal.BeforeAction)
                    {
                        SAPbouiCOM.IChooseFromListEvent oCFLEvento = default(SAPbouiCOM.IChooseFromListEvent);
                        oCFLEvento = (SAPbouiCOM.IChooseFromListEvent)pVal;

                        SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
                        oCFL = frmMalCikisNedeni.ChooseFromLists.Item(oCFLEvento.ChooseFromListUID);

                        SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);
                        SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                        SAPbouiCOM.Conditions oEmptyConts = new SAPbouiCOM.Conditions();

                        oCFL.SetConditions(oEmptyConts);
                        oCons = oCFL.GetConditions();

                        //oCon = oCons.Add();
                        //oCon.Alias = "ValidFor";
                        //oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        //oCon.CondVal = "Y";

                        //oCon.Relationship = BoConditionRelationship.cr_AND;

                        oCon = oCons.Add();
                        oCon.Alias = "Postable";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_EQUAL;
                        oCon.CondVal = "Y";

                        oCFL.SetConditions(oCons);
                    }
                    else if (pVal.ItemUID == "Item_0" && pVal.ColUID == "Col_3" && !pVal.BeforeAction)
                    {
                        try
                        {
                            SAPbouiCOM.DataTable oDataTable = ((SAPbouiCOM.ChooseFromListEvent)pVal).SelectedObjects;
                            string Val = "";
                            Val = oDataTable.GetValue("AcctCode", 0).ToString();


                            try
                            {
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_3").Cells.Item(pVal.Row).Specific).Value = Val;
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
        List<string> silinecekler = new List<string>();
        public void MenuEvent(ref MenuEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                if (pVal.MenuUID == "AIFRGHTCLK_DeleteRow" && pVal.BeforeAction)
                {
                    int row = oMatrix.GetNextSelectedRow();
                    if (row != -1)
                    {
                        if (((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_0").Cells.Item(row).Specific).Value != "")
                        {
                            silinecekler.Add(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_0").Cells.Item(row).Specific).Value);
                        }

                        oMatrix.DeleteRow(row);

                        if (frmMalCikisNedeni.Mode == BoFormMode.fm_OK_MODE)
                        {
                            frmMalCikisNedeni.Mode = BoFormMode.fm_UPDATE_MODE;
                        }
                    }
                }
                else if (pVal.MenuUID == "AIFRGHTCLK_AddRow" && pVal.BeforeAction)
                {
                    frmMalCikisNedeni.DataSources.DBDataSources.Item(0).Clear();
                    oMatrix.AddRow();
                }
            }
            catch (Exception)
            {
            }
        }

        public void RightClickEvent(ref ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
            try
            {
                var oForm = Handler.SAPApplication.Forms.ActiveForm;

                if (eventInfo.ItemUID != "")
                {
                    try
                    {
                        SAPbouiCOM.Matrix item = (SAPbouiCOM.Matrix)oForm.Items.Item(eventInfo.ItemUID).Specific;
                    }
                    catch (Exception)
                    {
                        Handler.SAPApplication.Menus.RemoveEx("AIFRGHTCLK_DeleteRow");
                        return;
                    }
                }
                else
                {
                    Handler.SAPApplication.Menus.RemoveEx("AIFRGHTCLK_DeleteRow");
                    Handler.SAPApplication.Menus.RemoveEx("AIFRGHTCLK_AddRow");
                    return;
                }

                if (oForm.Mode == SAPbouiCOM.BoFormMode.fm_FIND_MODE)
                {
                    Handler.SAPApplication.Menus.RemoveEx("AIFRGHTCLK_DeleteRow");
                    Handler.SAPApplication.Menus.RemoveEx("AIFRGHTCLK_AddRow");
                    return;
                }
                SAPbouiCOM.MenuItem oMenuItem = default(SAPbouiCOM.MenuItem);

                SAPbouiCOM.Menus oMenus = default(SAPbouiCOM.Menus);

                try
                {
                    SAPbouiCOM.MenuCreationParams oCreationPackage = default(SAPbouiCOM.MenuCreationParams);

                    oCreationPackage = (SAPbouiCOM.MenuCreationParams)Handler.SAPApplication.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams);

                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    try
                    {
                        oCreationPackage.UniqueID = "AIFRGHTCLK_DeleteRow";

                        oCreationPackage.String = "Satır Sil";

                        oCreationPackage.Enabled = true;

                        oMenuItem = Handler.SAPApplication.Menus.Item("1280");

                        oMenus = oMenuItem.SubMenus;

                        oMenus.AddEx(oCreationPackage);
                    }
                    catch
                    {
                    }

                    try
                    {
                        oCreationPackage.UniqueID = "AIFRGHTCLK_AddRow";

                        oCreationPackage.String = "Satır Ekle";

                        oCreationPackage.Enabled = true;

                        oMenuItem = Handler.SAPApplication.Menus.Item("1280");

                        oMenus = oMenuItem.SubMenus;

                        oMenus.AddEx(oCreationPackage);
                    }
                    catch (Exception)
                    {
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}