using System;
using System.Threading.Tasks;
using System.Web.Http;
using PhotoAward.MemberManagement.Interfaces;

namespace PhotoAward.Platform.Controller
{
    [Authorize]
    [RoutePrefix("api/Member")]
    public class MemberManagementController : ApiController
    {
        private readonly IMemberManagementClientFactory _memberManagementClientFactory;

        public MemberManagementController(IMemberManagementClientFactory memberManagementClientFactory)
        {
            _memberManagementClientFactory = memberManagementClientFactory;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Add")]
        public async Task<MemberDto> Add(MemberDto member)
        {
            var client = await this._memberManagementClientFactory.CreateMemberManagementClientAsync();
            var result = await client.AddMember(member);
            return result;
        }

        [HttpGet]
        [Route("Get/{email}")]
        public async Task<MemberDto> Get(string email)
        {
            var client = await this._memberManagementClientFactory.CreateMemberManagementClientAsync();
            var result = await client.GetMember(email);
            return result;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<MemberDto> Login(string email, string password)
        {
            var client = await this._memberManagementClientFactory.CreateMemberManagementClientAsync();
            var result = await client.LoginMember(email, password);
            return result;
        }

        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<MemberDto> ChangePassword (ChangePasswordDto dto)

        {
            var client = await this._memberManagementClientFactory.CreateMemberManagementClientAsync();
            var result = await client.ChangePassword(dto);
            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Backup")]
        public async Task<string> Backup()
        {
            var nameOfBackupSet = "test";
            await this._memberManagementClientFactory.TakeFullBackUpAsync(nameOfBackupSet);
            return "Backup erfolgt";
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Restore")]
        public async Task<string> Restore()
        {
            var nameOfBackupSet = "test";
            await this._memberManagementClientFactory.RestoreBackupAsync(nameOfBackupSet);
            return "Restore erfolgt";
            
        }

      
    }
}