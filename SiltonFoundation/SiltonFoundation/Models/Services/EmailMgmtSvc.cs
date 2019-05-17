using SiltonFoundation.Models;
using SiltonFoundation.Models.Interfaces;
using Amazon;
using System;
using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Threading.Tasks;
using SiltonFoundation.Models.Utils;

namespace SiltonFoundation.Models.Services
{
    public class EmailMgmtSvc : IEmailManager
    {
        public async Task<bool> Send(Email message)
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
                            Subject = new Content(message.Recipient),
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
