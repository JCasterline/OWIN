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
    }
}