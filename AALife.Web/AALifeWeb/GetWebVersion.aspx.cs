using System;

public partial class AALifeWeb_GetWebVersion : SyncBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String result = "{ \"version\":\"5.9.5\" }";
        
        Response.Write(result);
        Response.End();
    }
}