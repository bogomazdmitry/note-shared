using Newtonsoft.Json;

namespace NoteShared.DTO.DTO
{
    [JsonObject]
    public class DeclinedRequestSharedNoteNotificationContent
    {
        [JsonProperty("fromUserEmail")]
        public string FromUserEmail { get; set; }

        [JsonProperty("noteTextID")]
        public int NoteTextID { get; set; }
    }
}
