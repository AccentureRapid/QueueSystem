using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Security.AntiForgery;
using Pfizer.QueueSystem.Authorization;
using Pfizer.QueueSystem.Authorization.Roles;
using Pfizer.QueueSystem.Authorization.Users;
using Pfizer.QueueSystem.MultiTenancy;
using Pfizer.QueueSystem.Sessions;
using Pfizer.QueueSystem.Web.Controllers.Results;
using Pfizer.QueueSystem.Web.Models;
using Pfizer.QueueSystem.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Abp.Events.Bus;
using Pfizer.QueueSystem.Web.Models.Events;

namespace Pfizer.QueueSystem.Web.Controllers
{
    public class QueueSystemController : QueueSystemControllerBase
    {
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly LogInManager _logInManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly ILanguageManager _languageManager;
        private readonly ITenantCache _tenantCache;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IEventBus _eventBus;

        public QueueSystemController(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            IMultiTenancyConfig multiTenancyConfig,
            LogInManager logInManager,
            ISessionAppService sessionAppService,
            ILanguageManager languageManager, 
            ITenantCache tenantCache, 
            IAuthenticationManager authenticationManager,
            IEventBus eventBus)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
            _multiTenancyConfig = multiTenancyConfig;
            _logInManager = logInManager;
            _sessionAppService = sessionAppService;
            _languageManager = languageManager;
            _tenantCache = tenantCache;
            _authenticationManager = authenticationManager;
            _eventBus = eventBus;
        }

        public ActionResult Queue(string returnUrl = "")
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}