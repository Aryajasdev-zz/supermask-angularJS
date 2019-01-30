using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Sparsh.DataBaseConnectonCalss.Data;
using System.Text.RegularExpressions;

namespace wigsboot.Models
{
    public class products
    {
        public Int32 prodid { get; set; }
        public String name { get; set; }
        public String descr { get; set; }
        public Decimal shopprice { get; set; }
        public Decimal webprice { get; set; }
        public String img { get; set; }
        public String keywords { get; set; }
        public Int32 isstock { get; set; }       
        public String prodcode { get; set; }
        public String url { get; set; }
        public Int32 isxl { get; set; }       
        public Int32 sizeid { get; set; }
        public Int32 tierid { get; set; }
        public Int32 isballoon { get; set; }       
        public Int32 mprodid { get; set; }
        public Decimal retail_web_price {get; set;}
        public Int32 web_sale {get; set;}
        public Decimal original_price { get; set; }
        public Decimal retail_original_price { get; set; }
        public Int32 sales_offer { get; set;}    
        public String pssdes { get; set; }
        public String masksdes { get; set; }
        public String wigsdes { get; set; }
        public String fbdes { get; set; }
        public String pndes { get; set; }
        public String specification { get; set; }
        public String delivery { get; set; }       
        public IEnumerable<images> images { get; set;}
        public IEnumerable<cats> Cats { get; set; }
        public IEnumerable<sizes> sizes { get; set; }
        public IEnumerable<supplyprods> supplycodes { get; set; }

        public String mdes { get; set; }
        public String mkeys { get; set; }
        public String ptitle { get; set; }

