namespace crud_dotnet.Exceptions
{
    public abstract class CustomException : Exception
    {
        protected CustomException(string message) : base(message) { }
    }

    public class NotFoundException : CustomException
    {
        public NotFoundException(string entityName, object key)
            : base($"{entityName} com identificador '{key}' não foi encontrado.")
        { }
    }

    public class BusinessRuleException : CustomException
    {
        public BusinessRuleException(string message) : base(message) { }
    }

    public class DatabaseException : CustomException
    {
        public DatabaseException(string message, Exception? innerException = null)
            : base(message)
        {
            if (innerException != null)
                this.InnerException?.Data.Add("InnerError", innerException.Message);
        }
    }
}
