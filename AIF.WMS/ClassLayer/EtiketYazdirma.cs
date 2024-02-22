using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib; 
using CrystalDecisions.CrystalReports.Engine;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AIF.WMS.ClassLayer
{
    public class EtiketYazdirma
    {
        [ItemAtt(AIFConn.EtiketYazdirmaUID)]
        public SAPbouiCOM.Form frmEtiketYazdirma;

        [ItemAtt("Item_1")]
        public SAPbouiCOM.EditText oEdtKalemKodu;
        [ItemAtt("Item_3")]
        public SAPbouiCOM.EditText oEdtKalemTanimi;
        [ItemAtt("Item_5")]
        public SAPbouiCOM.EditText oEdtKaliteKontrolTarihi;
        [ItemAtt("Item_7")]
        public SAPbouiCOM.EditText oEdtYazdirmaMiktari;
        [ItemAtt("Item_9")]
        public SAPbouiCOM.EditText oEdtQuantity;
        [ItemAtt("Item_13")]
        public SAPbouiCOM.EditText oEdtBelgeNo;
        [ItemAtt("Item_15")]
        public SAPbouiCOM.ComboBox oCmbPrinter;

        [ItemAtt("Item_17")]
        public SAPbouiCOM.EditText oEdtMuhKatalogNo;

        [ItemAtt("Item_11")]
        public SAPbouiCOM.Button oBtnMuhKatalogNo;

        //public static string katalogNo = "";
        public void LoadForms(string _kalemKodu, string _kalemTanimi)
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.EtiketYazdirmaXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.EtiketYazdirmaXML));
            Functions.CreateUserOrSystemFormComponent<EtiketYazdirma>(AIFConn.EtktYzdr);

            kalemKodu = _kalemKodu;
            kalemTanimi = _kalemTanimi;
            InitForms();
        }
        string kalemKodu = "";
        string kalemTanimi = "";
        public void InitForms()
        {
            foreach (var item in PrinterSettings.InstalledPrinters)
            {
                oCmbPrinter.ValidValues.Add(item.ToString(), item.ToString());
            }
            //default
            PrintDocument printDocument = new PrintDocument();
            string defaultPrinter = printDocument.PrinterSettings.PrinterName;
            oCmbPrinter.Select(defaultPrinter, BoSearchKey.psk_ByValue);

            oEdtKalemKodu.Value = kalemKodu;
            oEdtKalemTanimi.Value = kalemTanimi;

            var query = "Select MAX(T0.\"DocNum\") as \"DocNum\" from \"OPDN\" AS T0 INNER JOIN \"PDN1\" AS T1 ON T0.\"DocEntry\" = T1.\"DocEntry\" where T1.\"ItemCode\" = '" + kalemKodu + "' ";

            ConstVariables.oRecordset.DoQuery(query);

            oEdtBelgeNo.Value = ConstVariables.oRecordset.Fields.Item(0).Value.ToString();

            oEdtKaliteKontrolTarihi.String = "A";

            oEdtYazdirmaMiktari.Item.Click();
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
                    if (!pVal.BeforeAction && pVal.ItemUID == "Item_10")
                    {
                        try
                        {
                            oEdtYazdirmaMiktari.Item.Click();
                            Print();
                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox("Yazdırma sırasında hata oluştu." + ex.Message);
                        }
                    }
                    else if (!pVal.BeforeAction && pVal.ItemUID == "Item_11")
                    {
                        try
                        {
                            if (oEdtKalemKodu.Value != "")
                            {
                                AIFConn.EtYazParam.LoadForms(oEdtKalemKodu.Value);
                            }
                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox("Hata oluştu." + ex.Message);
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


        private void Print()
        {
            #region Crystal reports işlemlerinin yapıldığı yer

            try
            {
                WriteToFile("Yazdırma Başladı.");
                int yazdirmakistenilenMikar = Convert.ToInt32(oEdtYazdirmaMiktari.Value);
                int ikilidenyazdirilacakMiktar_Gecici = 0;
                int birliyazilacak = 0;

                if (yazdirmakistenilenMikar % 2 != 0)
                {
                    ikilidenyazdirilacakMiktar_Gecici = yazdirmakistenilenMikar - 1;
                    birliyazilacak = 1;
                }
                else
                {
                    ikilidenyazdirilacakMiktar_Gecici = yazdirmakistenilenMikar;
                }

                int ikilidenyazdirilacakMiktar = Convert.ToInt32(ikilidenyazdirilacakMiktar_Gecici) / 2;
                WriteToFile("İkiliden yazdırılacak Miktar " + ikilidenyazdirilacakMiktar);
         
                string path = "";
                if (ikilidenyazdirilacakMiktar > 0)
                {
                    AIFCrystalWS.AIFCRYSTALSERVICE aifcrsy = new AIFCrystalWS.AIFCRYSTALSERVICE();

                    DateTime dt_2 = new DateTime(Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(0, 4)), Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(4, 2)), Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(6, 2)));

                    List<AIFCrystalWS.parametreler> param = new List<AIFCrystalWS.parametreler>();
                    param.Add(new AIFCrystalWS.parametreler
                    {
                        sira = 0,
                        parametreDegeri = oEdtKalemKodu.Value

                    });
                    param.Add(new AIFCrystalWS.parametreler
                    {
                        sira = 1,
                        parametreDegeri = dt_2

                    });
                    param.Add(new AIFCrystalWS.parametreler
                    {
                        sira = 2,
                        parametreDegeri = Convert.ToInt32(oEdtBelgeNo.Value)

                    });
                    param.Add(new AIFCrystalWS.parametreler
                    {
                        sira = 3,
                        parametreDegeri = oEdtQuantity.Value

                    });
                    param.Add(new AIFCrystalWS.parametreler
                    {
                        sira = 4,
                        parametreDegeri = oEdtMuhKatalogNo.Value != "" ? oEdtMuhKatalogNo.Value : oEdtKalemKodu.Value

                    });






                    //var resp = aifcrsy.PdfOlustur(ConstVariables.oCompanyObject.CompanyDB, "Ant_105_70_mm_2", param.ToArray(), ConstVariables.oCompanyObject.Server, "Eropa2018!", "Deneme");

                    //if (resp != null && resp != "")
                    //{
                    //    if (resp.Contains("hata"))
                    //    {
                    //        //XtraMessageBox.Show("PDF oluşturma sırasında hata oluştu!" + resp, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            
                    //        return;
                    //    }

                    //    string yenidosya = Path.GetTempFileName() + "Deneme_" + Guid.NewGuid().ToString() + ".pdf";



                    //    Byte[] bytes_2 = Convert.FromBase64String(resp);
                    //    File.WriteAllBytes(yenidosya, bytes_2);

                    //    SendToPrinter(yenidosya);
                    //}
                    //else
                    //{
                    //    //XtraMessageBox.Show("PDF oluşturma sırasında hata oluştu!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}

                    //return;
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "Ant_105_70_mm_2.rpt";
                    WriteToFile("Ant_105_70_mm_2.rpt dosya yolu " + path);


                    ReportDocument cryRpt = new ReportDocument();

                    WriteToFile("ReportDocument geçildi ");
                    cryRpt.Load(path);
                    WriteToFile("Load geçildi ");

                    string server = ConstVariables.oCompanyObject.Server;

                    cryRpt.SetDatabaseLogon("sa", "Eropa2018!", server, ConstVariables.oCompanyObject.CompanyDB);


                    WriteToFile("SetDatabaseLogon geçildi ");

                    cryRpt.VerifyDatabase();

                    WriteToFile("VerifyDatabase geçildi ");

                    cryRpt.SetParameterValue(0, oEdtKalemKodu.Value);

                    WriteToFile("SetParameterValue 0 geçildi ");

                    DateTime dt = new DateTime(Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(0, 4)), Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(4, 2)), Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(6, 2)));



                    WriteToFile("dt oluştu " + dt.ToShortDateString());

                    cryRpt.SetParameterValue(1, dt);

                    WriteToFile("SetParameterValue 1 geçildi ");

                    cryRpt.SetParameterValue(2, oEdtBelgeNo.Value == "" ? "0" : oEdtBelgeNo.Value);

                    WriteToFile("SetParameterValue 2 geçildi ");

                    cryRpt.SetParameterValue(3, oEdtQuantity.Value == "" ? "1" : oEdtQuantity.Value);

                    WriteToFile("SetParameterValue 3 geçildi ");

                    cryRpt.SetParameterValue(4, oEdtMuhKatalogNo.Value != "" ? oEdtMuhKatalogNo.Value : oEdtKalemKodu.Value);

                    WriteToFile("SetParameterValue 4 geçildi ");

                    cryRpt.PrintOptions.PrinterName = oCmbPrinter.Value.Trim();


                    WriteToFile("Printer name alındı " + cryRpt.PrintOptions.PrinterName);

                    cryRpt.PrintToPrinter(oEdtYazdirmaMiktari.Value == "" ? 1 : ikilidenyazdirilacakMiktar, false, 0, 1);

                    WriteToFile("PrintToPrinter geçildi ");

                    cryRpt.Close();
                    WriteToFile("Close geçildi ");
                }

                if (birliyazilacak == 1)
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory + "Ant_105_70_mm_1.rpt";
                    ReportDocument cryRpt = new ReportDocument();
                    cryRpt.Load(path);

                    string server = ConstVariables.oCompanyObject.Server;

                    cryRpt.SetDatabaseLogon("sa", "Eropa2018!", server, ConstVariables.oCompanyObject.CompanyDB);

                    cryRpt.VerifyDatabase();

                    cryRpt.SetParameterValue(0, oEdtKalemKodu.Value);

                    DateTime dt = new DateTime(Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(0, 4)), Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(4, 2)), Convert.ToInt32(oEdtKaliteKontrolTarihi.Value.Substring(6, 2)));

                    cryRpt.SetParameterValue(1, dt);
                    cryRpt.SetParameterValue(2, oEdtBelgeNo.Value == "" ? "0" : oEdtBelgeNo.Value);
                    cryRpt.SetParameterValue(3, oEdtQuantity.Value == "" ? "1" : oEdtQuantity.Value);
                    cryRpt.SetParameterValue(4, oEdtMuhKatalogNo.Value != "" ? oEdtMuhKatalogNo.Value : oEdtKalemKodu.Value);

                    cryRpt.PrintOptions.PrinterName = oCmbPrinter.Value.Trim();

                    cryRpt.PrintToPrinter(1, false, 0, 1);

                    cryRpt.Close();
                }
                //  }

                Handler.SAPApplication.StatusBar.SetText("İşlem tamamlandı.", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);

                try
                {
                    frmEtiketYazdirma.Close();
                }
                catch (Exception)
                {
                }
            }
            catch (Exception x)
            {
                Handler.SAPApplication.MessageBox("Print method hatası : " + x.Message);
            }

            #endregion Crystal reports işlemlerinin yapıldığı yer
        }

        public void SendToPrinter(string filePath, string Printer)
        {
            try
            {
                Process proc = new Process();
                proc.Refresh();

                proc.StartInfo = new ProcessStartInfo()
                {
                    //UseShellExecute = true,
                    //WindowStyle = ProcessWindowStyle.Hidden,
                    Verb = "print",
                    FileName = filePath,
                    //Arguments = String.Format("/t \"{0}\" \"{1}\"", filePath, Printer),
                    CreateNoWindow = true,
                   
                };
                proc.Start();
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //if (proc.HasExited == false)
                //{
                //    proc.WaitForExit(20000);
                //}
                //proc.EnableRaisingEvents = true;
                proc.Close();
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //proc.StartInfo.Verb = "Print";
                //proc.StartInfo.FileName = filePath;
                //proc.StartInfo.Arguments = String.Format("/t \"{0}\" \"{1}\"", filePath, Printer);
                //proc.StartInfo.UseShellExecute = false;
                //proc.StartInfo.CreateNoWindow = true;

            }
            catch (Exception e)
            {
            }
        }
        private void WriteToFile(string Message)
        {
            return;
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\YazdirmaLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

        private void SendToPrinter(string path)
        {
            //ProcessStartInfo info = new ProcessStartInfo();
            //info.Verb = "print";
            //info.FileName = path;
            //info.CreateNoWindow = true;




            //info.WindowStyle = ProcessWindowStyle.Hidden;

            //Process p = new Process();
            //p.StartInfo = info;
            //p.Start();

            //p.WaitForInputIdle();
            //System.Threading.Thread.Sleep(3000);
            //if (false == p.CloseMainWindow())
            //    p.Kill();

            PrintDocument document = new PrintDocument();
            document.PrinterSettings.PrintFileName = path;

            document.Print();

        }
    }
}
