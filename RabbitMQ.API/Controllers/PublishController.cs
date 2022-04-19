using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Manager.Abstract;
using RabbitMQ.Manager.Model;
using RabbitMQ.Utils.Model;

namespace RabbitMQ.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : Controller
    {
        private readonly IMessageService _messageService;
        public PublishController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpPost]
        public IActionResult Index(PublishRequestModel publishRequestModel)
        {
            try
            {
                bool response = _messageService.PublishMessage(publishRequestModel.Queue, publishRequestModel.Message);
                return response ? Ok() : BadRequest();
            }
            catch (Exception ex)
            {

                return Json(new { error = ex.Message });
            }
         
        }
    }
}
