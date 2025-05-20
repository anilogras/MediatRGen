using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;


namespace MediatRGen.Services
{
    public static class LangHandler
    {
        public static ILang Definitions()
        {
            switch ("tr")
            {
                case "tr":
                    return new LangTr();
                default:
                    throw new LanguageNotFoundException("Tanımlı Dil Bilgisi Bulunamadı");
            }
        }
    }
}
