using System.Threading.Tasks;
using System.Web.Http;
using PhotoAward.MemberManagement.Interfaces;

namespace PhotoAward.Platform.Controller
{
    [RoutePrefix("api/Member")]
    public class MemberManagementController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        public async Task<MemberDto> Add(MemberDto member)
        {
            var client = MemberManagementClientFactory.CreateMemberManagementClient();
            var result = await client.AddMember(member);
            return result;
        }

        [HttpGet]
        [Route("Get/{email}")]
        public async Task<MemberDto> Get(string email)
        {
            var client = MemberManagementClientFactory.CreateMemberManagementClient();
            var result = await client.GetMember(email);
            return result;
        }


    }
}