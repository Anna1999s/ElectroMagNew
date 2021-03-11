using DomainModel.Framework.Models;
using Google.Cloud.Translation.V2;
using Microsoft.Extensions.Options;

namespace DomainModel.Framework
{
    public class GoogleTranslateClient
    {
        private readonly IOptions<GoogleTranslateConfig> _googleTranslateConfig;

        public GoogleTranslateClient(IOptions<GoogleTranslateConfig> googleTranslateConfig)
        {
            _googleTranslateConfig = googleTranslateConfig;
        }

        public TranslationClient CreateClient()
        {
            var apiKeyTranslate = _googleTranslateConfig.Value.ApiKey;
            TranslationClient clientTranslate = TranslationClient.CreateFromApiKey(apiKeyTranslate, model: TranslationModel.ServiceDefault);
            return clientTranslate;
        }

        public string TranslateToEnglish(string value)
        {
            using (var client = CreateClient())
            {
                var result = client.TranslateText(value, LanguageCodes.English);

                return result.TranslatedText;
            }
        }

        public string TranslateToKorean(string value)
        {
            using (var client = CreateClient())
            {
                var result = client.TranslateText(value, LanguageCodes.Korean);

                return result.TranslatedText;
            }
        }

        public string TranslateToRussian(string value)
        {
            using (var client = CreateClient())
            {
                var result = client.TranslateText(value, LanguageCodes.Russian);

                return result.TranslatedText;
            }
        }
    }
}
