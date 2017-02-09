using System.Threading.Tasks;
using System.Web.Http;
using PhotoAward.MemberManagement.Interfaces;

namespace PhotoAward.Platform.Controller
{
    [RoutePrefix("api/Member")]
    public class MemberManagementController : ApiController
    {
        private readonly IMemberManagementClientFactory _memberManagementClientFactory;

        public MemberManagementController(IMemberManagementClientFactory memberManagementClientFactory)
        {
            _memberManagementClientFactory = memberManagementClientFactory;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<MemberDto> Add(MemberDto member)
        {
            var client = this._memberManagementClientFactory.CreateMemberManagementClient();
            var result = await client.AddMember(member);
            return result;
        }

        [HttpGet]
        [Route("Get/{email}")]
        public async Task<MemberDto> Get(string email)
        {
            var client = this._memberManagementClientFactory.CreateMemberManagementClient();
            var result = await client.GetMember(email);
            return result;
        }


    }
}