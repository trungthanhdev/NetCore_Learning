using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;  // Thêm using này để sử dụng QoS
public class MqttService
{
    private readonly IMqttClient _mqttClient;
    private readonly IMqttClientOptions _mqttOptions;

    public MqttService()
    {
        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();

        // Tạo cấu hình MQTT Client
        _mqttOptions = new MqttClientOptionsBuilder()
            .WithClientId("aspnetcore-client")  // Tạo Client ID
            .WithTcpServer("broker.hivemq.com", 1883)  // Kết nối với MQTT broker (có thể thay bằng broker riêng của bạn)
            .Build();

        // Kết nối broker khi khởi tạo
        ConnectMqttBroker().Wait();
    }

    // Kết nối với MQTT Broker
    private async Task ConnectMqttBroker()
    {
        if (!_mqttClient.IsConnected)
        {
            await _mqttClient.ConnectAsync(_mqttOptions);
        }
    }

    // Phương thức để gửi lệnh qua MQTT
    public async Task PublishCommand(string topic, string payload)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)  // Chọn topic gửi lệnh đến
            .WithPayload(payload)  // Dữ liệu cần gửi
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce)  // Sử dụng QoS level ExactlyOnce
            .WithRetainFlag()  // Lưu giữ thông điệp nếu cần
            .Build();

        if (!_mqttClient.IsConnected)
        {
            await ConnectMqttBroker();
        }

        await _mqttClient.PublishAsync(message);
    }
}
