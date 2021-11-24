using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace InsuranceApi.Domain.Interfaces.InfraIdentity {
    public interface IUser {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserName();
        int GetExternalId();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
