using Microsoft.AspNetCore.Mvc.Rendering;

namespace Spending.Web;

public static class HtmlExtensions
{
    public static string IsActivePage(this IHtmlHelper html, string page)
    {
        var routeData = html.ViewContext.RouteData;
        var currentPage = routeData.Values["page"]?.ToString().Replace("/", "");
        return string.Equals(currentPage, page, StringComparison.OrdinalIgnoreCase) ? "active" : string.Empty;
    }
}
