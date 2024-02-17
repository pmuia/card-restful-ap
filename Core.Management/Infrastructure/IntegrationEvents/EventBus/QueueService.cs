using Amazon.SQS.Model;
using Amazon.SQS;
using Core.Management.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Core.Management.Infrastructure.IntegrationEvents.EventBus
{
    public class QueueService : IQueueService
    {
        private readonly EventSetting setting;
        private readonly AmazonSQSClient sqsClient;
        private readonly ILogger<QueueService> logger;

        public QueueService(ILogger<QueueService> logger, IOptions<EventSetting> options, AmazonSQSClient sqsClient)
        {
            this.logger = logger;
            setting = options.Value;
            this.sqsClient = sqsClient;
        }

        public async Task<(bool successful, string messageId)> EnqueueMessage(dynamic payload)
        {
            try
            {
                SendMessageResponse response = await sqsClient.SendMessageAsync(setting.QueueUrl,
                    JsonSerializer.Serialize(payload, options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, Converters = { new JsonStringEnumConverter() } })).ConfigureAwait(false);

                return (response.HttpStatusCode == HttpStatusCode.OK, response.MessageId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error queueing action event - {ex?.Message}");
                throw;
            }
        }
    }
}
