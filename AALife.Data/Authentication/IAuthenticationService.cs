using AALife.Data.Domain;

namespace AALife.Data.Authentication
{
    /// <summary>
    /// Authentication service interface
    /// </summary>
    public partial interface IAuthenticationService 
    {
        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        void SignIn(UserTable user, bool createPersistentCookie, bool isApi = false);

        /// <summary>
        /// Sign out
        /// </summary>
        void SignOut();

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        UserTable GetAuthenticatedUser();

        /// <summary>
        /// Get authenticated customer by ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        UserTable GetAuthenticatedUserByTicket(string ticket);

        string GetTicket();
    }
}