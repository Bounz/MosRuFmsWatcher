namespace MosRuFmsWatcher
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class BookingScheduleResponse
    {
        [JsonProperty("schedule")]
        public Dictionary<string, Schedule> Schedule { get; set; }

        [JsonProperty("request")]
        public Request Request { get; set; }
    }

    public partial class Request
    {
        [JsonProperty("serviceId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ServiceId { get; set; }

        [JsonProperty("officeId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long OfficeId { get; set; }

        [JsonProperty("multiplier")]
        public long Multiplier { get; set; }

        [JsonProperty("startDate")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }

    public partial class Schedule
    {
        [JsonProperty("timePeriods")]
        public Dictionary<string, TimePeriod> TimePeriods { get; set; }
    }

    public partial class TimePeriod
    {
        [JsonProperty("endTime")]
        public EndTime EndTime { get; set; }

        [JsonProperty("allowedAppointment")]
        public bool AllowedAppointment { get; set; }

        [JsonProperty("orig_startTime")]
        public DateTimeOffset OrigStartTime { get; set; }

        [JsonProperty("orig_endTime")]
        public DateTimeOffset OrigEndTime { get; set; }
    }

    public enum EndTime { The0915, The0930, The0945, The1000, The1015, The1030, The1045, The1100, The1115, The1130, The1145, The1200, The1215, The1230, The1245, The1300, The1315, The1330, The1345, The1400, The1500, The1515, The1530, The1545, The1600, The1615, The1630, The1645, The1700, The1715, The1730, The1745, The1800, The1815, The1830, The1845, The1900, The1915, The1930, The1945, The2000 };


    internal class EndTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EndTime) || t == typeof(EndTime?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "09:15":
                    return EndTime.The0915;
                case "09:30":
                    return EndTime.The0930;
                case "09:45":
                    return EndTime.The0945;
                case "10:00":
                    return EndTime.The1000;
                case "10:15":
                    return EndTime.The1015;
                case "10:30":
                    return EndTime.The1030;
                case "10:45":
                    return EndTime.The1045;
                case "11:00":
                    return EndTime.The1100;
                case "11:15":
                    return EndTime.The1115;
                case "11:30":
                    return EndTime.The1130;
                case "11:45":
                    return EndTime.The1145;
                case "12:00":
                    return EndTime.The1200;
                case "12:15":
                    return EndTime.The1215;
                case "12:30":
                    return EndTime.The1230;
                case "12:45":
                    return EndTime.The1245;
                case "13:00":
                    return EndTime.The1300;
                case "13:15":
                    return EndTime.The1315;
                case "13:30":
                    return EndTime.The1330;
                case "13:45":
                    return EndTime.The1345;
                case "14:00":
                    return EndTime.The1400;
                case "15:00":
                    return EndTime.The1500;
                case "15:15":
                    return EndTime.The1515;
                case "15:30":
                    return EndTime.The1530;
                case "15:45":
                    return EndTime.The1545;
                case "16:00":
                    return EndTime.The1600;
                case "16:15":
                    return EndTime.The1615;
                case "16:30":
                    return EndTime.The1630;
                case "16:45":
                    return EndTime.The1645;
                case "17:00":
                    return EndTime.The1700;
                case "17:15":
                    return EndTime.The1715;
                case "17:30":
                    return EndTime.The1730;
                case "17:45":
                    return EndTime.The1745;
                case "18:00":
                    return EndTime.The1800;
                case "18:15":
                    return EndTime.The1815;
                case "18:30":
                    return EndTime.The1830;
                case "18:45":
                    return EndTime.The1845;
                case "19:00":
                    return EndTime.The1900;
                case "19:15":
                    return EndTime.The1915;
                case "19:30":
                    return EndTime.The1930;
                case "19:45":
                    return EndTime.The1945;
                case "20:00":
                    return EndTime.The2000;
            }
            throw new Exception("Cannot unmarshal type EndTime");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (EndTime)untypedValue;
            switch (value)
            {
                case EndTime.The0915:
                    serializer.Serialize(writer, "09:15");
                    return;
                case EndTime.The0930:
                    serializer.Serialize(writer, "09:30");
                    return;
                case EndTime.The0945:
                    serializer.Serialize(writer, "09:45");
                    return;
                case EndTime.The1000:
                    serializer.Serialize(writer, "10:00");
                    return;
                case EndTime.The1015:
                    serializer.Serialize(writer, "10:15");
                    return;
                case EndTime.The1030:
                    serializer.Serialize(writer, "10:30");
                    return;
                case EndTime.The1045:
                    serializer.Serialize(writer, "10:45");
                    return;
                case EndTime.The1100:
                    serializer.Serialize(writer, "11:00");
                    return;
                case EndTime.The1115:
                    serializer.Serialize(writer, "11:15");
                    return;
                case EndTime.The1130:
                    serializer.Serialize(writer, "11:30");
                    return;
                case EndTime.The1145:
                    serializer.Serialize(writer, "11:45");
                    return;
                case EndTime.The1200:
                    serializer.Serialize(writer, "12:00");
                    return;
                case EndTime.The1215:
                    serializer.Serialize(writer, "12:15");
                    return;
                case EndTime.The1230:
                    serializer.Serialize(writer, "12:30");
                    return;
                case EndTime.The1245:
                    serializer.Serialize(writer, "12:45");
                    return;
                case EndTime.The1300:
                    serializer.Serialize(writer, "13:00");
                    return;
                case EndTime.The1315:
                    serializer.Serialize(writer, "13:15");
                    return;
                case EndTime.The1330:
                    serializer.Serialize(writer, "13:30");
                    return;
                case EndTime.The1345:
                    serializer.Serialize(writer, "13:45");
                    return;
                case EndTime.The1400:
                    serializer.Serialize(writer, "14:00");
                    return;
                case EndTime.The1500:
                    serializer.Serialize(writer, "15:00");
                    return;
                case EndTime.The1515:
                    serializer.Serialize(writer, "15:15");
                    return;
                case EndTime.The1530:
                    serializer.Serialize(writer, "15:30");
                    return;
                case EndTime.The1545:
                    serializer.Serialize(writer, "15:45");
                    return;
                case EndTime.The1600:
                    serializer.Serialize(writer, "16:00");
                    return;
                case EndTime.The1615:
                    serializer.Serialize(writer, "16:15");
                    return;
                case EndTime.The1630:
                    serializer.Serialize(writer, "16:30");
                    return;
                case EndTime.The1645:
                    serializer.Serialize(writer, "16:45");
                    return;
                case EndTime.The1700:
                    serializer.Serialize(writer, "17:00");
                    return;
                case EndTime.The1715:
                    serializer.Serialize(writer, "17:15");
                    return;
                case EndTime.The1730:
                    serializer.Serialize(writer, "17:30");
                    return;
                case EndTime.The1745:
                    serializer.Serialize(writer, "17:45");
                    return;
                case EndTime.The1800:
                    serializer.Serialize(writer, "18:00");
                    return;
                case EndTime.The1815:
                    serializer.Serialize(writer, "18:15");
                    return;
                case EndTime.The1830:
                    serializer.Serialize(writer, "18:30");
                    return;
                case EndTime.The1845:
                    serializer.Serialize(writer, "18:45");
                    return;
                case EndTime.The1900:
                    serializer.Serialize(writer, "19:00");
                    return;
                case EndTime.The1915:
                    serializer.Serialize(writer, "19:15");
                    return;
                case EndTime.The1930:
                    serializer.Serialize(writer, "19:30");
                    return;
                case EndTime.The1945:
                    serializer.Serialize(writer, "19:45");
                    return;
                case EndTime.The2000:
                    serializer.Serialize(writer, "20:00");
                    return;
            }
            throw new Exception("Cannot marshal type EndTime");
        }

        public static readonly EndTimeConverter Singleton = new EndTimeConverter();
    }

}
