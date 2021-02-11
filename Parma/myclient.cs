using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Parma
{
    public class myclient
    {
        public const int Web_ERROR_UNKNOWN_ERROR = 0;
        public const int Web_ERROR_HOST_NOT_FOUND = -1;
        public const int Web_ERROR_CANT_CONNECT = -2;
        public const int Web_ERROR_UNAVAILABLE = -3;
        public const int Web_ERROR_UNKNOWN_CODE = -4;

        // конструктор по умолчанию будет задавать порт
        public HttpClient()
        {
            Port = 80;
        }

        //https://github.com/Stix61ru/Parma.git

        //пробую русские коммиты
        // Здесь будем хранить загруженную страницу
        StringBuilder pageContent = null;
        public StringBuilder PageContent
        {
            get { return pageContent; }
        }
        // хотя порт можно взять из URL, я завел переменную
        int Port { get; set; }

        // метод возвращает статус страницы
        public int GetPageStatus(Uri url)
        {
            IPAddress address = GetAddress(url.Host);
            if (address ==null )
            {
                return Web_ERROR_HOST_NOT_FOUND;
            }
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            EndPoint endPoint = new IPEndPoint(address, Port);
            try
            {
                socket.Connect(endPoint);
            }
            catch (Exception)
            {
                return Web_ERROR_CANT_CONNECT;
            }
            string command = GetCommand(url);
            Byte[] bytesSent = Encoding.ASCII.GetBytes(command.Substring(1, command.Length - 1) + "\r\n");
            socket.Send(bytesSent);
            byte[] buffer = new byte[1024];
            int readBytes;
            int result = Web_ERROR_UNAVAILABLE;
            pageContent = null;

            while ((readBytes=socket.Receive(buffer))>0)
            {
                string resultStr = System.Text.Encoding.ASCII.GetString(buffer, 0, readBytes);
                if (pageContent==null)
                {
                    string statusStr = resultStr.Remove(0, resultStr.IndexOf(' ') + 1);

                    try
                    {
                        result = Convert.ToInt32(statusStr.Substring(0, 3));
                    }
                    catch(Exception)
                    {
                        result = Web_ERROR_UNKNOWN_CODE;
                    }
                    pageContent = new StringBuilder();
                }
                pageContent.Append(resultStr);
            }
            socket.Close();
            return result;
        }
        // маленький метод для формирования HTTP-запроса
        protected string GetCommand(Uri url)
        {
            string command = "GET " + url.PathAndQuery + " HTTP/1.1\r\n";
            command += "Host: " + url.Host + "\r\n";
            command += "Usre-Agent: CyD Network Utilites\r\n";
            command += "Accept: */* \r\n";
            command += "Accept-Language: en-us \r\n";
            command += "Accept-Encoding: gzip, deflate \r\n";
            command += "\r\n";
            return command;
        }
    }
}
