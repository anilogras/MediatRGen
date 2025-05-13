using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Languages
{
    public class LangEn
    {
        public string InvalidCommandName => "Girilen Komut Adı Bulunamadı ...";
        public string InvalidParameter => "Parametreler başında `-` veya `--` olmalıdır.";

        public string InvalidParamForCreateSolution => "create-solution işlemi için geçersiz parametreler girildi ...";

        public string EnterCommand => "Komut Girin";

        public string ExistDirectory => "Verilen Dosya Yolunda Aynı İsimde Bir Proje Daha Önceden Oluşturulmuş";
    }
}
