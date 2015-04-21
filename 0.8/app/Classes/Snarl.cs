using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace TheSauceStation
{
    public class Snarl
    {
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string strClassName, string nptWindowName);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCTL lParam);

        const int WM_USER = 0x400;
        const int WM_COPYDATA = 0x4A;

        struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        struct COPYDATASTRUCTL
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData; 
        }


        public static string StripHTML(string str)
        {
            // Remove new lines since they are not visible in HTML
            str = str.Replace("\n", " ");
 
            // Remove tab spaces
            str = str.Replace("\t", " ");
 
            // Remove multiple white spaces from HTML
            str = Regex.Replace(str, "\\s+", " ");
 
            // Remove HEAD tag
            str = Regex.Replace(str, "<head.*?</head>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
 
            // Remove any JavaScript
            str = Regex.Replace(str, "<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
 
            // Replace special characters like &, <, >, " etc.
            StringBuilder sbHTML = new StringBuilder(str);
            // Note: There are many more special characters, these are just
            // most common. You can add new characters in this arrays if needed
            string[] OldWords = {"&nbsp;", "&amp;", "&quot;", "&lt;", "&gt;", "&reg;", "&copy;", "&bull;", "&trade;"};
            string[] NewWords = {" ", "&", "\"", "<", ">", "®", "©", "", ""};

            for(int i = 0; i < OldWords.Length; i++)
            {
              sbHTML.Replace(OldWords[i], NewWords[i]);
            }
 
            // Check if there are line breaks (<br>) or paragraph (<p>)
            sbHTML.Replace("<br>", "\n<br>");
            sbHTML.Replace("<br ", "\n<br ");
            sbHTML.Replace("<p ", "\n<p ");
 
            // Finally, remove all HTML tags and return plain text
            return System.Text.RegularExpressions.Regex.Replace(sbHTML.ToString(), "<[^>]*>", "");
        }


        public static string stringFromDictionary(Dictionary<string, string> dictionary)
        {
            string reply = "";
            if (dictionary != null)
            {
                if (dictionary.Count > 0)
                {
                    reply = "&";
                    foreach (KeyValuePair<string, string> entry in dictionary)
                    {
                        reply = reply + entry.Key + "=" + SnarlEncode(entry.Value) + "&";
                    }
                    reply = reply.TrimEnd('&');
                }
            }
            return reply;
        }


        public static string SnarlEncode(string str)
        {
            if (str != null && str != "")
            {
                string newStr = str.Replace("&", "&&");
                newStr = newStr.Replace("=", "==");
                newStr = newStr.Replace("::", ":");             // bug in Snarl code
                return newStr;
            }
            else
            {
                return "";
            }
        }

        public static Int32 NewDoRequest(string queryString)
        {
            IntPtr hWnd = IntPtr.Zero;
            hWnd = FindWindow("w>Snarl", "Snarl");
            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("Snarl not running");
                //snDoRequest = -SNARL_ERROR_NOT_RUNNING
                return 0;
            }
            else
            {
                Console.WriteLine("Snarl found");

                byte[] bRequest = new UTF8Encoding().GetBytes(queryString);
                int hr = 0;
                COPYDATASTRUCTL cds;
                cds.dwData = (IntPtr)0x534E4C03;        // use SNL,3 for now; swap to SNL,4 later
                cds.cbData = bRequest.Length;
                cds.lpData = Marshal.AllocHGlobal(bRequest.Length);
                Marshal.Copy(bRequest, 0, cds.lpData, bRequest.Length);
                hr = SendMessage(hWnd, WM_COPYDATA, Process.GetCurrentProcess().Id, ref cds);
                Marshal.FreeHGlobal(cds.lpData);
                Console.WriteLine("SendMessage() returned " + hr);
                return hr;
            }
        }



        public static Int32 DoRequest(string Request)
        {
            IntPtr hWnd = IntPtr.Zero;
            hWnd = FindWindow("w>Snarl", "Snarl");
            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("Snarl not running");
                //snDoRequest = -SNARL_ERROR_NOT_RUNNING
                return 0;
            }
            else
            {
                Console.WriteLine("Snarl found");

                int hr = 0;

                byte[] sarr = System.Text.Encoding.Default.GetBytes(Request);
                int len = sarr.Length;
                COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)0x534E4C03;        // use SNL,3 for now; swap to SNL,4 later
                cds.lpData = Request;
                cds.cbData = len + 1;

                hr = SendMessage(hWnd, WM_COPYDATA, Process.GetCurrentProcess().Id, ref cds);
                Console.WriteLine("SendMessage() returned " + hr);
                return hr;
            }
        }
    }
}

