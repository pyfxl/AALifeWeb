using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// FileHelper 的摘要说明
/// </summary>
public class FileHelper
{
	public FileHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static DataTable GetFileName(string dirPath)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("FileName");

        if (Directory.Exists(dirPath))
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            FileInfo[] files = dir.GetFiles("*.*");

            string[] fileNames = new string[files.Length];            

            foreach (FileInfo fileInfo in files)
            {
                DataRow dr = dt.NewRow();
                dr["FileName"] = fileInfo.Name;
                dt.Rows.Add(dr);
            }
        }

        return dt;
    }

    public static void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

}