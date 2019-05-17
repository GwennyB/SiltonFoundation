using Amazon;
using System;
using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Threading.Tasks;

namespace SiltonFoundation.Models
{
    public class Email
    {
        public string Sender { get; set; } = "admin@thesiltonfoundation.org";
        public string Recipient { get; set; }
        public string ConfigSet { get; set; }
        public string Subject { get; set; }
        public string BodyHtml { get; set; }
        public string BodyText { get; set; } = "Sent on behalf of The Silton Foundation using Amazon Simple Email Service";

        public static async Task<bool> Send(Email message)
        {
            bool status = false;

            {
                // Replace USWest2 with the AWS Region you're using for Amazon SES.
                // Acceptable values are EUWest1, USEast1, and USWest2.
                using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USWest2))
                {
                    var sendRequest = new SendEmailRequest
                    {
                        Source = message.Sender,
                        Destination = new Destination
                        {
                            ToAddresses =
                            new List<string> { message.Recipient }
                        },
                        Message = new Message
                        {
                            Subject = new Content(message.Subject),
                            Body = new Body
                            {
                                Html = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = message.BodyHtml
                                },
                                Text = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = message.BodyText
                                }
                            }
                        },
                        // If you are not using a configuration set, comment
                        // or remove the following line 
                        ConfigurationSetName = message.ConfigSet
                    };
                    try
                    {
                        Console.WriteLine("Sending email using Amazon SES...");
                        var response = await client.SendEmailAsync(sendRequest);
                        status = true;
                        Console.WriteLine("The email was sent successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("The email was not sent.");
                        Console.WriteLine("Error message: " + ex.Message);

                    }
                }
                return status;
            }
        }
    }
}
