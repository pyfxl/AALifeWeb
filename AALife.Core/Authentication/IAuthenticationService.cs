using AALife.Core.Domain;
using AALife.Core.Domain.Users;

namespace AALife.Core.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService 
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        void SignIn(UserTable user, bool createPersistentCookie, bool isApi = false);

        /// <summary>
        /// Sign out
        /// </summary>
        void SignOut();

        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <returns>User</returns>
        UserTable GetAuthenticatedUser();

        /// <summary>
        /// Get authenticated user by ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        UserTable GetAuthenticatedUserByTicket(string ticket);

        string GetTicket();
    }
}