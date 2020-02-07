using System;

public partial class AALifeWeb_GetWebVersion : SyncBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String result = "{ \"version\":\"6.0.4\" }";
        
        Response.Write(result);
        Response.End();
    }
}