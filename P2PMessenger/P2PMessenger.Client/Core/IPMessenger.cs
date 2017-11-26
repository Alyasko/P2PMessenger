namespace P2PMessenger.Client.Core
{
    interface IPMessenger
    {
        void Connect(string client);
        void StartServer();
        string DownloadHistory();
        string Read();
        void Send(string message);
        string GetMyIp();

        bool IsConnected { get; }
    }
}