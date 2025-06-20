﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Languages
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
        public string CreatedConfigFile => "Config Dosyası Oluşturuldu...";
        public string ConfigExist => "Config Dosyası Daha Önce  Oluşturulmuş.Tekrardan Oluşturulamaz...";
        public string ConfigNotFoundVersionExist => "Config Dosyasında Versiyon Bilgisi Bulunamadı...";
        public string ConfigNotFound => "Config Dosyasında Bulunamadı...";
        public string ModuleActive => "Modül Sistemi Kullanılacak Mı ?";
        public string GatewayActive => "Gateway Yapılandırılsın Mı ?";
        public string GatewayNotActive => "Gateway Yapılandırılması Yapılmadı...";

        public string ModuleName => "Modül Adı Girin ...";
        public string ModuleNameIsRequired => "Modül Adı Boş Geçilemez";
        public string UseOchelot => "Api Gateway Olarak Ochelot Aktifleştirilsin Mi?";
        public string Required => "Zorunlu Alan. Değer Girin ...";
        public string ModuleIsDefined => "Modül Daha Önceden Tanımlanmış...";
        public string ClassLibraryCreated => "Kütüphane Oluşturuldu...";
        public string CoreFilesCreated => "Core Kütüphaneler Oluşturuldu...";
        public string ConfigurationCreated => $"Konfigurasyon Dosyası Oluşturuldu...";
        public string ConfigurationUpdated => $"Konfigurasyon Dosyası Güncellendi...";
        public string FolderCreated => "Klasör Oluşturuldu";
        public string ClassNotFound => "Class Bulunamadı...";
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
        public string NugetPackagesCreated => "NuGet Paketleri Oluşturuldu...";
        public string NameSpaceNotFound => "Namespace Bulunamadı...";
        public string NugetPackageDeleted => "Nuget Paketleri Silindi";
        public string DirectoryDeleted => "Klasör Silindi...";
        public string DirectoryDeleteError => "Klasör Silme Hatası ....";
        public string DirectoryCreateError => "Klasör Oluşturulamadı...";
        public string DirectoryPathError => "Klasör Yolu Bulunamadı...";
        public string DirectoryPathCreated => "Klasör Yolu Oluşturuldu...";
        public string FileCreateError => "Dosya Oluşturulurken Hata Oluştu...";
        public string FileNotFound => "Dosya Bulunamadı...";
        public string FileReadError => "Dosya Okunamadı...";
        public string ConfigUpdated => "Config Güncellendi...";
        public string ConfigUpdateError => "Config Güncellenemedi...";
        public string FileFounded => "Dosya Bulundu...";
        public string FileFindError => "Dosya bulunamadı...";
        public string ClassLibraryCreateError => "Kütüphane Oluşturulamadı...";
        public string ClassLibraryNameCreated => "Kütüphane Adı Oluşturuldu";
        public string WebApiCreateError => "Web Api Oluşturulamadı...";
        public string ArgsSplited => "Args. Parse Edildi...";
        public string ParameterParseError =>"Parametre Parse İşleminde Hata Oluştu...";
        public string ParameterParsed => "Parametreler Parse Edildi...";
        public string PropertySetted => "Property Set Edildi...";
        public string PropertyNotFountOrWeritable => "Property Bulunamadı veya Set Edilebilir Değil...";
        public string ProcessInvoked => "İşlem Çalıştırıldı...";
        public string ProcessInvokeError => "İşlem Çalıştırılmasında Hata Oluştu";
        public string ClassLibraryBuild => "Class Library Derlendi...";
        public string ReWriteClassError => "Class Güncellenemedi...";
        public string ClassLibraryBuildError => "Class Library Derlenemedi...";
        public string ReWriteClass => "Class Güncellendi...";
        public string BaseClassSet => "Base Class Set Edildi...";
        public string BaseClassSetError => "Base Class Implementasyonunda Hata oluştu...";
        public string ClassCreated => "Class Oluşturuldu...";
        public string ClassCreateError => "Class Oluşturulamadı...";
        public string NameSpaceChanged => "Namespace Güncellendi...";
        public string NameSpaceChangeException => "Namespace Değiştirilken Hata Oluştu...";
        public string BackSlashClearError => "Backslash Temizlenirhen Sorun Oluştu...";
        public string UsingAdded => "Using Eklendi...";
        public string ParameterPropertySetError => "Parametre set edilirken Sorun Oluştur...";
        public string NameSpaceFound => "NameSpace Bulundu...";
    }
}
