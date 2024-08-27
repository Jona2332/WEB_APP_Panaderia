namespace WEB_APP_Panaderia.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentDate { get; set; }
        public string? Sender { get; set; }
        public string Recipients { get; set; }
    }
}