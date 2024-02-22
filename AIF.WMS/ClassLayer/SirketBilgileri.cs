using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AIF.WMS.ClassLayer
{
    public class SirketBilgileri
    {
        [ItemAtt(AIFConn.SirketBilgileriUID)]
        public SAPbouiCOM.Form frmSirketBilgileri;

        [ItemAtt("Item_9")]
        public SAPbouiCOM.EditText EdtDocEntry;

        [ItemAtt("1")]
        public SAPbouiCOM.Button btnAddOrUpdate;

        [ItemAtt("Item_10")]
        public SAPbouiCOM.EditText EdtSirketKodu;

        [ItemAtt("Item_11")]
        public SAPbouiCOM.EditText EdtSirketAdi;

        [ItemAtt("Item_12")]
        public SAPbouiCOM.EditText EdtLisansServer;

        [ItemAtt("Item_13")]
        public SAPbouiCOM.EditText EdtServer;

        [ItemAtt("Item_14")]
        public SAPbouiCOM.EditText EdtKullaniciKodu;

        [ItemAtt("Item_16")]
        public SAPbouiCOM.ComboBox CmbServerTipi;

        [ItemAtt("Item_18")]
        public SAPbouiCOM.Matrix oMatrixButonParam;

        private string header = @"<?xml version=""1.0"" encoding=""UTF-16"" ?><dbDataSources uid=""@AIF_WMS_CONSTRNG1""><rows>{0}</rows></dbDataSources>";

        private string row = "<row><cells><cell><uid>U_ButonAdi</uid><value>{0}</value></cell><cell><uid>U_AktfPsf</uid><value>{1}</value></cell></cells></row>";
        public void LoadForms()
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.SirketBilgieriFrmXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.SirketBilgieriFrmXML));
            Functions.CreateUserOrSystemFormComponent<SirketBilgileri>(AIFConn.SrktBlg);

            InitForms();
        }

        private class depo
        {
            public string depoadi { get; set; }
            public string aktifpasif { get; set; }
        }

        List<depo> depos = new List<depo>();
        public void InitForms()
        {
            try
            {
                frmSirketBilgileri.Freeze(true);

                frmSirketBilgileri.EnableMenu("1283", false);
                frmSirketBilgileri.EnableMenu("1284", false);
                frmSirketBilgileri.EnableMenu("1286", false);

                if (ConstVariables.oCompanyObject.UserName != "manager")
                {
                    Handler.SAPApplication.MessageBox("Yetkisiz giriş yapılamaz.Lütfen AIFTeam ile iletişime geçiniz.");
                    frmSirketBilgileri.Close();
                }

                #region buton column
                SAPbouiCOM.Column oCol = (SAPbouiCOM.Column)oMatrixButonParam.Columns.Item("Col_0");

                IList<Tuple<string, string>> Buton = new List<Tuple<string, string>>(); 
                   Buton.Add(Tuple.Create("SiparisliMalGirisi", "Siparişli Mal Girişi"));
                   Buton.Add(Tuple.Create("SiparissizMalGirisi", "Siparişsiz Mal Girişi"));
                   Buton.Add(Tuple.Create("BelgesizMalGirisi", "Belgesiz Mal Girişi"));
                   Buton.Add(Tuple.Create("TalepsizDepoNakli", "Talepsiz Depo Nakli"));
                   Buton.Add(Tuple.Create("TalebeBagliDepoNakli", "Talebe Bağlı Depo Nakli"));
                   Buton.Add(Tuple.Create("DepoSayimi", "Depo Sayımı"));
                   Buton.Add(Tuple.Create("BelgesizMalCikisi", "Belgesiz Mal Çıkışı"));
                   Buton.Add(Tuple.Create("SipariseBagliTeslimat", "Siparişe Bağlı Teslimat"));
                   Buton.Add(Tuple.Create("SiparissizTeslimat", "Siparişsiz Teslimat"));
                   Buton.Add(Tuple.Create("BarkodOlustur", "Barkod Oluştur"));
                   Buton.Add(Tuple.Create("UretimeMalCikisi", "Üretime Mal Çıkışı"));
                   Buton.Add(Tuple.Create("UretimdenMalGirisi", "Üretimden Mal Girişi"));
                   Buton.Add(Tuple.Create("MusteriFaturaIadesi", "Müşteri Fatura İadesi")); 
                   Buton.Add(Tuple.Create("TeslimatIadesi", "Teslimat İadesi"));
                   Buton.Add(Tuple.Create("Raporlar", "Raporlar"));
                   Buton.Add(Tuple.Create("TalepKabul", "Talep Kabul"));
                   Buton.Add(Tuple.Create("SatistanIade", "Satıştan İade"));
                   Buton.Add(Tuple.Create("CekmeListesi", "Çekme Listesi"));
                   Buton.Add(Tuple.Create("PaletYapma", "Palet Yapma"));
                   Buton.Add(Tuple.Create("KonteynerYapma", "Konteyner Yapma"));  
                   Buton.Add(Tuple.Create("MagazacilikIslemleri", "Mağazacılık İşlemleri"));
                   Buton.Add(Tuple.Create("IadeTalepleri", "İade Talepleri"));

                foreach (var item in Buton)
                {
                    oCol.ValidValues.Add(item.Item1, item.Item2);
                }
                oCol.ExpandType = BoExpandType.et_DescriptionOnly;
                #endregion

                ConstVariables.oRecordset = (Recordset)ConstVariables.oCompanyObject.GetBusinessObject(BoObjectTypes.BoRecordset);
                ConstVariables.oRecordset.DoQuery("Select TOP 1 \"DocEntry\" from \"@AIF_WMS_CONSTRNG\" as T0 where T0.\"U_CompanyDBCode\" = '" + ConstVariables.oCompanyObject.CompanyDB + "'");

                if (ConstVariables.oRecordset.RecordCount > 0)
                {
                    frmSirketBilgileri.Mode = BoFormMode.fm_FIND_MODE;
                    EdtDocEntry.Value = ConstVariables.oRecordset.Fields.Item("DocEntry").Value.ToString();
                    btnAddOrUpdate.Item.Click();


                    //DataTable dataTable = frmSirketBilgileri.DataSources.DataTables.Add("DATA");
                    //dataTable.Columns.Add("U_ButonAdi", BoFieldsType.ft_Text, DataSize: 150);
                    //dataTable.Columns.Add("U_AktfPsf", BoFieldsType.ft_Text, DataSize: 1);

                    //SAPbouiCOM.ColumnClass oColumn = (SAPbouiCOM.ColumnClass)oMatrixButonParam.Columns.Item("Col_0");
                    //oColumn.DataBind.Bind("DATA", "U_ButonAdi");

                    //dataTable.Rows.Clear();
                    //dataTable.Rows.Add();
                    //dataTable.SetValue("U_ButonAdi", 0, "Chntst");
                    //oMatrixButonParam.LoadFromDataSource();
                    //oMatrixButonParam.LoadFromDataSourceEx(); 

                    
                }
                else
                {
                    EdtSirketKodu.Value = ConstVariables.oCompanyObject.CompanyDB;
                    EdtSirketAdi.Value = ConstVariables.oCompanyObject.CompanyName;
                    EdtLisansServer.Value = ConstVariables.oCompanyObject.LicenseServer; //laptop-0pnk0hl7:40000
                    EdtServer.Value = ConstVariables.oCompanyObject.Server; //LAPTOP-0PNK0HL7
                    EdtKullaniciKodu.Value = ConstVariables.oCompanyObject.UserName;
                    //CmbServerTipi.Select(ConstVariables.oCompanyObject.DbServerType, BoSearchKey.psk_ByValue);

                    oMatrixButonParam.Clear();
                    frmSirketBilgileri.DataSources.DBDataSources.Item(1).Clear();
                    oMatrixButonParam.AddRow(21);

                    #region combobox olamdan butonları matrise ekler
                    //depos = new List<depo>();
                    //depos.Add(new depo { depoadi = "SiparisliMalGirisi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "SiparissizMalGirisi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "BelgesizMalGirisi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "TalepsizDepoNakli", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "TalebeBagliDepoNakli", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "DepoSayimi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "BelgesizMalCikisi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "SipariseBagliTeslimat", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "SiparissizTeslimat", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "BarkodOlustur", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "UretimeMalCikisi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "UretimdenMalGirisi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "MusteriFaturaIadesi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "TeslimatIadesi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "Raporlar", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "TalepKabul", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "SatistanIade", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "CekmeListesi", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "PaletYapma", aktifpasif = "N" });
                    //depos.Add(new depo { depoadi = "KonteynerYapma", aktifpasif = "N" });

                    //oMatrixButonParam.Clear();
                    //frmSirketBilgileri.DataSources.DBDataSources.Item(1).Clear();
                    //foreach (var item in depos)
                    //{

                    //    oMatrixButonParam.AddRow();
                    //    ((SAPbouiCOM.EditText)oMatrixButonParam.Columns.Item("Col_0").Cells.Item(oMatrixButonParam.RowCount).Specific).Value = item.depoadi;
                    //    //((SAPbouiCOM.EditText)oMatrixButonParam.Columns.Item("Col_1").Cells.Item(i).Specific).Value = item.aktifpasif; 

                    //} 
                    #endregion
                }
                CmbServerTipi.ExpandType = BoExpandType.et_DescriptionOnly;

            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox("Hata oluştu." + ex.Message);
            }

            finally
            {
                oMatrixButonParam.AutoResizeColumns();
                frmSirketBilgileri.Freeze(false);

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

            try
            {
                if (pVal.MenuUID == "AIFRGHTCLK_DeleteRow" && pVal.BeforeAction)
                {
                    int row = oMatrixButonParam.GetNextSelectedRow();
                    if (row != -1)
                    {
                        //if (((SAPbouiCOM.EditText)oMatrixKaliteKontrol.Columns.Item("Col_5").Cells.Item(row).Specific).Value != "")
                        //{
                        //    silinecekler.Add(((SAPbouiCOM.EditText)oMatrixKaliteKontrol.Columns.Item("Col_5").Cells.Item(row).Specific).Value);
                        //}
                        oMatrixButonParam.DeleteRow(row);
                        if (frmSirketBilgileri.Mode == BoFormMode.fm_OK_MODE)
                        {
                            frmSirketBilgileri.Mode = BoFormMode.fm_UPDATE_MODE;
                        }


                    }
                }
                else if (pVal.MenuUID == "AIFRGHTCLK_AddRow" && pVal.BeforeAction)
                {
                    frmSirketBilgileri.DataSources.DBDataSources.Item(1).Clear();
                    oMatrixButonParam.AddRow();

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
                        Handler.SAPApplication.Menus.RemoveEx("AIFRGHTCLK_AddRow");
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
