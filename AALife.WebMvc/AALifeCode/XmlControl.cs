using System.Xml;

public class XmlControl
{
    private string myXmlFile;
    private XmlDocument myXmlDoc = new XmlDocument();

    public XmlControl(string xmlFile)
    {
        try
        {
            myXmlDoc.Load(xmlFile);
        }
        catch { }

        myXmlFile = xmlFile;
    }

    public string GetText(string xmlNode)
    {
        XmlNode myXmlNode = myXmlDoc.SelectSingleNode(xmlNode);
        return myXmlNode.InnerText;
    }

    public void Dispose()
    {
        myXmlDoc = null;
    }

    public bool Save()
    {
        try
        {
            myXmlDoc.Save(myXmlFile);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public void Update(string xmlNode, string content)
    {
        XmlNode myXmlNode = myXmlDoc.SelectSingleNode(xmlNode);
        myXmlNode.InnerText = content;
    }
}