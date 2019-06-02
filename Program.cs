using System;
using System.IO.Pipes;
using System.Threading;
using System.Windows.Forms;
using MouseNet.Logophi.Utilities;

namespace MouseNet.Logophi {
    internal static class Program {
        private static AppContext _app;
        private static MessageReceiver _messageReceiver;

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
            //show the main window and reset the message receiver
            //to continue listening for new instances of Logophi
            _app.PresentMainForm();
            if (_messageReceiver.IsConnected)
                _messageReceiver.Disconnect();
            _messageReceiver.StartListening();
            }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
            {
            //use a Mutex to see if the program is already running
            //if so, send a message to the other instance to tell it
            //to show the main form
            //otherwise run the program
            using (new Mutex(true, "LogophiMtx", out var createdNew))
                {
                if (createdNew) Run();
                else SendMessage();
                }
            _messageReceiver.Dispose();
            }

        /// <summary>
        ///     Runs the main Logophi application.
        /// </summary>
        private static void Run()
            {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnApplicationExit;
            _app = new AppContext();
            //create a new message receiver to listen for other instances
            //of Logophi
            _messageReceiver =
                new MessageReceiver("LogophiMessageReceiver");
            _messageReceiver.Connected += OnMessageReceiverConnected;
            _messageReceiver.StartListening();
            Application.Run(_app);
            }

        /// <summary>
        ///     Uses a pipe client to try to connect to a running
        ///     instance of Logophi, signaling that it sould display
        ///     its main window.
        /// </summary>
        private static void SendMessage()
            {
            using (var pipeClient =
                new NamedPipeClientStream(
                    ".",
                    "LogophiMessageReceiver",
                    PipeDirection.Out,
                    PipeOptions.Asynchronous))
                {
                pipeClient.Connect();
                }
            }
    }
}