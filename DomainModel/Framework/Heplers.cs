using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Localization;

namespace DomainModel.Framework
{
    public class Heplers
    {
        public static Localization.Localization Localization(string value, string code)
        {
            if (code == Culture.RuCode)
            {
                var result = new Localization.Localization
                {
                    CultureCode = Culture.RuCode,
                    Value = value
                };
                return result;
            }
            else if (code == Culture.EnCode)
            {
                var result = new Localization.Localization
                {
                    CultureCode = Culture.EnCode,
                    Value = value
                };
                return result;
            }
            else if (code == Culture.KoCode)
            {
                var result = new Localization.Localization
                {
                    CultureCode = Culture.KoCode,
                    Value = value
                };
                return result;
            }
            else
            {
                var result = new Localization.Localization
                {
                    CultureCode = Culture.RuCode,
                    Value = value
                };
                return result;
            }
        }
    }
}
