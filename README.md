# todoApp

Projeyi ayağa kaldırmak için docker-compose dosyasının bulunduğu yerde komut satırını açın ve docker-compose up ifadesini yazın.

# Database Konfigürasyonu için aşağıdaki adımları uygulayın.#

CouchbaseDB uygulamasının arayüzüne ulaşmak için http://localhost:8091 adresine gidin.
Setup New Cluster seçeneğini seçin.
#
Gelen arayüzde 
# Cluster name alanına:Appcent#
# Admin kullanıcı adına: appcentdemo#
# Şifre kısımlarına : appcentdemo# yazın ve ilerleyin.
Şartları kabul edin ve finish with default seçeneğini ile kurulumu tamamlayın.
#
Ekranın sol tarafındaki buckets kısmına girin ve sağ üstteki ADD BUCKET butonuna tıklayarak yeni bir bucket oluşturma ekranına ulaşın.Açılan pencerede Bucket ismine küçük harfler ile todo yazın ve Memory Quota kısmını 500 yapın.Ardından Add Bucket butonuna tıklayın.
#
Daha sonra soldaki menüden Query kısmına gidin ve aşağıdaki queryleri ekleyip Execute ederek index alanlarını belirtin.
# 
CREATE PRIMARY INDEX `todo_primary` ON `todo`
CREATE INDEX `idx_ts_type` ON `todo`(`type`);
CREATE INDEX `idx_ts_id` ON `todo`(`id`);
CREATE INDEX `idx_ts_email` ON `todo`(`email`);
CREATE INDEX `idx_ts_userid` ON `todo`(`userid`);
#
Database işlemlerini yaptıktan sonra http://localhost:5001 adresine gidin ve Angular uygulamasında gelen ekrandan kayıt olduktan sonra giriş yapın.
