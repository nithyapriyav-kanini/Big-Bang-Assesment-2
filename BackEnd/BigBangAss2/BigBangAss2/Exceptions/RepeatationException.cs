namespace BigBangAss2.Exceptions
{
    public class RepeatationException : Exception
    {
        string message;
        public RepeatationException()
        {
            message = "This mail id is not available";
        }
        public RepeatationException(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
}
