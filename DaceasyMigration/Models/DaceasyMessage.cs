namespace DaceasyMigration.Models
{
    public class DaceasyMessage
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        public string Message
        {
            get
            {
                var message = string.Concat(Line1, Line2, Line3);
                if (message.Length > 100)
                {
                    message = message.Substring(0, 100);
                }
                return message;
            }
        }
    }
}