namespace DentalClinic.Windows
{
    using System;
    using System.Data.SqlClient;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// 
    /// </summary>
    public class RemotePort
    {
        #region Fields

        private bool connectionStatus;

        private bool smsServiceStatus;

        private ConnectionChangedEventArgs args = new ConnectionChangedEventArgs();

        private IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

        private Ping ping = new Ping();

        private PingReply pingReply;

        private SqlConnection sqlConnection = new SqlConnection();

        private TcpClient tcpClient = new TcpClient();

        private TcpConnectionInformation[] tcpConnInfoArray;

        #endregion // Fields

        #region Properties

        public bool ConnectionStatus
        {
            get { return connectionStatus; }
            set
            {
                connectionStatus = value;
                args.ConnectionStatus = connectionStatus;
                args.TimeOfChange = DateTime.Now;

                OnConnectionChange(args);
            }
        }

        public bool SmsServiceStatus
        {
            get { return smsServiceStatus; }
            set
            {
                smsServiceStatus = value;
                args.ConnectionStatus = smsServiceStatus;
                args.TimeOfChange = DateTime.Now;

                OnSmsServiceStatusChanged(args);
            }
        }

        public event EventHandler<ConnectionChangedEventArgs> ConnectionStatusChanged;

        public event EventHandler<ConnectionChangedEventArgs> SmsServiceStatusChanged;

        #endregion // Properties

        #region Constructors



        #endregion // Constructors

        #region Methods

        public bool CheckRemoteIp(string hostUri)
        {
            pingReply = ping.Send(hostUri);

            if (pingReply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckRemotePort(string hostUri, int portNumber)
        {
            tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            foreach(TcpConnectionInformation tcpi in tcpConnInfoArray)
            {
                if (tcpi.LocalEndPoint.Port == portNumber)
                {
                    return true;
                }
            }

            try
            {
                tcpClient.Connect(hostUri, portNumber);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                tcpClient.Close();
            }

        }

        public async Task<bool> CheckRemotePortAsync(string hostUri, int portNumber, int timeout = 2000)
        {

            var connectionTask = tcpClient.ConnectAsync(hostUri, portNumber).ContinueWith(task =>
            {
                return task.IsFaulted ? null : tcpClient;
            }, TaskContinuationOptions.ExecuteSynchronously);

            var timeoutTask = Task.Delay(timeout)
                                  .ContinueWith<TcpClient>(Task => null, TaskContinuationOptions.ExecuteSynchronously);

            var resultTask = Task.WhenAny(connectionTask, timeoutTask).Unwrap();

            resultTask.Wait();
            var resultTcpClient = await resultTask;

            if (resultTcpClient is null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CheckSqlConnectionAsync(string connectionString, CancellationToken cancellationToken, int timeout)
        {
            sqlConnection.ConnectionString = connectionString;

            var connectionTask = sqlConnection.OpenAsync(cancellationToken).ContinueWith(task =>
            {
                return task.IsFaulted ? null : sqlConnection;
            }, TaskContinuationOptions.ExecuteSynchronously);

            var timeoutTask = Task.Delay(timeout)
                                  .ContinueWith<SqlConnection>(Task => null, TaskContinuationOptions.ExecuteSynchronously);

            var resultTask = Task.WhenAny(connectionTask, timeoutTask).Unwrap();

            resultTask.Wait();

            var resultSqlConnection = await resultTask;

            if (resultSqlConnection is null)
            {
                return false;
            }
            return true;
           
        }

        public async Task StartCheckWithInterval(string hostUri, int portNumber, CancellationToken cancelationToken, int timeout = 1000)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    ConnectionStatus = await CheckRemotePortAsync(hostUri, portNumber, timeout);
                    if (cancelationToken.IsCancellationRequested)
                        break;
                    // To cancel task -> cancelationToken.Cancel;
                }
            });
        }

        public async Task StartCheckSmsServiceIPWithInterval(string hostUri, CancellationToken cancelationToken, int timeout = 4000)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    SmsServiceStatus = CheckRemoteIp(hostUri);

                    await Task.Delay(timeout);

                    if (cancelationToken.IsCancellationRequested)
                        break;
                    // To cancel task -> cancelationToken.Cancel;
                }
            });
        }

        public async Task StartSqlCheckWithInterval(string connectionString, CancellationToken cancelationToken, int delayInMillisecond = 1000)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    
                    ConnectionStatus = CheckDbConnection(connectionString);

                    await Task.Delay(delayInMillisecond);

                    if (cancelationToken.IsCancellationRequested)
                        break;
                    // To cancel task -> cancelationToken.Cancel;
                }
            });
        }

        public bool CheckDbConnection(string connectionString)
        {
            sqlConnection.ConnectionString = connectionString;

            try
            {
                sqlConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        protected virtual void OnConnectionChange(ConnectionChangedEventArgs e)
        {
            EventHandler<ConnectionChangedEventArgs> handler = ConnectionStatusChanged;
            if (!(handler is null))
            {
                handler(this, e);
            }
        }

        protected virtual void OnSmsServiceStatusChanged(ConnectionChangedEventArgs e)
        {
            EventHandler<ConnectionChangedEventArgs> handler = SmsServiceStatusChanged;
            if (!(handler is null))
            {
                handler(this, e);
            }
        }
        #endregion // Methods


    }

    public class ConnectionChangedEventArgs : EventArgs
    {
        public bool ConnectionStatus { get; set; }
        public DateTime TimeOfChange { get; set; }
    }

}
