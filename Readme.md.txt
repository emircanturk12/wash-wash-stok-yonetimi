# Wash & Wash – Stok Yönetim Sistemi

ASP.NET Core 8 ile geliştirilen, ürün ve stok takibini kolaylaştıran web tabanlı bir stok yönetim uygulamasıdır. Yönetici ve kullanıcı panelleri sayesinde stok hareketleri ve stok talepleri tek bir sistem üzerinden yönetilebilir.

## Özellikler

- **Ürün Yönetimi:** Ürün ekleme, düzenleme ve ürünleri listeleme.
- **Stok Takibi:** Stok artırma ve azaltma işlemleri.
- **Kritik Stok Takibi:** Stok seviyesi düşük ürünleri takip etme.
- **Stok Talepleri:** Kullanıcıların stok talebi oluşturabilmesi.
- **Yönetici Paneli:** Gelen talepleri görüntüleme ve yönetme.
- **Arama ve Kategori:** Ürünleri arama ve kategorilere göre filtreleme.
- **Envanter Maliyeti:** Stokların maliyetini takip etme.
- **Kullanıcı Arayüzü:** Karanlık mod desteği.

## Kullanılan Teknolojiler

- C#
- ASP.NET Core 8
- Razor Pages
- Entity Framework Core
- Microsoft SQL Server
- HTML, CSS ve JavaScript

## Gereksinimler

- .NET 8 SDK
- Microsoft SQL Server
- Visual Studio 2022 veya uyumlu bir geliştirme ortamı

## Kurulum

1. Projeyi bilgisayarınıza indirin veya klonlayın.
2. Projeyi Visual Studio ile açın.
3. SQL Server üzerinde `StokYonetimDB` isimli bir veritabanı oluşturun.
4. Projenin `appsettings.example.json` dosyasını örnek alarak `appsettings.json` dosyası oluşturun.
5. `appsettings.json` içerisindeki `DefaultConnection` bağlantı dizesini kendi SQL Server yapılandırmanıza göre düzenleyin.
6. Gerekli veritabanı tablolarını oluşturun. Projede migration bulunuyorsa migration'ları uygulayın; aksi durumda projenin tablo şemasını kullanın.
7. Gerekli NuGet paketlerini geri yükleyin.
8. Projeyi çalıştırın.

> **Not:** Veritabanı tablolarının oluşturulması, projenin mevcut veritabanı şemasına bağlıdır. Kurulumdan önce projedeki `Data` klasörünü ve varsa migration dosyalarını kontrol edin.

## Güvenlik

- Gerçek veritabanı parolalarınızı ve bağlantı bilgilerinizi GitHub'a yüklemeyin.
- `appsettings.json` dosyasını Git dışında tutun.
- Örnek yapılandırma dosyasında gerçek parola veya hassas bilgi bulundurmayın.

## Geliştirici

**Emir Cantürk**

Bilgisayar Programcılığı öğrencisi | Yazılım geliştirme ve veritabanı uygulamaları

Admin Paneli:admin
Şifre:1234
Kullanıcı paneli:emir1
Şifre:12345