namespace MediatRGen.Core.Exceptions
{

    public class InvalidCommandException : Exception
    {
        public string Way { get; set; }
        public string Message { get; set; }

        public InvalidCommandException()
        {

        }
        public InvalidCommandException(string message) : base(message)
        {
            Message = message;
            Way = "Deneme";
        }
    }

}
