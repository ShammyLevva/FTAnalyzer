using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer.Events
{
    public class GoogleWaitingEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public GoogleWaitingEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
