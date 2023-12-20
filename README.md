<h3>WhatsappBro Nedir ?</h3>
<p>WhatsappBro SignalR teknolojisi ile çalışan bir mesajlaşma uygulamasıdır.</p>

<h4>Özellikler</h4>
<p>Arkadaş eklemek</p>
<p>Arkadaşlar ile mesajlaşmak</p>

<h4>Kullanılan Teknoloji ve Mimariler</h4>
Onion Architecture
CQRS
SignalR
ASP.NET CORE MVC
BOOTSTRAP
JQUERY

<h4>Projenin kuruluşu</h4>
<ul>
  <li><p>appsettings.json dosyası içerisindeki ConnectionStrings ' i değiştir.</p>
<code>{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": { "DefaultConnection": "Server=DESKTOP-36N2808;Database=WhatsappBro;Trusted_Connection=true;" },
  "AllowedHosts": "*"
}
</code>
    </li>
  <li>
    <p>
    Package Manager Console daki Default Project seçim listesinden, <strong>src\Presentation\WebUI ' ı seç</strong></p>
   <ul><li> <code>add-migration [migration name]</code></li>
   <li> <code>update-database</code></li></ul>
    <p>yaz ve veritabanı oluştuktan sonra uygulamayı çalıştır.</p>
    <p>Kayıt ol ve giriş yap.</p>
  </li>
</ul>

<h4>Arkadaş Ekleme</h4>
<ul><li>Alttaki menüden ortadakini seç ve yukarıdaki arkadaş ekle butonuna tıkla.</li>
<li>Sisteme kayıt olan bütün kullanıcılar arama kutucuğuna yazılan kullanıcı adları ile bulunabilir. Ve eklenebilir.</li>

<h4>Mesaj Gönderme</h4>
<ul>
  <li>Eklediğiniz arkadaşlarınızın isimlerinin yanındaki mesaj gönder butonuna tıkladığınızda sağ tarafta bir mesaj içerik alanı açılır. Alt taraftaki kutucuğa mesaj girip gönder dediğinizde, mesaj gönderdiğiniz kişi ile bir sohbet başlar. Bu
  sohbeti, en aşşağıdaki menüden en sağdaki seçeneği (mesajlar) sekmesini seçtiğinizde görebilirsiniz.</li>
</ul>
