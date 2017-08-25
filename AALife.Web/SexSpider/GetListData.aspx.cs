using System;
using System.Text;

public partial class SexSpider_GetListData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder result = new StringBuilder();

        //max siteid 61

        //start
        result.Append("{");

        //1. 好了KK
        result.Append("\"siteid\":\"1\",\"siterank\":\"A01\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"好了KK\",\"listpage\":\"1-(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://se.haoa02.com\",\"sitelink\":\"http://se.haoa02.com/listhtml/1.html\",\"listdiv\":\"div.list ul li a\",\"imagediv\":\"div.main img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"");

        ////start
        //result.Append("\"site_list\":[");
        ////1. 好了KK
        //result.Append("{\"siteid\":\"1\",\"siterank\":\"A01\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"好了KK\",\"listpage\":\"1-(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://se.haoa02.com\",\"sitelink\":\"http://se.haoa02.com/listhtml/1.html\",\"listdiv\":\"div.list ul li a\",\"imagediv\":\"div.main img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////26. 激情五月
        //result.Append("{\"siteid\":\"26\",\"siterank\":\"A02\",\"viplevel\":\"1\",\"ishided\":\"1\",\"sitename\":\"激情五月【失效】\",\"listpage\":\"list_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.42pj.com\",\"sitelink\":\"http://www.42pj.com/p01/index.html\",\"listdiv\":\"div.typelist ul li a\",\"imagediv\":\"div.mtop img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////3. 摸逼逼
        //result.Append("{\"siteid\":\"3\",\"siterank\":\"A03\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"摸逼逼\",\"listpage\":\"(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.mbbb33.com\",\"sitelink\":\"http://www.mbbb33.com/mnv67/6/1.html\",\"listdiv\":\"div.zxlist li a\",\"imagediv\":\"div.introtxt img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"GetImageFilter\",\"pagefilter\":\"\"},");
        ////4. 哥哥嘿
        //result.Append("{\"siteid\":\"4\",\"siterank\":\"A04\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"哥哥嘿【未更新】\",\"listpage\":\"index_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.163hei.com\",\"sitelink\":\"http://www.163hei.com/tupian/toupai/index.html\",\"listdiv\":\"div.zxlist li a\",\"imagediv\":\"div.introtxt img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////5. AV天堂网
        //result.Append("{\"siteid\":\"5\",\"siterank\":\"A05\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"AV天堂网\",\"listpage\":\"list_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.avtt2014cc.net\",\"sitelink\":\"http://www.avtt2014cc.net/zipaitoupai/index.html\",\"listdiv\":\"div.news_right li a\",\"imagediv\":\"div#text_novel img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"SplitTitleFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////51. 爱色阁-自拍专区
        //result.Append("{\"siteid\":\"51\",\"siterank\":\"A07\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"爱色阁-自拍偷拍\",\"listpage\":\"list_8_(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.woai22.com\",\"sitelink\":\"http://www.woai22.com/html/tpzp/list_8_1.html\",\"listdiv\":\"table.listt td a\",\"imagediv\":\"div.content img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////7. 91Porn-原创申请
        //result.Append("{\"siteid\":\"7\",\"siterank\":\"A09\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"91Porn-原创申请\",\"listpage\":\"forumdisplay.php?fid=19&orderby=dateline&page=(*)\",\"pageencode\":\"utf-8\",\"domain\":\"https://91.p9a.space/\",\"sitelink\":\"https://91.p9a.space/forumdisplay.php?fid=19&orderby=dateline&page=1\",\"listdiv\":\"th.new a\",\"imagediv\":\"div.postmessage img[file]\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoNumberFilter\",\"imagefilter\":\"GetImageFilter\",\"pagefilter\":\"\"},");
        ////12. 91Porn-我爱我妻
        //result.Append("{\"siteid\":\"12\",\"siterank\":\"A10\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"91Porn-我爱我妻\",\"listpage\":\"forumdisplay.php?fid=21&orderby=dateline&page=(*)\",\"pageencode\":\"utf-8\",\"domain\":\"https://91.p9a.space/\",\"sitelink\":\"https://91.p9a.space/forumdisplay.php?fid=21&orderby=dateline&page=1\",\"listdiv\":\"th.new a\",\"imagediv\":\"div.postmessage img[file]\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoNumberFilter\",\"imagefilter\":\"GetImageFilter\",\"pagefilter\":\"\"},");
        ////8. 超碰在线视频
        //result.Append("{\"siteid\":\"8\",\"siterank\":\"A11\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"超碰在线视频\",\"listpage\":\"albums?page=(*)\",\"pageencode\":\"utf-8\",\"domain\":\"https://51.caoxee.com\",\"sitelink\":\"https://51.caoxee.com/albums?page=1\",\"listdiv\":\"div.album_box_new a\",\"imagediv\":\"div.box img\",\"pagediv\":\"div.pagination a\",\"pagelevel\":\"1\",\"listfilter\":\"GetTitleFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////22. 淫色淫色
        //result.Append("{\"siteid\":\"22\",\"siterank\":\"A12\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"淫色淫色【未更新】\",\"listpage\":\"index-(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.vf4e.com\",\"sitelink\":\"http://www.vf4e.com/AAtupian/AAtb/zipai/index.html\",\"listdiv\":\"div.list li a\",\"imagediv\":\"div.post-bd img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoSpanFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////23. Ady映画网
        //result.Append("{\"siteid\":\"23\",\"siterank\":\"A13\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"Ady映画网\",\"listpage\":\"New(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://jj.ady1.info\",\"sitelink\":\"http://jj.ady1.info/zjgx/New.html\",\"listdiv\":\"div.pre_b_classes_text h1 a\",\"imagediv\":\"div.movie_c_pre img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////25. 久久热
        //result.Append("{\"siteid\":\"25\",\"siterank\":\"A15\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"久久热\",\"listpage\":\"(*)/\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.99kk7.com\",\"sitelink\":\"http://www.99kk7.com/albums/\",\"listdiv\":\"div.thumb-content a\",\"imagediv\":\"div.models-slider-big img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"GetTitleFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////37. 久久热
        //result.Append("{\"siteid\":\"37\",\"siterank\":\"A16\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"久久热论坛\",\"listpage\":\"forum-169-(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.99rebbs7.com/\",\"sitelink\":\"http://www.99rebbs7.com/forum-169-1.html\",\"listdiv\":\"a.xst\",\"imagediv\":\"div.t_fsz img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////32. 色色999
        //result.Append("{\"siteid\":\"32\",\"siterank\":\"A18\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"色色999\",\"listpage\":\"23-(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.1111kf.com\",\"sitelink\":\"http://www.1111kf.com/artlist/23.html\",\"listdiv\":\"div.atrlist li a\",\"imagediv\":\"div#postmessage img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////33. 七次郎
        //result.Append("{\"siteid\":\"33\",\"siterank\":\"A19\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"七次郎-真实自拍\",\"listpage\":\"index_page_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.qicifu.com\",\"sitelink\":\"http://www.qicifu.com/pictures/6/index.html\",\"listdiv\":\"div.img-cont a:not([style])\",\"imagediv\":\"div.right-cont-img img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"GetTitleFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////57. 七次郎
        //result.Append("{\"siteid\":\"57\",\"siterank\":\"A19\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"七次郎-走光偷拍\",\"listpage\":\"index_page_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.qicifu.com\",\"sitelink\":\"http://www.qicifu.com/pictures/5/index.html\",\"listdiv\":\"div.img-cont a:not([style])\",\"imagediv\":\"div.right-cont-img img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"GetTitleFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////2. 一焦进洞
        //result.Append("{\"siteid\":\"2\",\"siterank\":\"A20\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"一焦进洞【重复】\",\"listpage\":\"(*).jsp\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.yjjdo.pw\",\"sitelink\":\"http://www.yjjdo.pw/yjj2/6/1.jsp\",\"listdiv\":\"ul.textList li a\",\"imagediv\":\"div.picContent img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoSpanFilter\",\"imagefilter\":\"GetImageFilter\",\"pagefilter\":\"\"},");
        ////38. ♂爱AB♀
        //result.Append("{\"siteid\":\"38\",\"siterank\":\"A21\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"♂爱AB♀【未更新】\",\"listpage\":\"list_10_(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.iab08.com\",\"sitelink\":\"http://www.iab08.com/ws/list_10_1.html\",\"listdiv\":\"ul.pic-list2 li a\",\"imagediv\":\"div.article_body img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoImageFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////39. 撸撸嘿
        //result.Append("{\"siteid\":\"39\",\"siterank\":\"A22\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"撸撸嘿【未更新】\",\"listpage\":\"albums?page=(*)\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.yehualu.la\",\"sitelink\":\"http://www.yehualu.la/albums?page=1\",\"listdiv\":\"div.row div.well a\",\"imagediv\":\"div.row div.thumb-overlay img\",\"pagediv\":\"\",\"pagelevel\":\"1\",\"listfilter\":\"NoImageFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////40. 开心五月
        //result.Append("{\"siteid\":\"40\",\"siterank\":\"A23\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"开心五月【未更新】\",\"listpage\":\"index(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.pppcao9.com\",\"sitelink\":\"http://www.pppcao9.com/html/zt/index.html\",\"listdiv\":\"ul.textList li a\",\"imagediv\":\"div.content img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoSpanFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////41. 银色网
        //result.Append("{\"siteid\":\"41\",\"siterank\":\"A25\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"银色网\",\"listpage\":\"19_(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.84kc.com\",\"sitelink\":\"http://www.84kc.com/html/part/19.html\",\"listdiv\":\"table.listt td a\",\"imagediv\":\"div.content img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoFontFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////28. 就去路
        //result.Append("{\"siteid\":\"28\",\"siterank\":\"A26\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"就去路\",\"listpage\":\"index(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.bangbanglu4.com\",\"sitelink\":\"http://www.bangbanglu4.com/tupian/wangyouzipai/index.html\",\"listdiv\":\"div.flare li a\",\"imagediv\":\"div.main_box img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoEmFilter,NoSpanFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////6. 神马影院
        //result.Append("{\"siteid\":\"6\",\"siterank\":\"A27\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"神马影院【未更新】\",\"listpage\":\"index60_(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.av902.info\",\"sitelink\":\"http://www.av902.info/l/m/index60.html\",\"listdiv\":\"div.list td a\",\"imagediv\":\"div.content img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////43. 插插插
        //result.Append("{\"siteid\":\"43\",\"siterank\":\"A28\",\"viplevel\":\"1\",\"ishided\":\"1\",\"sitename\":\"插插插【失效】\",\"listpage\":\"list_(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.872aa.com\",\"sitelink\":\"http://www.872aa.com/PIC01/index.html\",\"listdiv\":\"div.zxlist li a\",\"imagediv\":\"div.temp23 img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////45. 色哥哥
        //result.Append("{\"siteid\":\"45\",\"siterank\":\"A30\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"色哥哥\",\"listpage\":\"index32_(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.sggqu.com\",\"sitelink\":\"http://www.sggqu.com/html/part/index32.html\",\"listdiv\":\"div.zxlist li a\",\"imagediv\":\"div.newsdes img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////46. 66爱爱
        //result.Append("{\"siteid\":\"46\",\"siterank\":\"A31\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"66爱爱【未更新】\",\"listpage\":\"9_(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.40gege.com\",\"sitelink\":\"http://www.40gege.com/news/other/9.html\",\"listdiv\":\"ul.article-list li a\",\"imagediv\":\"div.show img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoFontFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////48. 台湾妹中文网
        //result.Append("{\"siteid\":\"48\",\"siterank\":\"A33\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"台湾妹中文网\",\"listpage\":\"list5(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.41xe.com\",\"sitelink\":\"http://www.41xe.com/tupianqu/TSE/list51.html\",\"listdiv\":\"div.list li a\",\"imagediv\":\"div.post img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoSpanFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////50. 良心撸
        //result.Append("{\"siteid\":\"50\",\"siterank\":\"A35\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"良心撸\",\"listpage\":\"(*).htm\",\"pageencode\":\"utf-8\",\"domain\":\"https://www.bbb860.com\",\"sitelink\":\"https://www.bbb860.com/htm/piclist1/1.htm\",\"listdiv\":\"ul.textList li a\",\"imagediv\":\"div.picContent img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoSpanFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////60. 野鸡网
        //result.Append("{\"siteid\":\"60\",\"siterank\":\"A36\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"野鸡网【未更新】\",\"listpage\":\"index_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.yeji77.com\",\"sitelink\":\"http://www.yeji77.com/picture/zipai/index.html\",\"listdiv\":\"div.left li a\",\"imagediv\":\"div.info-zi img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoImageFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////61. 我爱av
        //result.Append("{\"siteid\":\"61\",\"siterank\":\"A37\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"我爱av\",\"listpage\":\"23-(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.38tvtv.com\",\"sitelink\":\"http://www.38tvtv.com/artlist/23.html\",\"listdiv\":\"div.artlist li a\",\"imagediv\":\"div#postmessage img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////62. 好色ddse
        //result.Append("{\"siteid\":\"62\",\"siterank\":\"A38\",\"viplevel\":\"1\",\"ishided\":\"0\",\"sitename\":\"好色ddse\",\"listpage\":\"1-(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.ddse02.com\",\"sitelink\":\"http://www.ddse02.com/artlisthtml/1.html\",\"listdiv\":\"div.mainArea li a\",\"imagediv\":\"div.novelContent img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"NoSpanFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");

        //////////////////////////////////////////////////////////////////////////////VIP分隔线////////////////////////////////////////////////////////////////////////////

        ////31. 中国人体艺术
        //result.Append("{\"siteid\":\"31\",\"siterank\":\"B01\",\"viplevel\":\"2\",\"ishided\":\"0\",\"sitename\":\"中国人体艺术\",\"listpage\":\"index_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"https://www.rt114.net\",\"sitelink\":\"https://www.rt114.net/zg/index.html\",\"listdiv\":\"li.thumbnail a\",\"imagediv\":\"div.thumbnail img\",\"pagediv\":\"ul.pagination a\",\"pagelevel\":\"2\",\"listfilter\":\"GetTitleFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////21. 人体艺术
        //result.Append("{\"siteid\":\"21\",\"siterank\":\"B02\",\"viplevel\":\"2\",\"ishided\":\"0\",\"sitename\":\"人体艺术\",\"listpage\":\"(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.rentiyishu77.org/rentiwaipai/\",\"sitelink\":\"http://www.rentiyishu77.org/rentiwaipai/index.html\",\"listdiv\":\"ul.photo li a\",\"imagediv\":\"ul.file img\",\"pagediv\":\"ul.photo a\",\"pagelevel\":\"1\",\"listfilter\":\"NoImageFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////13. 妹子图
        //result.Append("{\"siteid\":\"13\",\"siterank\":\"B03\",\"viplevel\":\"3\",\"ishided\":\"0\",\"sitename\":\"妹子图\",\"listpage\":\"(*)\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.mzitu.com\",\"sitelink\":\"http://www.mzitu.com/page/1\",\"listdiv\":\"div.postlist li span a\",\"imagediv\":\"div.main-image img\",\"pagediv\":\"div.pagenavi a\",\"pagelevel\":\"2\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"GetSpanFilter\"},");
        ////14. 猫扑炫图
        //result.Append("{\"siteid\":\"14\",\"siterank\":\"B04\",\"viplevel\":\"3\",\"ishided\":\"1\",\"sitename\":\"猫扑炫图\",\"listpage\":\"list_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://tt.mop.com\",\"sitelink\":\"http://tt.mop.com/xuan/list_1.html\",\"listdiv\":\"div.picTitle a\",\"imagediv\":\"div.txtmod img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////58. 猫女郎
        //result.Append("{\"siteid\":\"58\",\"siterank\":\"B04\",\"viplevel\":\"3\",\"ishided\":\"0\",\"sitename\":\"猫女郎\",\"listpage\":\"1_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"http://tt.mop.com\",\"sitelink\":\"http://tt.mop.com/c44.html\",\"listdiv\":\"div.postTitle a\",\"imagediv\":\"div.txtmod img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////15. ZOL人像摄影
        //result.Append("{\"siteid\":\"15\",\"siterank\":\"B05\",\"viplevel\":\"3\",\"ishided\":\"0\",\"sitename\":\"ZOL人像摄影\",\"listpage\":\"d16_pic_new_p(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://bbs.zol.com.cn\",\"sitelink\":\"http://bbs.zol.com.cn/dcbbs/d16_pic_new_p1.html\",\"listdiv\":\"div.pic-infor h4 a\",\"imagediv\":\"div#bookContent img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"GetImageFilter\",\"pagefilter\":\"\"},");
        ////10. 1000人体
        //result.Append("{\"siteid\":\"10\",\"siterank\":\"B06\",\"viplevel\":\"2\",\"ishided\":\"0\",\"sitename\":\"1000人体【失效】\",\"listpage\":\"list_1_(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.renti1000.com\",\"sitelink\":\"http://www.renti1000.com/yazhourenti/index.html\",\"listdiv\":\"a.pic\",\"imagediv\":\"div.big_img img\",\"pagediv\":\"ul.pagelist a\",\"pagelevel\":\"1\",\"listfilter\":\"NoImageFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////19. 爱人体
        //result.Append("{\"siteid\":\"19\",\"siterank\":\"B07\",\"viplevel\":\"2\",\"ishided\":\"0\",\"sitename\":\"爱人体\",\"listpage\":\"(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.airenti99.org/yazhourenti/\",\"sitelink\":\"http://www.airenti99.org/yazhourenti/index.html\",\"listdiv\":\"ul.photo li a\",\"imagediv\":\"ul.file img\",\"pagediv\":\"ul.photo a\",\"pagelevel\":\"1\",\"listfilter\":\"NoImageFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////20. 漂漂美术馆
        //result.Append("{\"siteid\":\"20\",\"siterank\":\"B08\",\"viplevel\":\"3\",\"ishided\":\"0\",\"sitename\":\"漂漂美术馆\",\"listpage\":\"(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.ppmsg.net/jiepaimeinv/\",\"sitelink\":\"http://www.ppmsg.net/jiepaimeinv/index.html\",\"listdiv\":\"ul.image li a\",\"imagediv\":\"div#imagelist img\",\"pagediv\":\"ul.image a\",\"pagelevel\":\"1\",\"listfilter\":\"NoImageFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////54. 热推美女
        //result.Append("{\"siteid\":\"54\",\"siterank\":\"B09\",\"viplevel\":\"3\",\"ishided\":\"0\",\"sitename\":\"热推美女\",\"listpage\":\"index_(*).html\",\"pageencode\":\"utf-8\",\"domain\":\"https://www.retumm.com\",\"sitelink\":\"https://www.retumm.com/xg/index_2.html\",\"listdiv\":\"div.main dd:not(.page) a\",\"imagediv\":\"div.picbox img\",\"pagediv\":\"div.page a\",\"pagelevel\":\"2\",\"listfilter\":\"GetTitleFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////55. 人体艺术吧
        //result.Append("{\"siteid\":\"55\",\"siterank\":\"B10\",\"viplevel\":\"2\",\"ishided\":\"0\",\"sitename\":\"人体艺术吧\",\"listpage\":\"index(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.88rt.org\",\"sitelink\":\"http://www.88rt.org/index.html\",\"listdiv\":\"div#container div.imgholder a\",\"imagediv\":\"div#container img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"GetTitleFilter\",\"imagefilter\":\"GetImageFilter,Site55_Filter\",\"pagefilter\":\"\"},");
        ////56. 黑丝袜
        //result.Append("{\"siteid\":\"56\",\"siterank\":\"B11\",\"viplevel\":\"3\",\"ishided\":\"0\",\"sitename\":\"黑丝袜\",\"listpage\":\"(*).html\",\"pageencode\":\"gb2312\",\"domain\":\"http://www.heisiwa.com/jiepaimeinv/\",\"sitelink\":\"http://www.heisiwa.com/jiepaimeinv/index.html\",\"listdiv\":\"ul.photo li a\",\"imagediv\":\"ul.file img\",\"pagediv\":\"ul.image a\",\"pagelevel\":\"1\",\"listfilter\":\"NoImageFilter,NoHtmlFilter\",\"imagefilter\":\"\",\"pagefilter\":\"\"},");
        ////59. 豆瓣美女
        //result.Append("{\"siteid\":\"59\",\"siterank\":\"B12\",\"viplevel\":\"3\",\"ishided\":\"0\",\"sitename\":\"豆瓣美女\",\"listpage\":\"rank.htm?pager_offset=(*)\",\"pageencode\":\"utf-8\",\"domain\":\"http://www.dbmeinv.com\",\"sitelink\":\"http://www.dbmeinv.com/dbgroup/rank.htm?pager_offset=1\",\"listdiv\":\"div.bottombar a\",\"imagediv\":\"div.topic-figure img\",\"pagediv\":\"\",\"pagelevel\":\"0\",\"listfilter\":\"\",\"imagefilter\":\"\",\"pagefilter\":\"\"}");

        ////end
        //result.Append("],");

        ////外部词典
        //result.Append("\"ext_dic\":[\"内射\",\"內射\",\"车震\",\"大屁股\",\"人妻\",\"美臀\",\"肥臀\",\"翘臀\",\"换妻\",\"后入\",\"後入\",\"豐滿\",\"水多\",\"炮友\",\"網友\",\"白漿\",\"闺蜜\",\"陰道\",\"少婦\",\"熟婦\",\"賓館\",\"鮑魚\",\"極品\",\"鄰居\"],");

        ////停止词典
        //result.Append("\"stop_dic\":[\"的\",\"了\",\"在\",\"是\",\"我\",\"有\",\"和\",\"就\",\"不\",\"人\",\"都\",\"一\",\"一个\",\"上\",\"也\",\"很\",\"到\",\"说\",\"要\",\"去\",\"你\",\"会\",\"着\",\"没有\",\"看\",\"好\",\"自己\",\"这\"],");

        ////删除词典
        //result.Append("\"del_dic\":[]");

        //end
        result.Append("}");

        Response.Write(result.ToString());
        Response.End();
    }
}