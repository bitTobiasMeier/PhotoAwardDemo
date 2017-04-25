using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace PhotoAward.MemberManagement.Interfaces
{
    public interface IMemberManagement: IService
    {
        Task<MemberDto> AddMember(MemberDto member);

        Task<MemberDto> GetMember(string email);
        Task<MemberDto> LoginMember(string email, string password);
        Task<MemberDto> GetMemberOnMemberId(Guid memberId);
        Task<MemberDto> ChangePassword(ChangePasswordDto dto);
    }
}
