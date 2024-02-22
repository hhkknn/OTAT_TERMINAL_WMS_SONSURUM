using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Abstarct;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib;
using AIF.ObjectsDLL.Utils;
using AIF.WMS.HelperClass;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Handler = AIF.ObjectsDLL.Events.Handler;

namespace AIF.WMS.ClassLayer
{
    public class SayimListesi
    {
        [ItemAtt(AIFConn.SayimListesiUID)]
        public SAPbouiCOM.Form frmSayimListesi;

        [ItemAtt("Item_4")]
        public SAPbouiCOM.EditText oEditBelgeNo;

        [ItemAtt("Item_0")]
        public SAPbouiCOM.Matrix oMatrix;

        [ItemAtt("Item_3")]
        public SAPbouiCOM.Button obtnSayimOlustur;

        [ItemAtt("Item_6")]
        public SAPbouiCOM.EditText oEditBelgeTarihi;

        [ItemAtt("Item_7")]
        public SAPbouiCOM.EditText oEditOlusturan;

        [ItemAtt("Item_9")]
        public SAPbouiCOM.EditText oEditAciklama;

        [ItemAtt("Item_11")]
        public SAPbouiCOM.EditText oEditBelgeNumaralari;

        [ItemAtt("Item_15")]
        public SAPbouiCOM.ComboBox oComboSube;

