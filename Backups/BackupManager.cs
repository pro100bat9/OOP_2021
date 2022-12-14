using System;
using System.Collections.Generic;

namespace Backups
{
    public class BackupManager
    {
        public BackupManager()
        {
            BackupJob = new Backupjob();
            ZipId = Guid.NewGuid().ToString();
        }

        public Backupjob BackupJob { get; }
        protected string ZipId { get; }

        public List<JobObject> AddJobObject(string path, string name)
        {
            return BackupJob.AddJobObject(path, name);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
             BackupJob.RemoveJobObject(jobObject);
        }

        public RestorePoint CreateBackup(IAlgorithm algorithm, string path, List<JobObject> jobObjects, IRepository repository, DateTime dateTime)
        {
            var restorePoint = new RestorePoint(algorithm, dateTime, path);
            repository.CreateStorageZip(jobObjects, algorithm, path, ZipId, BackupJob, dateTime, restorePoint);
            BackupJob.RestorePoints.Add(restorePoint);
            return restorePoint;
        }

        public Backupjob GetBackupjob()
        {
            return BackupJob;
        }
    }
}