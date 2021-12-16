using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CDW_Builder_Action.Models.Zoom
{
    public partial class ZoomMeetingDetail
    {
        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("host_id")]
        public string HostId { get; set; }

        [JsonPropertyName("host_email")]
        public string HostEmail { get; set; }

        [JsonPropertyName("assistant_id")]
        public string AssistantId { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }

        [JsonPropertyName("type")]
        public long Type { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("agenda")]
        public string Agenda { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("start_url")]
        public Uri StartUrl { get; set; }

        [JsonPropertyName("join_url")]
        public Uri JoinUrl { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("h323_password")]
        public string H323Password { get; set; }

        [JsonPropertyName("pstn_password")]
        public string PstnPassword { get; set; }

        [JsonPropertyName("encrypted_password")]
        public string EncryptedPassword { get; set; }

        [JsonPropertyName("occurrences")]
        public List<Occurrence> Occurrences { get; set; }

        [JsonPropertyName("settings")]
        public Settings Settings { get; set; }

        [JsonPropertyName("recurrence")]
        public Recurrence Recurrence { get; set; }

        [JsonPropertyName("pre_schedule")]
        public bool PreSchedule { get; set; }
    }

    public partial class Occurrence
    {
        [JsonPropertyName("occurrence_id")]
        public string OccurrenceId { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public partial class Recurrence
    {
        [JsonPropertyName("type")]
        public long Type { get; set; }

        [JsonPropertyName("repeat_interval")]
        public long RepeatInterval { get; set; }

        [JsonPropertyName("end_date_time")]
        public string EndDateTime { get; set; }
    }

    public partial class Settings
    {
        [JsonPropertyName("host_video")]
        public bool HostVideo { get; set; }

        [JsonPropertyName("participant_video")]
        public bool ParticipantVideo { get; set; }

        [JsonPropertyName("cn_meeting")]
        public bool CnMeeting { get; set; }

        [JsonPropertyName("in_meeting")]
        public bool InMeeting { get; set; }

        [JsonPropertyName("join_before_host")]
        public bool JoinBeforeHost { get; set; }

        [JsonPropertyName("jbh_time")]
        public long JbhTime { get; set; }

        [JsonPropertyName("mute_upon_entry")]
        public bool MuteUponEntry { get; set; }

        [JsonPropertyName("watermark")]
        public bool Watermark { get; set; }

        [JsonPropertyName("use_pmi")]
        public bool UsePmi { get; set; }

        [JsonPropertyName("approval_type")]
        public long ApprovalType { get; set; }

        [JsonPropertyName("audio")]
        public string Audio { get; set; }

        [JsonPropertyName("auto_recording")]
        public string AutoRecording { get; set; }

        [JsonPropertyName("enforce_login")]
        public bool EnforceLogin { get; set; }

        [JsonPropertyName("enforce_login_domains")]
        public string EnforceLoginDomains { get; set; }

        [JsonPropertyName("alternative_hosts")]
        public string AlternativeHosts { get; set; }

        [JsonPropertyName("close_registration")]
        public bool CloseRegistration { get; set; }

        [JsonPropertyName("show_share_button")]
        public bool ShowShareButton { get; set; }

        [JsonPropertyName("allow_multiple_devices")]
        public bool AllowMultipleDevices { get; set; }

        [JsonPropertyName("registrants_confirmation_email")]
        public bool RegistrantsConfirmationEmail { get; set; }

        [JsonPropertyName("waiting_room")]
        public bool WaitingRoom { get; set; }

        [JsonPropertyName("request_permission_to_unmute_participants")]
        public bool RequestPermissionToUnmuteParticipants { get; set; }

        [JsonPropertyName("registrants_email_notification")]
        public bool RegistrantsEmailNotification { get; set; }

        [JsonPropertyName("meeting_authentication")]
        public bool MeetingAuthentication { get; set; }

        [JsonPropertyName("encryption_type")]
        public string EncryptionType { get; set; }

        [JsonPropertyName("approved_or_denied_countries_or_regions")]
        public ApprovedOrDeniedCountriesOrRegions ApprovedOrDeniedCountriesOrRegions { get; set; }

        [JsonPropertyName("breakout_room")]
        public ApprovedOrDeniedCountriesOrRegions BreakoutRoom { get; set; }

        [JsonPropertyName("alternative_hosts_email_notification")]
        public bool AlternativeHostsEmailNotification { get; set; }

        [JsonPropertyName("device_testing")]
        public bool DeviceTesting { get; set; }

        [JsonPropertyName("focus_mode")]
        public bool FocusMode { get; set; }
    }

    public partial class ApprovedOrDeniedCountriesOrRegions
    {
        [JsonPropertyName("enable")]
        public bool Enable { get; set; }
    }
}