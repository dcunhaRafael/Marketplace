using InsuranceApi.Domain.Interfaces.InfraIdentity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace InsuranceApi.Infra.CrossCutting.Identity.Extensions {
    public class AspNetUser : IUser {

        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor) {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetUserId() {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string GetUserEmail() {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public string GetUserName() {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserName() : "";
        }

        public int GetExternalId() {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetExtenalID() : 0;
        }

        public bool IsAuthenticated() {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role) {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> GetClaimsIdentity() {
            return _accessor.HttpContext.User.Claims;
        }
    }

    public static class ClaimsPrincipalExtensions {
        public static string GetUserId(this ClaimsPrincipal principal) {
            if (principal == null) {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal) {
            if (principal == null) {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal) {
            if (principal == null) {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("userName");
            return claim?.Value;
        }

        public static int GetExtenalID(this ClaimsPrincipal principal) {
            if (principal == null) {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("extID");
            return int.Parse(claim?.Value);
        }
    }
}
