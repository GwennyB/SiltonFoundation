using Amazon;
using System;
using System.Collections.Generic;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

// This class defines programmatically generating and sending emails. It uses AWS's Simple Email Service (SES) and associated SDK for SMTP formatting and authentication.
// Requires that the IAM account associated with the application has SES send permission, and that the account has verified the 'Sender' email in AWS-SES for the specified region endpoint, and that the IAM account has been migrated from 'sandbox' by AWS.

namespace SiltonFoundation.Models
{
    public class Email
    {

        //public IConfiguration Configuration { get; }

        //public Email(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

            // TODO: Change 'Sender' to a TSF account before push
        public string Sender { get; set; } = "gwen@buchthal.com";
        public string Recipient { get; set; }
        public string ConfigSet { get; set; }
        public string Subject { get; set; }
        public string BodyHtml { get; set; }
        public string BodyText { get; set; } = "Sent on behalf of The Silton Foundation using Amazon Simple Email Service";

        public async Task<bool> Send()
        {

            bool status = false;
            

            {
                using (var client = new AmazonSimpleEmailServiceClient(RegionEndpoint.USWest2))
                {
                    var sendRequest = new SendEmailRequest
                    {
                        Source = Sender,
                        Destination = new Destination
                        {
                            ToAddresses =
                            new List<string> { Recipient }
                        },
                        Message = new Message
                        {
                            Subject = new Content(Subject),
                            Body = new Body
                            {
                                Html = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = BodyHtml
                                },
                                Text = new Content
                                {
                                    Charset = "UTF-8",
                                    Data = BodyText
                                }
                            }
                        },
                        // TODO: Remove or define ConfigSet 
                        // ConfigurationSetName = ConfigSet
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
