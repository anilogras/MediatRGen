using MediatRGen.Exceptions;
using MediatRGen.States;

namespace MediatRGen.Languages
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
