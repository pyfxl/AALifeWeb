using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Domain.Messages;
using AALife.Data.Services;
using AALife.Data.Services.Messages;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            var result = _messageTemplateService.Get();

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
            _messageTemplateService.Add(model);

            //activity log
            _customerActivityService.InsertActivity(1, ActivityLogType.Insert, "插入消息记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]MessageTemplate model)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            var template = _messageTemplateService.Get(model.Id);
            template.Name = model.Name;
            template.SystemName = model.SystemName;
            template.Subject = model.Subject;
            template.Body = model.Body;
            template.IsActive = model.IsActive;

            //更新
            _messageTemplateService.Update(template);

            //activity log
            _customerActivityService.InsertActivity(1, ActivityLogType.Update, "更新消息记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete([FromBody]MessageTemplate model)
        {
            var template = _messageTemplateService.Get(model.Id);

            //删除
            _messageTemplateService.Delete(template);

            return Json(HttpStatusCode.OK);
        }

        #region 其它方法


        [Route("api/v1/messagetemplatesapi/{id}")]
        public IHttpActionResult SendTest(int id)
        {
            var template = _messageTemplateService.Get(id);

            var emailAccount = _settingService.LoadSetting<CommonSettings>(0);

            var email = new QueuedEmail
            {
                From = emailAccount.Email,
                FromName = emailAccount.DisplayName,
                To = emailAccount.SendTestEmailTo,
                ToName = "",
                Subject = template.Subject,
                Body = template.Body,
                CreatedDate = DateTime.UtcNow
            };

            _queuedEmailService.InsertQueuedEmail(email);

            return Json(HttpStatusCode.OK);
        }

        #endregion

    }
}