using System;
using System.Web;
using System.Web.Hosting;

namespace FluentMailer.Extensions
{
    internal static class StringExtensions
    {
        internal static string ResolvePath(this string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("Path cannot be a null");
            }

            if (!path.StartsWith("~"))
            {
                if (!path.StartsWith("/") && !path.StartsWith(@"\"))
                {
                    path = "/" + path;
                }

                path = "~" + path;
            }

            return HostingEnvironment.MapPath(path) ?? path.Replace("~", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}