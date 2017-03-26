namespace Parva.Application.Core
{
    public class ServiceResponse
    {
        public ServiceResponse()
        {
            this.Success = true;
        }

        public bool Success { get; private set; }

        public string Key { get; private set; }

        public string Message { get; private set; }

        public void AddError(string message)
        {
            this.Key = string.Empty;
            this.Message = message;
            this.Success = false;
        }

        public void AddError(string key, string message)
        {
            this.Key = key;
            this.Message = message;
            this.Success = false;
        }
    }
}