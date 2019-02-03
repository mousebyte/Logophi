using System;
using System.IO.Pipes;
using System.Threading;
using System.Windows.Forms;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi
{
    internal static class Program
    {
        private static MessageReceiver _messageReceiver;
        private static AppContext _app;

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
            {
            using (new Mutex(true, "LogophiMtx", out var createdNew))
                if (createdNew)
                    Run();
                else
                    SendMessage();
            }

        private static void Run()
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnApplicationExit;
            _app = new AppContext();
            _messageReceiver =
                new MessageReceiver("LogophiMessageReceiver");
            _messageReceiver.Connected += OnMessageReceiverConnected;
            _messageReceiver.StartListening();
            Application.Run(_app);
            }

        private static void SendMessage()
            {
            using (var pipeClient =
                new NamedPipeClientStream(
                    ".",
                    "LogophiMessageReceiver",
                    PipeDirection.Out,
                    PipeOptions.Asynchronous))
                pipeClient.Connect();
            }

        private static void OnApplicationExit
            (object sender,
             EventArgs e)
            {
            _messageReceiver?.Dispose();
            }

        private static void OnMessageReceiverConnected
            (object sender,
             EventArgs e)
            {
            _app.Activate();
            if (_messageReceiver.IsConnected)
                _messageReceiver.Disconnect();
            _messageReceiver.StartListening();
            }
    }
}