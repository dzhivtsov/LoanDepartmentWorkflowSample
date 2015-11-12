namespace GangsterBank.Web.Infrastructure.Extensions
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Web;

    using GangsterBank.Core.Extensions;

    public static class HttpPostedFileBaseExtensions
    {
        #region Public Methods and Operators

        public static byte[] ToByteArray(this HttpPostedFileBase file)
        {
            Contract.Requires<ArgumentNullException>(file.IsNotNull());
            using (var target = new MemoryStream())
            {
                file.InputStream.CopyTo(target);
                byte[] data = target.ToArray();
                return data;
            }
        }

        #endregion
    }
}