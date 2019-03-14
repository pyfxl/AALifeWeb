using AALife.Core;
using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Logging;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Core.Services.Messages;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.Data.Services.Messages;
using System;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class CommonSettingsController : BaseAdminController
    {
        private readonly ISettingService _settingService;
        private readonly IEmailSender _emailSender;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IMessageTemplateService _messageTemplateService;

        public CommonSettingsController(ISettingService settingService,
            IEmailSender emailSender,
            ICustomerActivityService customerActivityService,
            IMessageTemplateService messageTemplateService)
        {
            this._settingService = settingService;
            this._emailSender = emailSender;
            this._customerActivityService = customerActivityService;
            this._messageTemplateService = messageTemplateService;
        }

        // GET: Manage/CommonSettings
        public ActionResult Index()
        {
            var model = _settingService.LoadSetting<CommonSettings>(0);
            return View(model);
        }

        [HttpPost]
        [FormValueRequired("save")]
        public virtual ActionResult Index(CommonSettings settings)
        {
            if (ModelState.IsValid)
            {
                _settingService.SaveSetting(settings, 0);
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入网站配置记录。{0}", settings);

            SuccessNotification("添加成功。");

            return View(settings);
        }

        [HttpPost, ActionName("Index")]
        [FormValueRequired("sendtestemail")]
        public virtual ActionResult SendTestEmail(CommonSettings model)
        {
            if (!CommonHelper.IsValidEmail(model.SendTestEmailTo))
            {
                ErrorNotification("发送失败！", false);
                return View(model);
            }

            try
            {
                if (String.IsNullOrWhiteSpace(model.SendTestEmailTo))
                    throw new Exception("Enter test email address");

                var template = _messageTemplateService.GetMessageTemplateByName("EmailTest");

                string subject = template.Subject;
                string body = template.Body;
                _emailSender.SendEmail(model, subject, body, model.Email, model.DisplayName, model.SendTestEmailTo, null);
                SuccessNotification("发送成功。", false);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc.Message, false);
            }

            //If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}