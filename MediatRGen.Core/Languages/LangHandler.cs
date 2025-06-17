using MediatRGen.Core.Exceptions;


namespace MediatRGen.Core.Languages
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
