using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// UtilityHelper 的摘要说明
/// </summary>
public class UtilityHelper
{
	public UtilityHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //字符是否空
    public static bool StringIsEmpty(string str)
    {
        return (str == "" || str == null || str == "null");
    }
    
}