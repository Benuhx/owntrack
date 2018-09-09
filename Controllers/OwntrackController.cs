using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using owntrack.MQTT;

namespace owntrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwntrackController : ControllerBase
    {
        private readonly ILogger<OwntrackController> logger;
        private readonly MqttClient mqttClient;
        public OwntrackController(ILogger<OwntrackController> logger, ILogger<MqttClient> loggerMqttClient, IOptions<MqttSettings> mqttSettings)
        {            
            this.logger = logger;
            this.mqttClient = new MqttClient(loggerMqttClient, mqttSettings.Value);
        }

        // Just for testing
        [HttpGet]
        public string Get()
        {
            return "Server is active";
        }

        // Owntracks sends HTTP Post Request with the schema defined in OwntrackData.cs
        // Owntrack Docs: https://owntracks.org/booklet/tech/json/
        [HttpPost]
        public void Post([FromBody] OwntrackData owntrackData)
        {
            logger.LogInformation("Owntrack HTTP POST request received");

            var mqttMsg = new MqttMessage
            {
                Topic = "mobile/fire/batteryLevel",
                Message = Convert.ToString(owntrackData.Batt)
            };

            mqttClient.QueueMessage(mqttMsg);

            logger.LogInformation("Request done");
        }
    }
}