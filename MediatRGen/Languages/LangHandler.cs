using MediatRGen.Cli.States;
using MediatRGen.Cli.Exceptions;
using MediatRGen.Cli.Languages;

namespace MediatRGen.Cli.Languages
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
