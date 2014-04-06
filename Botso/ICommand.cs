using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Hipchat.Models;

namespace Botso
{
    public interface ICommand
    {
        string Pattern { get; }
        void Execute();
        SortedList<int, string> Arguments { get; set; }
        HipchatMessage OriginalMessage { get; set; }
    }
}