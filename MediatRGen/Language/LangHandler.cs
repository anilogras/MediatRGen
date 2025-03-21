using MediatRGen.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MediatRGen.Validator;

namespace MediatRGen.Language
{
    public static class LangHandler
    {
        public static ILang Definitions()
        {
            switch (GlobalState.instance.Lang)
            {
                case "tr":
                    return new LangTr();
                case "en":
                    return new LangEn();
                default:
                    throw new LanguageException("Tanımlı Dil Bilgisi Bulunamadı");
            }
        }


    }
}
