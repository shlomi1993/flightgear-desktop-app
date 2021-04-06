using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorDesktopApp.Model
{
    public interface ITelnetClient
    {
        //public string IPAddr { get; set; }
        //public int Port { get; set; }
        void connect(string ip, int port);
        void write(string command);
        string read(); // blocking call
        void disconnect();
    }
    public class TelnetClient : ITelnetClient
    {
        private Socket sender;
        private readonly Mutex mutex = new Mutex();

        //private string ipAddress;
        //private int port;

        //public string IPAddr
        //{
        //    get => ipAddress;
        //    set
        //    {
        //        if (ipAddress != value)
        //        {
        //            ipAddress = value;
        //        }
        //    }
        //}

        //public int Port
        //{
        //    get => port;
        //    set
        //    {
        //        if (port != value)
        //        {
        //            port = value;
        //        }
        //    }
        //}

        public void connect(string ip, int port)
        {
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
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void disconnect()
        {
            try { sender.Shutdown(SocketShutdown.Both); }
            catch (ArgumentNullException) { }
            catch (SocketException) { }
            catch (ObjectDisposedException) { }
            catch (Exception) { }
            finally { sender.Close(); }
        }

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
