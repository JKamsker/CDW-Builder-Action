using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CDW_Builder_Action.Models.Zoom
{
    public partial class ZoomMeetingList
    {
        [JsonPropertyName("page_size")]
        public long PageSize { get; set; }

        [JsonPropertyName("total_records")]
        public long TotalRecords { get; set; }

        [JsonPropertyName("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonPropertyName("meetings")]
        public List<ZoomMeetingListItem> Meetings { get; set; }
    }

    public partial class ZoomMeetingListItem
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("host_id")]
        public string HostId { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("type")]
        public long Type { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("join_url")]
        public string JoinUrl { get; set; }

        [JsonPropertyName("agenda")]
        public string Agenda { get; set; }
    }
}