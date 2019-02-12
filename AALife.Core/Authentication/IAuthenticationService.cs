using AALife.Core.Domain;
using AALife.Core.Domain.Customers;

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
        /// <param name="customer">Customer</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        void SignIn(Customer customer, bool createPersistentCookie, bool isApi = false);

        /// <summary>
        /// Sign out
        /// </summary>
        void SignOut();

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        Customer GetAuthenticatedCustomer();

        /// <summary>
        /// Get authenticated customer by ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        Customer GetAuthenticatedCustomerByTicket(string ticket);

        string GetTicket();
    }
}