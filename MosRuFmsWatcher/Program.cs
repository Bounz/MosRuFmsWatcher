using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace MosRuFmsWatcher
{
    class Program
    {
        private const string SmsPilotKey = "__YOUR SMSPILOT.RU API KEY HERE__";
        private static readonly DateTime ThresholdDate = new DateTime(2022, 04, 14);

        static async Task Main(string[] args)
        {
            JsonConvert.DefaultSettings = () => Converter.Settings;
            await DoWork();
            Console.Write("Done");
        }

        private static async Task DoWork()
        {
            var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());
            var api = RestService.For<IMosRuApi>("https://www.mos.ru", settings);

            do
            {
                try
                {
                    Console.WriteLine($"Start check at {DateTime.Now:g}");
                    var slots = await GetAffordableSlots(api);
                    var firstAvailable = slots.OrderBy(x => x.OrigStartTime).FirstOrDefault();
                    if (firstAvailable != null)
                    {
                        Console.WriteLine($"{firstAvailable.OrigStartTime:d} - {firstAvailable.OrigStartTime:t}");
                        await SendSms($"{firstAvailable.OrigStartTime:d} - {firstAvailable.OrigStartTime:t}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    await Task.Delay(TimeSpan.FromMinutes(2));
                }
            } while (true);
        }

        private static async Task<List<TimePeriod>> GetAffordableSlots(IMosRuApi mosRuApi)
        {
            var resp = await mosRuApi.GetTimeSlots(new GetScheduleRequest
            {
                multiplier = 1,
                officeId = 338,
                serviceId = 108
            }, CancellationToken.None);

            var result = new List<TimePeriod>();
            foreach (var date in resp.Schedule)
            {
                var day = date.Key;
                var availableSlots = date.Value.TimePeriods.Where(x => x.Value.AllowedAppointment);
                foreach (var slot in availableSlots)
                {
                    if(slot.Value.OrigStartTime.Date > ThresholdDate)
                        continue;

                    result.Add(slot.Value);
                    Console.WriteLine($"{day} - {slot.Value.OrigStartTime:t}");
                }
            }

            return result;
        }

        private static async Task SendSms(string content)
        {
            var settings = new RefitSettings(new NewtonsoftJsonContentSerializer());
            var api = RestService.For<ISmsPilotApi>("https://smspilot.ru/api.php", settings);
            var resp = await api.SendSms(new SendSmsRequest()
            {
                to = "__your mobile phone__",
                send = content,
                apikey = SmsPilotKey
            }, CancellationToken.None);
        }
    }
}
