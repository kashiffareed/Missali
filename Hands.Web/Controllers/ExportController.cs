using System;
using System.IO;
using System.Web.Mvc;
using System.Web;
using HtmlAgilityPack;


namespace Hands.Web.Controllers
{
    public class ExportController : ControllerBase
    {

        public static void ExportExcel(string htmlContent, string filename, string title)
        {

            System.Web.HttpContext.Current.Response.ContentType = "application/force-download";
            System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + filename + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
            System.Web.HttpContext.Current.Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            System.Web.HttpContext.Current.Response.Write("<head>");
            System.Web.HttpContext.Current.Response.Write("<META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
            System.Web.HttpContext.Current.Response.Write("<!--[if gte mso 9]><xml>");
            System.Web.HttpContext.Current.Response.Write("<x:ExcelWorkbook>");
            System.Web.HttpContext.Current.Response.Write("<x:ExcelWorksheets>");
            System.Web.HttpContext.Current.Response.Write("<x:ExcelWorksheet>");
            System.Web.HttpContext.Current.Response.Write("<x:Name>Report Data</x:Name>");
            System.Web.HttpContext.Current.Response.Write("<x:WorksheetOptions>");
            System.Web.HttpContext.Current.Response.Write("<x:Print>");
            System.Web.HttpContext.Current.Response.Write("<x:ValidPrinterInfo/>");
            System.Web.HttpContext.Current.Response.Write("</x:Print>");
            System.Web.HttpContext.Current.Response.Write("</x:WorksheetOptions>");
            System.Web.HttpContext.Current.Response.Write("</x:ExcelWorksheet>");
            System.Web.HttpContext.Current.Response.Write("</x:ExcelWorksheets>");
            System.Web.HttpContext.Current.Response.Write("</x:ExcelWorkbook>");
            System.Web.HttpContext.Current.Response.Write("</xml>");
            System.Web.HttpContext.Current.Response.Write("<![endif]--> ");
            System.Web.HttpContext.Current.Response.Write("</head>");
            System.Web.HttpContext.Current.Response.Write("<h2>" + title + "</h2>");
            //var rxStr = "<div[^<]+class=([\"'])mvc-grid-pager\\1.*</div>";

            //var rx = new System.Text.RegularExpressions.Regex(rxStr,
            //    System.Text.RegularExpressions.RegexOptions.IgnoreCase);


            //var nStr = rx.Replace(htmlContent, "");

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlContent);
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class='mvc-grid-pager']");
            //HtmlNode node1 = doc.DocumentNode.SelectSingleNode("//div[@id='hands']");
            node.RemoveAll();
            //node1.RemoveAll();

            var newHtml = doc.DocumentNode.InnerHtml;
            //htmlContent = doc.ToString()
            //    .Replace("~style~", "background-color:#666;color:white;font-size:14px; height:30px")
            //    .Replace("~abc~", "display:none");

            System.Web.HttpContext.Current.Response.Output.Write(newHtml);
            //HttpContext.Current.Response.End();
            //HttpContext.Current.Response.Clear();
        }

        public static string RenderViewToString(ControllerContext context, string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = context.RouteData.GetRequiredString("action");

            var viewData = new ViewDataDictionary(model);

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(context, viewName);
                var viewContext = new ViewContext(context, viewResult.View, viewData, new TempDataDictionary(), sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }

        }
    }

 
}