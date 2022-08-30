using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniTools
{
    public class UniTools
    {
        
        public string UniPathCreate(string paths, string defaultPath)
        {
            string output = "";

            string[] _temp = paths.Split("/");
            try
            {
                for (int i = 0; i < _temp.Length - 2; i++)
                {
                    output += _temp[i];
                    output += "/";
                }
            }
            catch
            {
                output = defaultPath;
            }
            if(output.Length <= 2)
            {
                output = defaultPath;
            }

            return output;
        }
    }



}
