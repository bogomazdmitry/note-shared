namespace NoteShared.DTO
{
    public class ServiceResponse<TModel>
    {
        public ServiceResponse() { }

        public ServiceResponse(string error)
        {
            Error = error;
            Success = false;
        }

        public ServiceResponse(TModel modelRequest)
        {
            ModelRequest = modelRequest;
            Success = true;
        }

        public ServiceResponse(ServiceResponse answerRequest)
        {
            Error = answerRequest.Error;
            Success = answerRequest.Success;
        }

        public ServiceResponse ConvertToServiceRespose()
        {
            ServiceResponse serviceRespose;
            if (Success)
            {
                serviceRespose = new ServiceResponse();
            }
            else
            {
                serviceRespose = new ServiceResponse(Error);
            }
            return serviceRespose;
        }

        public bool Success { get; private set; }

        public string Error { get; private set; }

        public TModel ModelRequest { get; private set; }
    }
}
