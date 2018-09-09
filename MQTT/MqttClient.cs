using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;

namespace owntrack.MQTT
{
    public class MqttClient
    {
        private readonly MqttFactory mqttFactoy;
        private readonly ILogger<MqttClient> logger;
        private readonly MqttSettings mqttSettings;
        private readonly Queue<MqttMessage> messageQueue;

        public MqttClient(ILogger<MqttClient> logger, MqttSettings mqttSettings)
        {
            this.logger = logger;
            this.mqttSettings = mqttSettings;
            this.messageQueue = new Queue<MqttMessage>();
            this.mqttFactoy = new MqttFactory();
        }

        public void QueueMessage(MqttMessage mqttMessage)
        {
            this.messageQueue.Enqueue(mqttMessage);
            TryPublishMessages();
        }

        private async Task TryPublishMessages()
        {
            var client = mqttFactoy.CreateMqttClient();
            var options = new MqttClientOptionsBuilder()
                          .WithClientId(mqttSettings.ClientId)
                          .WithTcpServer(mqttSettings.ServerIpOrDns, mqttSettings.Port)
                          .WithCredentials(mqttSettings.Username, mqttSettings.Password)
                          .WithCleanSession()
                          .Build();
            try
            {
                await client.ConnectAsync(options);
            }
            catch (Exception e)
            {
                logger.LogError("Error while connecting to MQTT Server: " + e.ToString());
                return;
            }
            logger.LogInformation("Succesfull connected to MQTT Server");

            MqttMessage curMsg;
            var failedMessages = new List<MqttMessage>();
            while (messageQueue.TryDequeue(out curMsg))
            {
                var msg = new MqttApplicationMessageBuilder()
                            .WithTopic(curMsg.Topic)
                            .WithPayload(curMsg.Message)
                            .Build();
                try
                {
                    await client.PublishAsync(msg);
                }
                catch (Exception e)
                {
                    failedMessages.Add(curMsg);
                    logger.LogError("Failed to send message: " + e.ToString());
                }
                logger.LogInformation("Published Message sucessfully");
            }

            await client.DisconnectAsync();
            client.Dispose();

            foreach (var msg in failedMessages)
            {
                messageQueue.Enqueue(msg);
            }
        }
    }
}