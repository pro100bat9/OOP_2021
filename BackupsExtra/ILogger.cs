using System.Collections.Generic;
using Backups;

namespace BackupsExtra
{
    public interface ILogger
    {
         void Notify(string path, RestorePoint restorePoint);
    }
}