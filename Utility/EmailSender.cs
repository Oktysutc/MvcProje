using Microsoft.AspNetCore.Identity.UI.Services;

namespace MvcProje.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // sizler buraya mail gonderme işlemlerinizi ekleyebilirsiniz
            return Task.CompletedTask;
        }
    }
}