        public static IEnumerable<products> getprods(Int32 code, Int32 ncode, Int32 issale, Int32 siteid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<products> prod = new List<products>();                
                SqlParameter[] arParams1 = new SqlParameter[3];
                arParams1[0] = new SqlParameter("@code", SqlDbType.BigInt);
                arParams1[0].Value = code;
                arParams1[1] = new SqlParameter("@ncode", SqlDbType.BigInt);
                arParams1[1].Value = ncode;
                arParams1[2] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[2].Value = siteid;

                sql = "usp_get_url_data_service";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    products pd = new products();
                    pd.prodid = Convert.ToInt32(dr[0]);
                    pd.name = dr[1].ToString();
                    pd.descr = dr[2].ToString();
                    pd.shopprice = Convert.ToDecimal(dr[3]);
                    pd.webprice = Convert.ToDecimal(dr[4]);
                    pd.img = "http://www.partysuperstores.co.uk/img/" + dr[5].ToString();
                    pd.keywords = dr[6].ToString();
                    pd.isstock = Convert.ToInt32(dr[7]);
                    pd.prodcode = dr[8].ToString();
                    pd.url = dr[9].ToString();
                    pd.isxl = Convert.ToInt32(dr[10]);
                    pd.sizeid = Convert.ToInt32(dr[11]);
                    pd.tierid = Convert.ToInt32(dr[12]);
                    pd.isballoon = Convert.ToInt32(dr[13]);
                    pd.mprodid = Convert.ToInt32(dr[14]);
                    pd.retail_web_price = Convert.ToDecimal(dr[15]);
                    pd.web_sale = Convert.ToInt32(dr[16]);
                    pd.original_price = Convert.ToDecimal(dr[17]);
                    pd.retail_original_price = Convert.ToDecimal(dr[18]);
                    pd.sales_offer = Convert.ToInt32(dr[19]);
                    pd.pssdes = dr[20].ToString();
                    pd.masksdes = dr[21].ToString();
                    pd.wigsdes = dr[22].ToString();
                    pd.fbdes = dr[23].ToString();
                    pd.pndes = dr[24].ToString();
                    pd.specification = dr[25].ToString();
                    pd.delivery = dr[26].ToString();
                    //pd.images = getimages(pd.prodid);
                    pd.Cats = getcats(pd.prodid);
                    pd.sizes = getsizes(pd.mprodid);
                    //pd.supplycodes = supplyprods.getsupplyprods(pd.prodid);                   
                    prod.Add(pd);
                }                   
                return prod;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getprods model");
                return null;
            }
        }
        public static products getproduct(int prodid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                products pd = new products();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@prodid", SqlDbType.VarChar);
                arParams1[0].Value = prodid;
                sql = "usp_get_prod_by_id_service";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    pd.prodid = Convert.ToInt32(dr[0]);
                    pd.name = dr[1].ToString();
                    pd.descr = dr[2].ToString();
                    pd.shopprice = Convert.ToDecimal(dr[3]);
                    pd.webprice = Convert.ToDecimal(dr[4]);
                    pd.img = "http://www.partysuperstores.co.uk/img/" + dr[5].ToString();
                    pd.keywords = dr[6].ToString();
                    pd.isstock = Convert.ToInt32(dr[7]);
                    pd.prodcode = dr[8].ToString();
                    pd.url = dr[9].ToString();
                    pd.isxl = Convert.ToInt32(dr[10]);
                    pd.sizeid = Convert.ToInt32(dr[11]);
                    pd.tierid = Convert.ToInt32(dr[12]);
                    pd.isballoon = Convert.ToInt32(dr[13]);
                    pd.mprodid = Convert.ToInt32(dr[14]);
                    pd.retail_web_price = Convert.ToDecimal(dr[15]);
                    pd.web_sale = Convert.ToInt32(dr[16]);
                    pd.original_price = Convert.ToDecimal(dr[17]);
                    pd.retail_original_price = Convert.ToDecimal(dr[18]);
                    pd.sales_offer = Convert.ToInt32(dr[19]);
                    pd.pssdes = dr[20].ToString();
                    pd.masksdes = dr[21].ToString();
                    pd.wigsdes = dr[22].ToString();
                    pd.fbdes = dr[23].ToString();
                    pd.pndes = dr[24].ToString();
                    pd.specification = dr[25].ToString();
                    pd.delivery = dr[26].ToString();
                    pd.images = getimages(pd.prodid);
                    pd.Cats = getcats(pd.prodid);
                    pd.sizes = getsizes(pd.mprodid);
                    pd.supplycodes = supplyprods.getsupplyprods(pd.prodid);
                }
                return pd;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static products getproduct(String GUID)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                products pd = new products();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@GUID", SqlDbType.VarChar);
                arParams1[0].Value = GUID;
                sql = "usp_get_prod_by_guid_service";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    pd.prodid = Convert.ToInt32(dr[0]);
                    pd.name = dr[1].ToString();
                    pd.descr = dr[2].ToString();
                    pd.shopprice = Convert.ToDecimal(dr[3]);
                    pd.webprice = Convert.ToDecimal(dr[4]);
                    pd.img = "http://www.partysuperstores.co.uk/img/" + dr[5].ToString();
                    pd.keywords = dr[6].ToString();
                    pd.isstock = Convert.ToInt32(dr[7]);
                    pd.prodcode = dr[8].ToString();
                    pd.url = dr[9].ToString();
                    pd.isxl = Convert.ToInt32(dr[10]);
                    pd.sizeid = Convert.ToInt32(dr[11]);
                    pd.tierid = Convert.ToInt32(dr[12]);
                    pd.isballoon = Convert.ToInt32(dr[13]);
                    pd.mprodid = Convert.ToInt32(dr[14]);
                    pd.retail_web_price = Convert.ToDecimal(dr[15]);
                    pd.web_sale = Convert.ToInt32(dr[16]);
                    pd.original_price = Convert.ToDecimal(dr[17]);
                    pd.retail_original_price = Convert.ToDecimal(dr[18]);
                    pd.sales_offer = Convert.ToInt32(dr[19]);
                    pd.pssdes = dr[20].ToString();
                    pd.masksdes = dr[21].ToString();
                    pd.wigsdes = dr[22].ToString();
                    pd.fbdes = dr[23].ToString();
                    pd.pndes = dr[24].ToString();
                    pd.specification = dr[25].ToString();
                    pd.delivery = dr[26].ToString();
                    pd.images = getimages(pd.prodid);
                    pd.Cats = getcats(pd.prodid);
                    pd.sizes = getsizes(pd.mprodid);
                    pd.supplycodes = supplyprods.getsupplyprods(pd.prodid);
                }
                return pd;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static Int32 getprodid(String url)
        {
            try
            {
                String sql = "Select prodid from product_master where url='"+url+"'";
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }else
                {
                    return 0;
                }
            }
            catch {
                return 0;
            }
        }

        public static products getprod(string url)
        {
            try
            {                        
                products pd = new products();
                if (url != null && url != "")
                {
                    Int32 prodid = getprodid(url);
                    pd = getproduct(prodid);
                }    
                return pd;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getprods model");
                return null;
            }
        }

        public static IEnumerable<images> getimages(Int32 prodid)
        {
            try
            {
                List<images> images = new List<images>();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@prodid", SqlDbType.BigInt);
                arParams1[0].Value = prodid;
                String sql = "usp_get_prod_images";
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    images img = new images();
                    img.image = "http://www.partysuperstores.co.uk/img/" + dr[0].ToString();
                    images.Add(img);
                }
                return images;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getprods model");
                return null;
            }
        }

        public static IEnumerable<cats> getcats(Int32 prodid)
        {
            try
            {
                List<cats> cats = new List<cats>();
                String sql = "Select a.catcode,b.name,b.url,a.ismain,b.issale from cats_products a,cats_master b where a.catcode=b.code and a.prodid="+prodid;
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    cats ct = new cats();
                    ct.code = Convert.ToInt32(dr[0]);
                    ct.name = dr[1].ToString();
                    ct.url = dr[2].ToString();
                    ct.ismain = Convert.ToInt32(dr[3]);
                    ct.issale = Convert.ToInt32(dr[4]);
                    cats.Add(ct);
                }
                return cats;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getprods model");
                return null;
            }
        }

        public static string stem(String word)
        {
            word = Regex.Replace(word, "'s$", "");
            if (word == "lady") word = "lad";
            if (word.Length > 2) word = word = Regex.Replace(word, "s$", "");
            word = Regex.Replace(word, "sses$", "ss");
            word = Regex.Replace(word, "ies$", "ss");
            word = Regex.Replace(word, "bb$", "b");
            word = Regex.Replace(word, "dd$", "d");
            word = Regex.Replace(word, "ff$", "f");
            word = Regex.Replace(word, "gg$", "g");
            word = Regex.Replace(word, "mm$", "m");
            word = Regex.Replace(word, "nn$", "n");
            word = Regex.Replace(word, "pp$", "p");
            word = Regex.Replace(word, "rr$", "r");
            word = Regex.Replace(word, "tt$", "t");
            word = Regex.Replace(word, "ww$", "w");
            word = Regex.Replace(word, "xx$", "x");
            word = Regex.Replace(word, "ational$", "ate");
            word = Regex.Replace(word, "enci$", "enc");
            word = Regex.Replace(word, "anci$", "ance");
            word = Regex.Replace(word, "izer$", "iz");
            word = Regex.Replace(word, "iser$", "is");
            word = Regex.Replace(word, "abli$", "abl");
            word = Regex.Replace(word, "alli$", "al");
            word = Regex.Replace(word, "entli$", "ent");
            word = Regex.Replace(word, "eli$", "e");
            word = Regex.Replace(word, "ousli$", "ous");
            word = Regex.Replace(word, "ization$", "iz");
            word = Regex.Replace(word, "isation$", "is");
            word = Regex.Replace(word, "ation$", "at");
            word = Regex.Replace(word, "ator$", "at");
            word = Regex.Replace(word, "alism$", "al");
            word = Regex.Replace(word, "tional$", "tion");
            word = Regex.Replace(word, "iveness$", "ive");
            word = Regex.Replace(word, "fulnes$", "ful");
            word = Regex.Replace(word, "ousness$", "ous");
            word = Regex.Replace(word, "aliti$", "al");
            word = Regex.Replace(word, "iviti$", "ive");
            word = Regex.Replace(word, "biliti$", "ble");
            word = Regex.Replace(word, "icate$", "ic");
            word = Regex.Replace(word, "ative$", "");
            word = Regex.Replace(word, "alize$", "al");
            word = Regex.Replace(word, "iciti$", "ic");
            word = Regex.Replace(word, "ical$", "ic");
            word = Regex.Replace(word, "ful$", "");
            word = Regex.Replace(word, "ness$", "");
            word = Regex.Replace(word, "ti$", "t");            
            word = Regex.Replace(word, "ance$", "");                        
            word = Regex.Replace(word, "sion$", "s");
            word = Regex.Replace(word, "([^p])tion$", "\\1t");           
            //word = Regex.Replace(word, "i$", "");
            if (word.Length > 7)
            {
                word = Regex.Replace(word, "al$", "");
                word = Regex.Replace(word, "ence$", "");
                word = Regex.Replace(word, "er$", "");
                word = Regex.Replace(word, "ic$", "");
                word = Regex.Replace(word, "able$", "");
                word = Regex.Replace(word, "ible$", "");
                word = Regex.Replace(word, "ant$", "");
                word = Regex.Replace(word, "ement$", "");
                word = Regex.Replace(word, "ment$", "");
                word = Regex.Replace(word, "ent$", "");
                word = Regex.Replace(word, "ou$", "");
                word = Regex.Replace(word, "ism$", "");
                word = Regex.Replace(word, "ate$", "");
                word = Regex.Replace(word, "iti$", "");
                word = Regex.Replace(word, "ous$", "");
                word = Regex.Replace(word, "ive$", "");
                word = Regex.Replace(word, "ize$", "");
                word = Regex.Replace(word, "ise$", "");            
            }
            return word;
        }

        public static String getsearchcodes(Int32 cat, Int32 ncat)
        {
            try
            {
                String sql = "select catcode from main_catindex where maincatid=" + cat;
                DataTable dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, sql).Tables[0];
                Int32 cnt = dt.Rows.Count;
                int i = 1;
                String ctcode = "";
                if (cnt > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Int32 cc = Convert.ToInt32(dr[0]);
                        Int32 ncode = (Convert.ToInt32(cc) + Convert.ToInt32(Common.steps[Common.getlevel(cc)]));
                        if (i == cnt)
                        {
                            ctcode += "(b.catcode>=" + cc + " and b.catcode <" + ncode + ")";
                        }
                        else
                        {
                            ctcode += "(b.catcode>=" + cc + " and b.catcode < " + ncode + ") or ";
                        }
                        i++;
                    }
                }
                else
                {
                    ctcode = "(1=1)";
                }
                return ctcode;
            }
            catch
            {
                return "(1=1)";
            }
	    }

        public static IEnumerable<products> searchitems(string url,Int32 catcode, Int32 ncatcode)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<products> prod = new List<products>();
                if (!url.Equals(DBNull.Value))
                {
                    if (url != "")
                    {
                        String stxt="";
                        Int32 i = 0;
                        url = Common.RemoveSpecialCharacters(url);
                        url = url.Replace("and", " ");
                        String cccode = "b.catcode >= " + catcode + " and b.catcode < " + ncatcode;
                        String[] srch = url.Trim().Split(' ');
                        sql = "select a.prodid from product_master a,cats_products b where a.prodid=b.prodid and (" + cccode + ")";
                        foreach (string s in srch)
                        {                            
                            stxt = stem(s);
                            if (i < srch.Count())
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((name,descr),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (name like '%" + stxt + "%' or descr like '" + stxt + "%')";
                                }
                            }
                            else
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((name,descr),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (name like '%" + stxt + "%' or descr like '" + stxt + "%')";
                                }
                            }
                            i++;
                        }
                        i = 0;
                        sql = sql + " union all select a.prodid from product_master a,cats_products b where a.prodid=b.prodid and (" + cccode + ") ";
                        foreach (string s in srch)
                        {
                            stxt = stem(s);
                            if (i < srch.Count())
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((keywords),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (keywords like '%" + stxt + "%')";
                                }
                            }
                            else
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((keywords),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (keywords like '%" + stxt + "%')";
                                }
                            }
                            i++;
                        }
                        i = 0;
                        sql = sql + " union all Select b.prodid from cats_products b, cats_master a where b.catcode=a.code and (" + cccode + ")";
                        foreach (string s in srch)
                        {
                            stxt = stem(s);
                            if (i < srch.Count())
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((name,longname),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (name like '%" + stxt + "%' or longname like '" + stxt + "%')";
                                }
                            }
                            else
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((name,longname),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (name like '%" + stxt + "%' or longname like '" + stxt + "%')";
                                }
                            }
                            i++;
                        }
                        i = 0;
                        sql = sql + " union all Select prodid from supplyprod_details where isdead=1 ";
                        foreach (string s in srch)
                        {
                            stxt = stem(s);
                            if (i < srch.Count())
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((prodcode),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (prodcode like '%" + stxt + "%')";
                                }
                            }
                            else
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((prodcode),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (prodcode like '%" + stxt + "%')";
                                }
                            }
                            i++;
                        }                        
                        sql = sql.Replace('\\', ' ').Trim();
                        String ssql = "SELECT prodid as id,isnull(name,'') as name,isnull(descr,'') as descr,isnull(price,0) as price,isnull(webprice,0) as webprice,isnull(img,'') as img,isnull(keywords,'') as keywords,isnull(isstock,0) as instock,isnull(prodcode,'') as video,isnull(url,'') as url,isnull(isxl,0) as isxl,isnull(sizeid,0) as sizeid,isnull(tierid,0) as tierid,isnull(isballoon,0) as isballoon,mprodid,rwprice,wsale,oprice,rrprice,soffer,pssdes,mdes,wdes,fbdes,pndes, specification,delivery from product_master b where status=1 and prodid in(" + sql + ") and prodid=mprodid and tierid in (1,2,4) and price>0 and isstock<>2 and " + cccode;
                        dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, ssql).Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            products pd = new products();
                            pd.prodid = Convert.ToInt32(dr[0]);
                            pd.name = dr[1].ToString();
                            pd.descr = dr[2].ToString();
                            pd.shopprice = Convert.ToDecimal(dr[3]);
                            pd.webprice = Convert.ToDecimal(dr[4]);
                            pd.img = "http://www.partysuperstores.co.uk/img/" + dr[5].ToString();
                            pd.keywords = dr[6].ToString();
                            pd.isstock = Convert.ToInt32(dr[7]);
                            pd.prodcode = dr[8].ToString();
                            pd.url = dr[9].ToString();
                            pd.isxl = Convert.ToInt32(dr[10]);
                            pd.sizeid = Convert.ToInt32(dr[11]);
                            pd.tierid = Convert.ToInt32(dr[12]);
                            pd.isballoon = Convert.ToInt32(dr[13]);
                            pd.mprodid = Convert.ToInt32(dr[14]);
                            pd.retail_web_price = Convert.ToDecimal(dr[15]);
                            pd.web_sale = Convert.ToInt32(dr[16]);
                            pd.original_price = Convert.ToDecimal(dr[17]);
                            pd.retail_original_price = Convert.ToDecimal(dr[18]);
                            pd.sales_offer = Convert.ToInt32(dr[19]);
                            pd.pssdes = dr[20].ToString();
                            pd.masksdes = dr[21].ToString();
                            pd.wigsdes = dr[22].ToString();
                            pd.fbdes = dr[23].ToString();
                            pd.pndes = dr[24].ToString();
                            pd.specification = dr[25].ToString();
                            pd.delivery = dr[26].ToString();                            
                            //pd.Cats = getcats(pd.prodid);
                            pd.sizes = getsizes(pd.mprodid);                            
                            prod.Add(pd);
                        }
                    }
                }
                return prod;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " searchitems model");
                return null;
            }
        }

        public static IEnumerable<products> searchitems(string url)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<products> prod = new List<products>();
                if (!url.Equals(DBNull.Value))
                {
                    if (url != "")
                    {
                        String stxt = "";
                        Int32 i = 0;
                        url = Common.RemoveSpecialCharacters(url);
                        url = url.Replace("and", " ");
                        String[] srch = url.Trim().Split(' ');
                        sql = "select a.prodid from product_master a,cats_products b where a.prodid=b.prodid ";
                        foreach (string s in srch)
                        {
                            stxt = stem(s);
                            if (i < srch.Count())
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((name,descr),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (name like '%" + stxt + "%' or descr like '" + stxt + "%')";
                                }
                            }
                            else
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((name,descr),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (name like '%" + stxt + "%' or descr like '" + stxt + "%')";
                                }
                            }
                            i++;
                        }
                        i = 0;
                        sql = sql + " union all select a.prodid from product_master a,cats_products b where a.prodid=b.prodid ";
                        foreach (string s in srch)
                        {
                            stxt = stem(s);
                            if (i < srch.Count())
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((keywords),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (keywords like '%" + stxt + "%')";
                                }
                            }
                            else
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((keywords),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (keywords like '%" + stxt + "%')";
                                }
                            }
                            i++;
                        }
                        i = 0;
                        sql = sql + " union all Select prodid from supplyprod_details where isdead=1 ";
                        foreach (string s in srch)
                        {
                            stxt = stem(s);
                            if (i < srch.Count())
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((prodcode),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (prodcode like '%" + stxt + "%')";
                                }
                            }
                            else
                            {
                                if (stxt.Length > 4)
                                {
                                    sql = sql + " and Contains((prodcode),'" + '"' + stxt + '*' + '"' + "')";
                                }
                                else
                                {
                                    sql = sql + " and (prodcode like '%" + stxt + "%')";
                                }
                            }
                            i++;
                        }                        
                        sql = sql.Replace('\\', ' ').Trim();
                        String ssql = "SELECT prodid as id,isnull(name,'') as name,isnull(descr,'') as descr,isnull(price,0) as price,isnull(webprice,0) as webprice,isnull(img,'') as img,isnull(keywords,'') as keywords,isnull(isstock,0) as instock,isnull(prodcode,'') as video,isnull(url,'') as url,isnull(isxl,0) as isxl,isnull(sizeid,0) as sizeid,isnull(tierid,0) as tierid,isnull(isballoon,0) as isballoon,mprodid,rwprice,wsale,oprice,rrprice,soffer,pssdes,mdes,wdes,fbdes,pndes, specification,delivery from product_master where status=1 and prodid in(" + sql + ") and prodid=mprodid and tierid in (1,2,4) and price>0 and isstock<>2";
                        dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.Text, ssql).Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            products pd = new products();
                            pd.prodid = Convert.ToInt32(dr[0]);
                            pd.name = dr[1].ToString();
                            pd.descr = dr[2].ToString();
                            pd.shopprice = Convert.ToDecimal(dr[3]);
                            pd.webprice = Convert.ToDecimal(dr[4]);
                            pd.img = "http://www.partysuperstores.co.uk/img/" + dr[5].ToString();
                            pd.keywords = dr[6].ToString();
                            pd.isstock = Convert.ToInt32(dr[7]);
                            pd.prodcode = dr[8].ToString();
                            pd.url = dr[9].ToString();
                            pd.isxl = Convert.ToInt32(dr[10]);
                            pd.sizeid = Convert.ToInt32(dr[11]);
                            pd.tierid = Convert.ToInt32(dr[12]);
                            pd.isballoon = Convert.ToInt32(dr[13]);
                            pd.mprodid = Convert.ToInt32(dr[14]);
                            pd.retail_web_price = Convert.ToDecimal(dr[15]);
                            pd.web_sale = Convert.ToInt32(dr[16]);
                            pd.original_price = Convert.ToDecimal(dr[17]);
                            pd.retail_original_price = Convert.ToDecimal(dr[18]);
                            pd.sales_offer = Convert.ToInt32(dr[19]);
                            pd.pssdes = dr[20].ToString();
                            pd.masksdes = dr[21].ToString();
                            pd.wigsdes = dr[22].ToString();
                            pd.fbdes = dr[23].ToString();
                            pd.pndes = dr[24].ToString();
                            pd.specification = dr[25].ToString();
                            pd.delivery = dr[26].ToString();
                            pd.Cats = getcats(pd.prodid);
                            pd.sizes = getsizes(pd.mprodid);
                            prod.Add(pd);
                        }
                    }
                }
                return prod;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " searchitems model");
                return null;
            }
        }

        public static string SafeSqlLiteral(string strValue)
        {
            strValue = strValue.Replace("'", "''"); // Most important one! This line alone can prevent most injection attacks
            strValue = strValue.Replace("--", "").Replace("[", "[[]").Replace("%", "[%]").Replace(" OR ", "").Replace(" or ", "");
            strValue = strValue.Replace(" AND ", "").Replace(" and ", "").Replace("\\\r\n", "").Replace("\\\r\n\r\n", "");

            string[] myArray = new string[] { "xp_ ", "update ", "insert ", "select ", "drop ", "alter ", "create ", "rename ", "delete ", "replace " };
            int i = 0;
            int i2 = 0;
            int intLenghtLeft = 0;
            for (i = 0; i < myArray.Length; i++)
            {
                string strWord = myArray[i];
                Regex rx = new Regex(strWord, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection matches = rx.Matches(strValue);
                i2 = 0;
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    intLenghtLeft = groups[0].Index + myArray[i].Length + i2;
                    strValue = strValue.Substring(0, intLenghtLeft - 1) + "&nbsp;" + strValue.Substring(strValue.Length - (strValue.Length - intLenghtLeft), strValue.Length - intLenghtLeft);
                    i2 += 5;
                }
            }
            return strValue;
        }

        public static IEnumerable<products> searchritems(string url, Int32 prodid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<products> prod = new List<products>();
                if (!url.Equals(DBNull.Value))
                {
                    if (url != "")
                    {
                        SqlParameter[] arParams1 = new SqlParameter[3];
                        arParams1[0] = new SqlParameter("@search", SqlDbType.VarChar);
                        arParams1[0].Value = url;
                        arParams1[1] = new SqlParameter("@prodid", SqlDbType.BigInt);
                        arParams1[1].Value = prodid;
                        arParams1[2] = new SqlParameter("@siteid", SqlDbType.BigInt);
                        arParams1[2].Value = Common.siteid;
                        sql = "usp_search_rprods";
                        dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            products pd = new products();
                            pd.prodid = Convert.ToInt32(dr[0]);
                            pd.name = dr[1].ToString();
                            pd.descr = dr[2].ToString();
                            pd.shopprice = Convert.ToDecimal(dr[3]);
                            pd.img = dr[4].ToString();
                            pd.keywords = dr[5].ToString();
                            pd.isstock = Convert.ToInt32(dr[6]);                            
                            pd.prodcode = dr[8].ToString();
                            pd.url = dr[9].ToString();
                            pd.isxl = Convert.ToInt32(dr[10]);
                            prod.Add(pd);
                        }
                    }
                }
                return prod;
            }
            catch(Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), "search items");
                return null;
            }
        }

        public static IEnumerable<sizes> getsizes(Int32 prodid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<sizes> sz = new List<sizes>();
                if (prodid > 0)
                {
                    SqlParameter[] arParams1 = new SqlParameter[2];
                    arParams1[0] = new SqlParameter("@prodid", SqlDbType.BigInt);
                    arParams1[0].Value = prodid;
                    arParams1[1] = new SqlParameter("@siteid", SqlDbType.BigInt);
                    arParams1[1].Value = Common.siteid;
                    sql = "usp_get_sitesizes";
                    dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        sizes siz = new sizes();
                        siz.sizeid = Convert.ToInt32(dr[0]);
                        siz.sizename = dr[1].ToString();
                        siz.esize = dr[2].ToString();
                        siz.sizeprice = Convert.ToDecimal(dr[3]);
                        siz.isstock = Convert.ToInt16(dr[4]);
                        sz.Add(siz);
                    }
                }
                return sz;
            }
            catch(Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), "getsizes");
                return null;
            }
        }

        public static IEnumerable<products> getpopular()
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<products> prod = new List<products>();
                SqlParameter[] arParams1 = new SqlParameter[1];
                arParams1[0] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[0].Value = Common.siteid;
                sql = "usp_get_popular_items";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    products pd = new products();
                    pd.prodid = Convert.ToInt32(dr[0]);
                    pd.name = dr[1].ToString();
                    pd.descr = dr[2].ToString();
                    pd.shopprice = Convert.ToDecimal(dr[3]);
                    pd.img = dr[4].ToString();
                    pd.isstock = Convert.ToInt32(dr[5]);                    
                    pd.prodcode = dr[7].ToString();
                    pd.url = dr[8].ToString();
                    pd.isxl = Convert.ToInt32(dr[9]);
                    prod.Add(pd);
                }
                return prod;
            }
            catch(Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), "getpopular");
                return null;
            }
        }

        public static IEnumerable<products> getrelprods(Int32 prodid)
        {
            try
            {
                DataTable dt = null;
                String sql = "";
                List<products> prod = new List<products>();
                SqlParameter[] arParams1 = new SqlParameter[2];
                arParams1[0] = new SqlParameter("@prodid", SqlDbType.BigInt);
                arParams1[0].Value = prodid;
                arParams1[1] = new SqlParameter("@siteid", SqlDbType.BigInt);
                arParams1[1].Value = Common.siteid;
                sql = "usp_get_related_prods_site";
                dt = DataBaseConnectionClass.ExecuteDataset(Common.getconnectionstring(), CommandType.StoredProcedure, sql, arParams1).Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    products pd = new products();
                    pd.prodid = Convert.ToInt32(dr[0]);
                    pd.name = dr[1].ToString();
                    pd.descr = dr[2].ToString();
                    pd.shopprice = Convert.ToDecimal(dr[3]);
                    pd.img = dr[4].ToString();
                    pd.url = dr[5].ToString();   
                    prod.Add(pd);
                }
                return prod;
            }
            catch (Exception ex)
            {
                Int32 linenumber = Common.GetLineNumber(ex);
                logs.ErrorLog(ex.Message + " - line number " + linenumber.ToString(), " getrelprods model");
                return null;
            }
        }       

    }
}