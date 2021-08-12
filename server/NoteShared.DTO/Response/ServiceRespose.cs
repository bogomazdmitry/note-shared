namespace NoteShared.DTO
{
    public class ServiceRespose
    {

        public ServiceRespose() 
        {
            Success = true;
        }

        public ServiceRespose(string error)
        {
            Error = error;
            Success = false;
        }

        public bool Success { get; private set; }

        public string Error { get; private set; }
        
    }
}
