using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        private BackupManager _backupManager;

        [SetUp]
        public void Setup()
        {
            _backupManager = new BackupManager();
        }

        [Test]
        public void CreateSplitStorages()
        {
            var repository = new VirtualRepository();
           
            var jobObject = new JobObject(@"../../../Files/FileB", "FileB");
            List<JobObject> jobObjects1 = _backupManager.AddJobObject(@"../../../Files/FileA", "FileA");
            jobObjects1.Add(jobObject);
            IAlgorithm split = new SplitAlgorithm();
            DateTime dateTime = DateTime.Now;
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            _backupManager.RemoveJobObject(jobObject);
            jobObjects1.Remove(jobObject);
            _backupManager.CreateBackup(split, @"../../../BackupFiles/", jobObjects1, repository, dateTime);
            Backupjob backupjob = _backupManager.GetBackupJob();
            int storageCount = 0;
            foreach (RestorePoint restorePoint in backupjob.RestorePoints)
            {
                storageCount += restorePoint.ListStorages.Count;
            }
            Assert.AreEqual(storageCount, 3);
            Assert.AreEqual(_backupManager.GetBackupJob().RestorePoints.Count, 2);
        }
    [Test]
    public void CreateSingleStorages()
    {
        var repository = new VirtualRepository();
        var jobObject = new JobObject(@"../../../Files/FileA", "FileA");
        List<JobObject> jobObjects = _backupManager.AddJobObject(@"../../../Files/FileB", "FileA");
        jobObjects.Add(jobObject);
        IAlgorithm single = new SingleAlgorithm();
        DateTime dateTime = DateTime.Now;
        _backupManager.CreateBackup(single, @"../../../BackupFiles/Single", jobObjects, repository, dateTime);
        Assert.AreEqual(_backupManager.GetBackupJob().RestorePoints.Count, 1);
    }
     }
}