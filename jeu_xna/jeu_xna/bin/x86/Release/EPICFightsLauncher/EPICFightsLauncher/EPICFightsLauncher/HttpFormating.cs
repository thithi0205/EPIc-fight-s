using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPICFightsLauncher
{
    public static class HttpFormating
    {
        public static string Convert(string result)
        {
            string resultfinal = "";

            for (int i = result.Length - 1; (result[i] != '!' || i < 0); i--)
            {
                resultfinal = result[i] + resultfinal;
            }

            return resultfinal;
        }

    }
}
