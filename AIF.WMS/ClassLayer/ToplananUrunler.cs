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
    public class ToplananUrunler
    {
        [ItemAtt(AIFConn.ToplananUrunlerUID)]
        public SAPbouiCOM.Form frmToplananUrun;

        [ItemAtt("Item_2")]
        public SAPbouiCOM.Matrix oMatrix;

        SAPbouiCOM.DataTable oDataTable = null;

        public void LoadForms(string _docEntry)
        {
            ConstVariables.oFnc.LoadSAPXML(AIFConn.ToplananUrunlerXML, Assembly.GetExecutingAssembly().GetManifestResourceStream(AIFConn.ToplananUrunlerXML));
            Functions.CreateUserOrSystemFormComponent<ToplananUrunler>(AIFConn.TplnUrun);

            docEntry = _docEntry;
            InitForms();
        }

        string docEntry = "";
        public void InitForms()
        {
            try
            {
                frmToplananUrun.Freeze(true);

                oDataTable = frmToplananUrun.DataSources.DataTables.Add("DATA");

                string condition = ConstVariables.oCompanyObject.DbServerType == BoDataServerTypes.dst_HANADB ? "IFNULL" : "ISNULL"; 

                #region
                string sql = "";


                // sql= "  select * from (Select T0.\"U_SiparisNumarasi\" as \"SiparisNumarasi\",T1.\"DocDate\" as \"SiparisTarihi\",T1.\"DocDueDate \" as \"TeslimatTarihi\",T0.\"U_SiparisSatirNo\" as \"SiparisSatirNo\",T2.\"ItemCode\" as \"UrunKodu\",T2.\"Dscription\" as \"UrunTanimi\",T2.\"Quantity\" as \"ToplamSiparisMiktari\", (T2.\"Quantity\" - T2.\"OpenQty\") as \"SevkSipMiktari\", (T2.\"OpenQty\" - T0.\"U_Miktar\") AS \"AcikSiparisMiktari\",T2.\"WhsCode\" as \"SiparisDepoKodu\",T0.\"U_Miktar\" as \"ToplananMiktar\",T0.\"U_PaletNo\" as \"PaletNo\",(T2.\"OpenQty\" - ISNULL(T0.\"U_Miktar\", 0)) as \"PlanlananSiparisMiktari\",(SELECT Count(\"DocEntry\") FROM \"@AIF_WMS_KNTYNR1\" AS T98 WHERE T98.\"U_SiparisNo\" = T0.\"U_SiparisNumarasi\" and T98.\"U_SipSatirNo\" = T0.\"U_SiparisSatirNo\") AS \"KonteynerVarmi\",T0.\"U_TeslimatNo\",T0.\"DocEntry\" AS \"ToplananDocEntry\",T1.\"NumAtCard\" as \"MuhatapReferansNo\",(Select TOP 1 " + condition + "(\"Substitute\",'')  from OSCN as T77 where T77.\"CardCode\" = T1.\"CardCode\" and T77.\"ItemCode\" = T2.\"ItemCode\" and T77.\"IsDefault\" = 'Y') as \"MuhatapKatalogNo\" from \"@AIF_WMS_TOPLANAN\" as T0 INNER JOIN ORDR AS T1 ON T0.\"U_SiparisNumarasi\" = T1.\"DocEntry\" INNER JOIN RDR1 AS T2 ON T1.\"DocEntry\" = T2.\"DocEntry\" and T2.\"LineNum\" = T0.\"U_SiparisSatirNo\" WHERE T0.\"U_BelgeNo\" = '" + docEntry + "' ) as tbl WHERE tbl.\"KonteynerVarmi\" <= 0";
                #endregion

                #region cekme no gelmiyorsa paletten tamamlandı
                //string sss = "Select \"U_CkmGrpla\" from \"@AIF_WMS_GNLPRM\" ";

                //ConstVariables.oRecordset.DoQuery(sss);
                //string cekmelistesigrupla = ConstVariables.oRecordset.Fields.Item(0).Value.ToString();

                ////if (cekmelistesigrupla != "Y")
                ////{
                //    sql = "  select * from (Select T0.\"U_SiparisNumarasi\" as \"SiparisNumarasi\",T1.\"DocDate\" as \"SiparisTarihi\",T1.\"DocDueDate\" as \"TeslimatTarihi\",T0.\"U_SiparisSatirNo\" as \"SiparisSatirNo\",T2.\"ItemCode\" as \"UrunKodu\",T2.\"Dscription\" as \"UrunTanimi\",T2.\"Quantity\" as \"ToplamSiparisMiktari\", (T2.\"Quantity\" - T2.\"OpenQty\") as \"SevkSipMiktari\", (T2.\"OpenQty\" - T0.\"U_Miktar\") AS \"AcikSiparisMiktari\",T2.\"WhsCode\" as \"SiparisDepoKodu\",T0.\"U_Miktar\" as \"ToplananMiktar\",T0.\"U_PaletNo\" as \"PaletNo\",(T2.\"OpenQty\" - ISNULL(T0.\"U_Miktar\", 0)) as \"PlanlananSiparisMiktari\", case when T0.\"U_BelgeNo\" = 0 then(select \"U_CekmeNo\" from \"@AIF_WMS_PALET1\" where \"U_PaletNo\" = T0.\"U_PaletNo\") else T0.\"U_BelgeNo\" end as \"CekmeNo\",T0.\"U_TeslimatNo\",T0.\"DocEntry\" AS \"ToplananDocEntry\",T1.\"NumAtCard\" as \"MuhatapReferansNo\",(Select TOP 1 " + condition + "(\"Substitute\",'')  from OSCN as T77 where T77.\"CardCode\" = T1.\"CardCode\" and T77.\"ItemCode\" = T2.\"ItemCode\" and T77.\"IsDefault\" = 'Y') as \"MuhatapKatalogNo\" from \"@AIF_WMS_TOPLANAN\" as T0 INNER JOIN ORDR AS T1 ON T0.\"U_SiparisNumarasi\" = T1.\"DocEntry\" INNER JOIN RDR1 AS T2 ON T1.\"DocEntry\" = T2.\"DocEntry\" and T2.\"LineNum\" = T0.\"U_SiparisSatirNo\" WHERE T0.\"U_BelgeNo\" = '" + docEntry + "' ) as tbl ";

                //    sql += "WHERE CONCAT(tbl.\"CekmeNo\",tbl.\"SiparisNumarasi\",\"SiparisSatirNo\") not in ((SELECT CONCAT(case when T99.\"U_CekmeNo\" = 0 then(select top 1 \"U_CekmeNo\" from \"@AIF_WMS_PALET1\" where \"U_PaletNo\" = T99.\"U_PaletNo\")  else T99.\"U_CekmeNo\" end ,\"U_SiparisNo\",\"U_SipSatirNo\") FROM \"@AIF_WMS_KNTYNR1\" AS T99)) ";
                ////}
                ////else
                ////{
                ////    sql = "  select * from (Select T0.\"U_SiparisNumarasi\" as \"SiparisNumarasi\",T1.\"DocDate\" as \"SiparisTarihi\",T1.\"DocDueDate\" as \"TeslimatTarihi\",T0.\"U_SiparisSatirNo\" as \"SiparisSatirNo\",T2.\"ItemCode\" as \"UrunKodu\",T2.\"Dscription\" as \"UrunTanimi\",T2.\"Quantity\" as \"ToplamSiparisMiktari\", (T2.\"Quantity\" - T2.\"OpenQty\") as \"SevkSipMiktari\", (T2.\"OpenQty\" - T0.\"U_Miktar\") AS \"AcikSiparisMiktari\",T2.\"WhsCode\" as \"SiparisDepoKodu\",T0.\"U_Miktar\" as \"ToplananMiktar\",T0.\"U_PaletNo\" as \"PaletNo\",(T2.\"OpenQty\" - ISNULL(T0.\"U_Miktar\", 0)) as \"PlanlananSiparisMiktari\", case when T0.\"U_BelgeNo\" = 0 then(select \"U_CekmeNo\" from \"@AIF_WMS_PALET1\" where \"U_PaletNo\" = T0.\"U_PaletNo\") else T0.\"U_BelgeNo\" end as \"CekmeNo\",T0.\"U_TeslimatNo\",T0.\"DocEntry\" AS \"ToplananDocEntry\",T1.\"NumAtCard\" as \"MuhatapReferansNo\",(Select TOP 1 " + condition + "(\"Substitute\",'')  from OSCN as T77 where T77.\"CardCode\" = T1.\"CardCode\" and T77.\"ItemCode\" = T2.\"ItemCode\" and T77.\"IsDefault\" = 'Y') as \"MuhatapKatalogNo\" from \"@AIF_WMS_TOPLANAN\" as T0 LEFT JOIN ORDR AS T1 ON T0.\"U_SiparisNumarasi\" = T1.\"DocEntry\" LEFT JOIN RDR1 AS T2 ON T1.\"DocEntry\" = T2.\"DocEntry\" and T2.\"LineNum\" = T0.\"U_SiparisSatirNo\" WHERE T0.\"U_BelgeNo\" = '" + docEntry + "' ) as tbl "; 
                ////}
                #endregion

                #region 24.03.2022
                sql = "SELECT \"SiparisNumarasi\",\"SiparisTarihi\",\"TeslimatTarihi\",\"UrunKodu\",\"SiparisSatirNo\",\"UrunTanimi\",\"YabanciAd\",\"ToplamSiparisMiktari\",\"SevkSipMiktari\", \"AcikSiparisMiktari\",\"SiparisDepoKodu\",\"ToplananMiktar\",\"PaletNo\",\"PlanlananSiparisMiktari\",\"MuhatapReferansNo\",\"MuhatapKatalogNo\" FROM ";
                sql += " (SELECT T0.\"U_SiparisNumarasi\" AS \"SiparisNumarasi\", T1.\"DocDate\" AS \"SiparisTarihi\", T1.\"DocDueDate\" AS \"TeslimatTarihi\", T0.\"U_SiparisSatirNo\" AS \"SiparisSatirNo\", T2.\"ItemCode\" AS \"UrunKodu\", T2.\"Dscription\" AS \"UrunTanimi\", T3.FrgnName as \"YabanciAd\",T2.\"Quantity\" AS \"ToplamSiparisMiktari\", (T2.\"Quantity\" - T2.\"OpenQty\") AS \"SevkSipMiktari\", (T2.\"OpenQty\" - T0.\"U_Miktar\") AS \"AcikSiparisMiktari\", T2.\"WhsCode\" AS \"SiparisDepoKodu\", T0.\"U_Miktar\" AS \"ToplananMiktar\", T0.\"U_PaletNo\" AS \"PaletNo\", (T2.\"OpenQty\" - ISNULL(T0.\"U_Miktar\", 0)) AS \"PlanlananSiparisMiktari\", T0.\"U_BelgeNo\" AS \"CekmeNo\", T0.\"U_TeslimatNo\", T0.\"DocEntry\" AS \"ToplananDocEntry\", T1.\"NumAtCard\" AS \"MuhatapReferansNo\", ";
                sql += "(SELECT TOP 1 ISNULL(\"Substitute\", '') FROM OSCN AS T77 ";
                sql += "WHERE T77.\"CardCode\" = T1.\"CardCode\" AND T77.\"ItemCode\" = T2.\"ItemCode\" AND T77.\"IsDefault\" = 'Y' ) AS \"MuhatapKatalogNo\",T3.FrgnName, T2.Price, t1.NumAtCard, T1.CardCode, t1.CardName, (SELECT distinct T5.U_KonteynerNo FROM \"@AIF_WMS_KNTYNR1\" t4 ";
                sql += "LEFT JOIN \"@AIF_WMS_KNTYNR\" t5 ON t5.DocEntry = T4.DocEntry ";
                sql += "where T4.U_SiparisNo = t0.U_SiparisNumarasi AND T4.U_SipSatirNo = T0.U_SiparisSatirNo AND (case when ISNULL(T4.U_PaletNo, '') = '' then cast(T4.U_CekmeNo as nvarchar) else T4.U_PaletNo end) = (case when ISNULL(T0.U_PaletNo,'')= '' then cast(T0.U_BelgeNo as nvarchar) else T0.U_PaletNo end) ) as \"Konteyner\",T0.\"U_Kaynak\" as \"Kaynak\" FROM \"@AIF_WMS_TOPLANAN\" AS T0 ";
                sql += "INNER JOIN ORDR AS T1 ON T0.\"U_SiparisNumarasi\" = T1.\"DocEntry\" INNER JOIN RDR1 AS T2 ON T1.\"DocEntry\" = T2.\"DocEntry\" AND T2.\"LineNum\" = T0.\"U_SiparisSatirNo\" INNER JOIN OITM AS T3 ON T2.ItemCode = T3.ItemCode ) AS tbl ";
                sql += "inner JOIN \"@AIF_WMS_SIPKAR\" Tb2 ON tb2.DocEntry = tbl.\"CekmeNo\" ";
                sql += "WHERE Concat(CASE WHEN ISNULL(tbl.PaletNo,'') = '' THEN cast(tbl.CekmeNo AS nvarchar) ELSE ISNULL(tbl.PaletNo,'') END,  ISNULL(tbl.Kaynak, CONCAT(tbl.SiparisNumarasi, tbl.SiparisSatirNo))) NOT IN((SELECT DISTINCT Concat(CASE WHEN ISNULL(U_PaletNo,'') = '' THEN cast(U_CekmeNo AS nvarchar) ELSE ISNULL(U_PaletNo,'') END, ISNULL(T99.U_Kaynak, CONCAT(T99.U_SiparisNo, T99.U_SipSatirNo))) FROM \"@AIF_WMS_KNTYNR1\" AS T99)) and tbl.CekmeNo = '" + docEntry + "' ";
                sql += " ORDER BY tbl.CekmeNo,  tb2.U_Aciklama";
                #endregion

                oDataTable.Clear();
                oDataTable.ExecuteQuery(sql);

                oMatrix.Clear();
                oMatrix.Columns.Item("Col_0").DataBind.Bind("DATA", "SiparisNumarasi");
                oMatrix.Columns.Item("Col_1").DataBind.Bind("DATA", "SiparisTarihi");
                oMatrix.Columns.Item("Col_2").DataBind.Bind("DATA", "TeslimatTarihi");
                oMatrix.Columns.Item("Col_3").DataBind.Bind("DATA", "SiparisSatirNo");
                oMatrix.Columns.Item("Col_4").DataBind.Bind("DATA", "UrunKodu");
                oMatrix.Columns.Item("Col_5").DataBind.Bind("DATA", "UrunTanimi");
                oMatrix.Columns.Item("Col_6").DataBind.Bind("DATA", "ToplamSiparisMiktari");
                oMatrix.Columns.Item("Col_7").DataBind.Bind("DATA", "SevkSipMiktari");
                oMatrix.Columns.Item("Col_8").DataBind.Bind("DATA", "AcikSiparisMiktari");
                oMatrix.Columns.Item("Col_9").DataBind.Bind("DATA", "PlanlananSiparisMiktari");
                oMatrix.Columns.Item("Col_10").DataBind.Bind("DATA", "SiparisDepoKodu");
                oMatrix.Columns.Item("Col_13").DataBind.Bind("DATA", "ToplananMiktar");
                oMatrix.Columns.Item("Col_15").DataBind.Bind("DATA", "PaletNo");
                oMatrix.Columns.Item("Col_11").DataBind.Bind("DATA", "MuhatapReferansNo");
                oMatrix.Columns.Item("Col_12").DataBind.Bind("DATA", "MuhatapKatalogNo");
                oMatrix.Columns.Item("Col_16").DataBind.Bind("DATA", "YabanciAd");

                oMatrix.LoadFromDataSource();
                oMatrix.AutoResizeColumns();
                oMatrix.Item.AffectsFormMode = false;

            }
            catch (Exception ex)
            {
                Handler.SAPApplication.MessageBox(ex.Message);
            }
            finally
            {
                frmToplananUrun.Freeze(false);
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

        bool formkapaniyor = false;
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
                    if (pVal.ItemUID == "Item_1" && !pVal.BeforeAction)
                    {
                        try
                        {
                            frmToplananUrun.Close();
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