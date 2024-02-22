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
    public class ButtonParametre
    {
        [ItemAtt(AIFConn.BtnParamUID)]
        public SAPbouiCOM.Form frmButonParametre;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.EditText EdtKullaniciKodu;

        [ItemAtt("Item_20")]
        public SAPbouiCOM.EditText EdtKullaniciAdi;

        [ItemAtt("Item_14")]
        public SAPbouiCOM.EditText edtDocEntry;

        [ItemAtt("1")]
        public SAPbouiCOM.Button btnAddOrUpdate;

        [ItemAtt("Item_2")]
        public SAPbouiCOM.CheckBox chkSipMalGrs;
        [ItemAtt("Item_3")]
        public SAPbouiCOM.CheckBox chkSiparissizMalGrs;
        [ItemAtt("Item_4")]
        public SAPbouiCOM.CheckBox chkBelgesizMalGrs;
        [ItemAtt("Item_5")]
        public SAPbouiCOM.CheckBox chkTalepsizDepNak;
        [ItemAtt("Item_6")]
        public SAPbouiCOM.CheckBox chkTalebeBagDepNak;
        [ItemAtt("Item_7")]
        public SAPbouiCOM.CheckBox chkDepSayim;
        [ItemAtt("Item_8")]
        public SAPbouiCOM.CheckBox chkBelgMalCikis;
        [ItemAtt("Item_9")]
        public SAPbouiCOM.CheckBox chkSipBagTes;
        [ItemAtt("Item_10")]
        public SAPbouiCOM.CheckBox chkSiprsszTeslmt;
        [ItemAtt("Item_11")]
        public SAPbouiCOM.CheckBox chkBarkodOls;
        [ItemAtt("Item_12")]
        public SAPbouiCOM.CheckBox chkUrtMalCikis;
        [ItemAtt("Item_13")]
        public SAPbouiCOM.CheckBox chkUrtdenMalGiris;
        [ItemAtt("Item_16")]
        public SAPbouiCOM.CheckBox chkMustFatIade;
        [ItemAtt("Item_17")]
        public SAPbouiCOM.CheckBox chkTeslimtIade;
        [ItemAtt("Item_21")]
        public SAPbouiCOM.CheckBox chkRaporlar;
        [ItemAtt("Item_22")]
        public SAPbouiCOM.CheckBox chkTalepKabul;
        [ItemAtt("Item_23")]
        public SAPbouiCOM.CheckBox chkSatistanIade;

        [ItemAtt("Item_24")]
        public SAPbouiCOM.CheckBox chkCekmeList;
        [ItemAtt("Item_25")]
        public SAPbouiCOM.CheckBox chkPaletYapma;
        [ItemAtt("Item_26")]
        public SAPbouiCOM.CheckBox chkKonteynerYapma;

        [ItemAtt("Item_19")]
        public SAPbouiCOM.StaticText oStaticSube;
        [ItemAtt("Item_27")]
        public SAPbouiCOM.Button oBtnSube;

        [ItemAtt("Item_28")]
        public SAPbouiCOM.Matrix oMatrixSubeTayin;

        [ItemAtt("Item_29")]
        public SAPbouiCOM.CheckBox chkMagazacilikIslemleri;

        [ItemAtt("Item_30")]
        public SAPbouiCOM.CheckBox chkIadeTalep;

        public void LoadForms()
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.BtnParamFrmXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.BtnParamFrmXML));
            Functions.CreateUserOrSystemFormComponent<ButtonParametre>(AIFConn.BtnParam);

            //chkSipMalGrs.Item.Visible = false;
            //chkSiparissizMalGrs.Item.Visible = false;
            //chkBelgesizMalGrs.Item.Visible = false;
            //chkTalepsizDepNak.Item.Visible = false;
            //chkTalebeBagDepNak.Item.Visible = false;
            //chkDepSayim.Item.Visible = false;
            //chkBelgMalCikis.Item.Visible = false;
            //chkSipBagTes.Item.Visible = false;
            //chkSiprsszTeslmt.Item.Visible = false;
            //chkBarkodOls.Item.Visible = false;
            //chkUrtMalCikis.Item.Visible = false;
            //chkUrtdenMalGiris.Item.Visible = false;
            //chkMustFatIade.Item.Visible = false;
            //chkTeslimtIade.Item.Visible = false;
            //chkRaporlar.Item.Visible = false;
            //chkTalepKabul.Item.Visible = false;
            //chkSatistanIade.Item.Visible = false;
            //chkCekmeList.Item.Visible = false;
            //chkPaletYapma.Item.Visible = false;
            //chkKonteynerYapma.Item.Visible = false;

            InitForms();
        }
        public SAPbouiCOM.DataTable oDataTable = null;
        public void InitForms()
        {
            try
            {
                frmButonParametre.Freeze(true);

                frmButonParametre.EnableMenu("1283", false);
                frmButonParametre.EnableMenu("1284", false);
                frmButonParametre.EnableMenu("1286", false);

                oDataTable = frmButonParametre.DataSources.DataTables.Add("DATA");

                ConstVariables.oRecordset.DoQuery("Select * from \"@AIF_WMS_GNLPRM\" where \"U_SubeSecimi\" ='Y' ");
                if (ConstVariables.oRecordset.RecordCount >0)
                {
                    oStaticSube.Item.Visible = true;
                    oBtnSube.Item.Visible = true;
                    oMatrixSubeTayin.Item.Visible = true;
                }
                else
                {
                    oStaticSube.Item.Visible = false;
                    oBtnSube.Item.Visible = false;
                    oMatrixSubeTayin.Item.Visible = false;
                }

                ConstVariables.oRecordset.DoQuery("Select \"U_ButonAdi\",\"U_AktfPsf\" from \"@AIF_WMS_CONSTRNG1\" ");

                if (ConstVariables.oRecordset.RecordCount > 0)
                {
                    while (!ConstVariables.oRecordset.EoF)
                    {
                        string butonadi = ConstVariables.oRecordset.Fields.Item("U_ButonAdi").Value.ToString();
                        string butondurum = ConstVariables.oRecordset.Fields.Item("U_AktfPsf").Value.ToString();

                        if (butonadi == "SiparisliMalGirisi" && butondurum == "Y")
                        {
                            chkSipMalGrs.Item.Visible = true;
                        }

                        if (butonadi == "SiparissizMalGirisi" && butondurum == "Y")
                        {
                            chkSiparissizMalGrs.Item.Visible = true;
                        }

                        if (butonadi == "BelgesizMalGirisi" && butondurum == "Y")
                        {
                            chkBelgesizMalGrs.Item.Visible = true;
                        }

                        if (butonadi == "TalepsizDepoNakli" && butondurum == "Y")
                        {
                            chkTalepsizDepNak.Item.Visible = true;
                        }

                        if (butonadi == "TalebeBagliDepoNakli" && butondurum == "Y")
                        {
                            chkTalebeBagDepNak.Item.Visible = true;
                        }

                        if (butonadi == "DepoSayimi" && butondurum == "Y")
                        {
                            chkDepSayim.Item.Visible = true;
                        }

                        if (butonadi == "BelgesizMalCikisi" && butondurum == "Y")
                        {
                            chkBelgMalCikis.Item.Visible = true;
                        }

                        if (butonadi == "SipariseBagliTeslimat" && butondurum == "Y")
                        {
                            chkSipBagTes.Item.Visible = true;
                        }

                        if (butonadi == "SiparissizTeslimat" && butondurum == "Y")
                        {
                            chkSiprsszTeslmt.Item.Visible = true;
                        }

                        if (butonadi == "BarkodOlustur" && butondurum == "Y")
                        {
                            chkBarkodOls.Item.Visible = true;
                        }  
                        if (butonadi == "UretimeMalCikisi" && butondurum == "Y")
                        {
                            chkUrtMalCikis.Item.Visible = true;
                        }

                        if (butonadi == "UretimdenMalGirisi" && butondurum == "Y")
                        {
                            chkUrtdenMalGiris.Item.Visible = true;
                        }

                        if (butonadi == "MusteriFaturaIadesi" && butondurum == "Y")
                        {
                            chkMustFatIade.Item.Visible = true;
                        } 
                        if (butonadi == "TeslimatIadesi" && butondurum == "Y")
                        {
                            chkTeslimtIade.Item.Visible = true;
                        }
                        if (butonadi == "Raporlar" && butondurum == "Y")
                        {
                            chkRaporlar.Item.Visible = true;
                        }

                        if (butonadi == "TalepKabul" && butondurum == "Y")
                        {
                            chkTalepKabul.Item.Visible = true;
                        }

                        if (butonadi == "SatistanIade" && butondurum == "Y")
                        {
                            chkSatistanIade.Item.Visible = true;
                        } 
                        if (butonadi == "CekmeListesi" && butondurum == "Y")
                        {
                            chkCekmeList.Item.Visible = true;
                        }

                        if (butonadi == "PaletYapma" && butondurum == "Y")
                        {
                            chkPaletYapma.Item.Visible = true;
                        }

                        if (butonadi == "KonteynerYapma" && butondurum == "Y")
                        {
                            chkKonteynerYapma.Item.Visible = true;
                        }

                        if (butonadi == "MagazacilikIslemleri" && butondurum == "Y")
                        {
                            chkMagazacilikIslemleri.Item.Visible = true;
                        }

                        if (butonadi == "IadeTalepleri" && butondurum == "Y")
                        {
                            chkIadeTalep.Item.Visible = true;
                        }

                        ConstVariables.oRecordset.MoveNext();
                    } 
                }

                ConstVariables.oRecordset.DoQuery("Select TOP 1 \"DocEntry\" from \"@AIF_WMS_BTN\" as T0");

                //if (ConstVariables.oRecordset.RecordCount > 0)
                //{
                //    frmButonParametre.Mode = BoFormMode.fm_FIND_MODE;
                //    edtDocEntry.Value = ConstVariables.oRecordset.Fields.Item("DocEntry").Value.ToString();
                //    btnAddOrUpdate.Item.Click();


                //    //DataTable dataTable = frmSirketBilgileri.DataSources.DataTables.Add("DATA");
                //    //dataTable.Columns.Add("U_ButonAdi", BoFieldsType.ft_Text, DataSize: 150);
                //    //dataTable.Columns.Add("U_AktfPsf", BoFieldsType.ft_Text, DataSize: 1);

                //    //SAPbouiCOM.ColumnClass oColumn = (SAPbouiCOM.ColumnClass)oMatrixButonParam.Columns.Item("Col_0");
                //    //oColumn.DataBind.Bind("DATA", "U_ButonAdi");

                //    //dataTable.Rows.Clear();
                //    //dataTable.Rows.Add();
                //    //dataTable.SetValue("U_ButonAdi", 0, "Chntst");
                //    //oMatrixButonParam.LoadFromDataSource();
                //    //oMatrixButonParam.LoadFromDataSourceEx(); 


                //}
            }
            catch (Exception ex)
            {
            }

            finally
            {
                frmButonParametre.Freeze(false);
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
                    if (pVal.ItemUID == "Item_27" && !pVal.BeforeAction)
                    {
                        try
                        { 
                            AIFConn.SubeTayin.LoadForms();
                            //frmButonParametre.DataSources.DBDataSources.Item("@AIF_WMS_BTN1").Clear();
                            //oMatrixSubeTayin.Clear();

                            if (frmButonParametre.Mode == BoFormMode.fm_OK_MODE)
                            {
                                frmButonParametre.Mode = BoFormMode.fm_UPDATE_MODE;
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
                        oCFL = frmButonParametre.ChooseFromLists.Item(oCFLEvento.ChooseFromListUID);

                        SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);
                        SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                        SAPbouiCOM.Conditions oEmptyConts = new SAPbouiCOM.Conditions();

                        oCFL.SetConditions(oEmptyConts);
                        oCons = oCFL.GetConditions();

                        oCon = oCons.Add();
                        oCon.Alias = "Active";
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
                            Val = oDataTable.GetValue("empID", 0).ToString();

                            ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                            ConstVariables.oRecordset.DoQuery("Select * from \"@AIF_WMS_BTN\" where \"U_UserCode\"='" + Val + "'");

                            if (ConstVariables.oRecordset.RecordCount > 0)
                            {
                                Handler.SAPApplication.MessageBox(string.Format("{0} kullanıcısı için daha önce giriş yapılmıştır. Numarası {1}'dir. Tekrar giriş yapılamaz.", oDataTable.GetValue("firstName", 0).ToString() + " " + oDataTable.GetValue("lastName", 0).ToString(), ConstVariables.oRecordset.Fields.Item("DocEntry").Value.ToString()));
                                //frmButonParametre.Mode = BoFormMode.fm_FIND_MODE;
                                //edtDocEntry.Value = ConstVariables.oRecordset.Fields.Item("DocEntry").Value.ToString();
                                //btnAddOrUpdate.Item.Click();
                                return false;
                            }

                            try
                            {
                                EdtKullaniciKodu.Value = Val;
                            }
                            catch (Exception)
                            {
                            }
                            //var asdas = oDataTable.SerializeAsXML(BoDataTableXmlSelect.dxs_All);

                            Val = oDataTable.GetValue("firstName", 0).ToString();

                            string val2 = oDataTable.GetValue("lastName", 0).ToString();

                            try
                            {
                                EdtKullaniciAdi.Value = Val + " " + val2;
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