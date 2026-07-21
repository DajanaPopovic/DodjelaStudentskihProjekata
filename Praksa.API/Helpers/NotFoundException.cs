// Helpers/NotFoundException.cs / BusinessException.cs
namespace Praksa.API.Helpers
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}