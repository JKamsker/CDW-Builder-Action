using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDW_Builder_Action.Models.Database
{
    public class WorkshopEvent
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("date")]
        public DateTimeOffset EventDate { get; set; }

        [BsonElement("type")]
        public string Type { get; set; } = "CoderDojo Virtual";

        [BsonElement("location")]
        public string Location { get; set; } = "CoderDojo Online";

        [BsonElement("workshops")]
        public List<Workshop> Workshops { get; set; } = new();
    }

    public partial class Workshop
    {
        [BsonElement("begintime")]
        public string Begintime { get; set; }

        [BsonElement("endtime")]
        public string Endtime { get; set; }

        [BsonElement("status")]
        public WorkshopStatus Status { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("targetAudience")]
        public string TargetAudience { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("prerequisites")]
        public string Prerequisites { get; set; }

        [BsonElement("mentors")]
        public List<string> Mentors { get; set; }

        [BsonElement("shortCode")]
        public string ShortCode { get; set; }

        [BsonElement("createZoom")]
        public bool CreateZoom { get; set; }

        [BsonElement("callbackMessageSequenceNumber")]
        public long CallbackMessageSequenceNumber { get; set; }

        [BsonElement("uniqueStateId")]
        public Guid UniqueStateId { get; set; }

        [BsonElement("discordMessage")]
        public DiscordMessage DiscordMessage { get; set; }

        //[BsonElement("zoomUser")]
        //public string ZoomUser { get; set; }

        //[BsonElement("zoom")]
        //public string Zoom { get; set; }

        //[BsonElement("zoomShort")]
        //public ZoomShort ZoomShort { get; set; }

        public List<IWorkshopJoinDetails> JoinDetails { get; set; } = new();
    }

    public interface IWorkshopJoinDetails
    {
        public string Url { get; }
    }

    public class ZoomJoinDetails : IWorkshopJoinDetails
    {
        public string ExternalId { get; set; }
        public string Url { get; set; }
        public string User { get; set; }
    }

    public class ShortJoinDetails : IWorkshopJoinDetails
    {
        [BsonElement("externalId")]
        public string ExternalId { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }
    }

    public partial class DiscordMessage
    {
        [BsonElement("GuildId")]
        public double GuildId { get; set; }

        [BsonElement("ChannelId")]
        public double ChannelId { get; set; }

        [BsonElement("MessageId")]
        public double MessageId { get; set; }
    }

    //public partial class ZoomShort
    //{
    //    [BsonElement("_id")]
    //    public string Id { get; set; }

    //    [BsonElement("Url")]
    //    public string Url { get; set; }

    //    [BsonElement("AccessKey")]
    //    public string AccessKey { get; set; }

    //    [BsonElement("ShortLink")]
    //    public string ShortLink { get; set; }
    //}
}