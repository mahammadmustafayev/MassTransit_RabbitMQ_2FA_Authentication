using MassTransit;
using MassTransit_RabbitMQ_2FA_Authentication.Models;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit_RabbitMQ_2FA_Authentication.Controllers;

public class LoginController : Controller
{
    private readonly IPublishEndpoint _publishEndpoint;

    public LoginController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    private static string _generatedCode;
    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }

        _generatedCode = new Random().Next(100000, 999999).ToString();

        var loginEvent = new LoginEvent
        {
            Email = loginVM.Email,
            Code = _generatedCode,
        };

        await _publishEndpoint.Publish(loginEvent);

        TempData["Email"] = loginVM.Email;
        ViewBag.Mail = loginVM.Email;
        return RedirectToAction("VerifyCode");
    }
    [HttpGet("VerifyCode")]
    public IActionResult VerifyCode()
    {
        return View();
    }
    [HttpPost("VerifyCode")]
    public async Task<IActionResult> VerifyCode(CodeVerificationViewModel codeVerification)
    {
        if (codeVerification.Code != _generatedCode)
        {
            ModelState.AddModelError(string.Empty, "Invalid code. Please try again.");
            return View(codeVerification);
        }
        var mail = new DataModel { Message = ViewBag.Mail };
        return RedirectToAction("Index", "Home", mail);
    }
}
public class DataModel
{
    public string Message { get; set; }
}
