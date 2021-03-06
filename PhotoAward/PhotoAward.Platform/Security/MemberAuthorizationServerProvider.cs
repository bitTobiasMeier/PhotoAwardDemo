﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using PhotoAward.MemberManagement.Interfaces;

namespace PhotoAward.Platform.Security
{
    public class MemberAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IMemberManagementClientFactory _memberManagementClientFactory;

        public MemberAuthorizationServerProvider(IMemberManagementClientFactory memberManagementClientFactory)
        {
            _memberManagementClientFactory = memberManagementClientFactory;
        }
        

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Proxy zum MemberManamgement-Service erzeugen
            var client= await this._memberManagementClientFactory.CreateMemberManagementClientAsync();
            //Login durchführen
            var member =  await client.LoginMember(context.UserName, context.Password);
            if (member != null)
            {
                //Identität erzeugen und claims hinzufügen
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("username", member.Email));
                identity.AddClaim(new Claim(ClaimTypes.Name, member.FirstName + " " + member.Surname));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, member.FirstName ));
                identity.AddClaim(new Claim(ClaimTypes.Surname, member.Surname));
                identity.AddClaim(new Claim(ClaimTypes.Email, member.Email));
                //und validieren
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "The given combination of username and password is incorrect");
            }
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
    }
}
