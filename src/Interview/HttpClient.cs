using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Interview
{
    public class HttpClient
    {
        async public static Task Http1()
        {
            Http11.MessageResponse response = Http11.Send(Http11.Method.GET, "/",
                new Dictionary<string, string>
                {
                    { "Host", "www.univr.it:443" },
                    { "Connection", "Close" },
                });

            if (response.Headers.ContainsKey("Location") && response.Status == "302")
            {
                Uri location = new Uri(response.Headers["Location"][0]);
                response = Http11.Send(Http11.Method.GET, location.PathAndQuery,
                new Dictionary<string, string>
                {
                    { "Host", $"{location.Authority}:{location.Port}" },
                    { "Connection", "Close" },
                });
            }
        }
    }

    public static class Http11
    {
        public static class Method
        {
            public static string GET { get; set; } = "GET";
        }

        class Message
        {
            public string Method { get; set; }
            public string RequestUrl { get; set; }
            public string Version { get; set; } = "HTTP/1.1";
            public Dictionary<string, string> Headers { get; set; }
            public string Body { get; set; }
            public byte[] ToByteRequest()
            {
                /*
                    <method> <request-URL> <version>
                    <headers>

                    <entity-body>
                */

                StringBuilder builder = new StringBuilder();
                builder.AppendFormat("{0} {1} {2}\r\n", Method, RequestUrl, Version);
                if (Headers != null)
                {
                    foreach (KeyValuePair<string, string> header in Headers)
                    {
                        builder.AppendFormat("{0}: {1}\r\n", header.Key, header.Value);
                    }
                }

                builder.Append("\r\n");

                if (!string.IsNullOrEmpty(Body))
                {
                    builder.Append(Body);
                }

                return Encoding.ASCII.GetBytes(builder.ToString());
            }
        }

        public class MessageResponse
        {
            public MessageResponse(MemoryStream ms) => Parse(ms);
            public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();
            public string Version { get; set; }
            public string Status { get; set; }
            public string Reason { get; set; }
            public string Body { get; set; }
            private void Parse(MemoryStream ms)
            {
                /*
                    <version> <status> <reason-phrase>
                    <headers>

                    <entity-body>
                */
                StreamReader readResultHeaders = new StreamReader(ms, Encoding.ASCII);
                var firstRowResult = readResultHeaders.ReadLine().Split(' ');
                Version = firstRowResult[0];
                Status = firstRowResult[1];
                Reason = firstRowResult.Length == 3 ? firstRowResult[2] : "";

                string header;
                while ((header = readResultHeaders.ReadLine()) != "\n\r" && header is object && !string.IsNullOrEmpty(header))
                {
                    string key = header.Substring(0, header.IndexOf(':'));
                    if (!Headers.ContainsKey(key))
                    {
                        Headers.Add(key, new List<string>());
                    }
                    string value = header.Substring(header.IndexOf(':') + 1).Trim();
                    Headers[key].Add(value);
                }

                if (ms.Position < ms.Length)
                {
                    Body = Encoding.ASCII.GetString(ms.GetBuffer(), (int)ms.Position, (int)ms.Length - (int)ms.Position);
                }
            }
        }

        public static MessageResponse Send(string method, string request_url, Dictionary<string, string> headers, string body = null)
        {
            Message msg = new Message();
            msg.Method = method;
            msg.RequestUrl = request_url;
            msg.Headers = headers;
            msg.Body = body;

            Uri uri = new Uri($"https://{headers["Host"]}", UriKind.Absolute);
            IPHostEntry hostEntry = Dns.GetHostEntry(uri.Authority);
            IPEndPoint ipe = new IPEndPoint(hostEntry.AddressList[0], uri.Port);
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipe);
            using NetworkStream ns = new NetworkStream(socket);
            using SslStream ssl = new SslStream(ns);
            ssl.AuthenticateAsClient(uri.Host);
            byte[] request = msg.ToByteRequest();
            ssl.Write(request, 0, request.Length);
            Span<byte> received = stackalloc byte[1024];
            int receivedBytes = 0;
            using MemoryStream ms = new MemoryStream();
            while ((receivedBytes = ssl.Read(received)) > 0)
            {
                ms.Write(received.Slice(0, receivedBytes));
            }
            ms.Position = 0;
            return new MessageResponse(ms);
        }
    }
}
