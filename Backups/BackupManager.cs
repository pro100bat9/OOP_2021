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

        private string ZipId { get; }

        private Backupjob BackupJob { get; }

        public List<JobObject> AddJobObject(string path, string name)
        {
            var jobObject = new JobObject(path, name);
            BackupJob.JobObjects.Add(jobObject);
            return BackupJob.JobObjects;
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            BackupJob.JobObjects.Remove(jobObject);
        }

        public void CreateBackup(IAlgorithm algorithm, string path, List<JobObject> jobObjects, IRepository repository, DateTime dateTime)
        {
            var restorePoint = new RestorePoint(algorithm, dateTime, path);
            repository.CreateStorageZip(jobObjects, algorithm, path, ZipId, BackupJob, dateTime, restorePoint);
            BackupJob.RestorePoints.Add(restorePoint);
        }

        public Backupjob GetBackupjob()
        {
            return BackupJob;
        }
    }
}