namespace OwinIdentitySqlServerRepository.DataAccess
{
    public static class Statements
    {
        public static string FindUserByLoginInfo
        {
            get
            {
                return
                    "SELECT * FROM Users WHERE Users.Id IN(SELECT UserId FROM UserLogins WHERE LoginProvider=@loginprovider AND ProviderKey=@providerkey)";
            }
        }

        public static string GetUserClaims
        {
            get
            {
                return "SELECT Claims.* FROM Claims, UserClaims WHERE (Claims.Id=UserClaims.ClaimId AND UserClaims.UserId=@userid) " +
                       "OR Claims.Id IN(SELECT ClaimId FROM RoleClaims INNER JOIN Roles ON RoleClaims.RoleId=Roles.Id " +
                       "INNER JOIN UserRoles ON Roles.Id=UserRoles.RoleId WHERE UserId=@userid)";
            }
        }

        public static string RemoveUserClaim
        {
            get
            {
                return
                    "DELETE FROM UserClaims WHERE UserId=@userid AND ClaimId=(SELECT Id FROM Claims WHERE Type=@type AND Value=@value";
            }
        }

        public static string AddUserClaim
        {
            get
            {
                return
                    "INSERT INTO UserClaims(UserId, ClaimId) VALUES(@userid, (SELECT Id FROM Claims WHERE Type=@type AND Value=@value))";
            }
        }

        public static string AddRoleClaim
        {
            get
            {
                return
                    "INSERT INTO RoleClaims(RoleId, ClaimId) VALUES(@roleid, (SELECT Id FROM Claims WHERE Type=@type AND Value=@value))";
            }
        }
    }
}