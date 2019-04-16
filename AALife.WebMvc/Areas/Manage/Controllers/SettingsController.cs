using AALife.Core;
using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Logging;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Core.Services.Messages;
using AALife.Data;
using System;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class SettingsController : BaseAdminController
    {
        private readonly ISettingService _settingService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IEmailSender _emailSender;
        private readonly IMessageTemplateService _messageTemplateService;

        public SettingsController(ISettingService settingService,
            ICustomerActivityService customerActivityService,
            IEmailSender emailSender,
            IMessageTemplateService messageTemplateService)
        {
            this._settingService = settingService;
            this._customerActivityService = customerActivityService;
            this._emailSender = emailSender;
            this._messageTemplateService = messageTemplateService;
        }

        #region defaults

        // GET: Manage/DefaultSettings
        public ActionResult Index()
        {
            var model = _settingService.LoadSetting<DefaultSettings>(default(Guid));
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Index(DefaultSettings settings)
        {
            if (ModelState.IsValid)
            {
                _settingService.SaveSetting(settings, default(Guid));
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入默认配置记录。{0}", settings);

            SuccessNotification("添加成功。");

            return View(settings);
        }

        #endregion

        #region sites

        // GET: Manage/SiteSettings
        public ActionResult Sites()
        {
            var model = _settingService.LoadSetting<SiteSettings>(default(Guid));
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Sites(SiteSettings settings)
        {
            if (ModelState.IsValid)
            {
                _settingService.SaveSetting(settings, default(Guid));
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入网站配置记录。{0}", settings);

            SuccessNotification("添加成功。");

            return View(settings);
        }

        #endregion

        #region commons
        
        // GET: Manage/CommonSettings
        public ActionResult Commons()
        {
            var model = _settingService.LoadSetting<CommonSettings>(default(Guid));
            return View(model);
        }

        [HttpPost]
        [FormValueRequired("save")]
        public virtual ActionResult Commons(CommonSettings settings)
        {
            if (ModelState.IsValid)
            {
                _settingService.SaveSetting(settings, default(Guid));
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入基础配置记录。{0}", settings);

            SuccessNotification("添加成功。");

            return View(settings);
        }

        [HttpPost, ActionName("Commons")]
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

                var template = _messageTemplateService.GetMessageTemplateByName("TestEmail");

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

        #endregion
    }
}