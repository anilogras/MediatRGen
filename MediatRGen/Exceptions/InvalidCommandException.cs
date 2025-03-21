namespace MediatRGen
{

    public class InvalidCommandException : Exception
    {
        public string Way { get; set; }

        public InvalidCommandException(string message) : base(message)
        {
            this.Way = "Deneme";
        }
    }

}
