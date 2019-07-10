using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Http11
{
    public class Http1Test
    {
        async public static Task Test()
        {
            var header = new Dictionary<string, string>
            {
                { "Host", "www.univr.it:443" },
                { "Connection", "Close" },
            };

            Http11.ResponseMessage response = await Http11.Send(Http11.Method.GET, "/", header);

            if (response.Headers.ContainsKey("Location") && response.Status == "302")
            {
                Uri location = new Uri(response.Headers["Location"][0]);
                response = await Http11.Send(Http11.Method.GET, location.PathAndQuery, header);
            }
        }
    }

    public static class Http11
    {
        public static class Method
        {
            public static string GET { get; set; } = "GET";
        }

        public class RequestMessage
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
                    throw new NotSupportedException("Send body is not supported");
                }

                return Encoding.ASCII.GetBytes(builder.ToString());
            }
        }

        public class ResponseMessage
        {
            public Dictionary<string, List<string>> Headers { get; set; } = new Dictionary<string, List<string>>();
            public string Version { get; set; }
            public string Status { get; set; }
            public string Reason { get; set; }
            public string Body { get; set; }
            public static ResponseMessage Parse(MessageReader ms)
            {
                /*
                    <version> <status> <reason-phrase>
                    <headers>

                    <entity-body>
                */

                ResponseMessage response = new ResponseMessage();

                var firstRowResult = ms.ReadStatusOrHeaderLine().Split(' ');
                response.Version = firstRowResult[0];
                response.Status = firstRowResult[1];
                response.Reason = firstRowResult.Length == 3 ? firstRowResult[2] : "";

                while (!ms.EOF)
                {
                    string line = ms.ReadStatusOrHeaderLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    string key = line.Substring(0, line.IndexOf(':'));
                    if (!response.Headers.ContainsKey(key))
                    {
                        response.Headers.Add(key, new List<string>());
                    }
                    string value = line.Substring(line.IndexOf(':') + 1).Trim();
                    response.Headers[key].Add(value);
                }

                int lenght;
                if (response.Headers.ContainsKey("Content-Length") && (lenght = int.Parse(response.Headers["Content-Length"][0])) > 0)
                {
                    if (response.Headers.ContainsKey("Content-Type"))
                    {
                        Encoding encoding = ParseEncoding(response.Headers["Content-Type"][0]);
                        response.Body = ms.ReadBody(encoding);
                    }
                    else
                    {
                        // Redirection status code 300-399
                        if (!response.Status.StartsWith("3"))
                        {
                            throw new NotSupportedException("Content-Type expected");
                        }
                    }
                }

                Debug.Assert(ms.EOF);

                return response;
            }
        }

        public class MessageReader
        {
            byte[] _payload;
            int _currentPos = 0;
            int _maxPos = 0;

            public MessageReader(byte[] payload, int offset, int lenght)
            {
                _payload = payload;
                _currentPos = offset;
                _maxPos = lenght - offset;
            }

            public bool EOF
            {
                get
                {
                    return _currentPos >= _maxPos;
                }
            }

            public string ReadStatusOrHeaderLine()
            {
                int start = _currentPos;

                while (_payload[_currentPos] != (char)10)
                {
                    _currentPos++;
                }

                if (EOF && start == _currentPos)
                {
                    return string.Empty;
                }
                else
                {
                    try
                    {
                        return Encoding.ASCII.GetString(_payload, start, (_currentPos - start) - ((_payload[_currentPos - 1] == (char)13 ? 2 : 1) - 1));
                    }
                    finally
                    {
                        _currentPos++;
                    }
                }
            }

            public string ReadBody(Encoding encoder)
            {
                int bodyLen = _maxPos - _currentPos;
                try
                {
                    return encoder.GetString(_payload, _currentPos, bodyLen);
                }
                finally
                {
                    _currentPos += bodyLen + 1;
                }
            }
        }

        private static Encoding ParseEncoding(string contentType)
        {
            if (contentType.Contains("charset"))
            {
                string charset = contentType.Substring(contentType.IndexOf("charset") + 8).Trim();
                return Encoding.GetEncoding(charset);
            }

            // https://www.ietf.org/rfc/rfc2045.txt 
            // Default RFC 822 messages without a MIME Content-Type header are taken
            // by this protocol to be plain text in the US-ASCII character set
            return Encoding.ASCII;
        }

        async public static Task<ResponseMessage> Send(string method, string request_url, Dictionary<string, string> headers, string body = null)
        {
            RequestMessage msg = new RequestMessage
            {
                Method = method,
                RequestUrl = request_url,
                Headers = headers,
                Body = body
            };

            if (!headers.ContainsKey("Host"))
            {
                throw new NotSupportedException("'Host' header expected");
            }

            Uri uri = new Uri($"https://{headers["Host"]}", UriKind.Absolute);
            IPHostEntry hostEntry = Dns.GetHostEntry(uri.Authority);
            IPEndPoint ipe = new IPEndPoint(hostEntry.AddressList[0], uri.Port);
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipe);
            using NetworkStream ns = new NetworkStream(socket);
            using SslStream ssl = new SslStream(ns);
            await ssl.AuthenticateAsClientAsync(uri.Host);
            byte[] request = msg.ToByteRequest();
            ssl.Write(request, 0, request.Length);
            byte[] received = ArrayPool<byte>.Shared.Rent(1024);
            try
            {
                using MemoryStream ms = new MemoryStream();
                int receivedBytes;
                int totalReceivedBytes = 0;
                while ((receivedBytes = ssl.Read(received)) > 0)
                {
                    ms.Write(received, 0, receivedBytes);
                    totalReceivedBytes += receivedBytes;
                }
                return ResponseMessage.Parse(new MessageReader(ms.ToArray(), 0, totalReceivedBytes));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(received);
            }
        }
    }
}