        //[ItemAtt("1")]
        //public SAPbouiCOM.Button btnAddOrUpdate;
        bool IsBranch = false;
        public void LoadForms()
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.SayimListesiXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.SayimListesiXML));
            Functions.CreateUserOrSystemFormComponent<SayimListesi>(AIFConn.Sayim);

            InitForms();
        }

        private SAPbouiCOM.DataTable oDataTable = null;

        public void InitForms()
        {
            try
            {
                frmSayimListesi.EnableMenu("1283", false);
                frmSayimListesi.EnableMenu("1284", false);
                frmSayimListesi.EnableMenu("1286", false);
                oDataTable = frmSayimListesi.DataSources.DataTables.Add("DATA");
                oMatrix.AutoResizeColumns();
                //List<Helper.ValidValue> list;
                ////list = Helper.GetValidValuesFromRS("Select \"USER_CODE\" as \"value\", \"U_Name\" as \"description\"  from OUSR");
                //list = Helper.GetValidValuesFromRS("Select \"U_SayimTarihi\" as \"value\",\"U_SayimTarihi\" as \"description\" from \"@AIF_WMS_WHSCOUNT\"");

                //Helper nesne = new Helper();
                //nesne.ComboAction(frmSayimListesi, "Item_2", Helper.ActionCombo.add, list);

                //ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                //ConstVariables.oRecordset.DoQuery("Select \"DocEntry\" from \"@AIF_UVT_ACTPARAM\"");

                //if (ConstVariables.oRecordset.RecordCount > 0)
                //{
                //    frmAktivteParametre.Mode = BoFormMode.fm_FIND_MODE;
                //    EdtDocEntry.Value = "1";
                //    btnAddOrUpdate.Item.Click();

                //}

                //SAPbobsCOM.Items oItem = (SAPbobsCOM.Items)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
                //oItem.GetByKey("TEST1234");
                //oItem.DepreciationParameters.SetCurrentLine(1);
                //oItem.DepreciationParameters.DepreciationType = "MV_DB";
                //int Update = oItem.Update();

                string CheckBranchQuery = "Select * from OADM";
                ConstVariables.oRecordset.DoQuery(CheckBranchQuery);

                if (ConstVariables.oRecordset.Fields.Item("MltpBrnchs").Value.ToString() == "Y")
                {
                    IsBranch = true;

                    string ss = "Select  \"BPLId\",\"BPLName\" from \"OBPL\" where \"Disabled\" = 'N' ";

                    ConstVariables.oRecordset.DoQuery(ss);

                    while (!ConstVariables.oRecordset.EoF)
                    {
                        oComboSube.ValidValues.Add(ConstVariables.oRecordset.Fields.Item(0).Value.ToString(), ConstVariables.oRecordset.Fields.Item(1).Value.ToString());


                        ConstVariables.oRecordset.MoveNext();
                    }

                }

            }
            catch (Exception ex)
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
                    break;

                case BoEventTypes.et_CLICK:
                    if (pVal.ItemUID == "Item_3" && !pVal.BeforeAction)
                    {
                        if (IsBranch)
                        {
                            if (oComboSube.Value.Trim() == "")
                            {
                                Handler.SAPApplication.MessageBox("Şube kodu seçimi yapılmadan işleme devam edilemez.");
                                return false;
                            }
                        }
                        SayimOlustur();
                    }
                    else if (pVal.ItemUID == "Item_12" && !pVal.BeforeAction)
                    {
                        AIFConn.Symlr.LoadForms(oEditBelgeNumaralari.Value);
                    }
                    else if (pVal.ItemUID == "Item_13" && !pVal.BeforeAction)
                    {
                        DetaylariListele(oEditBelgeNumaralari.Value);
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
                    if (pVal.ItemUID == "Item_4" && pVal.BeforeAction)
                    {
                        SAPbouiCOM.IChooseFromListEvent oCFLEvento = default(SAPbouiCOM.IChooseFromListEvent);
                        oCFLEvento = (SAPbouiCOM.IChooseFromListEvent)pVal;

                        SAPbouiCOM.ChooseFromList oCFL = default(SAPbouiCOM.ChooseFromList);
                        oCFL = frmSayimListesi.ChooseFromLists.Item(oCFLEvento.ChooseFromListUID);

                        SAPbouiCOM.Conditions oCons = default(SAPbouiCOM.Conditions);
                        SAPbouiCOM.Condition oCon = default(SAPbouiCOM.Condition);
                        SAPbouiCOM.Conditions oEmptyConts = new SAPbouiCOM.Conditions();

                        oCFL.SetConditions(oEmptyConts);
                        oCons = oCFL.GetConditions();

                        oCon = oCons.Add();
                        oCon.Alias = "U_SayimNumarasi";
                        oCon.Operation = SAPbouiCOM.BoConditionOperation.co_IS_NULL;

                        oCFL.SetConditions(oCons);
                    }
                    else if (pVal.ItemUID == "Item_4" && !pVal.BeforeAction)
                    {
                        try
                        {
                            SAPbouiCOM.DataTable oDataTable = ((SAPbouiCOM.ChooseFromListEvent)pVal).SelectedObjects;
                            string Val = "";
                            Val = oDataTable.GetValue("DocEntry", 0).ToString();
                            try
                            {
                                oEditBelgeNo.Value = Val;
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                Val = oDataTable.GetValue("U_SayimTarihi", 0).ToString();
                                DateTime dt1 = Convert.ToDateTime(Val);
                                oEditBelgeTarihi.Value = dt1.ToString("yyyyMMdd");
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                Val = oDataTable.GetValue("U_KullaniciId", 0).ToString();
                                oEditOlusturan.Value = Val;
                            }
                            catch (Exception)
                            {
                            }

                            try
                            {
                                Val = oDataTable.GetValue("U_Aciklama", 0).ToString();
                                oEditAciklama.Value = Val;
                            }
                            catch (Exception)
                            {
                            }

                            //DetaylariListele(oEditBelgeNo.Value.ToString());
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

        private void DetaylariListele(string docEntry)
        {
            try
            {
                string condition = ConstVariables.oCompanyObject.DbServerType == BoDataServerTypes.dst_HANADB ? "IFNULL" : "ISNULL";
                bool ilk = true;
                frmSayimListesi.Freeze(true);
                string sql = "Select T1.\"DocEntry\",T1.\"U_SayimTarihi\",T1.\"U_KullaniciId\" as \"KullaniciId\",T1.\"U_KullaniciAdi\" as \"KullaniciAdi\",T0.\"U_Barkod\" as \"Barkod\",T0.\"U_KalemKodu\" as \"KalemKodu\",T0.\"U_KalemTanimi\" as \"KalemTanimi\",T0.\"U_DepoKodu\" as \"DepoKodu\",T0.\"U_DepoAdi\" as \"DepoAdi\",T0.\"U_Miktar\" as \"Miktar\",T0.\"U_DepoYeriId\" as \"DepoYeriId\",T0.\"U_DepoYeriAdi\" as \"DepoYeriAdi\" from \"@AIF_WMS_WHSCOUNT1\" as T0 INNER JOIN \"@AIF_WMS_WHSCOUNT\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where 1=1 and " + condition + "(U_SayimNumarasi,'') = '' ";


                if (oEditBelgeNumaralari.Value != "")
                {
                    var split = docEntry.Split('|');

                    foreach (var item in split)
                    {
                        if (ilk)
                        {
                            sql += " and T0.\"DocEntry\" IN('" + item + "'";
                            ilk = false;
                        }
                        else
                        {
                            sql += ",'" + item + "'";
                        }
                    }

                    sql += ")";
                }

                ilk = true;
                oDataTable.Clear();
                oDataTable.ExecuteQuery(sql);
                oMatrix.Clear();

                oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "Barkod");
                oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "KalemKodu");
                oMatrix.Columns.Item("Col_2").DataBind.Bind("DATA", "KalemTanimi");
                oMatrix.Columns.Item("Col_3").DataBind.Bind("DATA", "DepoKodu");
                oMatrix.Columns.Item("Col_4").DataBind.Bind("DATA", "DepoAdi");
                oMatrix.Columns.Item("Col_5").DataBind.Bind("DATA", "Miktar");
                oMatrix.Columns.Item("Col_6").DataBind.Bind("DATA", "DepoYeriId");
                oMatrix.Columns.Item("Col_7").DataBind.Bind("DATA", "DepoYeriAdi");
                oMatrix.Columns.Item("Col_8").DataBind.Bind("DATA", "KullaniciId");
                oMatrix.Columns.Item("Col_9").DataBind.Bind("DATA", "KullaniciAdi");
                oMatrix.Columns.Item("Col_10").DataBind.Bind("DATA", "DocEntry");
                oMatrix.LoadFromDataSource();
                oMatrix.AutoResizeColumns();

                if (!oDataTable.IsEmpty)
                {
                    var val = oDataTable.GetValue("U_SayimTarihi", 0).ToString();

                    oEditBelgeTarihi.Value = Convert.ToDateTime(val).ToString("yyyyMMdd");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                frmSayimListesi.Freeze(false);
            }
        }

        private List<DepodakiKalemListesi> depodakiKalemListesis = new List<DepodakiKalemListesi>();

        private class DepodakiKalemListesi
        {
            public string ItemCode { get; set; }

            public string DepoKodu { get; set; }
        }

        private void SayimOlustur()
        {
            try
            {

                if (oMatrix.RowCount == 0)
                {
                    Handler.SAPApplication.MessageBox("Ürün olmadan sayım oluşturulamaz.");
                    return;

                }


                string coklusayar = "";

                string ssss = "Select \"U_SayimCoklu\" from \"@AIF_WMS_GNLPRM\" ";

                ConstVariables.oRecordset.DoQuery(ssss);

                coklusayar = ConstVariables.oRecordset.Fields.Item(0).Value.ToString();

                ConstVariables.oCompanyObject.StartTransaction();
                int Progress = 0;
                SAPbouiCOM.ProgressBar oProgressBar = null;
                SAPbobsCOM.Recordset recordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                recordset.DoQuery("Select \"U_DepoYeriCalisir\" from \"@AIF_WMS_GNLPRM\" ");

                string depoYerleriIleCalisir = recordset.Fields.Item(0).Value.ToString();

                SAPbobsCOM.CompanyService oCS = (SAPbobsCOM.CompanyService)ConstVariables.oCompanyObject.GetCompanyService();
                SAPbobsCOM.InventoryCountingsService oICS = (SAPbobsCOM.InventoryCountingsService)oCS.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryCountingsService);
                SAPbobsCOM.InventoryCounting oIC = (SAPbobsCOM.InventoryCounting)oICS.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCounting);
                string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                            select new
                            {
                                ItemCode = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value,
                                DepoKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value,
                                Miktar = parseNumber.parservalues<double>((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_5" select new XElement(y.Element("Value"))).First().Value),
                                DepoYeriId = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_6" select new XElement(y.Element("Value"))).First().Value,
                                KullaniciId = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_8" select new XElement(y.Element("Value"))).First().Value,
                                DocEntry = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_10" select new XElement(y.Element("Value"))).First().Value,
                            }).ToList();

                var SayanKisiler = (from x in XDocument.Parse(xml).Descendants("Row")
                                    select new
                                    {
                                        kisiler = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_8" select new XElement(y.Element("Value"))).First().Value,
                                    }).ToList();

                var depolar = rows.Select(m => new { m.DepoKodu, m.DepoYeriId }).Distinct();

                //string sql = "";
                //sql = "Select * from \"@AIF_WMS_WHSCOUNT2\" where \"DocEntry\" = '" + oEditBelgeNo.Value + "'";

                //ConstVariables.oRecordset.DoQuery(sql);
                //string xmll2 = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                //XDocument xDoc2 = XDocument.Parse(xmll2);
                //XNamespace ns2 = "http://www.sap.com/SBO/SDK/DI";
                //var rows_Partiler = (from t in xDoc2.Descendants(ns2 + "Row")
                //                     select new
                //                     {
                //                         KalemKodu = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_KalemKodu" select new XElement(y.Element(ns2 + "Value"))).First().Value,
                //                         DepoKodu = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_DepoKodu" select new XElement(y.Element(ns2 + "Value"))).First().Value,
                //                         PartiNo = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_PartiNo" select new XElement(y.Element(ns2 + "Value"))).First().Value,
                //                         Miktar = parseNumber.parservalues<double>((from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_Miktar" select new XElement(y.Element(ns2 + "Value"))).First().Value)
                //                     }).ToList();



                string date = oEditBelgeTarihi.Value.ToString();

                DateTime dt = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4, 2)), Convert.ToInt32(date.Substring(6, 2)));


                oIC.CountDate = dt;
                if (IsBranch)
                {
                    oIC.BranchID = Convert.ToInt32(oComboSube.Value.Trim());
                }
                else
                {
                    oIC.BranchID = 1;
                }
                string ilkid = ""; //Sayan kisilerin ilki

                if (SayanKisiler.Count > 0)
                {
                    //oIC.SingleCounterType = SAPbobsCOM.CounterTypeEnum.ctEmployee;
                    //oIC.SingleCounterID = Convert.ToInt32(oEditOlusturan.Value);
                    if (SayanKisiler.Count > 1)
                    {
                        if (Program.mKod == "70TRMN")
                        {
                            oIC.CountingType = SAPbobsCOM.CountingTypeEnum.ctSingleCounter; 
                        }
                        else
                        {
                            oIC.CountingType = SAPbobsCOM.CountingTypeEnum.ctMultipleCounters;
                        }
                    }
                    else
                    {
                        oIC.CountingType = SAPbobsCOM.CountingTypeEnum.ctSingleCounter;
                    }
                    int i = 1;
                    var list = SayanKisiler.Distinct();

                    if (oIC.CountingType == CountingTypeEnum.ctMultipleCounters)
                    {
                        if (list.Count() > 1)
                        {
                            foreach (var item in list)
                            {
                                oIC.IndividualCounters.Add();
                                oIC.IndividualCounters.Item(oIC.IndividualCounters.Count - 1).CounterNumber = i;
                                oIC.IndividualCounters.Item(oIC.IndividualCounters.Count - 1).CounterType = SAPbobsCOM.CounterTypeEnum.ctEmployee;
                                oIC.IndividualCounters.Item(oIC.IndividualCounters.Count - 1).CounterID = Convert.ToInt32(item.kisiler);
                                i++;

                                if (ilkid == "")
                                {
                                    ilkid = item.kisiler;
                                }
                            }
                        }
                        else
                        {
                            if (Program.mKod == "70TRMN")
                            {
                                oIC.CountingType = SAPbobsCOM.CountingTypeEnum.ctSingleCounter;
                                oIC.SingleCounterType = CounterTypeEnum.ctUser;
                                oIC.SingleCounterID = 1;
                            }
                            else
                            {
                                oIC.CountingType = SAPbobsCOM.CountingTypeEnum.ctSingleCounter;
                                oIC.SingleCounterType = CounterTypeEnum.ctEmployee;
                                oIC.SingleCounterID = Convert.ToInt32(SayanKisiler[0].kisiler);
                            }
                        }
                    }
                    else
                    {
                        if (Program.mKod == "70TRMN")
                        {
                            oIC.CountingType = SAPbobsCOM.CountingTypeEnum.ctSingleCounter;
                            oIC.SingleCounterType = CounterTypeEnum.ctUser;
                            oIC.SingleCounterID = 1;
                        }
                        else
                        {
                            oIC.CountingType = SAPbobsCOM.CountingTypeEnum.ctSingleCounter;
                            oIC.SingleCounterType = CounterTypeEnum.ctEmployee;
                            oIC.SingleCounterID = Convert.ToInt32(SayanKisiler[0].kisiler);
                        }
                    }


                    //oIC.IndividualCounters.Add();
                    //oIC.IndividualCounters.Item(oIC.IndividualCounters.Count - 1).CounterNumber = 2;
                    //oIC.IndividualCounters.Item(oIC.IndividualCounters.Count - 1).CounterType = SAPbobsCOM.CounterTypeEnum.ctEmployee;
                    //oIC.IndividualCounters.Item(oIC.IndividualCounters.Count - 1).CounterID = 40;

                }

                oIC.Remarks = oEditAciklama.Value;

                SAPbobsCOM.InventoryCountingLines oICLS = null;
                SAPbobsCOM.InventoryCountingLine oICL = null;

                oProgressBar = Handler.SAPApplication.StatusBar.CreateProgressBar("test", rows.Count, true);

                foreach (var DEK754 in depolar)
                {
                    oProgressBar.Text = DEK754 + "için sayım oluşturuluyor...";
                    foreach (var DEG248 in rows.Where(x => x.DepoKodu == DEK754.DepoKodu && x.DepoYeriId == DEK754.DepoYeriId))
                    {
                        Progress += 1;
                        oProgressBar.Value = Progress;



                        string sql = "";
                        sql = "Select * from \"@AIF_WMS_WHSCOUNT2\" where \"DocEntry\" = '" + DEG248.DocEntry + "'";

                        ConstVariables.oRecordset.DoQuery(sql);
                        string xmll2 = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
                        XDocument xDoc2 = XDocument.Parse(xmll2);
                        XNamespace ns2 = "http://www.sap.com/SBO/SDK/DI";
                        var rows_Partiler = (from t in xDoc2.Descendants(ns2 + "Row")
                                             select new
                                             {
                                                 KalemKodu = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_KalemKodu" select new XElement(y.Element(ns2 + "Value"))).First().Value,
                                                 DepoKodu = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_DepoKodu" select new XElement(y.Element(ns2 + "Value"))).First().Value,
                                                 PartiNo = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_PartiNo" select new XElement(y.Element(ns2 + "Value"))).First().Value,
                                                 Miktar = parseNumber.parservalues<double>((from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_Miktar" select new XElement(y.Element(ns2 + "Value"))).First().Value)
                                             }).ToList();

                        if (eklenenlers.Where(x => x.depokodu == DEK754.DepoKodu && x.depoYeriId == DEK754.DepoYeriId && x.kalemKodu == DEG248.ItemCode).Count() > 0)
                        {
                            continue;
                        }


                        oICLS = oIC.InventoryCountingLines;
                        oICL = oICLS.Add();
                        //oICL = oICLS.Item();
                        oICL.ItemCode = DEG248.ItemCode;
                        oICL.CountedQuantity = 0;
                        if (Program.mKod == "70TRMN")
                        {
                            //if (oIC.CountingType == CountingTypeEnum.ctMultipleCounters)
                            //{
                            //    oICL.CounterType = SAPbobsCOM.CounterTypeEnum.ctEmployee; 
                            //}
                            //else
                            //{
                            //    oICL.CounterType = SAPbobsCOM.CounterTypeEnum.ctUser;
                            //}
                            //oICL.MultipleCounterRole = SAPbobsCOM.MultipleCounterRoleEnum.mcrIndividualCounter;
                            oICL.CounterType = SAPbobsCOM.CounterTypeEnum.ctUser;
                            //oICL.CounterID = Convert.ToInt32(DEG248.KullaniciId);
                            oIC.SingleCounterType = CounterTypeEnum.ctUser;
                            oIC.SingleCounterID = 1;
                        }
                        else
                        {
                            oICL.CounterType = SAPbobsCOM.CounterTypeEnum.ctEmployee;
                            //oICL.MultipleCounterRole = SAPbobsCOM.MultipleCounterRoleEnum.mcrIndividualCounter;
                            oICL.CounterID = Convert.ToInt32(DEG248.KullaniciId);
                        }


                        if (depoYerleriIleCalisir != "Y")
                        {
                            if (rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == DEK754.DepoKodu).Count() > 0)
                            {
                                var miktar = rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == DEK754.DepoKodu).Sum(y => y.Miktar);

                                oICL.CountedQuantity = Convert.ToDouble(miktar);

                                #region Partiler
                                int partiSira = 0;
                                var partiler = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu).Select(y => y.PartiNo).Distinct();
                                foreach (var item in partiler)
                                {
                                    //var toplamParti = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Count() == 0 ? 0 : Convert.ToDouble(rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Select(y => y.Miktar).FirstOrDefault());


                                    var toplamParti = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Count() == 0 ? 0 : Convert.ToDouble(rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Sum(y => y.Miktar));


                                    var query = "select ISNULL(SUM(T1.Quantity),0) as Miktar from OBTN T0 inner join OBTQ T1 on T0.ItemCode = T1.ItemCode and T0.SysNumber = T1.SysNumber inner join OITM T2 on T0.ItemCode = T2.ItemCode where T1.Quantity > 0 and T0.DistNumber = N'" + item + "' and T1.WhsCode = '" + DEK754 + "' and T0.ItemCode = '" + DEG248.ItemCode + "'";

                                    ConstVariables.oRecordset.DoQuery(query);

                                    if (toplamParti >= Convert.ToDouble(ConstVariables.oRecordset.Fields.Item(0).Value))
                                    {
                                        //query = "select T1.\"AbsEntry\" from OBTN T0 inner join OBTQ T1 on T0.ItemCode = T1.ItemCode and T0.SysNumber = T1.SysNumber inner join OITM T2 on T0.ItemCode = T2.ItemCode where T1.Quantity > 0 and T0.DistNumber = N'" + item + "' and T1.WhsCode = '" + DEK754 + "' and T0.ItemCode = '" + DEG248.ItemCode + "'";

                                        //ConstVariables.oRecordset.DoQuery(query);
                                        oICL.InventoryCountingBatchNumbers.Add();
                                        oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).BatchNumber = item;
                                        oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).Quantity = toplamParti;
                                    }
                                    else
                                    {
                                        oICL.InventoryCountingBatchNumbers.Add();
                                        oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).BatchNumber = item;
                                        oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).Quantity = toplamParti;
                                    }

                                    //oIC.InventoryCountingLines.Item(1).InventoryCountingBatchNumbers.Item(1).q 

                                }
                                #endregion
                            }
                        }
                        else if (depoYerleriIleCalisir == "Y")
                        {
                            if (rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.DepoYeriId == DEK754.DepoYeriId).Count() > 0)
                            {
                                var miktar = rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.DepoYeriId == DEK754.DepoYeriId).Sum(y => y.Miktar);

                                oICL.CountedQuantity = Convert.ToDouble(miktar);

                                #region Partiler
                                int partiSira = 0;
                                var partiler = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu).Select(y => y.PartiNo).Distinct();
                                foreach (var item in partiler)
                                {
                                    var toplamParti = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Count() == 0 ? 0 : Convert.ToDouble(rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Select(y => y.Miktar).FirstOrDefault());


                                    var query = "select ISNULL(SUM(T1.Quantity),0) as Miktar from OBTN T0 inner join OBTQ T1 on T0.ItemCode = T1.ItemCode and T0.SysNumber = T1.SysNumber inner join OITM T2 on T0.ItemCode = T2.ItemCode where T1.Quantity > 0 and T0.DistNumber = N'" + item + "' and T1.WhsCode = '" + DEK754 + "' and T0.ItemCode = '" + DEG248.ItemCode + "'";

                                    ConstVariables.oRecordset.DoQuery(query);

                                    if (toplamParti >= Convert.ToDouble(ConstVariables.oRecordset.Fields.Item(0).Value))
                                    {
                                        //query = "select T1.\"AbsEntry\" from OBTN T0 inner join OBTQ T1 on T0.ItemCode = T1.ItemCode and T0.SysNumber = T1.SysNumber inner join OITM T2 on T0.ItemCode = T2.ItemCode where T1.Quantity > 0 and T0.DistNumber = N'" + item + "' and T1.WhsCode = '" + DEK754 + "' and T0.ItemCode = '" + DEG248.ItemCode + "'";

                                        //ConstVariables.oRecordset.DoQuery(query);
                                        oICL.InventoryCountingBatchNumbers.Add();
                                        oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).BatchNumber = item;
                                        oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).Quantity = toplamParti;
                                    }
                                    else
                                    {
                                        oICL.InventoryCountingBatchNumbers.Add();
                                        oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).BatchNumber = item;
                                        oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).Quantity = toplamParti;
                                    }

                                    //oIC.InventoryCountingLines.Item(1).InventoryCountingBatchNumbers.Item(1).q 

                                }
                                #endregion
                            }
                        }

                        oICL.WarehouseCode = DEK754.DepoKodu;
                        if (DEK754.DepoYeriId != "")
                        {
                            oICL.BinEntry = Convert.ToInt32(DEK754.DepoYeriId);

                        }
                        oICL.Counted = SAPbobsCOM.BoYesNoEnum.tYES;

                        var asdas = oICLS.GetXMLSchema();
                        eklenenlers.Add(new eklenenler { depokodu = DEK754.DepoKodu, depoYeriId = DEK754.DepoYeriId, kalemKodu = oICL.ItemCode });
                    }
                }



                try
                {
                    oProgressBar.Stop();

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(oProgressBar);

                    oProgressBar = null;

                    GC.Collect();


                    if (oMatrix.RowCount > 0)
                    {
                        try
                        {
                            var asdasd = oICL.GetXMLSchema();
                            var a1 = oICS.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCounting);


                            bool ret = false;
                            try
                            {
                                SAPbobsCOM.InventoryCountingParams oICP = oICS.Add(oIC);
                                if (ConstVariables.oCompanyObject.InTransaction)
                                {
                                    ConstVariables.oCompanyObject.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                                    oIC = oICS.Get(oICP);
                                    oICLS = oIC.InventoryCountingLines;

                                    for (int i = 0; i <= oICLS.Count - 1; i++)
                                    {
                                        oICL = oICLS.Item(i);

                                        double miktar = depoYerleriIleCalisir == "Y" ? rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == oICL.WarehouseCode && x.DepoYeriId == oICL.BinEntry.ToString() && x.KullaniciId == oICL.CounterID.ToString()).Sum(y => y.Miktar) : oICL.CounterID != -1 ? rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == oICL.WarehouseCode && x.KullaniciId == oICL.CounterID.ToString()).Sum(y => y.Miktar) : rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == oICL.WarehouseCode).Sum(y => y.Miktar);


                                        int iLine = 0;
                                        if (iLine != oICL.LineNumber)
                                        {

                                            oICL.CountedQuantity = miktar;

                                        }


                                    }
                                    try
                                    {
                                        try
                                        {
                                            oICS.Update(oIC);
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        var docentries = (from x in XDocument.Parse(xml).Descendants("Row")
                                                          select new
                                                          {
                                                              docEntry = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_10" select new XElement(y.Element("Value"))).First().Value,
                                                          }).ToList();


                                        var dist = docentries.Distinct();

                                        foreach (var item in dist)
                                        {
                                            ConstVariables.oRecordset.DoQuery("UPDATE \"@AIF_WMS_WHSCOUNT\" set \"U_SayimNumarasi\" = '" + oICP.DocumentEntry + "' where \"DocEntry\" = " + item.docEntry + "");
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                    ret = true;
                                }
                            }
                            catch (Exception ex)
                            {
                                Handler.SAPApplication.MessageBox("Sayım oluşurken hata oluştu. " + ex.Message);
                                if (ConstVariables.oCompanyObject.InTransaction)
                                {
                                    ConstVariables.oCompanyObject.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                                    return;
                                }
                            }

                            if (!ret)
                            {
                                return;
                            }


                            eklenenlers = new List<eklenenler>();
                            //SAPbobsCOM.InventoryCountingBatchNumbers inventoryCountingBatchNumber = null;
                            //inventoryCountingBatchNumber.ba = "ANC";
                            //oICL.InventoryCountingBatchNumbers = inventoryCountingBatchNumber;
                            ConstVariables.oRecordset.DoQuery("Select DocEntry from  \"OINC\" order by DocEntry desc");

                            string objectKey = ConstVariables.oRecordset.Fields.Item(0).Value.ToString();

                            ConstVariables.oRecordset.DoQuery("UPDATE \"@AIF_WMS_WHSCOUNT\" SET \"U_SayimNumarasi\"='" + objectKey + "' where \"DocEntry\" = '" + oEditBelgeNo.Value + "' ");

                            Handler.SAPApplication.MessageBox("Sayım başarıyla oluşturuldu.");

                            oMatrix.Clear();
                            oEditOlusturan.Value = "";
                            oEditBelgeTarihi.Value = "";
                            oEditBelgeNo.Value = "";
                            oEditAciklama.Value = "";
                            oEditBelgeNumaralari.Value = "";

                            try
                            {
                                Handler.SAPApplication.ActivateMenuItem("3082");

                                SAPbouiCOM.Form oForm = Handler.SAPApplication.Forms.ActiveForm;

                                oForm.Mode = BoFormMode.fm_FIND_MODE;
                                ((SAPbouiCOM.EditText)oForm.Items.Item("1470000016").Specific).Value = objectKey;
                                Handler.SAPApplication.SendKeys("^{ENTER}");
                            }
                            catch (Exception)
                            {
                            }



                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox("Sayım oluşurken hata oluştu. " + ex.Message);
                        }
                        finally
                        {
                            eklenenlers = new List<eklenenler>();
                        }
                    }

                }
                catch (Exception)
                {
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                if (ConstVariables.oCompanyObject.InTransaction)
                {
                    try
                    {
                        ConstVariables.oCompanyObject.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            #region Eski Sayım sistemi
            //SAPbobsCOM.Recordset recordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            //recordset.DoQuery("Select \"U_DepoYeriCalisir\" from \"@AIF_WMS_GNLPRM\" ");

            //string depoYerleriIleCalisir = recordset.Fields.Item(0).Value.ToString();

            //int Progress = 0;
            //SAPbouiCOM.ProgressBar oProgressBar = null;
            //try
            //{
            //    string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
            //    var rows = (from x in XDocument.Parse(xml).Descendants("Row")
            //                select new
            //                {
            //                    ItemCode = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value,
            //                    DepoKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value,
            //                    Miktar = parseNumber.parservalues<double>((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_5" select new XElement(y.Element("Value"))).First().Value),
            //                    DepoYeriId = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_6" select new XElement(y.Element("Value"))).First().Value,
            //                }).ToList();

            //    SAPbobsCOM.CompanyService oCS = (SAPbobsCOM.CompanyService)ConstVariables.oCompanyObject.GetCompanyService();
            //    SAPbobsCOM.InventoryCountingsService oICS = (SAPbobsCOM.InventoryCountingsService)oCS.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryCountingsService);
            //    SAPbobsCOM.InventoryCounting oIC = (SAPbobsCOM.InventoryCounting)oICS.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCounting);

            //    var depolar = rows.Select(m => new { m.DepoKodu, m.DepoYeriId }).Distinct();

            //    string sql = "";

            //    foreach (var item in depolar)
            //    {
            //        sql = "Select T0.\"ItemCode\",\"WhsCode\" from \"OITW\" as T0 INNER JOIN OITM as T1 ON T0.\"ItemCode\" = T1.\"ItemCode\" where T0.\"WhsCode\" = '" + item.DepoKodu + "' and T1.\"InvntItem\" = 'Y'";

            //        ConstVariables.oRecordset.DoQuery(sql);
            //        string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
            //        XDocument xDoc = XDocument.Parse(xmll);
            //        XNamespace ns = "http://www.sap.com/SBO/SDK/DI";
            //        var rowsx = (from t in xDoc.Descendants(ns + "Row")
            //                     select new DepodakiKalemListesi
            //                     {
            //                         ItemCode = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "ItemCode" select new XElement(y.Element(ns + "Value"))).First().Value,
            //                         DepoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "WhsCode" select new XElement(y.Element(ns + "Value"))).First().Value
            //                     }).ToList();

            //        depodakiKalemListesis.AddRange(rowsx);
            //    }

            //    sql = "Select * from \"@AIF_WMS_WHSCOUNT2\" where \"DocEntry\" = '" + oEditBelgeNo.Value + "'";

            //    ConstVariables.oRecordset.DoQuery(sql);
            //    string xmll2 = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
            //    XDocument xDoc2 = XDocument.Parse(xmll2);
            //    XNamespace ns2 = "http://www.sap.com/SBO/SDK/DI";
            //    var rows_Partiler = (from t in xDoc2.Descendants(ns2 + "Row")
            //                         select new
            //                         {
            //                             KalemKodu = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_KalemKodu" select new XElement(y.Element(ns2 + "Value"))).First().Value,
            //                             DepoKodu = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_DepoKodu" select new XElement(y.Element(ns2 + "Value"))).First().Value,
            //                             PartiNo = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_PartiNo" select new XElement(y.Element(ns2 + "Value"))).First().Value,
            //                             Miktar = (from y in t.Element(ns2 + "Fields").Elements(ns2 + "Field") where y.Element(ns2 + "Alias").Value == "U_Miktar" select new XElement(y.Element(ns2 + "Value"))).First().Value
            //                         }).ToList();

            //    string date = oEditBelgeTarihi.Value.ToString();

            //    DateTime dt = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4, 2)), Convert.ToInt32(date.Substring(6, 2)));
            //    oIC.CountDate = dt;
            //    oIC.BranchID = 1;
            //    if (oEditOlusturan.Value != "")
            //    {
            //        oIC.SingleCounterType = SAPbobsCOM.CounterTypeEnum.ctEmployee;
            //        oIC.SingleCounterID = Convert.ToInt32(oEditOlusturan.Value);
            //    }
            //    SAPbobsCOM.InventoryCountingLines oICLS = null;
            //    SAPbobsCOM.InventoryCountingLine oICL = null;

            //    foreach (var DEK754 in depolar)
            //    {
            //        oProgressBar = Handler.SAPApplication.StatusBar.CreateProgressBar("test", depodakiKalemListesis.Count, true);
            //        oProgressBar.Text = DEK754 + "için sayım oluşturuluyor...";
            //        foreach (var DEG248 in depodakiKalemListesis.Where(x => x.DepoKodu == DEK754.DepoKodu))
            //        {
            //            Progress += 1;
            //            oProgressBar.Value = Progress;


            //            if (eklenenlers.Where(x => x.depokodu == DEK754.DepoKodu && x.depoYeriId == DEK754.DepoYeriId && x.kalemKodu == DEG248.ItemCode).Count() > 0)
            //            {
            //                continue;
            //            }


            //            oICLS = oIC.InventoryCountingLines;
            //            oICL = oICLS.Add();
            //            oICL.ItemCode = DEG248.ItemCode;
            //            oICL.CountedQuantity = 0;

            //            if (depoYerleriIleCalisir != "Y")
            //            {
            //                if (rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == DEK754.DepoKodu).Count() > 0)
            //                {
            //                    var miktar = rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == DEK754.DepoKodu).Sum(y => y.Miktar);

            //                    oICL.CountedQuantity = Convert.ToDouble(miktar);
            //                    int partiSira = 0;
            //                    var partiler = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu).Select(y => y.PartiNo).Distinct();
            //                    foreach (var item in partiler)
            //                    {
            //                        var toplamParti = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Count() == 0 ? 0 : Convert.ToDouble(rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Select(y => y.Miktar).FirstOrDefault());


            //                        var query = "select ISNULL(SUM(T1.Quantity),0) as Miktar from OBTN T0 inner join OBTQ T1 on T0.ItemCode = T1.ItemCode and T0.SysNumber = T1.SysNumber inner join OITM T2 on T0.ItemCode = T2.ItemCode where T1.Quantity > 0 and T0.DistNumber = N'" + item + "' and T1.WhsCode = '" + DEK754 + "' and T0.ItemCode = '" + DEG248.ItemCode + "'";

            //                        ConstVariables.oRecordset.DoQuery(query);

            //                        if (toplamParti >= Convert.ToDouble(ConstVariables.oRecordset.Fields.Item(0).Value))
            //                        {
            //                            //query = "select T1.\"AbsEntry\" from OBTN T0 inner join OBTQ T1 on T0.ItemCode = T1.ItemCode and T0.SysNumber = T1.SysNumber inner join OITM T2 on T0.ItemCode = T2.ItemCode where T1.Quantity > 0 and T0.DistNumber = N'" + item + "' and T1.WhsCode = '" + DEK754 + "' and T0.ItemCode = '" + DEG248.ItemCode + "'";

            //                            //ConstVariables.oRecordset.DoQuery(query);
            //                            oICL.InventoryCountingBatchNumbers.Add();
            //                            oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).BatchNumber = item;
            //                            oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).Quantity = toplamParti;
            //                        }
            //                        else
            //                        {
            //                            oICL.InventoryCountingBatchNumbers.Add();
            //                            oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).BatchNumber = item;
            //                            oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).Quantity = toplamParti;
            //                        }

            //                        //oIC.InventoryCountingLines.Item(1).InventoryCountingBatchNumbers.Item(1).q 

            //                    }
            //                }
            //            }
            //            else if (depoYerleriIleCalisir == "Y")
            //            {
            //                if (rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.DepoYeriId == DEK754.DepoYeriId).Count() > 0)
            //                {
            //                    var miktar = rows.Where(x => x.ItemCode == oICL.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.DepoYeriId == DEK754.DepoYeriId).Sum(y => y.Miktar);

            //                    oICL.CountedQuantity = Convert.ToDouble(miktar);
            //                    int partiSira = 0;
            //                    var partiler = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu).Select(y => y.PartiNo).Distinct();
            //                    foreach (var item in partiler)
            //                    {
            //                        var toplamParti = rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Count() == 0 ? 0 : Convert.ToDouble(rows_Partiler.Where(x => x.KalemKodu == DEG248.ItemCode && x.DepoKodu == DEK754.DepoKodu && x.PartiNo == item).Select(y => y.Miktar).FirstOrDefault());


            //                        var query = "select ISNULL(SUM(T1.Quantity),0) as Miktar from OBTN T0 inner join OBTQ T1 on T0.ItemCode = T1.ItemCode and T0.SysNumber = T1.SysNumber inner join OITM T2 on T0.ItemCode = T2.ItemCode where T1.Quantity > 0 and T0.DistNumber = N'" + item + "' and T1.WhsCode = '" + DEK754 + "' and T0.ItemCode = '" + DEG248.ItemCode + "'";

            //                        ConstVariables.oRecordset.DoQuery(query);

            //                        if (toplamParti >= Convert.ToDouble(ConstVariables.oRecordset.Fields.Item(0).Value))
            //                        {
            //                            //query = "select T1.\"AbsEntry\" from OBTN T0 inner join OBTQ T1 on T0.ItemCode = T1.ItemCode and T0.SysNumber = T1.SysNumber inner join OITM T2 on T0.ItemCode = T2.ItemCode where T1.Quantity > 0 and T0.DistNumber = N'" + item + "' and T1.WhsCode = '" + DEK754 + "' and T0.ItemCode = '" + DEG248.ItemCode + "'";

            //                            //ConstVariables.oRecordset.DoQuery(query);
            //                            oICL.InventoryCountingBatchNumbers.Add();
            //                            oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).BatchNumber = item;
            //                            oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).Quantity = toplamParti;
            //                        }
            //                        else
            //                        {
            //                            oICL.InventoryCountingBatchNumbers.Add();
            //                            oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).BatchNumber = item;
            //                            oICL.InventoryCountingBatchNumbers.Item(oICL.InventoryCountingBatchNumbers.Count - 1).Quantity = toplamParti;
            //                        }

            //                        //oIC.InventoryCountingLines.Item(1).InventoryCountingBatchNumbers.Item(1).q 

            //                    }
            //                }
            //            }

            //            oICL.WarehouseCode = DEK754.DepoKodu;
            //            if (DEK754.DepoYeriId != "")
            //            {
            //                oICL.BinEntry = Convert.ToInt32(DEK754.DepoYeriId);

            //            }
            //            oICL.Counted = SAPbobsCOM.BoYesNoEnum.tYES;

            //            eklenenlers.Add(new eklenenler { depokodu = DEK754.DepoKodu, depoYeriId = DEK754.DepoYeriId, kalemKodu = oICL.ItemCode });
            //        }

            //        try
            //        {
            //            oProgressBar.Stop();

            //            System.Runtime.InteropServices.Marshal.ReleaseComObject(oProgressBar);

            //            oProgressBar = null;

            //            GC.Collect();
            //        }
            //        catch (Exception)
            //        {
            //        }
            //    }

            //            if (oMatrix.RowCount > 0)
            //            {
            //                try
            //                {
            //                    var asdasd = oICL.GetXMLSchema();
            //                    var a1 = oICS.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCounting);



            //                    SAPbobsCOM.InventoryCountingParams oICP = oICS.Add(oIC);


            //                    eklenenlers = new List<eklenenler>();
            //                    //SAPbobsCOM.InventoryCountingBatchNumbers inventoryCountingBatchNumber = null;
            //                    //inventoryCountingBatchNumber.ba = "ANC";
            //                    //oICL.InventoryCountingBatchNumbers = inventoryCountingBatchNumber;
            //                    ConstVariables.oRecordset.DoQuery("Select DocEntry from  \"OINC\" order by DocEntry desc");

            //                    string objectKey = ConstVariables.oRecordset.Fields.Item(0).Value.ToString();

            //                    ConstVariables.oRecordset.DoQuery("UPDATE \"@AIF_WMS_WHSCOUNT\" SET \"U_SayimNumarasi\"='" + objectKey + "' where \"DocEntry\" = '" + oEditBelgeNo.Value + "' ");

            //                    Handler.SAPApplication.MessageBox("Sayım başarıyla oluşturuldu.");
            //                }
            //                catch (Exception ex)
            //                {
            //                    Handler.SAPApplication.MessageBox("Sayım oluşurken hata oluştu. " + ex.Message);
            //                }
            //                finally
            //                {
            //                    eklenenlers = new List<eklenenler>();
            //                }
            //            }
            //        }
            //            catch (Exception ex)
            //            {
            //                Handler.SAPApplication.MessageBox("Sayım oluşurken hata oluştu. " + ex.Message);
            //            }
            //            finally
            //            {
            //                try
            //                {
            //                    if (oProgressBar != null)
            //                    {
            //                        oProgressBar.Stop();

            //                        System.Runtime.InteropServices.Marshal.ReleaseComObject(oProgressBar);

            //                        oProgressBar = null;

            //                        GC.Collect();
            //                    }
            //                }
            //                catch (Exception)
            //{
            //}
            //            }
            #endregion
        }

        List<eklenenler> eklenenlers = new List<eklenenler>();
        class eklenenler
        {
            public string depokodu { get; set; }

            public string depoYeriId { get; set; }

            public string kalemKodu { get; set; }
        }
        public void MenuEvent(ref MenuEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;
        }

        public void RightClickEvent(ref ContextMenuInfo eventInfo, out bool BubbleEvent)
        {
            BubbleEvent = true;
        }

        public void secilmisBelgeNumaralariniYaz(string belgeNumaralari)
        {
            oEditBelgeNumaralari.Value = belgeNumaralari;

        }
    }
}