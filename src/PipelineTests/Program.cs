using System;
using System.Buffers;
using System.IO;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace PipelineTests
{
    class Program
    {
        static void Main(string[] args)
        {

            PipeMem m = new PipeMem();

            Task.Run(async () =>
            {
                var r = new Random();
                while (true)
                {
                    await m.WriteAsync(BitConverter.GetBytes(r.Next()));
                    await Task.Delay(100);
                }
            });

            Task.Run(async () =>
            {
                var r = new Random();
                while (true)
                {
                    Memory<byte> data = new Memory<byte>();
                    await m.ReadAsync(data);
                    await Task.Delay(100);
                }
            });

            Console.Read();
        }
    }


    public class PipeMem : Stream
    {
        Pipe p = new Pipe(new PipeOptions() { });

        public PipeMem()
        {

        }

        public override bool CanRead => throw new NotImplementedException();

        public override bool CanSeek => throw new NotImplementedException();

        public override bool CanWrite => throw new NotImplementedException();

        public override long Length => throw new NotImplementedException();

        public override long Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public async override ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        {
            ReadResult readResult = await p.Reader.ReadAsync();
            long bytesToCopyCount = Math.Min(buffer.Length, readResult.Buffer.Length);
            ReadOnlySequence<byte> slice = readResult.Buffer.Slice(0, bytesToCopyCount);
            Console.WriteLine(slice.IsSingleSegment);
            bool isCompleted = readResult.IsCompleted && slice.End.Equals(readResult.Buffer.End);

            if (isCompleted)
            {
                p.Reader.Complete();
            }

            slice.CopyTo(buffer.Span);
            p.Reader.AdvanceTo(slice.End);
            return (int)bytesToCopyCount;
        }

        public async override ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
        {
            await p.Writer.WriteAsync(buffer, cancellationToken);
        }
    }
}
