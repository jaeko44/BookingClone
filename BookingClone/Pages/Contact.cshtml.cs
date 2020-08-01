using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingClone.Pages
{
    public class ContactModel : PageModel
    {

        [BindProperty]
        public ContactFormModel Contact { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your contact page.";
        }

        public IActionResult OnPost()
        {
            string title = $@"New email inquiry from: {Contact.Name} for BookingInternational";
            var mailbody = $@"Haelo website owner, This is a new contact request from your website: Name: {Contact.Name} Phone Number: {Contact.PhoneNumber} Email: {Contact.Email} Message: ""{Contact.Message}"" Cheers, The websites contact form";
            SendMail(mailbody, title);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("Contact");
        }

        private void SendMail(string mailbody, string title)
        {

            using (var message = new MailMessage(Contact.Email, "me@mydomain.com"))
            {
                message.To.Add(new MailAddress("me@mydomain.com"));
                message.From = new MailAddress(Contact.Email);
                message.Subject = title;
                message.Body = mailbody;
                using (var smtpClient = new SmtpClient("mail.mydomain.com"))
                {
                    smtpClient.Send(message);
                }
            }
        }
    }
}
