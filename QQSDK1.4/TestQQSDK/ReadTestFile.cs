using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace TestQQSDK
{
    public class ReadTestFile
    {

        public static string Read(string name)
        {
            string fileContents=string.Empty  ;
            string path = Environment.CurrentDirectory;
            int index = path.IndexOf("TestResults");
            if (index > -1)
            {
                path = path.Substring(0, index);
                path = string.Format("{0}TestResults\\File\\{1}", path,name);
                using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
                {
                    fileContents = sr.ReadToEnd();
                }
            }
            return fileContents;
        }

        public static string GetLogin1()
        {
            return Read("login1.txt");
        }
        public static string GetLogin2()
        {
            return Read("login2.txt");
        }
        public static string GetPollKickmessage()
        {
            return Read("PollKickmessage.txt");
        }

        public static string GetPollMessage()
        {
            return Read("PollMessage.txt");
        }

        public static string GetPollTips()
        {
            return Read("PollTipse.txt");
        }
    }
}
