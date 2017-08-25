using AALife.Model;
using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;

public class BackupHelper
{
    static BackupHelper()
    {
    }
    
    //备份写sql文件
    public static void WriteBackupFile(string path, string content)
    {
        if (!File.Exists(path))
        {
            FileStream fs = File.Create(path);
            fs.Close();
        }

        Encoding enc = new UTF8Encoding(false);
        StreamWriter sw = new StreamWriter(path, false, enc);
        sw.WriteLine(content.Trim());
        sw.Close();
        sw.Dispose();
    }

    //从sql文件恢复备份
    public static void ReaderBackupFile(string path)
    {
        string dbProviderName = WebConfiguration.DbProviderName;
        string dbConnectionString = WebConfiguration.DbConnectionString;
        DbProviderFactory factory = DbProviderFactories.GetFactory(dbProviderName);
        using (DbConnection conn = factory.CreateConnection())
        {
            conn.ConnectionString = dbConnectionString;
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            DbCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = trans;

            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path, Encoding.GetEncoding("utf-8"));
                string str = "";
                while ((str = sr.ReadLine()) != null)
                {
                    comm.CommandText += str + "\r\n";
                }
                comm.ExecuteNonQuery();
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                sr.Close();
                conn.Close();
            }
        }
    }

    //从sql文件恢复备份app
    public static void ReaderBackupFileFromApp(int userId, string path)
    {
        string dbProviderName = WebConfiguration.DbProviderName;
        string dbConnectionString = WebConfiguration.DbConnectionString;
        DbProviderFactory factory = DbProviderFactories.GetFactory(dbProviderName);
        using (DbConnection conn = factory.CreateConnection())
        {
            conn.ConnectionString = dbConnectionString;
            conn.Open();
            DbTransaction trans = conn.BeginTransaction();
            DbCommand comm = conn.CreateCommand();
            comm.Connection = conn;
            comm.Transaction = trans;

            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path, Encoding.GetEncoding("utf-8"));
                string str = "";
                bool onFlag = false;
                while ((str = sr.ReadLine()) != null)
                {
                    if (str == "")
                    {
                        comm.CommandText += "\r\n";
                    }
                    if (str.StartsWith("{"))
                    {
                        comm.CommandText += GetUserTableSqlFromApp(userId, str) + "\r\n";
                    }
                    if (str.StartsWith("INSERT INTO ItemTable (ItemID")) 
                    {
                        comm.CommandText += GetItemTableSqlFromApp(userId, str) + "\r\n";
                    }
                    if (str.StartsWith("INSERT INTO ItemTable (ItemWebID"))
                    {
                        if (!onFlag)
                        {
                            comm.CommandText += "SET IDENTITY_INSERT ItemTable ON" + "\r\n";
                            onFlag = true;
                        }
                        comm.CommandText += GetItemTableSqlFromWeb(userId, str) + "\r\n";                        
                    }
                    if (str.StartsWith("INSERT INTO CategoryTable"))
                    {
                        if (onFlag)
                        {
                            comm.CommandText += "SET IDENTITY_INSERT ItemTable OFF" + "\r\n";
                            comm.CommandText += "\r\n";
                            onFlag = false;
                        }
                        comm.CommandText += GetCatTableSqlFromApp(userId, str) + "\r\n";
                    }
                    if (str.StartsWith("INSERT INTO ZhuanTiTable"))
                    {
                        comm.CommandText += GetZhuanTableSqlFromApp(userId, str) + "\r\n";
                    }
                    if (str.StartsWith("INSERT INTO ZhuanZhangTable"))
                    {
                        comm.CommandText += GetZhangTableSqlFromApp(userId, str) + "\r\n";
                    }
                    if (str.StartsWith("INSERT INTO CardTable"))
                    {
                        comm.CommandText += GetCardTableSqlFromApp(userId, str) + "\r\n";
                    }
                }
                comm.ExecuteNonQuery();
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                sr.Close();
                conn.Close();
            }
        }
    }

    //获取用户sql从app 
    public static string GetUserTableSqlFromApp(int userId, string str)
    {
        UserInfo user = JsonHelper.JsonDeserialize<UserInfo>(str);

        string result = string.Format(@"UPDATE UserTable SET MoneyStart='{1}', UserWorkDay='{2}', CategoryRate='{3}', IsUpdate='{4}', Synchronize='{5}' WHERE UserID = {0}",
                                      userId, user.UserMoney, user.UserWorkDay, user.CategoryRate, 1, 1);

        return result;
    }

    //商品
    public static string GetItemTableSqlFromApp(int userId, string str)
    {
        string[] arr = GetSqlArray(str);

        string result = string.Format(@"INSERT INTO ItemTable (ItemAppID, ItemType, ItemName, CategoryTypeID, ItemPrice, ItemBuyDate, UserID, ModifyDate, Recommend, Synchronize, RegionID, RegionType, ZhuanTiID, CardID) VALUES ({0},{1},{2},{3},{4},{5}, '{6}',{7},{8},{9},{10},{11},{12},{13})",
                                      arr[0], arr[9], arr[1], arr[4], arr[2], arr[3], userId, arr[3], arr[5], arr[6], arr[7], arr[8], arr[10], arr[11]);

        return result;
    }

    //商品
    public static string GetItemTableSqlFromWeb(int userId, string str)
    {
        string[] arr = GetSqlArray(str);

        string result = string.Format(@"INSERT INTO ItemTable (ItemID, ItemType, ItemName, CategoryTypeID, ItemPrice, ItemBuyDate, UserID, ModifyDate, Recommend, Synchronize, RegionID, RegionType, ZhuanTiID, CardID) VALUES ({0},{1},{2},{3},{4},{5}, '{6}',{7},{8},{9},{10},{11},{12},{13})",
                                      arr[0], arr[9], arr[1], arr[4], arr[2], arr[3], userId, arr[3], arr[5], arr[6], arr[7], arr[8], arr[10], arr[11]);

        return result;
    }
    
    //类别
    public static string GetCatTableSqlFromApp(int userId, string str)
    {
        string[] arr = GetSqlArray(str);

        string result = string.Format(@"INSERT INTO UserCategoryTable (CategoryTypeName, CategoryTypePrice, UserID, CategoryTypeID, Synchronize, CategoryTypeLive, ModifyDate) VALUES ({0},{1}, '{2}', {3},{4},{5}, '{6}')",
                                      arr[1].Trim(), arr[2], userId, arr[0], arr[6], arr[5], DateTime.Now);

        return result;
    }

    //专题
    public static string GetZhuanTableSqlFromApp(int userId, string str)
    {
        string[] arr = GetSqlArray(str);

        string result = string.Format(@"INSERT INTO ZhuanTiTable (ZhuanTiName, UserID, ZTID, ZhuanTiImage, Synchronize, ZhuanTiLive, ModifyDate) VALUES ({0}, '{1}', {2},{3},{4},{5}, '{6}')",
                                      arr[1].Trim(), userId, arr[0], arr[2], arr[4], arr[3], DateTime.Now);

        return result;
    }

    //转账
    public static string GetZhangTableSqlFromApp(int userId, string str)
    {
        string[] arr = GetSqlArray(str);

        string result = string.Format(@"INSERT INTO ZhuanZhangTable (ZhuanZhangFrom, ZhuanZhangTo, ZhuanZhangMoney, ZhuanZhangDate, ZhuanZhangNote, UserID, Synchronize, ZhuanZhangLive, ZZID, ModifyDate) VALUES ({0},{1},{2},{3},{4}, '{5}',{6},{7}, {8},{9})",
                                      arr[1].Trim(), arr[2], arr[3], arr[4], arr[5], userId, arr[7], arr[6], arr[0], arr[4]);

        return result;
    }

    //钱包
    public static string GetCardTableSqlFromApp(int userId, string str)
    {
        string[] arr = GetSqlArray(str);

        string result = string.Format(@"INSERT INTO CardTable (CardName, UserID, CDID, CardMoney, MoneyStart, Synchronize, CardLive, ModifyDate) VALUES ({0}, '{1}', {2},{3},{4},{5},{6}, '{7}')",
                                      arr[1].Trim(), userId, arr[0], 0, arr[2], arr[4], arr[3], DateTime.Now);

        return result;
    }

    //取sql值返回数组
    public static string[] GetSqlArray(string str)
    {
        string sql = str.Substring(str.IndexOf("VALUES (") + 8);
        sql = sql.Substring(0, sql.LastIndexOf(");"));
        return sql.Split(',');
    }
}