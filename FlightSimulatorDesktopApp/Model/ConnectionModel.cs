using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.Model
{
    public interface IConnectionModel : INotifyPropertyChanged
    {
        // Properties.
        public string IPAddr { get; }
        public int Port { get; }
        public string ConnectionStatus { get; }

        // Connection methods.
        public void connect(string ip, int port);
        public void disconnect();
        public void write(string command);
        public string read(); // blocking call
                
    }

    public class ConnectionModel : IConnectionModel
    {
        // Privates.
        private Socket sender;
        private readonly Mutex mutex = new Mutex();
        private string m_ip;
        private int m_port;
        private string status;

        // Norifier.
        public event PropertyChangedEventHandler PropertyChanged;

        public ConnectionModel()
        {
            status = "Disconnected";
        }

        // IP, Port and status properties.
        public string IPAddr { get => m_ip; }
        public int Port { get => m_port; }
        public string ConnectionStatus { get => status; }

        // Notification method.
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        // Connection method.
        public void connect(string ip, int port)
        {
            if (!status.Equals("Disconnected"))
            {
                disconnect();
            }

            try
            {
                // Parse the given IP.
                IPAddress givenIP = IPAddress.Parse(ip);
                IPEndPoint enpoint = new IPEndPoint(givenIP, port);

                // Create a TCP/IP socket.  
                sender = new Socket(givenIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Set timeout for synchronous receive methods to 10 seconds
                sender.ReceiveTimeout = 10000;

                // Connect the socket to the remote endpoint.
                sender.Connect(enpoint);

                // Update IP and Port privates and connection status (isConnected).
                m_ip = ip;
                m_port = port;
                status = "Connected to IP " + ip + " in port " + port + "."; 

                // Notify change.
                NotifyPropertyChanged("ConnectionStatus");

            }
            catch (Exception)
            {
                disconnect();
            }
        }

        // Disconnection method.
        public void disconnect()
        {
            if (status.Equals("Disconnected"))
                return;

            try
            {
                sender.Shutdown(SocketShutdown.Both);
            }
            catch (ArgumentNullException) { }
            catch (SocketException) { }
            catch (ObjectDisposedException) { }
            catch (Exception) { }
            finally
            {
                sender.Close();
                status = "Disconnected";
            }
        }

        // Read (recieve) method. Blocking call feature implemented by mutex.
        public string read()
        {
            try
            {
                mutex.WaitOne();
                byte[] bytes = new byte[1024];
                int recieve = sender.Receive(bytes);
                string str = Encoding.ASCII.GetString(bytes, 0, recieve);
                mutex.ReleaseMutex();
                return str;
            }
            catch (ArgumentNullException)
            {
                mutex.ReleaseMutex();
                return "Error";
            }
            catch (SocketException)
            {
                mutex.ReleaseMutex();
                return "Error";
            }
            catch (ObjectDisposedException)
            {
                mutex.ReleaseMutex();
                return "Error";
            }
            catch (Exception)
            {
                mutex.ReleaseMutex();
                return "Error";
            }
        }

        // Write (send) method.
        public void write(string command)
        {
            try
            {
                // Encode the data string into a byte array and sent it via the socket.  
                mutex.WaitOne();
                byte[] msg = Encoding.ASCII.GetBytes(command + "\n");
                sender.Send(msg);
            }
            catch (ArgumentNullException) { }
            catch (SocketException) { }
            catch (ObjectDisposedException) { }
            catch (Exception) { }
            finally { mutex.ReleaseMutex(); }
        }
    }

}
