namespace NoteShared.DTO
{
    public class ServiceRespose<TModel>
    {
        public ServiceRespose() { }

        public ServiceRespose(string error)
        {
            Error = error;
            Success = false;
        }

        public ServiceRespose(TModel modelRequest)
        {
            ModelRequest = modelRequest;
            Success = true;
        }

        public ServiceRespose(ServiceRespose answerRequest)
        {
            Error = answerRequest.Error;
            Success = answerRequest.Success;
        }

        public bool Success { get; private set; }

        public string Error { get; private set; }

        public TModel ModelRequest { get; private set; }
    }
}
