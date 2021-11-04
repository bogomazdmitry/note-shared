using Newtonsoft.Json;

namespace NoteShared.DTO.DTO
{
    [JsonObject]
    public class AcceptedRequestSharedNoteNotificationContent
    {
        [JsonProperty("fromUserEmail")]
        public string FromUserEmail { get; set; }

        [JsonProperty("noteTextID")]
        public int NoteText { get; set; }
    }
}
