using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCore_Learning.Dtos.Account.request;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Models;

namespace NetCore_Learning.Controllers
{
    [ApiController]
    [Route("api/gate")]
    public class GateController : ControllerBase
    {
        private readonly MqttService _mqttService;

        public GateController(MqttService mqttService)
        {
            _mqttService = mqttService;
        }

        // API để mở cổng
        [HttpPost("open")]
        public async Task<IActionResult> OpenGate()
        {
            await _mqttService.PublishCommand("gate/control", "open");  // Gửi lệnh mở cổng
            return Ok("Mở cổng thành công");
        }

        // API để đóng cổng
        [HttpPost("close")]
        public async Task<IActionResult> CloseGate()
        {
            await _mqttService.PublishCommand("gate/control", "close");  // Gửi lệnh đóng cổng
            return Ok("Đóng cổng thành công");
        }
    }
}