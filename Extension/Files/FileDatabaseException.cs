using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC.Files
{
    public class FileDatabaseException : Exception
    {
        private string p;

        

        public FileDatabaseException(string message,Exception e):base(message ,e)
        {
           

        }

        public FileDatabaseException(string message):base(message)
        {
          
        }

        
    }
}
