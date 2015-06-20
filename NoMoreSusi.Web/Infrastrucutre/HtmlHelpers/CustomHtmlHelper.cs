using System.Text;
using System.Web.Mvc;

namespace NoMoreSusi.Web.Infrastrucutre.HtmlHelpers
{
    public static class CustomHtmlHelper
    {
        public static MvcHtmlString SubmitButton(this HtmlHelper helper, string buttonText, object htmlAttributes = null)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<input type = 'submit' value = '{0}' ", buttonText);
            var attributes = helper.AttributeEncode(htmlAttributes);
            if (!string.IsNullOrEmpty(attributes))
            {
                attributes = attributes.Trim('{', '}');
                var attrValuePairs = attributes.Split(',');
                foreach (var attrValuePair in attrValuePairs)
                {
                    var equalIndex = attrValuePair.IndexOf('=');
                    var attrValue = attrValuePair.Split('=');
                    html.AppendFormat("{0}='{1}' ", attrValuePair.Substring(0, equalIndex).Trim(), attrValuePair.Substring(equalIndex + 1).Trim());
                }
            }

            html.Append("/>");
            return new MvcHtmlString(html.ToString());
        }
    }
}