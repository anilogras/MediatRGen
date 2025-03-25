using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Languages
{
    public class LangTr : ILang
    {
        public string InvalidCommandName => "Girilen Komut Adı Bulunamadı ...";
        public string InvalidParameter => "Parametreler Başında `-` veya `--` Olmalıdır.";
        public string InvalidParamForCreateSolution => "create-solution İşlemi İçin Geçersiz Parametreler Girildi ...";
        public string EnterCommand => "Komut Girin";
        public string ExistDirectory => "Verilen Dosya Yolunda Aynı İsimde Bir Proje Daha Önceden Oluşturulmuş";
        public string FileExist => "Daha Önceden Aynı İsimli Proje Oluşturulmuş.";
        public string YouCanWriteCode => "Üstteki Dizine Giderek Kodlamaya Başlayabilirsiniz.";

    }
}
