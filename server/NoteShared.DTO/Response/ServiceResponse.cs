namespace NoteShared.DTO
{
    public class ServiceResponse
    {

        public ServiceResponse()
        {
            Success = true;
        }

        public ServiceResponse(string error)
        {
            Error = error;
            Success = false;
        }

        public bool Success { get; private set; }

        public string Error { get; private set; }

    }
}
