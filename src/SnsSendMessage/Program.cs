using System;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace SnsSendMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello AmazonSimpleNotificationServiceClient!");

            if (args == default) { throw new ArgumentNullException(nameof(args)); }

            if (args.Length < 1) { throw new ArgumentException("Missing paramaters"); }

            var topicArn = args[0]; // arg[0] - Topic Arn

            var message = "Hello " + args[1] + " at " + DateTime.Now.ToShortTimeString(); // arg[1] - Message

            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials;
            if (chain.TryGetAWSCredentials("ysp", out awsCredentials))
            {
                var client = new AmazonSimpleNotificationServiceClient(region: Amazon.RegionEndpoint.EUWest2, credentials: awsCredentials);

                var request = new PublishRequest()
                {
                    Message = message,
                    TopicArn = topicArn
                };

                try
                {
                    var response = client.PublishAsync(request).GetAwaiter().GetResult();

                    Console.WriteLine($"{message}");
                    Console.WriteLine($"{response.MessageId}");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error occured: {ex.Message}");
                }
            }

        }
    }
}
