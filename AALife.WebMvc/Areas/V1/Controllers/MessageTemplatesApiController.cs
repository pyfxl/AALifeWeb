using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Logging;
using AALife.Core.Domain.Messages;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Core.Services.Messages;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class MessageTemplatesApiController : BaseApiController
    {
        private readonly IMessageTemplateService _messageTemplateService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly ISettingService _settingService;

        public MessageTemplatesApiController(IMessageTemplateService messageTemplateService,
            ICustomerActivityService customerActivityService,
            IQueuedEmailService queuedEmailService,
            ISettingService settingService)
        {
            this._messageTemplateService = messageTemplateService;
            this._customerActivityService = customerActivityService;
            this._queuedEmailService = queuedEmailService;
            this._settingService = settingService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var result = _messageTemplateService.GetAllMessageTemplates();

            var grid = new DataSourceResult
            {
                Data = result,
                Total = result.Count()
            };

            return Json(grid);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]MessageTemplate model)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            //插入
            _messageTemplateService.InsertMessageTemplate(model);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入消息记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]MessageTemplate model)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            var template = _messageTemplateService.GetMessageTemplateById(model.Id);
            template.Name = model.Name;
            template.SystemName = model.SystemName;
            template.Subject = model.Subject;
            template.Body = model.Body;
            template.IsActive = model.IsActive;

            //更新
            _messageTemplateService.UpdateMessageTemplate(template);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Update, "更新消息记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete([FromBody]MessageTemplate model)
        {
            var template = _messageTemplateService.GetMessageTemplateById(model.Id);

            //删除
            _messageTemplateService.DeleteMessageTemplate(template);

            return Json(HttpStatusCode.OK);
        }

        #region 其它方法


        [Route("api/v1/messagetemplatesapi/{id}")]
        public IHttpActionResult SendTest(int id)
        {
            var template = _messageTemplateService.GetMessageTemplateById(id);

            var emailAccount = _settingService.LoadSetting<CommonSettings>(default(Guid));

            var email = new QueuedEmail
            {
                From = emailAccount.Email,
                FromName = emailAccount.DisplayName,
                To = emailAccount.SendTestEmailTo,
                ToName = "",
                Subject = template.Subject,
                Body = template.Body,
                CreatedDate = DateTime.Now
            };

            _queuedEmailService.InsertQueuedEmail(email);

            return Json(HttpStatusCode.OK);
        }

        #endregion

    }
}