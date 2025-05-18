using MediatRGen.Cli.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Cli.Languages
{
    public class LangTr : ILang
    {
        public string InvalidCommandName => "Girilen Komut Adı Bulunamadı ...";
        public string InvalidParameter => "Parametreler Başında `-` veya `--` Olmalıdır.";
        public string InvalidParamForCreateSolution => "create-solution İşlemi İçin Geçersiz Parametreler Girildi ...";
        public string EnterCommand => "Komut Girin";
        public string ExistDirectory => "Verilen Dosya Yolunda Aynı İsimde Bir Proje Daha Önceden Oluşturulmuş";
        public string ProjectExist => "Daha Önceden Aynı İsimli Proje Oluşturulmuş.";
        public string YouCanWriteCode => "Üstteki Dizine Giderek Kodlamaya Başlayabilirsiniz.";
        public string CreatedConfigFile => "Config Dosyası Oluşturuldu";
        public string ConfigExist => "Config Dosyası Daha Önce  Oluşturulmuş.Tekrardan Oluşturulamaz...";
        public string ConfigNotFoundVersionExist => "Config Dosyasında Versiyon Bilgisi Bulunamadı...";
        public string ConfigNotFound => "Config Dosyasında Bulunamadı...";
        public string ModuleActive => "Modül Sistemi Kullanılacak Mı ?";
        public string GatewayActive => "Gateway Yapılandırılsın Mı ?";
        public string ModuleName => "Modül Adı Girin ...";
        public string ModuleNameIsRequired => "Modül Adı Boş Geçilemez";
        public string UseOchelot => "Api Gateway Olarak Ochelot Aktifleştirilsin Mi?";
        public string Required => "Zorunlu Alan. Değer Girin ...";
        public string ModuleIsDefined => "Modül Daha Önceden Tanımlanmış...";
        public string ClassLibraryCreated => "Kütüphane Oluşturuldu...";
        public string CoreFilesCreated => "Core Kütüphaneler Oluşturuldu...";
        public string ConfigurationCreated => $"Konfigurasyon Dosyası Oluşturuldu ({GlobalState.ConfigFileName})";
        public string ConfigurationUpdated => $"Konfigurasyon Dosyası Güncellendi ({GlobalState.ConfigFileName})";
        public string FolderCreated => "Klasör Oluşturuldu";
        public string FileCreated => "Dosya Oluşturuldu...";
        public string EnterProjectName => "Proje Adı Girin...";
        public string EnterModuleName => "Modül Adı Girin...";
        public string WebApiCreated => "WebApi Katmanı Oluşturuldu...";
        public string NugetPackageCreated => "Nuget Paketi Eklendi...";
        public string ModuleCreated => " Modülü Oluşturuldu...";
        public string EnterEntityName => "Entity Adı Girin...";
        public string DirectoryCreated => "Klasör Oluşturuldu ...";
        public string EntityNotFound => "Entity Bulunamadı...";
        public string ServiceCreated => "Servis Oluşturuldu...";


    }
}
