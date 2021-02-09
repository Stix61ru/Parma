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
    }
}
