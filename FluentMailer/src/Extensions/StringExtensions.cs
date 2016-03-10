using System;
using System.Web;

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

            return HttpContext.Current.Server.MapPath(path);
        }
    }
}