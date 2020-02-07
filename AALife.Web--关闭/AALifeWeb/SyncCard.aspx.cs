using AALife.BLL;
using AALife.Model;
using System;
using System.Data;

public partial class AALifeWeb_SyncCard : SyncBase
{
    private CardTableBLL bll = new CardTableBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        int cardId = Convert.ToInt32(Request.Form["cardid"]);
        string cardName = Request.Form["cardname"].ToString();
        decimal cardMoney = Convert.ToDecimal(Request.Form["cardmoney"]);
        byte cardLive = Convert.ToByte(Request.Form["cardlive"]);
        int userId = Convert.ToInt32(Request.Form["userid"]);
        string isUpdate = Request.Form["isupdate"] ?? "0";

        CardInfo card = bll.GetCardByCardId(userId, cardId);
        card.CDID = cardId;
        card.CardName = cardName;
        card.CardMoney = cardMoney;
        card.UserID = userId;
        card.CardLive = cardLive;
        card.Synchronize = 0;
        card.ModifyDate = DateTime.Now;

        if (isUpdate == "1")
        {
            card.CardMoney = 0;
            card.MoneyStart = Convert.ToDecimal(cardMoney);
        }

        bool success = false;
        if (card.CardID == 0)
        {
            success = bll.InsertCard(card);
        }
        else
        {
            success = bll.UpdateCard(card);
        }
                                
        string result = "{";
        if (success)
        {
            result += "\"result\":\"ok\"";
        }
        else
        {
            result += "\"result\":\"error\"";
        } 
        result += "}";

        Response.Write(result);
        Response.End();
    }

}