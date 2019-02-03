using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace MouseNet.Logophi.Utilities
{
    internal class MessageReceiver : IDisposable
    {
        private readonly CancellationTokenSource _ctSource =
            new CancellationTokenSource();

        private readonly PipeServer _pipeServer;
        private readonly SynchronizationContext _sync;

        public MessageReceiver
            (string name)
            {
            _pipeServer = new PipeServer(this, name);
            _sync = SynchronizationContext.Current;
            }

        public bool IsConnected => _pipeServer.IsConnected;

        public void Disconnect()
            {
            _pipeServer.Disconnect();
            }

        public void Dispose()
            {
            _ctSource?.Dispose();
            _pipeServer?.Dispose();
            }

        public void CancelListening()
            {
            _pipeServer.CancelWait();
            }

        public void StartListening()
            {
            _pipeServer.BeginWait();
            }

        public void StartReading()
            {
            Task.Run(ReadLoop, _ctSource.Token);
            }

        public void StopReading()
            {
            _ctSource.Cancel();
            }

        private void ReadLoop()
            {
            string temp;
            using (var sr = new StreamReader(_pipeServer.Stream))
                while ((temp = sr.ReadLine()) != null)
                    InvokeMessageReceived(this, temp);
            }

        private void InvokeMessageReceived
            (object sender,
             string args)
            {
            _sync.Send(s => MessageReceived?.Invoke(sender, args),
                       null);
            }

        private void InvokeConnected
            (object sender,
             EventArgs args)
            {
            _sync.Send(s => Connected?.Invoke(sender, args), null);
            }

        public event EventHandler Connected;
        public event EventHandler<string> MessageReceived;

        private class PipeServer : IDisposable
        {
            private readonly MessageReceiver _owner;
            private readonly NamedPipeServerStream _stream;
            private IAsyncResult _waitOperation;

            public PipeServer
                (MessageReceiver owner,
                 string pipeName)
                {
                _owner = owner;
                _stream = new NamedPipeServerStream(
                    pipeName,
                    PipeDirection.In,
                    NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Message,
                    PipeOptions.Asynchronous);
                }

            public bool IsConnected => _stream.IsConnected;
            public Stream Stream => _stream;

            public void Disconnect()
                {
                _stream.Disconnect();
                }

            public void Dispose()
                {
                _stream?.Dispose();
                }

            public void BeginWait()
                {
                _waitOperation =
                    _stream.BeginWaitForConnection(
                        WaitCallback,
                        null);
                }

            public void CancelWait()
                {
                if (_waitOperation == null) return;
                _stream.EndWaitForConnection(_waitOperation);
                _waitOperation = null;
                }

            private void WaitCallback
                (IAsyncResult result)
                {
                try
                    {
                    _stream.EndWaitForConnection(result);
                    _owner.InvokeConnected(_owner, EventArgs.Empty);
                    } catch (ObjectDisposedException) { }
                }
        }
    }
}