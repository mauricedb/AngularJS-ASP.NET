using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using RestDemo.Models;

// config.Formatters.Add(new BufferedJpegMediaTypeFormatter(HttpContext.Current.Server.MapPath("~/Images")));

namespace RESTfulBrowser.Formatters
{
    public class BufferedJpegMediaTypeFormatter : BufferedMediaTypeFormatter
    {
        private readonly string _folder;
        public BufferedJpegMediaTypeFormatter(string folder)
        {
            _folder = folder;
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/jpeg"));
            this.AddQueryStringMapping("$format", "jpeg", new MediaTypeHeaderValue("image/jpeg"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return typeof(Book).IsAssignableFrom(type);
        }

        public override void WriteToStream(
               Type type, object value, Stream writeStream, HttpContent content)
        {
            var book = value as Book;
            if (book != null)
            {
                var path = Path.Combine(_folder, book.ImageName);
                var buffer = File.ReadAllBytes(path);
                writeStream.Write(buffer, 0, buffer.Length);
            }
        }
    }
}