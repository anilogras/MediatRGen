namespace MediatRGen.Exceptions
{

    public class InvalidCommandException : Exception
    {
        public string Way { get; set; }

        public InvalidCommandException(string message) : base(message)
        {
            Way = "Deneme";
        }
    }

}
