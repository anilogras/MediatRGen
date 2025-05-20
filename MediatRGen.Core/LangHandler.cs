using MediatRGen.Core.Exceptions;
using MediatRGen.Core.Languages;
using MediatRGen.Core.States;


namespace MediatRGen.Core
{
    public static class LangHandler
    {
        public static ILang Definitions()
        {
            switch (GlobalState.Instance.Lang)
            {
                case "tr":
                    return new LangTr();
                default:
                    throw new LanguageNotFoundException("Tanımlı Dil Bilgisi Bulunamadı");
            }
        }
    }
}
