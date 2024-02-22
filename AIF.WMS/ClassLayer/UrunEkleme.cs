using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Abstarct;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib;
using AIF.ObjectsDLL.Utils;
using AIF.WMS.HelperClass;
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
    public class UrunEkleme
    {
        [ItemAtt(AIFConn.UrunEklemeUID)]
        public SAPbouiCOM.Form frmUrunEkleme;

        [ItemAtt("Item_26")]
        public SAPbouiCOM.Matrix oMatrix;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.Button btnSec;

        [ItemAtt("2")]
        public SAPbouiCOM.Button btnIptal;

        [ItemAtt("Item_12")]
        public SAPbouiCOM.EditText oEditSonTeslimatTarihi;

        [ItemAtt("Item_14")]
        public SAPbouiCOM.EditText oEditSiparisNumarasi;

        [ItemAtt("Item_24")]
        public SAPbouiCOM.EditText oEditMuhatapKodu;

        [ItemAtt("Item_17")]
        public SAPbouiCOM.CheckBox oChkSifirMiktarlar;

        [ItemAtt("Item_19")]
        public SAPbouiCOM.CheckBox oChkDepoSecimi;

        [ItemAtt("Item_21")]
        public SAPbouiCOM.EditText oEditDepolar;

        private string header = @"<?xml version=""1.0"" encoding=""UTF-16"" ?><dbDataSources uid=""@AIF_WMS_SIPKAR1""><rows>{0}</rows></dbDataSources>";

        private string row = "<row>" +
            "<cells>" +
            "<cell><uid>U_SiparisNumarasi</uid><value>{0}</value></cell>" +
            "<cell><uid>U_SiparisTarihi</uid><value>{1}</value></cell>" +
            "<cell><uid>U_TeslimatTarihi</uid><value>{2}</value></cell>" +
            "<cell><uid>U_SipSatirNo</uid><value>{3}</value></cell>" +
            "<cell><uid>U_UrunKodu</uid><value>{4}</value></cell>" +
            "<cell><uid>U_UrunTanimi</uid><value>{5}</value></cell>" +
            "<cell><uid>U_UrunYTanim</uid><value>{6}</value></cell>" +
            "<cell><uid>U_TopSipMik</uid><value>{7}</value></cell>" +
            "<cell><uid>U_SevkSipMik</uid><value>{8}</value></cell>" +
            "<cell><uid>U_AcikSipMik</uid><value>{9}</value></cell>" +
            "<cell><uid>U_PlanSipMik</uid><value>{10}</value></cell>" +
            "<cell><uid>U_SipDepoKodu</uid><value>{11}</value></cell>" +
            "<cell><uid>U_DepoStokMik</uid><value>{12}</value></cell>" +
            "<cell><uid>U_GenelStokMik</uid><value>{13}</value></cell>" +
            "<cell><uid>U_SiraNo</uid><value>{14}</value></cell>" +
            "<cell><uid>U_MuhRefNo</uid><value>{15}</value></cell>" +
            "<cell><uid>U_MuhKatNo</uid><value>{16}</value></cell>" +
            "<cell><uid>U_BirimFiyat</uid><value>{17}</value></cell>" +
            "<cell><uid>U_HesapSipMik</uid><value>{18}</value></cell>" +
            "<cell><uid>U_TopSatirTutar</uid><value>{19}</value></cell>" +
            "</cells>" +
            "</row>";

        public void LoadForms(string _musteriKodu, List<_SiparisKarsilama> _oncekiEklenenlers)
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.UrunEklemeXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.UrunEklemeXML));
            Functions.CreateUserOrSystemFormComponent<UrunEkleme>(AIFConn.UrunEkle);
            musteriKodu = _musteriKodu;
            oEditMuhatapKodu.Value = musteriKodu;

            oncekiEklenenlers = _oncekiEklenenlers;
            InitForms();
        }

        string musteriKodu = "";
        SAPbouiCOM.DataTable oDataTable = null;
        public void InitForms()
        {
            try
            {
                frmUrunEkleme.Freeze(true);

                oDataTable = frmUrunEkleme.DataSources.DataTables.Add("DATA");


                oEditMuhatapKodu.Item.AffectsFormMode = false;
                oEditSiparisNumarasi.Item.AffectsFormMode = false;
                oEditSonTeslimatTarihi.Item.AffectsFormMode = false;
                oChkSifirMiktarlar.Item.AffectsFormMode = false;
                oChkSifirMiktarlar.Item.AffectsFormMode = false;


                oMatrix.Item.AffectsFormMode = false;

                listele(false);

                //oMatrix.Columns.Item("Col_19").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
                oMatrix.Columns.Item("Col_20").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;

            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox(ex.Message);
            }
            finally
            {
                frmUrunEkleme.Freeze(false);
            }
        }

        List<_SiparisKarsilama> oncekiEklenenlers = new List<_SiparisKarsilama>();
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

        bool formkapaniyor = false;
        public bool SAP_ItemEvent(string FormUID, ref ItemEvent pVal, ref bool BubbleEvent)
        {
            BubbleEvent = true;

            switch (pVal.EventType)
            {
                case BoEventTypes.et_ALL_EVENTS:
                    break;

                case BoEventTypes.et_ITEM_PRESSED:
                    if (pVal.ItemUID == "Item_17" && !pVal.BeforeAction)
                    {
                        try
                        {
                            frmUrunEkleme.Freeze(true);

                            //if (frmSiparisKarsilama.Mode == BoFormMode.fm_OK_MODE || frmSiparisKarsilama.Mode == BoFormMode.fm_UPDATE_MODE)
                            //{
                            //    Handler.SAPApplication.MessageBox("Ekli olan belge için filtreleme tekrar yapılamaz.");
                            //    BubbleEvent = false;
                            //    return false;
                            //}

                            //if (frmSiparisKarsilama.Mode == BoFormMode.fm_FIND_MODE)
                            //{
                            //    return false;
                            //}

                            listele(oChkSifirMiktarlar.Checked);
                        }
                        catch (Exception)
                        {
                        }
                        finally
                        {
                            frmUrunEkleme.Freeze(false);
                        }
                    }
                    else if (pVal.ItemUID == "Item_1" && !pVal.BeforeAction && formkapaniyor)
                    {
                        AIFConn.SipKarsi.modDegistir();
                        formkapaniyor = false;
                    }
                    break;

                case BoEventTypes.et_KEY_DOWN:
                    break;

                case BoEventTypes.et_GOT_FOCUS:
                    break;

                case BoEventTypes.et_LOST_FOCUS:
                    if (pVal.ColUID == "Col_9" && !pVal.BeforeAction)
                    {

                    }
                    break;

                case BoEventTypes.et_COMBO_SELECT:

                    break;

                case BoEventTypes.et_CLICK:
                    if (pVal.ItemUID == "Item_1" && !pVal.BeforeAction)
                    {
                        try
                        {
                            string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                            var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                        select new _SiparisKarsilama
                                        {
                                            siraNo = x.ElementsBeforeSelf().Count() + 1,
                                            siparisNumarasi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_0" select new XElement(y.Element("Value"))).First().Value,
                                            siparisTarihi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value.Contains("/") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value.Contains(".") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_1" select new XElement(y.Element("Value"))).First().Value, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : "",
                                            teslimatTarihi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value.Contains("/") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value.Contains(".") ? DateTime.ParseExact((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_2" select new XElement(y.Element("Value"))).First().Value, "dd.MM.yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd") : "",
                                            siparisSatirNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_3" select new XElement(y.Element("Value"))).First().Value,
                                            urunKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_4" select new XElement(y.Element("Value"))).First().Value,
                                            urunTanimi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_5" select new XElement(y.Element("Value"))).First().Value,
                                            urunYTanimi = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_21" select new XElement(y.Element("Value"))).First().Value,
                                            toplamSatisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_6" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            sevksiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_7" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            acikSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_8" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            planlananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            siparisDepoKodu = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_10" select new XElement(y.Element("Value"))).First().Value,
                                            depoStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_11" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            genelStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_12" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            paletNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_15" select new XElement(y.Element("Value"))).First().Value,
                                            toplananMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_13" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            muhatapReferansNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_16" select new XElement(y.Element("Value"))).First().Value,
                                            muhatapKatalogNo = (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_17" select new XElement(y.Element("Value"))).First().Value,
                                            birimFiyat = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_18" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            hesaplananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_19" select new XElement(y.Element("Value"))).First().Value.ToString()),
                                            toplamSatirTutari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_20" select new XElement(y.Element("Value"))).First().Value.ToString()),

                                        }).ToList();

                            List<_SiparisKarsilama> finalList = new List<_SiparisKarsilama>();
                            finalList.AddRange(rows.Where(x => x.planlananSiparisMiktari > 0).ToList());




                            AIFConn.SipKarsi.UrunEkle(finalList);

                            try
                            {
                                //formkapaniyor = true;
                                frmUrunEkleme.Mode = BoFormMode.fm_OK_MODE;
                                frmUrunEkleme.Close();
                                //frmUrunEkleme.Items.Item("2").Click();

                                AIFConn.SipKarsi.modDegistir();
                            }
                            catch (Exception)
                            {

                            }


                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox("Aktarımda Hata oluştu." + ex.Message);
                        }

                    }
                    else if (pVal.ItemUID == "Item_20" && !pVal.BeforeAction)
                    {
                        try
                        {
                            AIFConn.DepSecim.LoadForms(oEditDepolar.Value);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (pVal.ItemUID == "Item_22" && !pVal.BeforeAction)
                    {
                        try
                        {
                            listele(oChkSifirMiktarlar.Checked);
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
                    if (pVal.ItemUID == "Item_26" && pVal.ColUID == "Col_9" && pVal.ItemChanged && !pVal.BeforeAction)
                    {
                        var depoStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_11").Cells.Item(pVal.Row).Specific).Value.ToString());
                        var planlananMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value.ToString());
                        var genelStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_12").Cells.Item(pVal.Row).Specific).Value.ToString());
                        var acikMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_8").Cells.Item(pVal.Row).Specific).Value.ToString());
                        var urunKodu = ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_4").Cells.Item(pVal.Row).Specific).Value.ToString();


                        if (planlananMiktar == 0)
                        {
                            return false;
                        }
                        if (genelStokMiktari >= planlananMiktar)
                        {
                            if (planlananMiktar > depoStokMiktari)
                            {
                                Handler.SAPApplication.MessageBox("İlgili Ürün Sevkiyat Deposunda Bulunmamaktadır. Lütfen Diğer Depoları Kontrol Ediniz.");

                                BubbleEvent = false;
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = "0";
                                return false;
                            }
                        }
                        else
                        {
                            if (planlananMiktar > depoStokMiktari)
                            {
                                Handler.SAPApplication.MessageBox("Planlanan Miktar Depo Stok Miktarından Büyük Olamaz. Maksimum Miktar Otomatik Olarak Atanmıştır.");

                                BubbleEvent = false;
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = HelperClass.parseNumber_Seperator.setDoubleVal(depoStokMiktari.ToString()).ToString();
                                return false;
                            }
                        }

                        string xml = oMatrix.SerializeAsXML(BoMatrixXmlSelect.mxs_All);
                        var rows = (from x in XDocument.Parse(xml).Descendants("Row")
                                    where (from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_4" select new XElement(y.Element("Value"))).First().Value == urunKodu
                                    select new _SiparisKarsilama
                                    {
                                        //planlananSiparisMiktari = Convert.ToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value),
                                        planlananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in x.Element("Columns").Elements("Column") where y.Element("ID").Value == "Col_9" select new XElement(y.Element("Value"))).First().Value.ToString()), // seperator den 100000 geldiğindn hata veriyor
                                    }).ToList();

                        if (rows.Count > 0)
                        {
                            if (rows.Sum(x => x.planlananSiparisMiktari) > depoStokMiktari)
                            {
                                Handler.SAPApplication.MessageBox("Belge Üzerinde Seçilen İlgili Ürün Miktarı Sevkiyat Depo Miktarından Büyük Girilemez.");
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = "0";
                                return false;
                            }
                        }


                        double YuzdeOnFazla = acikMiktar + ((acikMiktar * 10) / 100);
                        double YuzdeOnAz = acikMiktar - ((acikMiktar * 10) / 100);

                        if (planlananMiktar > YuzdeOnFazla)
                        {
                            var resp = Handler.SAPApplication.MessageBox("Açık Miktardan %10 Miktar Fazlasını Girdiniz. Devam Etmek İstiyor Musunuz?", 1, "Evet", "Hayır");
                            if (resp == 2)
                            {
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = "0";
                            }
                        }

                        if (planlananMiktar < YuzdeOnAz)
                        {
                            var resp = Handler.SAPApplication.MessageBox("Açık Miktardan %10 Az Miktarını Girdiniz. Devam Etmek İstiyor Musunuz?", 1, "Evet", "Hayır");
                            if (resp == 2)
                            {
                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_9").Cells.Item(pVal.Row).Specific).Value = "0";
                            }
                        }


                        ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_19").Cells.Item(pVal.Row).Specific).Value = HelperClass.parseNumber_Seperator.setDoubleVal(planlananMiktar.ToString()).ToString();

                        double toplamSatirTutari = 0;
                        double birimFiyat = 0;

                        birimFiyat = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_18").Cells.Item(pVal.Row).Specific).Value.ToString());


                        toplamSatirTutari = birimFiyat * planlananMiktar;

                        try
                        {
                            frmUrunEkleme.Freeze(true);
                            oMatrix.Columns.Item("Col_20").Editable = true;
                            //((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_20").Cells.Item(pVal.Row).Specific).Value = toplamSatirTutari.ToString();
                            ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_20").Cells.Item(pVal.Row).Specific).Value = HelperClass.parseNumber_Seperator.setDoubleVal(toplamSatirTutari.ToString()).ToString();
                            oMatrix.Columns.Item("Col_20").Editable = false;
                        }
                        catch (Exception)
                        {
                        }

                        finally
                        {
                            oMatrix.AutoResizeColumns();

                            frmUrunEkleme.Freeze(false);
                        }


                    }
                    else if (pVal.ItemUID == "Item_26" && pVal.ColUID == "Col_19" && pVal.ItemChanged && !pVal.BeforeAction)
                    {
                        try
                        {
                            double toplamSatirTutari = 0;
                            double birimFiyat = 0;
                            double hesaplananMik = 0;

                            hesaplananMik = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_19").Cells.Item(pVal.Row).Specific).Value.ToString());

                            birimFiyat = HelperClass.parseNumber_Seperator.ConvertToDouble(((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_18").Cells.Item(pVal.Row).Specific).Value.ToString());


                            toplamSatirTutari = birimFiyat * hesaplananMik;

                            try
                            {
                                frmUrunEkleme.Freeze(true);
                                oMatrix.Columns.Item("Col_20").Editable = true;
                                //((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_20").Cells.Item(pVal.Row).Specific).Value =toplamSatirTutari.ToString();

                                ((SAPbouiCOM.EditText)oMatrix.Columns.Item("Col_20").Cells.Item(pVal.Row).Specific).Value = HelperClass.parseNumber_Seperator.setDoubleVal(toplamSatirTutari.ToString()).ToString();
                                oMatrix.Columns.Item("Col_20").Editable = false;
                            }
                            catch (Exception)
                            {

                            }

                            finally
                            {
                                oMatrix.AutoResizeColumns();
                                frmUrunEkleme.Freeze(false);

                            }
                        }
                        catch (Exception)
                        {
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


        //private void listele(bool sifirMiktarlariGoster)
        //{
        //    string condition = ConstVariables.oCompanyObject.DbServerType == BoDataServerTypes.dst_HANADB ? "IFNULL" : "ISNULL";

        //    string sql = "Select * from (Select T0.\"DocEntry\" as \"SiparisNumarasi\",T0.\"DocDate\" as \"SiparisTarihi\",T0.\"DocDueDate\" as \"TeslimatTarihi\",T1.\"LineNum\" as \"SiparisSatirNo\",T1.\"ItemCode\" as \"UrunKodu\",T1.\"Dscription\" as \"UrunTanimi\",T1.\"Quantity\" as \"ToplamSiparisMiktari\",(T1.\"Quantity\" -  T1.\"OpenQty\") as \"SevkSiparisMiktari\",T1.\"OpenQty\" - " + condition + "((Select SUM(T98.\"U_PlanSipMik\") from \"@AIF_WMS_SIPKAR1\" as T98 where T98.\"U_SiparisNumarasi\" = T0.\"DocEntry\" and T98.\"U_SipSatirNo\" = T1.\"LineNum\"),0) as \"AcikSiparisMiktari\", CAST(0 AS DECIMAL(15,2)) as \"PlanlananSiparisMiktari\",T1.\"WhsCode\" as \"SiparisDepoKodu\", (Select SUM(T99.\"OnHand\") from OITW as T99 where T99.\"ItemCode\" = T1.\"ItemCode\" and T99.\"WhsCode\" = T1.\"WhsCode\") as \"DepoStokMiktari\", (Select SUM(T99.\"OnHand\") from OITW as  T99 where T99.\"ItemCode\" = T1.\"ItemCode\" and T99.\"WhsCode\" != T1.\"WhsCode\") as \"GenelStokMiktari\" from ORDR as T0 INNER JOIN RDR1 AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"CardCode\" = '" + oEditMuhatapKodu.Value + "' and T1.\"LineStatus\" = 'O' ";

        //    if (oEditSonTeslimatTarihi.Value != "")
        //    {
        //        sql += " and T0.\"DocDueDate\" <='" + oEditSonTeslimatTarihi.Value + "'";
        //    }


        //    if (oEditSiparisNumarasi.Value != "")
        //    {
        //        sql += " and T0.\"DocEntry\" <='" + oEditSiparisNumarasi.Value + '"';
        //    }

        //    sql += " ) as tbl where tbl.\"AcikSiparisMiktari\">0 ";

        //    if (!sifirMiktarlariGoster)
        //    {
        //        sql += " and (tbl.\"DepoStokMiktari\">0 or tbl.\"GenelStokMiktari\">0)";

        //    }


        //    oDataTable.Clear();
        //    oMatrix.Clear();

        //    oDataTable.ExecuteQuery(sql);


        //    oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "SiparisNumarasi");
        //    oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "SiparisTarihi");
        //    oMatrix.Columns.Item("Col_2").DataBind.Bind("DATA", "TeslimatTarihi");
        //    oMatrix.Columns.Item("Col_3").DataBind.Bind("DATA", "SiparisSatirNo");
        //    oMatrix.Columns.Item("Col_4").DataBind.Bind("DATA", "UrunKodu");
        //    oMatrix.Columns.Item("Col_5").DataBind.Bind("DATA", "UrunTanimi");
        //    oMatrix.Columns.Item("Col_6").DataBind.Bind("DATA", "ToplamSiparisMiktari");
        //    oMatrix.Columns.Item("Col_7").DataBind.Bind("DATA", "SevkSiparisMiktari");
        //    oMatrix.Columns.Item("Col_8").DataBind.Bind("DATA", "AcikSiparisMiktari");
        //    oMatrix.Columns.Item("Col_9").DataBind.Bind("DATA", "PlanlananSiparisMiktari");
        //    oMatrix.Columns.Item("Col_10").DataBind.Bind("DATA", "SiparisDepoKodu");
        //    oMatrix.Columns.Item("Col_11").DataBind.Bind("DATA", "DepoStokMiktari");
        //    oMatrix.Columns.Item("Col_12").DataBind.Bind("DATA", "GenelStokMiktari");


        //    oMatrix.LoadFromDataSource();
        //    oMatrix.AutoResizeColumns();


        //    oMatrix.CommonSetting.FixedColumnsCount = 3; 
        //}


        private void listele(bool sifirMiktarlariGoster)
        {
            bool ilk = true;
            string condition = ConstVariables.oCompanyObject.DbServerType == BoDataServerTypes.dst_HANADB ? "IFNULL" : "ISNULL";

            //string sql = "Select * from (Select T0.\"DocEntry\" as \"SiparisNumarasi\",T0.\"DocDate\" as \"SiparisTarihi\",T0.\"DocDueDate\" as \"TeslimatTarihi\",T1.\"LineNum\" as \"SiparisSatirNo\",T1.\"ItemCode\" as \"UrunKodu\",T1.\"Dscription\" as \"UrunTanimi\",T1.\"Quantity\" as \"ToplamSiparisMiktari\",(T1.\"Quantity\" -  T1.\"OpenQty\") as \"SevkSiparisMiktari\",T1.\"OpenQty\" - " + condition + "((Select SUM(T98.\"U_PlanSipMik\") from \"@AIF_WMS_SIPKAR1\" as T98 where T98.\"U_SiparisNumarasi\" = T0.\"DocEntry\" and T98.\"U_SipSatirNo\" = T1.\"LineNum\"),0) as \"AcikSiparisMiktari\", 0 as \"PlanlananSiparisMiktari\",T1.\"WhsCode\" as \"SiparisDepoKodu\", (Select SUM(T99.\"OnHand\") from OITW as T99 where T99.\"ItemCode\" = T1.\"ItemCode\" and T99.\"WhsCode\" = T1.\"WhsCode\") as \"DepoStokMiktari\", (Select SUM(T99.\"OnHand\") from OITW as  T99 where T99.\"ItemCode\" = T1.\"ItemCode\" and T99.\"WhsCode\" != T1.\"WhsCode\" "; 

            string sql = "Select * from (Select T0.\"DocEntry\" as \"SiparisNumarasi\",T0.\"DocDate\" as \"SiparisTarihi\",T1.\"ShipDate\" as \"TeslimatTarihi\",T1.\"LineNum\" as \"SiparisSatirNo\",T1.\"ItemCode\" as \"UrunKodu\",T1.\"Dscription\" as \"UrunTanimi\",( select T6.FrgnName from OITM T6 where T6.ItemCode=T1.ItemCode) as 'YabanciAd',T1.\"Quantity\" as \"ToplamSiparisMiktari\",(T1.\"Quantity\" -  T1.\"OpenQty\") as \"SevkSiparisMiktari\",T1.\"OpenQty\" as \"AcikSiparisMiktari\", 0 as \"PlanlananSiparisMiktari\",T1.\"WhsCode\" as \"SiparisDepoKodu\", (Select SUM(T99.\"OnHand\") from OITW as T99 where T99.\"ItemCode\" = T1.\"ItemCode\" and T99.\"WhsCode\" = T1.\"WhsCode\") as \"DepoStokMiktari\", (Select SUM(T99.\"OnHand\") from OITW as  T99 where T99.\"ItemCode\" = T1.\"ItemCode\" and T99.\"WhsCode\" != T1.\"WhsCode\" ";

            if (oEditDepolar.Value != "")
            {
                var split = oEditDepolar.Value.Split('|');
                foreach (var item in split)
                {
                    if (ilk)
                    {
                        sql += " and T99.\"WhsCode\" NOT IN ('" + item + "'";
                        ilk = false;
                    }
                    else
                    {
                        sql += ",'" + item + "'";
                    }
                }

                sql += ")";
            }


            sql += ") as \"GenelStokMiktari\",'' as \"PaletNo\",0 as \"PlanlananMiktar\",T0.\"NumAtCard\" as \"MuhatapReferansNo\",(Select TOP 1 " + condition + "(\"Substitute\",'')  from OSCN as T77 where T77.\"CardCode\" = T0.\"CardCode\" and T77.\"ItemCode\" = T1.\"ItemCode\" and T77.\"IsDefault\" = 'Y') as \"MuhatapKatalogNo\",T1.\"Price\" as \"BirimFiyat\",0 as \"HesaplananMik\",CAST(0 as DECIMAL(15,2)) as \"ToplamSatirTutari\" from ORDR as T0 INNER JOIN RDR1 AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T0.\"CardCode\" = '" + oEditMuhatapKodu.Value + "' and T1.\"LineStatus\" = 'O' ";

            if (oEditSonTeslimatTarihi.Value != "")
            {
                sql += " and T0.\"DocDueDate\" <='" + oEditSonTeslimatTarihi.Value + "'";
            }


            if (oEditSiparisNumarasi.Value != "")
            {
                sql += " and T0.\"DocEntry\" <='" + oEditSiparisNumarasi.Value + "'";
            }

            sql += " ) as tbl where tbl.\"AcikSiparisMiktari\">0 ";

            ConstVariables.oRecordset.DoQuery(sql);

            string xmll = ConstVariables.oRecordset.GetFixedXML(SAPbobsCOM.RecordsetXMLModeEnum.rxmData);
            XDocument xDoc = XDocument.Parse(xmll);
            XNamespace ns = "http://www.sap.com/SBO/SDK/DI";
            var rows = (from t in xDoc.Descendants(ns + "Row")
                        select new _SiparisKarsilama()
                        {
                            siparisNumarasi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisNumarasi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            siparisTarihi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisTarihi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            teslimatTarihi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "TeslimatTarihi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            siparisSatirNo = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisSatirNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            urunKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "UrunKodu" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            urunTanimi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "UrunTanimi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString().Replace("&", "-"),
                            urunYTanimi = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "YabanciAd" select new XElement(y.Element(ns + "Value"))).First().Value.ToString().Replace("&", "-"),
                            toplamSatisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "ToplamSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            sevksiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SevkSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            acikSiparisMiktari = oncekiEklenenlers.Where(x => x.siparisNumarasi == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisNumarasi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString() && x.siparisSatirNo == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisSatirNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()).Count() > 0 ? HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "AcikSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()) - oncekiEklenenlers.Where(x => x.siparisNumarasi == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisNumarasi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString() && x.siparisSatirNo == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisSatirNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()).Select(y => y.planlananSiparisMiktari).FirstOrDefault() : HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "AcikSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),// listeye ekleme aşagıda kaldırılmış burada kaldırılması unutuldugundan miktarlar - geliyordu
                            //acikSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "AcikSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()), 
                            planlananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "PlanlananSiparisMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            siparisDepoKodu = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisDepoKodu" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            //depoStokMiktari = oncekiEklenenlers.Where(x => x.siparisNumarasi == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisNumarasi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString() && x.siparisSatirNo == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisSatirNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()).Count() > 0 ? HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "DepoStokMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()) - oncekiEklenenlers.Where(x => x.siparisNumarasi == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisNumarasi" select new XElement(y.Element(ns + "Value"))).First().Value.ToString() && x.siparisSatirNo == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisSatirNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()).Select(y => y.planlananSiparisMiktari).FirstOrDefault() : HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "DepoStokMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()), // listeye ekleme aşagıda kaldırılmış burada kaldırılması unutuldugundan miktarlar - geliyordu
                            //depoStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "DepoStokMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),

                            depoStokMiktari = oncekiEklenenlers.Where(x => x.urunKodu == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "UrunKodu" select new XElement(y.Element(ns + "Value"))).First().Value.ToString() && x.siparisDepoKodu == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisDepoKodu" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()).Count() > 0 ? HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "DepoStokMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()) - oncekiEklenenlers.Where(x => x.urunKodu == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "UrunKodu" select new XElement(y.Element(ns + "Value"))).First().Value.ToString() && x.siparisDepoKodu == (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "SiparisDepoKodu" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()).Sum(y => y.planlananSiparisMiktari) : HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "DepoStokMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            genelStokMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "GenelStokMiktari" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            siraNo = 0,
                            paletNo = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "PaletNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            toplananMiktar = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "PlanlananMiktar" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            muhatapReferansNo = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "MuhatapReferansNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            muhatapKatalogNo = (from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "MuhatapKatalogNo" select new XElement(y.Element(ns + "Value"))).First().Value.ToString(),
                            birimFiyat = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "BirimFiyat" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            hesaplananSiparisMiktari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "HesaplananMik" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),
                            toplamSatirTutari = HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "BirimFiyat" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()) * HelperClass.parseNumber_Seperator.ConvertToDouble((from y in t.Element(ns + "Fields").Elements(ns + "Field") where y.Element(ns + "Alias").Value == "HesaplananMik" select new XElement(y.Element(ns + "Value"))).First().Value.ToString()),

                        }).ToList();
            //rows.AddRange(oncekiEklenenlers);

            //var sonHaliRows = rows.GroupBy(x => x.siparisNumarasi).Select(yl => new _SiparisKarsilama { siparisNumarasi = yl.First().siparisNumarasi, depoStokMiktari = yl.First().depoStokMiktari, siparisSatirNo = yl.First().siparisSatirNo });

            //foreach (var item in sonHaliRows.Where(x => x.depoStokMiktari == 0))
            //{
            //    rows.RemoveAll(x => x.siparisNumarasi == item.siparisNumarasi && x.siparisSatirNo == item.siparisSatirNo);
            //}




            if (!sifirMiktarlariGoster)
            {
                rows.RemoveAll(x => x.depoStokMiktari == 0 && x.genelStokMiktari == 0);

            }

            rows.RemoveAll(x => x.acikSiparisMiktari <= 0);

            rows.RemoveAll(x => x.siparisNumarasi == "0");


            //string data2 = string.Join("", rows.Select(s => string.Format(row, s.siparisNumarasi, s.siparisTarihi, s.teslimatTarihi, s.siparisSatirNo, s.urunKodu, s.urunTanimi, s.toplamSatisMiktari, s.sevksiparisMiktari, s.acikSiparisMiktari, s.planlananSiparisMiktari, s.siparisDepoKodu, s.depoStokMiktari, s.genelStokMiktari, "", s.muhatapReferansNo, s.muhatapKatalogNo, s.birimFiyat, s.hesaplananSiparisMiktari, s.toplamSatirTutari)));

            string data2 = string.Join("", rows.Select(s => string.Format(row, s.siparisNumarasi, s.siparisTarihi, s.teslimatTarihi, s.siparisSatirNo, s.urunKodu, s.urunTanimi,s.urunYTanimi, s.toplamSatisMiktari, s.sevksiparisMiktari, s.acikSiparisMiktari, s.planlananSiparisMiktari, s.siparisDepoKodu, s.depoStokMiktari, s.genelStokMiktari, "", s.muhatapReferansNo, s.muhatapKatalogNo, parseNumber_Seperator.setDoubleVal(s.birimFiyat.ToString()).ToString(), s.hesaplananSiparisMiktari, parseNumber_Seperator.setDoubleVal(s.toplamSatirTutari.ToString()).ToString())));


            //string data2 = string.Join("", rows.Select(s => string.Format(row, s.siparisNumarasi, s.siparisTarihi, s.teslimatTarihi, s.siparisSatirNo, s.urunKodu, s.urunTanimi, s.toplamSatisMiktari, s.sevksiparisMiktari, s.acikSiparisMiktari, s.planlananSiparisMiktari, s.siparisDepoKodu, s.depoStokMiktari, s.genelStokMiktari, "", s.paletNo, s.toplananMiktar, "AA")).Take(5));//oldold


            var asdas = string.Format(header, data2);


            frmUrunEkleme.DataSources.DBDataSources.Item("@AIF_WMS_SIPKAR1").LoadFromXML(string.Format(header, data2));

            oMatrix.Columns.Item("Col_13").Visible = false;
            oMatrix.Columns.Item("Col_15").Visible = false;

            oMatrix.AutoResizeColumns();
            oMatrix.CommonSetting.FixedColumnsCount = 4;


        }

        public void depolariYaz(string depolar)
        {
            oEditDepolar.Value = depolar;
            if (oEditDepolar.Value != "")
            {
                oChkDepoSecimi.Item.Enabled = true;
                oChkDepoSecimi.Checked = true;
            }
            else
            {
                oChkDepoSecimi.Item.Enabled = true;
                oChkDepoSecimi.Checked = false;
            }

            oChkDepoSecimi.Item.Enabled = false;
        }
    }
}