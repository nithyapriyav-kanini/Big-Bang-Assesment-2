namespace BigBangAss2.ErrorMessage
{
    public class Messages
    {
        public List<string> messages = new List<string>();
        public Messages()
        {
            messages = new List<string>() {
                "Cannot Register at this time",
                "Working with database, try again later",
                "Mail Id Not Available",
                "Check your credentials",
                "Doctors List is Empty"
            };
        }
    }
}
