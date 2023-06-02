using Demo.Api.InputModels;
using Demo.Api.Services;
using Demo.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactRequestController : ControllerBase
    {
        private readonly IContactRequestService _contactRequestService;

        public ContactRequestController(IContactRequestService contactRequestService)
        {
            _contactRequestService = contactRequestService;
        }

        [HttpPost]
        public async Task<ActionResult<ContactRequest>> Create(ContactRequestInputModel contactRequestInputModel , CancellationToken token)
        {
            var contactRequest = await _contactRequestService.Create(contactRequestInputModel, token);
            await _contactRequestService.Save(token);
            return Ok(contactRequest);
        }
    }
}
