using Newtonsoft.Json;

namespace NoteShared.DTO.DTO
{
    [JsonObject]
    public class RequestSharedNoteNotificationContent
    {
        [JsonProperty("fromUserEmail")]
        public string FromUserEmail { get; set; }

        [JsonProperty("noteTextID")]
        public int NoteTextID { get; set; }
    }
}
