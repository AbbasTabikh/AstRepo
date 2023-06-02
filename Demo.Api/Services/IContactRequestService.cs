using Demo.Api.InputModels;
using Demo.Data.Models;

namespace Demo.Api.Services
{
    public interface IContactRequestService
    {
        Task<ContactRequest> Create(ContactRequestInputModel contactRequestInputModel , CancellationToken token);
        System.Threading.Tasks.Task Save(CancellationToken token);
    }
}
