namespace BigBangAss2.Models
{
    public class Error
    {
        public int ID { get; set; }
        public string? Message { get; set; }
        public Error()
        {

        }

        public Error(int ID, string Message)
        {
            this.ID = ID;
            this.Message = Message;
        }
    }
}
