using System.Collections.Generic;
using Abp.Localization;

namespace Pfizer.QueueSystem.Web.Models
{
    public class LanguageSelectionViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        public string CurrentUrl { get; set; }
    }
}