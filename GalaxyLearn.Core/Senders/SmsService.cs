using Kavenegar;
using Kavenegar.Exceptions;

namespace GalaxyLearn.Core.Senders
{
    public interface ISmsSevice
    {
        bool SendSms(string toNumber, string message);
    }

    public class SmsService : ISmsSevice
    {
        public bool SendSms(string toNumber, string message)
        {
            try
            {
                var sender = "1000689696"; 
                var receptor = toNumber;
                var api = new KavenegarApi("6C466A675A6B53594B5873436A6B32636A503462446A345241702B4659387364626A53473358563250396B3D");
                api.Send(sender, receptor, message);
                return true;
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
                return false;
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
                return false;
            }
        }
    }
}
