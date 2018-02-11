using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    public class MultiProgressBar
    {
        Dictionary<string, ProgressBar> listPB;

        public MultiProgressBar(Dictionary<string, ProgressBar> list)
        {
            listPB = list;
        }

        public void SetValue(string name, int value)
        {
            if (listPB.ContainsKey(name))
                listPB[name].Value = value;
            else
                throw new ArgumentException();
        }
    }
}
