using CDW_Builder_Action.Models.Zoom;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using ZoomNet;

namespace CDW_Builder_Action.Helpers
{
    public static class ZoomClientDependencyExtensions
    {
        public static IServiceCollection AddZoomClient(this IServiceCollection serviceCollection, string zoomToken)
        {
            serviceCollection.AddTransient<ZoomClient>()
                .AddHttpClient("zoom", c =>
                {
                    c.BaseAddress = new Uri("https://api.zoom.us/v2/");
                    AddHeader(HttpRequestHeader.Authorization, $"Bearer {zoomToken}");
                    AddHeader(HttpRequestHeader.ContentType, "application/json;charset='utf-8'");
                    AddHeader(HttpRequestHeader.Accept, "application/json");
                    c.DefaultRequestHeaders.Add("Timeout", "1000000000");

                    void AddHeader(HttpRequestHeader header, string value) => c.DefaultRequestHeaders.Add(header.ToString(), value);
                });

            //var apiKey = "... your API key ...";
            //var apiSecret = "... your API secret ...";
            //var connectionInfo = new JwtConnectionInfo(zoomToken, apiSecret);

            //var zoomClient = new ZoomClient(connectionInfo);

            return serviceCollection;
        }
    }

    public class ZoomClient
    {
        private readonly HttpClient _client;

        public ZoomClient(IHttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient("zoom");
        }

        public async IAsyncEnumerable<ZoomUserDetail> EnumerateUsersAsync([EnumeratorCancellation] CancellationToken token = default)
        {
            var usersListAsync = await GetAsync<ZoomUsersRoot>($"users");
            foreach (var user in usersListAsync.Users)
            {
                yield return await GetAsync<ZoomUserDetail>($"users/{user.Id}");
                token.ThrowIfCancellationRequested();
            }
        }

        public async IAsyncEnumerable<ZoomMeetingDetail> EnumerateMeetingsAsync(ZoomUser user)
        {
            ZoomMeetingList? meetingsList = null;
            do
            {
                meetingsList = await GetAsync<ZoomMeetingList>
                (
                    string.IsNullOrEmpty(meetingsList?.NextPageToken)
                    ? $"users/{user.Id}/meetings?type=scheduled"
                    : $"users/{user.Id}/meetings?type=scheduled&next_page_token={meetingsList.NextPageToken}"
                );

                foreach (var meeting in meetingsList.Meetings)
                {
                    yield return await GetAsync<ZoomMeetingDetail>($"meetings/{meeting.Id}");
                }
            } while (!string.IsNullOrEmpty(meetingsList?.NextPageToken));
        }

        private async Task<T> GetAsync<T>(string url)
        {
            using var getResponse = await _client.GetAsync(url);
            var getJsonContent = await getResponse.Content.ReadAsStringAsync();
            getResponse.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(getJsonContent)
                ?? throw new Exception($"Zoom returned an invalid response: {getJsonContent}");
        }

        public async Task UpdateMeetingAsync(ZoomMeetingDetail meeting, ZoomMeetingCreationDto creationDto)
        {
            creationDto = String.IsNullOrEmpty(creationDto.Password)
                ? creationDto with { Password = meeting.Password ?? CreateRandomPassword(10) } : creationDto;

            var meetingRequest = new HttpRequestMessage
            {
                RequestUri = new Uri($"meetings/{meeting.Id}", UriKind.Relative),
                Method = HttpMethod.Patch,
                Content = creationDto.AsJsonContent()
            };

            using var getResponse = await _client.SendAsync(meetingRequest);
            var responseContent = await getResponse.Content.ReadAsStringAsync();
            getResponse.EnsureSuccessStatusCode();
        }

        public async Task<ZoomMeetingDetail> CreateZoomMeetingAsync(ZoomMeetingCreationDto creationDto)
        {
            var zoomUrl = $"users/{creationDto.UserId}/meetings";
            creationDto = String.IsNullOrEmpty(creationDto.Password) ? creationDto with { Password = CreateRandomPassword(10) } : creationDto;

            var meetingRequest = new HttpRequestMessage
            {
                RequestUri = new Uri(zoomUrl, UriKind.Relative),
                Method = HttpMethod.Post,
                Content = creationDto.AsJsonContent()
            };

            using var getResponse = await _client.SendAsync(meetingRequest);
            getResponse.EnsureSuccessStatusCode();
            var getJsonContent = getResponse.Content.ReadAsStringAsync().Result;
            return JsonSerializer.Deserialize<ZoomMeetingDetail>(getJsonContent);
        }

        private string CreateRandomPassword(int length)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            for (int i = 0; i < length; i++)
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }

            return builder.ToString();
        }
    }

    public record ZoomMeetingCreationDto(DateTimeOffset StartTime, string Title, string Description, string ShortCode, string UserId, string? Password = null)
    {
        public StringContent AsJsonContent()
            => new StringContent(Serialize(), Encoding.UTF8, "application/json");

        public string Serialize() => JsonSerializer.Serialize(new
        {
            topic = $"CoderDojo Online: {Title}",
            type = "2",
            start_time = SerializeDateTime(),
            duration = "120",
            schedule_for = UserId,
            timezone = $"Europe/Vienna",
            password = Password,
            agenda = $"{Description}\n\nShortcode: {ShortCode}",
            settings = new { host_video = "true", participant_video = "true", audio = "voip", join_before_host = "true" }
        });

        public string SerializeDateTime() => StartTime.ToString("yyyy-MM-ddTHH-mm-ss");
    }
}