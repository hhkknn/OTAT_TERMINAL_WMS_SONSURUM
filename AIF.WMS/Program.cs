using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Xml;
using AIF.ObjectsDLL;
using AIF.ObjectsDLL.Events;
using AIF.ObjectsDLL.Lib;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using SAPbouiCOM.Framework;

namespace AIF.WMS
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
    (se, cert, chain, sslerror) =>
    {
        return true;
    };
                //commit..
                ConstVariables.oFnc.SetApplication();

                if (!(ConstVariables.oFnc.CookieConnect() == 0))
                {
                    Handler.SAPApplication.MessageBox("DI Api Conection Failed");
                    System.Environment.Exit(0);
                }
                if (!(ConstVariables.oFnc.ConnectionContext() == 0))
                {
                    Handler.SAPApplication.MessageBox("Failed to Connect Company");
                    System.Environment.Exit(0);
                }
                 #region CONSTRNG TABLOSU - ŞİRKET BİLGİLERİ - SİSTEMDE İLK KURULACAK VE DOLDURULACAK TABLODUR.MÜŞTERİ KODU ALANI BOŞ OLURSA ALAN VE TABLO AÇILMAZ.
                Dictionary<string, string> fields = new Dictionary<string, string>();

                if (!TableCreation.TableExists("AIF_WMS_CONSTRNG"))
                {
                    TableCreation.CreateTable("AIF_WMS_CONSTRNG", "Terminal Şirket Bilgi", SAPbobsCOM.BoUTBTableType.bott_Document);
                    TableCreation.CreateTable("AIF_WMS_CONSTRNG1", "Terminal Şirket Bilgi1", SAPbobsCOM.BoUTBTableType.bott_DocumentLines);

                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "CompanyDB", "Şirket Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 100, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "CompanyDBCode", "Şirket Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "LicenseServer", "Lisans Server", SAPbobsCOM.BoFieldTypes.db_Alpha, 30, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "Server", "Server", SAPbobsCOM.BoFieldTypes.db_Alpha, 30, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "UserName", "Kullanıcı Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "Password", "Şifre", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "DbServerType", "Veritabanı Tipi", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "MusteriKodu", "Müşteri Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, SAPbobsCOM.BoFldSubTypes.st_None);
                    //TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG", "MusteriKodu", "Müşteri Kodu", SAPbobsCOM.BoFieldTypes.db_Alpha, 20, SAPbobsCOM.BoFldSubTypes.st_None, clist: MusteriKodlari);

                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG1", "ButonAdi", "Buton Adı", SAPbobsCOM.BoFieldTypes.db_Alpha, 150, SAPbobsCOM.BoFldSubTypes.st_None);
                    TableCreation.CreateUserFields("@AIF_WMS_CONSTRNG1", "AktfPsf", "Aktif / Pasif", SAPbobsCOM.BoFieldTypes.db_Alpha, 1, SAPbobsCOM.BoFldSubTypes.st_None);

                }

                #region old
                //if (!UdoCreation.UDOExists("AIF_WMS_CONSTRNG"))
                //{
                //    fields.Clear();
                //    fields.Add("DocEntry", "Kod");
                //    fields.Add("U_CompanyDB", "Şirket Adı");
                //    fields.Add("U_CompanyDBCode", "Şirket Kodu");
                //    fields.Add("U_LicenseServer", "Lisans Server");
                //    fields.Add("U_Server", "Server");
                //    fields.Add("U_UserName", "Kullanıcı Adı");
                //    fields.Add("U_Password", "Şifre");
                //    fields.Add("U_DbServerType", "Veritabanı Tipi");
                //    fields.Add("U_MusteriKodu", "Müşteri Kodu"); //10TRMN = OTAT KODU   //30TRMN = ANATOLYA KODU //70TRMN = ZWILLING KODU    

                //    UdoCreation.RegisterUDOForDefaultForm("AIF_WMS_CONSTRNG", "AIF_WMS_CONSTRNG", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_CONSTRNG", "");
                //} 
                #endregion

                if (!UdoCreation.UDOExists("AIF_WMS_CONSTRNG"))
                {
                    fields.Clear();
                    fields.Add("DocEntry", "Kod");
                    fields.Add("U_CompanyDB", "Şirket Adı");
                    fields.Add("U_CompanyDBCode", "Şirket Kodu");
                    fields.Add("U_LicenseServer", "Lisans Server");
                    fields.Add("U_Server", "Server");
                    fields.Add("U_UserName", "Kullanıcı Adı");
                    fields.Add("U_Password", "Şifre");
                    fields.Add("U_DbServerType", "Veritabanı Tipi");
                    fields.Add("U_MusteriKodu", "Müşteri Kodu"); //10TRMN = OTAT KODU  20TRMN=YÖRÜK //30TRMN = ANATOLYA KODU //70TRMN = ZWILLING KODU    

                    List<FormColumn> fc = new List<FormColumn>();
                    List<ChildTable> chList = new List<ChildTable>();

                    ChildTable ch = new ChildTable();
                    ch.TableName = "AIF_WMS_CONSTRNG1";
                    fc = new List<FormColumn>();

                    fc.Add(new FormColumn { FormColumnAlias = "DocEntry", FormColumnDescription = "DocEntry", Editable = SAPbobsCOM.BoYesNoEnum.tNO });
                    fc.Add(new FormColumn { FormColumnAlias = "LineId", FormColumnDescription = "LineId", Editable = SAPbobsCOM.BoYesNoEnum.tNO });

                    fc.Add(new FormColumn { FormColumnAlias = "U_ButonAdi", FormColumnDescription = "Buton Adı", Editable = SAPbobsCOM.BoYesNoEnum.tYES });
                    fc.Add(new FormColumn { FormColumnAlias = "U_AktfPsf", FormColumnDescription = "Aktif / Pasif", Editable = SAPbobsCOM.BoYesNoEnum.tYES });

                    ch.FormColumn = fc;
                    chList.Add(ch);

                    UdoCreation.RegisterUDOWithChildTable("AIF_WMS_CONSTRNG", "Terminal Şirket Bilgi", SAPbobsCOM.BoUDOObjType.boud_Document, fields, "AIF_WMS_CONSTRNG", "", chList: chList);
                }

                #region AIF_WMS_CONSTRNG TABLOSUNDAKİ MÜŞTERİ KODU SORGUSU

                ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                string sql = "SELECT \"U_MusteriKodu\" FROM \"@AIF_WMS_CONSTRNG\" ";

                ConstVariables.oRecordset.DoQuery(sql);

                if (ConstVariables.oRecordset.RecordCount > 0)
                {
                    mKod = ConstVariables.oRecordset.Fields.Item("U_MusteriKodu").Value.ToString();

                    #region cc
                    try
                    {
                        #region config
                        IFirebaseConfig config = new FirebaseConfig
                        {
                            BasePath = "https://mfhcdc-e278f-default-rtdb.firebaseio.com/",
                        };

                        IFirebaseClient client;
                        #endregion

                        client = new FireSharp.FirebaseClient(config);

                        if (client == null)
                        {
                            //MessageBox.Show("Base Bağlantı hatasi.");
                        }
                        else
                        {
                            if (mKod == "")
                            {
                                Handler.SAPApplication.MessageBox("Müşteri kodu bulunamadı.");
                                System.Windows.Forms.Application.Exit();
                                return;
                            }
                            FirebaseResponse response = client.Get(mKod);

                            if (response != null)
                            {
                                Veri result = response.ResultAs<Veri>();

                                if (result != null)
                                {
                                    if (!string.IsNullOrEmpty(result.val.ToString()))
                                    {
                                        DateTime dt1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                                        DateTime dt3 = DateTime.ParseExact(result.val, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                                        //dt2 = new DateTime(dt2.Year, dt2.Month, dt2.Day);
                                        DateTime date = GetTime().Date;

                                        int d = Convert.ToInt32((dt3 - date).TotalDays);

                                        if (d <= 0)
                                        {
                                            //if (date == result.val)
                                            //{
                                            Handler.SAPApplication.MessageBox("Program kullanım süresi dolmuştur. Kullanıma devam edebilmek için AIFTEAM ile irtibata geçiniz.");
                                            #region menu remove
                                            try
                                            {
                                                //if (muhatapmutabakat == "Y")
                                                //{
                                                //    Handler.SAPApplication.Menus.RemoveEx("mhtpMtbkt");
                                                //} 
                                            }
                                            catch (Exception)
                                            {
                                            }
                                            #endregion
                                            System.Windows.Forms.Application.Exit();
                                            //System.Windows.Forms.Application.ExitThread();
                                            return;
                                            //Close();
                                            //}
                                        }

                                        if (d > 0)
                                        {
                                            if (!string.IsNullOrEmpty(result.inf.ToString()))
                                            {
                                                if (Convert.ToInt32(result.inf) != 0)
                                                {
                                                    if (d <= Convert.ToInt32(result.inf))
                                                    {
                                                        Handler.SAPApplication.MessageBox("Program kullanım süresinin bitimine " + d + " gün kalmıştır. Kullanıma devam edebilmek için AIFTEAM ile irtibata geçiniz.");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Handler.SAPApplication.MessageBox("Base hatası oluştu.");
                        return;
                    }
                    #endregion cc
                }
                else
                {
                    #region ürün yetki tablosu kurulu değil ise ilk onu açar
                    try
                    {
                        XmlDocument XmlDoc = ConstVariables.oFnc.getXMLDocument(Assembly.GetExecutingAssembly().GetManifestResourceStream("AIF.WMS.FormsView.Menu.xml"));
                        ConstVariables.oFnc.XmlMenuImport(XmlDoc);
                        Handler.SAPApplication.LoadBatchActions(XmlDoc.InnerXml);
                    }
                    catch (Exception ex)
                    {
                        Handler.SAPApplication.MessageBox(ex.ToString() + Environment.NewLine + "ExitThread");
                        System.Windows.Forms.Application.ExitThread();
                    }
                    #endregion
                }

                System.Runtime.InteropServices.Marshal.ReleaseComObject(ConstVariables.oRecordset);
                ConstVariables.oRecordset = null;
                GC.Collect();

                #region CREATETABLE
                try
                {

                    if (mKod != "" && mKod != null)
                    {
                        DataTables.CreateTables.CreateAndCheckFields();


                        try
                        {
                            XmlDocument XmlDoc = ConstVariables.oFnc.getXMLDocument(Assembly.GetExecutingAssembly().GetManifestResourceStream("AIF.WMS.FormsView.Menu.xml"));
                            ConstVariables.oFnc.XmlMenuImport(XmlDoc);
                            Handler.SAPApplication.LoadBatchActions(XmlDoc.InnerXml);
                        }
                        catch (Exception ex)
                        {
                            Handler.SAPApplication.MessageBox(ex.ToString() + Environment.NewLine + "ExitThread");
                            System.Windows.Forms.Application.ExitThread();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Handler.SAPApplication.MessageBox("Hata oluştu" + ex.Message);
                }
                #endregion CREATETABLE 


                #endregion AIF_WMS_CONSTRNG TABLOSUNDAKİ MÜŞTERİ KODU SORGUSU

                #endregion CONSTRNG TABLOSU - ŞİRKET BİLGİLERİ - SİSTEMDE İLK KURULACAK VE DOLDURULACAK TABLODUR.MÜŞTERİ KODU ALANI BOŞ OLURSA ALAN VE TABLO AÇILMAZ.

                #region sistem seperatör
                ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

                ConstVariables.oRecordset = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                ConstVariables.oRecordset1 = (SAPbobsCOM.Recordset)ConstVariables.oCompanyObject.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                ConstVariables.oRecordset.DoQuery("Select \"DecSep\",\"ThousSep\" from \"OADM\" ");

                decimalSeperator = ConstVariables.oRecordset.Fields.Item("DecSep").Value.ToString();
                thousandsSeperator = ConstVariables.oRecordset.Fields.Item("ThousSep").Value.ToString();
                #endregion

                System.Windows.Forms.Application.Run();


            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox(ex.Message);
                System.Windows.Forms.Application.ExitThread();
            }
        }
        public static string decimalSeperator = "";
        public static string thousandsSeperator = "";
        public static string mKod;

        static void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }

        public static DateTime GetTime()
        {
            try
            {
                using (var response =
                  WebRequest.Create("http://www.google.com").GetResponse())
                    //string todaysDates =  response.Headers["date"];
                    return DateTime.ParseExact(response.Headers["date"],
                        "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                        CultureInfo.InvariantCulture.DateTimeFormat,
                        DateTimeStyles.AssumeUniversal);
            }
            catch (WebException)
            {
                return DateTime.Now; //In case something goes wrong. 
            }
        }

        public class Veri
        {
            public string val { get; set; }
            public string inf { get; set; }
        }
    }
}
