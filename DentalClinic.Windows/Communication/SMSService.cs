// Przypominamy o wizycie dnia ...... o godz. ....
// Dental SPA Gabinety Dentystyczne


namespace DentalClinic.Windows
{
    using DentalClinic.Data;
    using DentalClinic.XmlData;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows;

    /// <summary>
    /// 
    /// </summary>
    public class SMSService : ISmsService
    {
        private const string MessagesUrlPath = "services/api/messaging";
        private bool lastSmsFailed = false;

        public string IpAddress { get; set; }
        public int PortNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public GetMessageResponse Result { get; set; }



        public SMSService(string ipAddress, int portNumber, string userName, string password)
        {
            this.IpAddress = ipAddress;
            this.PortNumber = portNumber;
            this.UserName = userName;
            this.Password = password;
        }

        public async void RetrieveAllMessages()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConstructBaseUri();
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(
                            ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", UserName, Password))
                        )
                    );

                    HttpResponseMessage response = await client.GetAsync(MessagesUrlPath);
                    if (response.IsSuccessStatusCode)
                    {
                        Result = await response.Content.ReadAsAsync<GetMessageResponse>();
                    }
                    else
                    {
                        MessageBox.Show(response.ToString(), "Odpowiedź", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Wiadomość", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {

            }
        }

        public async void SendMessage(ISms sms)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = ConstructBaseUri();
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(
                            ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", UserName, Password))
                        )
                    );

                    var postData = new List<KeyValuePair<string, string>>();

                    if (sms.PersonId == -100 && !string.IsNullOrWhiteSpace(sms.Phone))
                    {
                        postData.Add(new KeyValuePair<string, string>("to", sms.Phone));
                    }
                    else
                    {
                        postData.Add(new KeyValuePair<string, string>("to", personSmsPhone(sms.PersonId)));
                    }
                        

                    postData.Add(new KeyValuePair<string, string>("message", sms.Content));
                    HttpContent content = new FormUrlEncodedContent(postData);

                    HttpResponseMessage response = await client.PostAsync(MessagesUrlPath, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(response.ToString(), "Odpowiedź", MessageBoxButton.OK, MessageBoxImage.Error);
                        OnSmsServiceMalfunction(new ConnectionChangedEventArgs { ConnectionStatus = false, TimeOfChange = DateTime.Now });
                        lastSmsFailed = true;
                    }
                    if (lastSmsFailed)
                    {
                        OnSmsServiceOnLine(new ConnectionChangedEventArgs { ConnectionStatus = true, TimeOfChange = DateTime.Now });
                        lastSmsFailed = false;
                    }
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Wiadomość", MessageBoxButton.OK, MessageBoxImage.Error);
                OnSmsServiceMalfunction(new ConnectionChangedEventArgs { ConnectionStatus = false, TimeOfChange = DateTime.Now });
                lastSmsFailed = true;
            }
            finally
            {

            }
        }

        protected string ConstructBaseUri()
        {
            UriBuilder uriBuilder = new UriBuilder("http", IpAddress, PortNumber);
            return uriBuilder.ToString();
        }

        public event EventHandler<ConnectionChangedEventArgs> SmsServiceMalfunction;

        public event EventHandler<ConnectionChangedEventArgs> SmsServiceOnLine;

        private string personSmsPhone(int personId)
        {
            try
            {
                return prepareNumber(MainDataContext.MainContext.People.Include("Addresses").Where(x => x.Id == personId).Select(y => y.Addresses.First().CellPhone).First());
            }
            catch
            {
                MessageBox.Show("Brak informacji w bazie danych o numerze telefonu osoby.");
            }
            return null;
        }

        private string prepareNumber(string number)
        {
            return Regex.Replace(number, @"[^\d]", ""); ;
        }

        protected virtual void OnSmsServiceMalfunction(ConnectionChangedEventArgs e)
        {
            if (!(SmsServiceMalfunction is null))
            {
                SmsServiceMalfunction(this, e);
            }
        }

        protected virtual void OnSmsServiceOnLine(ConnectionChangedEventArgs e)
        {
            if (!(SmsServiceOnLine is null))
            {
                SmsServiceOnLine(this, e);
            }
        }
    }

    public class GetMessageResponse : BaseResponse
    {
        public List<DeviceMessage> Message { get; set; }
    }

    public abstract class BaseResponse
    {
        public string RequestMethod { get; set; }
        public string Description { get; set; }
        public bool IsSuccessful { get; set; }
    }

    public class DeviceMessage : BaseMessage
    {
        public string MessageType { get; set; }
        public string Receiver { get; set; }
        public string Sender { get; set; }
        public string ThreadId { get; set; }
        public bool Read { get; set; }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "Date = ", Date, "\r\n");
            str = String.Concat(str, "Id = ", Id, "\r\n");
            str = String.Concat(str, "Message = ", Message, "\r\n");
            str = String.Concat(str, "Number = ", Number, "\r\n");
            str = String.Concat(str, "MessageType = ", MessageType, "\r\n");
            str = String.Concat(str, "Receiver = ", Receiver, "\r\n");
            str = String.Concat(str, "Sender = ", Sender, "\r\n");
            str = String.Concat(str, "ThreadId = ", ThreadId, "\r\n");
            str = String.Concat(str, "Read = ", Read, "\r\n");
            return str;
        }
    }

    public abstract class BaseMessage
    {
        public string Date { get; set; }
        public string Id { get; set; }
        public string Message { get; set; }
        public string Number { get; set; }
    }
}
