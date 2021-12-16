using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace CDW_Builder_Action.Models.Zoom
{
    public partial class ZoomUsersRoot
    {
        [JsonPropertyName("page_count")]
        public long PageCount { get; set; }

        [JsonPropertyName("page_number")]
        public long PageNumber { get; set; }

        [JsonPropertyName("page_size")]
        public long PageSize { get; set; }

        [JsonPropertyName("total_records")]
        public long TotalRecords { get; set; }

        [JsonPropertyName("next_page_token")]
        public string NextPageToken { get; set; }

        [JsonPropertyName("users")]
        public List<ZoomUser> Users { get; set; }
    }

    public partial class ZoomUser
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("type")]
        public long Type { get; set; }

        [JsonPropertyName("pmi")]
        public long Pmi { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("verified")]
        public long Verified { get; set; }

        [JsonPropertyName("dept")]
        public string Dept { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("last_login_time")]
        public string LastLoginTime { get; set; }

        [JsonPropertyName("last_client_version")]
        public string LastClientVersion { get; set; }

        [JsonPropertyName("pic_url")]
        public string PicUrl { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("role_id")]
        public string RoleId { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }
    }

    public partial class ZoomUserDetail : ZoomUser
    {

        [JsonPropertyName("role_name")]
        public string RoleName { get; set; }

        [JsonPropertyName("use_pmi")]
        public bool UsePmi { get; set; }

        [JsonPropertyName("personal_meeting_url")]
        public Uri PersonalMeetingUrl { get; set; }

        [JsonPropertyName("host_key")]
        public string HostKey { get; set; }

        [JsonPropertyName("cms_user_id")]
        public string CmsUserId { get; set; }

        [JsonPropertyName("jid")]
        public string Jid { get; set; }

        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("phone_country")]
        public string PhoneCountry { get; set; }
        [JsonPropertyName("job_title")]
        public string JobTitle { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("login_types")]
        public List<long> LoginTypes { get; set; }

        [JsonPropertyName("account_number")]
        public long AccountNumber { get; set; }
    }
    //[JsonPropertyName("id")]
    //public string Id { get; set; }

    //[JsonPropertyName("first_name")]
    //public string FirstName { get; set; }

    //[JsonPropertyName("last_name")]
    //public string LastName { get; set; }

    //[JsonPropertyName("email")]
    //public string Email { get; set; }

    //[JsonPropertyName("type")]
    //public long Type { get; set; }


    //[JsonPropertyName("pmi")]
    //public long Pmi { get; set; }


    //[JsonPropertyName("timezone")]
    //public string Timezone { get; set; }

    //[JsonPropertyName("verified")]
    //public long Verified { get; set; }

    //[JsonPropertyName("dept")]
    //public string Dept { get; set; }

    //[JsonPropertyName("created_at")]
    //public string CreatedAt { get; set; }

    //[JsonPropertyName("last_login_time")]
    //public string LastLoginTime { get; set; }

    //[JsonPropertyName("last_client_version")]
    //public string LastClientVersion { get; set; }

    //[JsonPropertyName("pic_url")]
    //public Uri PicUrl { get; set; }



    //[JsonPropertyName("group_ids")]
    //public List<object> GroupIds { get; set; }

    //[JsonPropertyName("im_group_ids")]
    //public List<object> ImGroupIds { get; set; }


    //[JsonPropertyName("language")]
    //public string Language { get; set; }


    //[JsonPropertyName("phone_number")]
    //public string PhoneNumber { get; set; }

    //[JsonPropertyName("status")]
    //public string Status { get; set; }



    //[JsonPropertyName("role_id")]
    //public string RoleId { get; set; }


}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
