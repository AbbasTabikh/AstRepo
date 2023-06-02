
using Demo.Api.InputModels;
using Demo.Api.Mapping;
using Demo.Data.Data;
using Demo.Data.Models;

namespace Demo.Api.Services
{
    public class ContactRequestService : IContactRequestService
    {
        private readonly DataContext _contactRepository;

        public ContactRequestService(DataContext contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactRequest> Create(ContactRequestInputModel contactRequestInputModel, CancellationToken token)
        {
            var contactRequest = contactRequestInputModel.ToEntity();
            var result = await _contactRepository.ContactRequests.AddAsync(contactRequest , token);
            return contactRequest;
        }

        public async System.Threading.Tasks.Task Save(CancellationToken token)
        {
            await _contactRepository.SaveChangesAsync();
        }
    }
}
