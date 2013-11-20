using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _30Fans.Misc {
    public interface ILanguageHandler {
        void SetLanguage(string language);
        void ApplyCurrentLanguage();
    }
}