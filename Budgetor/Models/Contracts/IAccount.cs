using Budgetor.Models.Accounts;

namespace Budgetor.Models.Contracts
{
    public interface IAccountDetail
    {
        AccountDetailVM MapToAccountDetailVM();
    }
}
