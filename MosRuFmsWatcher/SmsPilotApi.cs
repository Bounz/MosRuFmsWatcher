using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace MosRuFmsWatcher
{
    public interface ISmsPilotApi
    {
        [Post("")]
        Task<SendSmsResponse> SendSms([Body(BodySerializationMethod.UrlEncoded)] SendSmsRequest query, CancellationToken cancellationToken);
    }

    public class SendSmsRequest
    {
        public string send { get; set; }
        public string to { get; set; }
        public string apikey { get; set; }
        public string format { get; set; } = "json";
    }

    public partial class SendSmsResponse
    {
        [JsonProperty("send")]
        public List<Send> Send { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("cost")]
        public string Cost { get; set; }

        [JsonProperty("server_packet_id")]
        public string ServerPacketId { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("description_ru")]
        public string DescriptionRu { get; set; }
    }

    public partial class Send
    {
        [JsonProperty("server_id")]
        public string ServerId { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
