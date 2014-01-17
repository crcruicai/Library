using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC.Files
{
    public class FileDatabaseException : Exception
    {
        private string _P;

        

        public FileDatabaseException(string message,Exception e, string p):base(message ,e)
        {
            this._P = p;


        }

        public FileDatabaseException(string message, string p):base(message)
        {
            this._P = p;

        }


    }
}
