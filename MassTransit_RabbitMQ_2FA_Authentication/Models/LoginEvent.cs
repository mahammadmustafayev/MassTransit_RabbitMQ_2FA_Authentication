namespace MassTransit_RabbitMQ_2FA_Authentication.Models
{
    public class LoginEvent
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
