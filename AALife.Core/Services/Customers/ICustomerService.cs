using AALife.Core.Domain.Customers;

namespace AALife.Core.Services
{
    public partial interface ICustomerService :IBaseService<Customer>
    {
        Customer GetCustomerByUsername(string userName);

        Customer Login(string userName, string userPassword);

        void ResetPassword(string userName, string userPassword);
    }
}
