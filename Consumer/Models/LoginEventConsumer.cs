using MailKit.Net.Smtp;
using MassTransit;
using MimeKit;

namespace Consumer.Models;

public class LoginEventConsumer : IConsumer<LoginEvent>
{

    public async Task Consume(ConsumeContext<LoginEvent> context)
    {
        var email = context.Message.Email;
        var code = context.Message.Code;

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Mahammad", "mustafamehmed1251@gmail.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Your Verification Code";
        message.Body = new TextPart("plain")
        {
            Text = $"Your verification code is: {code}"
        };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, false);
        await client.AuthenticateAsync("mustafamehmed1251@gmail.com", "A1251n?mz");
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}
