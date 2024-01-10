using EmployeeManagement.Models.Domain;

namespace EmployeeManagement.Models.Repositories.Abstract
{
    public interface IUserAuthentication
    {
        bool Login(string email, string password, out Employee? user);

    }
}
