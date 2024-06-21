using bShop.Server.Data.Entities.Users;
using MailKit.Net.Smtp;
using MimeKit;

namespace bShop.Server.Services;


public interface IEmailSender
{
    Task SendConfirmationLinkAsync(ShopUser user, string email, string confirmationLink);
    Task SendPasswordResetCodeAsync(ShopUser user, string email, string resetCode);
    Task SendPasswordResetLinkAsync(ShopUser user, string email, string resetLink);
    Task SendPasswordResettedAsync(ShopUser user, string link);
    Task SendPasswordChangedAsync(ShopUser user, string link);
    Task SendEmailAsync(string email, string subject, string body);
}
public class EmailSender(IConfiguration Configuration) : IEmailSender
{
    public async Task SendConfirmationLinkAsync(ShopUser user, string email, string confirmationLink) =>
         await SendEmailAsync(email, "Email confirmation",
             $"""
            <h1>Blazor Shop</h2>
            <br/>
            <p>If you did not registed in Blazor Shop just ignore this email.</p>
            <br/>
            <p>Thanks for registiration in Blazor Shop</p>
            <p>To finish registiration please click on the email confirmation link to confirm your email: {email}</p>
            <a href="{confirmationLink}">Confirm email</a>
            """);

    public async Task SendPasswordResetCodeAsync(ShopUser user, string email, string resetCode) =>
        await SendEmailAsync(email, "Reset Password",
            $"""
            <h1>Blazor Shop</h2>
            <br/>
            <p>If you did not reset your password in Blazor Shop just ignore this email.</p>
            <br/>
            <p>To reset password please click on password reseting link to reset your password</p>
            <a href="{resetCode}">Reset Password</a>
            """);

    public async Task SendPasswordResetLinkAsync(ShopUser user, string email, string resetLink) =>
        await SendEmailAsync(email, "Reset Password",
                $"""
            <h1>Blazor Shop</h2>
            <br/>
            <p>If you did not reset your password in Blazor Shop just ignore this email.</p>
            <br/>
            <p>To reset password please click on password reseting link to reset your password</p>
            <a href="{resetLink}">Reset Password</a>
            """);

    public async Task SendPasswordResettedAsync(ShopUser user, string link) =>
        await SendEmailAsync(user.Email!, "Your Password Reseted",
                $"""
            <h1>Blazor Shop</h2>
            <br/>
            <p>If you did not reset your password in Blazor Shop just rereset it again to restore you password.</p>
            <br/>
            <p>To reset password please click on the link</p>
            <a href="{link}">Reset Password</a>
            """);

    public async Task SendPasswordChangedAsync(ShopUser user, string link) =>
        await SendEmailAsync(user.Email!, "Your Password Changed",
                $"""
            <h1>Blazor Shop</h2>
            <br/>
            <p>If you did not change your password in Blazor Shop just reresset it again to restore you password.</p>
            <br/>
            <p>To reset password please click on the link</p>
            <a href="{link}">Reset Password</a>
            """);

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Blazor Shop", "info@blazorshop.com"));
        emailMessage.To.Add(new MailboxAddress("New user", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

        using var client = new SmtpClient();
        try
        {
            client.Connect(Configuration.GetSection("EmailSettings:SmtpServer").Value!, int.Parse(Configuration.GetSection("EmailSettings:Port").Value!), true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(Configuration.GetSection("EmailSettings:Username").Value!, Configuration.GetSection("EmailSettings:Password").Value!);
            await client.SendAsync(emailMessage);
        }
        catch (Exception ex)
        {
            if (ex.HResult == -2146233088)
                throw new Exception("Email username or password is invalid.");
            else
                throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
