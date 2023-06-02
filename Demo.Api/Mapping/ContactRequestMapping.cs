using Demo.Api.InputModels;
using Demo.Data.Models;

namespace Demo.Api.Mapping
{
    internal static class ContactRequestMapping
    {
        public static ContactRequest ToEntity(this ContactRequestInputModel contactRequestInputModel)
        {
            return new ContactRequest
            {
                 Content = contactRequestInputModel.Content,
                 Email = contactRequestInputModel.Email,
                 IPAddress = contactRequestInputModel.IPAddress,
                 PhoneNumber = contactRequestInputModel.PhoneNumber,
                 UserFullName = contactRequestInputModel.UserFullName
            };
        }
    }
}
