using System;
using System.Threading.Tasks;
using System.Web.Http;
using PhotoAward.MemberManagement.Interfaces;
using PhotoAward.PhotoManagement.Interfaces;

namespace PhotoAward.AdministrationPlatform.Controllers
{
    
    [RoutePrefix("api/Administration")]
    public class AdministrationController : ApiController
    {
        private readonly IMemberManagementClientFactory _memberManagementClientFactory;
        private readonly IPhotoManagementClientFactory _photoManagementClientFactory;

        public AdministrationController(IMemberManagementClientFactory memberManagementClientFactory, IPhotoManagementClientFactory photoManagementClientFactory)
        {
            _memberManagementClientFactory = memberManagementClientFactory;
            _photoManagementClientFactory = photoManagementClientFactory;
        }

        [HttpGet]
        public string Ping()
        {
            return "Hello";
        }

        
        [HttpGet]
        [Route("Backup/{backupset}")]
        public async Task<string> Backup(string backupset)
        {
            var taskBackupPhoto = this._photoManagementClientFactory.TakeFullBackUpAsync(backupset);
            await this._memberManagementClientFactory.TakeFullBackUpAsync(backupset);
            await taskBackupPhoto;
            return "Backup gestarted ...";
        }

    
        [HttpGet]
        [Route("Restore/{backupset}")]
        public async Task<string> Restore(string backupset)
        {
            var taskRestorePhoto = this._photoManagementClientFactory.RestoreFullBackup(backupset);
            await this._memberManagementClientFactory.RestoreBackupAsync(backupset);
            await taskRestorePhoto;
            return "Restore gestartet ...";

        }
    }
}
