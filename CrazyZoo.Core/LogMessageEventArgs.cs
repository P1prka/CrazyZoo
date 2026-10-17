using System;

namespace CrazyZoo.Core;

public class LogMessageEventArgs : EventArgs
{
    public string Message { get; }

    public LogMessageEventArgs(string message)
    {
        Message = message;
    }
}

