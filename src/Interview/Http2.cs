using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Http2
{
    public class Http2Test
    {
        static IWebHost kestrel;

        private static void StartKestrel(string[] args)
        {
            // Stephen sample https://github.com/dotnet/corefx/issues/38911#issue-460733080
            const int HttpsPort = 80;

            Console.WriteLine("Starting kestrel...");
            (kestrel = WebHost
                .CreateDefaultBuilder(args)
                .ConfigureLogging(log => log.AddFilter("Microsoft.AspNetCore", level => true))
                .UseKestrel(ko =>
                {
                    ko.ListenLocalhost(HttpsPort, listenOptions =>
                    {
                        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;
                    });
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("/", async context =>
                        {
                            await context.Response.WriteAsync("HELLO HTTP2");
                        });
                        endpoints.MapPost("/", async context =>
                        {
                            await context.Request.Body.CopyToAsync(context.Response.Body);
                        });
                    });
                })
                .Build())
                .Start();
        }

        async public static Task Test(string[] args)
        {
            StartKestrel(args);

            Task.Run(() =>
            {
                System.Threading.Thread.Sleep(10000);
                kestrel.Dispose();
            });

            await Http2.SomeTest();
        }
    }

    public static class Http2
    {
        public async static Task SomeTest()
        {
            const int HttpsPort = 80;
            IPEndPoint ipe = new IPEndPoint(IPAddress.Loopback, HttpsPort);
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipe);

            if (!socket.Connected)
            {
                throw new SocketException();
            }

            using NetworkStream ns = new NetworkStream(socket);

            byte[] testh2support = Encoding.ASCII.GetBytes("PRI * HTTP/2.0\r\n\r\nSM\r\n\r\n");
            byte[] settings = GetSetting();
            byte[] startConversation = new byte[testh2support.Length + settings.Length];
            Array.Copy(testh2support, 0, startConversation, 0, testh2support.Length);
            Array.Copy(settings, 0, startConversation, testh2support.Length, settings.Length);

            await ns.WriteAsync(startConversation, 0, startConversation.Length);

            byte[] received = ArrayPool<byte>.Shared.Rent(1024);
            try
            {
                using MemoryStream ms = new MemoryStream();
                int receivedBytes;
                int totalReceivedBytes = 0;
                while ((receivedBytes = await ns.ReadAsync(received, 0, received.Length)) > 0)
                {
                    ms.Write(received, 0, receivedBytes);
                    totalReceivedBytes += receivedBytes;
                }
                // return ResponseMessage.Parse(new MessageReader(ms.ToArray(), 0, totalReceivedBytes));
                Console.WriteLine($"Received {totalReceivedBytes}");
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(received);
            }
        }

        private static byte[] GetSetting()
        {
            MemoryStream settings = new MemoryStream();

            // SETTINGS_HEADER_TABLE_SIZE
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((ushort)0x1)), 0, sizeof(short));
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((uint)4096)), 0, sizeof(int));

            // SETTINGS_ENABLE_PUSH
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((ushort)0x2)), 0, sizeof(short));
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((uint)0)), 0, sizeof(int));

            // SETTINGS_MAX_CONCURRENT_STREAMS
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((ushort)0x3)), 0, sizeof(short));
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((uint)100)), 0, sizeof(int));

            // SETTINGS_INITIAL_WINDOW_SIZE 
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((ushort)0x4)), 0, sizeof(short));
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((uint)65535)), 0, sizeof(int));

            // SETTINGS_MAX_FRAME_SIZE 
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((ushort)0x05)), 0, sizeof(short));
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((uint)16384)), 0, sizeof(int));

            // SETTINGS_MAX_HEADER_LIST_SIZE 
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((ushort)0x06)), 0, sizeof(short));
            settings.Write(RevertIfNeeded(BitConverter.GetBytes((uint)16384)), 0, sizeof(int));

            Frame frame = new Frame(0, FrameType.SETTINGS);
            frame.WritePayload(settings.ToArray(), 0, (int)settings.Length);
            return frame.ToBytes();
        }

        private static byte[] RevertIfNeeded(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }
    }

    public enum FrameType : byte
    {
        SETTINGS = 0x4
    }

    public class Frame
    {
        MemoryStream _payload = new MemoryStream();
        int _streamIdentifier = 0;
        FrameType _frameType;
        byte _flag = 0;

        public Frame(int streamIdentifier, FrameType frameType)
        {
            if (streamIdentifier < 0)
            {
                throw new ArgumentException("Stream identifier cannot be less than 0");
            }

            _streamIdentifier = streamIdentifier;
            _frameType = frameType;
        }

        public void WritePayload(byte[] buffer, int offset, int count)
        {
            _payload.Write(buffer, offset, count);
        }

        public void SetFlag(byte flag)
        {

        }

        public byte[] ToBytes()
        {
            MemoryStream ms = new MemoryStream();

            // payload len
            byte[] framePayloadLen = RevertIfNeeded(BitConverter.GetBytes(_payload.Length));
            ms.Write(framePayloadLen, sizeof(long) - 3, 3);

            // type
            ms.WriteByte((byte)_frameType);

            // flags
            ms.WriteByte(_flag);

            // stream identifier
            byte[] streamIdentifierBytes = BitConverter.GetBytes(_streamIdentifier);
            ms.Write(streamIdentifierBytes, 0, sizeof(int));

            // copy payload
            _payload.Position = 0;
            _payload.CopyTo(ms);

            return ms.ToArray();
        }

        private static byte[] RevertIfNeeded(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }
            return bytes;
        }
    }
}
