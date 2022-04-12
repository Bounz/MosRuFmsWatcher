using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Refit;

namespace MosRuFmsWatcher
{
    [Headers(
        "cookie: __INSERT YOUR COOKIE HERE__"
        )]
    public interface IMosRuApi
    {
        [Post("/pgu/ru/application/oiv/booking/")]
        Task<BookingResponse> GetBookings(string issueIdOrKey, CancellationToken cancellationToken);

        [Headers("Content-Type: application/x-www-form-urlencoded")]
        [Post("/pgu/ru/application/oiv/booking/")]
        Task<BookingScheduleResponse> GetTimeSlots([Body(BodySerializationMethod.UrlEncoded)] GetScheduleRequest query, CancellationToken cancellationToken);
    }

    public class GetScheduleRequest
    {
        public bool ajaxSend { get; set; } = true;

        public string ajaxAction { get; set; } = "getTimeSlots";
        public int serviceId { get; set; }
        public int officeId { get; set; }
        public int multiplier { get; set; }
    }


    public partial class BookingResponse
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("service_description")]
        public string ServiceDescription { get; set; }

        [JsonProperty("queue")]
        public Dictionary<string, QueueValue> Queue { get; set; }

        [JsonProperty("metro")]
        public Dictionary<string, string> Metro { get; set; }

        [JsonProperty("offices")]
        public Dictionary<string, Office> Offices { get; set; }

        [JsonProperty("service")]
        public Service Service { get; set; }

        [JsonProperty("count_offices")]
        public long CountOffices { get; set; }
    }

    public partial class Office
    {
        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("agencyId")]
        public long AgencyId { get; set; }

        [JsonProperty("auxiliaryServicesIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> AuxiliaryServicesIds { get; set; }

        [JsonProperty("bookableServices")]
        public BookableServices BookableServices { get; set; }

        [JsonProperty("bookableServicesIds")]
        public BookableServicesIds BookableServicesIds { get; set; }

        [JsonProperty("camerasUrl", NullValueHandling = NullValueHandling.Ignore)]
        public Uri CamerasUrl { get; set; }

        [JsonProperty("externalIds", NullValueHandling = NullValueHandling.Ignore)]
        public ExternalIds ExternalIds { get; set; }

        [JsonProperty("headFIO")]
        public string HeadFio { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("okato", NullValueHandling = NullValueHandling.Ignore)]
        public Okato? Okato { get; set; }

        [JsonProperty("openDate")]
        public DateTimeOffset OpenDate { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("queuesInOffice", NullValueHandling = NullValueHandling.Ignore)]
        public QueuesInOfficeUnion? QueuesInOffice { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("isVirtual")]
        public bool IsVirtual { get; set; }

        [JsonProperty("website", NullValueHandling = NullValueHandling.Ignore)]
        public string Website { get; set; }

        [JsonProperty("workScheduleId")]
        public long WorkScheduleId { get; set; }

        [JsonProperty("multiplier")]
        public long Multiplier { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty("servicesInOffice", NullValueHandling = NullValueHandling.Ignore)]
        public ServicesInOffice ServicesInOffice { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("buses", NullValueHandling = NullValueHandling.Ignore)]
        public string Buses { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("metroLocations", NullValueHandling = NullValueHandling.Ignore)]
        public MetroLocations? MetroLocations { get; set; }

        [JsonProperty("shuttles", NullValueHandling = NullValueHandling.Ignore)]
        public string Shuttles { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("trolleys", NullValueHandling = NullValueHandling.Ignore)]
        public string Trolleys { get; set; }

        [JsonProperty("trams", NullValueHandling = NullValueHandling.Ignore)]
        public string Trams { get; set; }
    }

    public partial class MetroLocation
    {
        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("metroStationId")]
        public long MetroStationId { get; set; }
    }

    public partial class BookableServices
    {
        [JsonProperty("bookableService")]
        public List<BookableService> BookableService { get; set; }
    }

    public partial class BookableService
    {
        [JsonProperty("maxMultiplier")]
        public long MaxMultiplier { get; set; }

        [JsonProperty("serviceId")]
        public long ServiceId { get; set; }
    }

    public partial class ExternalIds
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("registryName")]
        public string RegistryName { get; set; }
    }

    public partial class QueuesInOfficeElement
    {
        [JsonProperty("queueId")]
        public long QueueId { get; set; }

        [JsonProperty("scheduleId")]
        public long ScheduleId { get; set; }
    }

    public partial class ServicesInOffice
    {
        [JsonProperty("queues")]
        public List<QueueElement> Queues { get; set; }

        [JsonProperty("serviceId")]
        public long ServiceId { get; set; }
    }

    public partial class QueueElement
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public partial class QueueValue
    {
        [JsonProperty("wait_count")]
        public long WaitCount { get; set; }

        [JsonProperty("waitingAvg")]
        public string WaitingAvg { get; set; }
    }

    public partial class Service
    {
        [JsonProperty("bookableOfficesIds")]
        public List<long> BookableOfficesIds { get; set; }

        [JsonProperty("customerTypeId")]
        public long CustomerTypeId { get; set; }

        [JsonProperty("externalId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ExternalId { get; set; }

        [JsonProperty("extraTerr")]
        public long ExtraTerr { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("inputFields")]
        public List<InputField> InputFields { get; set; }

        [JsonProperty("isDigital")]
        public bool IsDigital { get; set; }

        [JsonProperty("isOnline")]
        public bool IsOnline { get; set; }

        [JsonProperty("isPopular")]
        public bool IsPopular { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("officesIds")]
        public List<long> OfficesIds { get; set; }

        [JsonProperty("regTime")]
        public long RegTime { get; set; }

        [JsonProperty("serviceProviderId")]
        public long ServiceProviderId { get; set; }
    }

    public partial class InputField
    {
        [JsonProperty("fieldId")]
        public long FieldId { get; set; }

        [JsonProperty("inputFieldDescription")]
        public string InputFieldDescription { get; set; }

        [JsonProperty("inputFieldName")]
        public string InputFieldName { get; set; }

        [JsonProperty("inputFieldTag")]
        public string InputFieldTag { get; set; }

        [JsonProperty("inputMaxLength")]
        public long InputMaxLength { get; set; }

        [JsonProperty("inputTypeClass")]
        public string InputTypeClass { get; set; }

        [JsonProperty("inputTypeName")]
        public string InputTypeName { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("mandatory")]
        public bool Mandatory { get; set; }

        [JsonProperty("orderPos")]
        public long OrderPos { get; set; }

        [JsonProperty("regexp")]
        public string Regexp { get; set; }

        [JsonProperty("sampleInput")]
        public string SampleInput { get; set; }
    }


    public partial struct MetroLocations
    {
        public MetroLocation MetroLocation;
        public List<MetroLocation> MetroLocationArray;

        public static implicit operator MetroLocations(MetroLocation MetroLocation) => new MetroLocations { MetroLocation = MetroLocation };
        public static implicit operator MetroLocations(List<MetroLocation> MetroLocationArray) => new MetroLocations { MetroLocationArray = MetroLocationArray };
    }

    public partial struct BookableServicesIds
    {
        public long? Integer;
        public List<long> IntegerArray;

        public static implicit operator BookableServicesIds(long Integer) => new BookableServicesIds { Integer = Integer };
        public static implicit operator BookableServicesIds(List<long> IntegerArray) => new BookableServicesIds { IntegerArray = IntegerArray };
    }

    public partial struct Okato
    {
        public long? Integer;
        public List<long> StringArray;

        public static implicit operator Okato(long Integer) => new Okato { Integer = Integer };
        public static implicit operator Okato(List<long> StringArray) => new Okato { StringArray = StringArray };
    }

    public partial struct QueuesInOfficeUnion
    {
        public QueuesInOfficeElement QueuesInOfficeElement;
        public List<QueuesInOfficeElement> QueuesInOfficeElementArray;

        public static implicit operator QueuesInOfficeUnion(QueuesInOfficeElement QueuesInOfficeElement) => new QueuesInOfficeUnion { QueuesInOfficeElement = QueuesInOfficeElement };
        public static implicit operator QueuesInOfficeUnion(List<QueuesInOfficeElement> QueuesInOfficeElementArray) => new QueuesInOfficeUnion { QueuesInOfficeElementArray = QueuesInOfficeElementArray };
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                MetroLocationsConverter.Singleton,
                BookableServicesIdsConverter.Singleton,
                OkatoConverter.Singleton,
                QueuesInOfficeUnionConverter.Singleton,
                EndTimeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class MetroLocationsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MetroLocations) || t == typeof(MetroLocations?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<MetroLocation>(reader);
                    return new MetroLocations { MetroLocation = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<MetroLocation>>(reader);
                    return new MetroLocations { MetroLocationArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type MetroLocations");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (MetroLocations)untypedValue;
            if (value.MetroLocationArray != null)
            {
                serializer.Serialize(writer, value.MetroLocationArray);
                return;
            }
            if (value.MetroLocation != null)
            {
                serializer.Serialize(writer, value.MetroLocation);
                return;
            }
            throw new Exception("Cannot marshal type MetroLocations");
        }

        public static readonly MetroLocationsConverter Singleton = new MetroLocationsConverter();
    }

    internal class BookableServicesIdsConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(BookableServicesIds) || t == typeof(BookableServicesIds?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new BookableServicesIds { Integer = integerValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<long>>(reader);
                    return new BookableServicesIds { IntegerArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type BookableServicesIds");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (BookableServicesIds)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.IntegerArray != null)
            {
                serializer.Serialize(writer, value.IntegerArray);
                return;
            }
            throw new Exception("Cannot marshal type BookableServicesIds");
        }

        public static readonly BookableServicesIdsConverter Singleton = new BookableServicesIdsConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }


    internal class OkatoConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Okato) || t == typeof(Okato?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new Okato { Integer = l };
                    }
                    break;
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<long>>(reader);
                    return new Okato { StringArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type Okato");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Okato)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            if (value.StringArray != null)
            {
                var converter = DecodeArrayConverter.Singleton;
                converter.WriteJson(writer, value.StringArray, serializer);
                return;
            }
            throw new Exception("Cannot marshal type Okato");
        }

        public static readonly OkatoConverter Singleton = new OkatoConverter();
    }

    internal class DecodeArrayConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(List<long>);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            reader.Read();
            var value = new List<long>();
            while (reader.TokenType != JsonToken.EndArray)
            {
                var converter = ParseStringConverter.Singleton;
                var arrayItem = (long)converter.ReadJson(reader, typeof(long), null, serializer);
                value.Add(arrayItem);
                reader.Read();
            }
            return value;
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (List<long>)untypedValue;
            writer.WriteStartArray();
            foreach (var arrayItem in value)
            {
                var converter = ParseStringConverter.Singleton;
                converter.WriteJson(writer, arrayItem, serializer);
            }
            writer.WriteEndArray();
            return;
        }

        public static readonly DecodeArrayConverter Singleton = new DecodeArrayConverter();
    }


    internal class QueuesInOfficeUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(QueuesInOfficeUnion) || t == typeof(QueuesInOfficeUnion?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<QueuesInOfficeElement>(reader);
                    return new QueuesInOfficeUnion { QueuesInOfficeElement = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<QueuesInOfficeElement>>(reader);
                    return new QueuesInOfficeUnion { QueuesInOfficeElementArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type QueuesInOfficeUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (QueuesInOfficeUnion)untypedValue;
            if (value.QueuesInOfficeElementArray != null)
            {
                serializer.Serialize(writer, value.QueuesInOfficeElementArray);
                return;
            }
            if (value.QueuesInOfficeElement != null)
            {
                serializer.Serialize(writer, value.QueuesInOfficeElement);
                return;
            }
            throw new Exception("Cannot marshal type QueuesInOfficeUnion");
        }

        public static readonly QueuesInOfficeUnionConverter Singleton = new QueuesInOfficeUnionConverter();
    }


}
