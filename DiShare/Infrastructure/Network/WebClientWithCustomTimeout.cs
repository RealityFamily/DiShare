using System;
using System.Net;

namespace DiShare.Infrastructure.Network
{
    public class WebClientWithCustomTimeout : WebClient
    {
        private static readonly int DefaultTimeOut = 300000;

        public int Timeout { get; }

        public long Range { get; }

        public WebClientWithCustomTimeout()
            : this(WebClientWithCustomTimeout.DefaultTimeOut, 0L)
        {
        }

        public WebClientWithCustomTimeout(long range)
            : this(WebClientWithCustomTimeout.DefaultTimeOut, range)
        {
        }

        public WebClientWithCustomTimeout(int timeout, long range)
        {
            this.Timeout = timeout;
            this.Range = range;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(address);
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            httpWebRequest.AddRange(this.Range);
            if (httpWebRequest != null)
                httpWebRequest.Timeout = this.Timeout;
            return (WebRequest)httpWebRequest;
        }
    }
}