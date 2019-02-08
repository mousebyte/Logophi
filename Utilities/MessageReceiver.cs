using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Threading.Tasks;

namespace MouseNet.Logophi.Utilities
{
    /// <inheritdoc />
    /// <summary>
    /// Hosts a <see cref="NamedPipeServerStream" /> and notifies listeners
    /// when a connection is made or a message is recieved.
    /// </summary>
    internal class MessageReceiver : IDisposable
    {
        private readonly CancellationTokenSource _ctSource =
            new CancellationTokenSource();

        private readonly PipeServer _pipeServer;
        private readonly SynchronizationContext _sync;

        /// <summary>
        /// Creates a new <see cref="MessageReceiver"/> object.
        /// </summary>
        /// <param name="name">The name to use for the <see cref="NamedPipeServerStream"/>
        /// hosted by the <see cref="MessageReceiver"/>.</param>
        public MessageReceiver
            (string name)
            {
            _pipeServer = new PipeServer(this, name);
            _sync = SynchronizationContext.Current;
            }

        /// <summary>
        /// Gets a value indicating whether or not the pipe server is connected
        /// to a client.
        /// </summary>
        public bool IsConnected => _pipeServer.IsConnected;

        /// <summary>
        /// Disconnects the pipe server from the clients.
        /// </summary>
        public void Disconnect()
            {
            _pipeServer.Disconnect();
            }

        /// <inheritdoc />
        public void Dispose()
            {
            _ctSource?.Dispose();
            _pipeServer?.Dispose();
            }

        /// <summary>
        /// Signals for the pipe server to stop listening for connections.
        /// </summary>
        public void CancelListening()
            {
            _pipeServer.CancelWait();
            }

        /// <summary>
        /// Signals for the pipe server to start asynchronously listening for connections.
        /// </summary>
        public void StartListening()
            {
            _pipeServer.BeginWait();
            }

        /// <summary>
        /// Signals for the pipe server to start asynchronously reading messages from the stream.
        /// </summary>
        public void StartReading()
            {
            Task.Run(ReadLoop, _ctSource.Token);
            }

        /// <summary>
        /// Signals for the pipe server to stop reading messages from the stream.
        /// </summary>
        public void StopReading()
            {
            _ctSource.Cancel();
            }

        /// <summary>
        /// The message read loop.
        /// </summary>
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

        /// <summary>
        /// Invoked when the pipe server has received a connection to a client.
        /// </summary>
        public event EventHandler Connected;
        
        /// <summary>
        /// Invoked when a message is received through the pipe stream.
        /// </summary>
        public event EventHandler<string> MessageReceived;

        /// <inheritdoc />
        /// <summary>
        /// Wraps a <see cref="NamedPipeServerStream" /> and provides functions
        /// to wait for connections.
        /// </summary>
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

            /// <summary>
            /// Gets a value indicating whether or not the pipe server is connected
            /// to a client.
            /// </summary>
            public bool IsConnected => _stream.IsConnected;
            
            /// <summary>
            /// Gets a stream object from which messages can be read.
            /// </summary>
            public Stream Stream => _stream;

            /// <summary>
            /// Disconnects clients from the pipe server.
            /// </summary>
            public void Disconnect()
                {
                _stream.Disconnect();
                }

            /// <inheritdoc />
            public void Dispose()
                {
                _stream?.Dispose();
                }

            /// <summary>
            /// Begins an asynchronous operation to wait for a connection.
            /// </summary>
            public void BeginWait()
                {
                //begin a wait operation
                //WaitCallback will be called when the operation is completed
                _waitOperation =
                    _stream.BeginWaitForConnection(
                        WaitCallback,
                        null);
                }

            /// <summary>
            /// Cancels a connection wait operation.
            /// </summary>
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
                    //attempt to end the wait operation and invoke the owner's Connected event
                    _stream.EndWaitForConnection(result);
                    _owner.InvokeConnected(_owner, EventArgs.Empty);
                    } catch (ObjectDisposedException) { }
                }
        }
    }
}