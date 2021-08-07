# What is SNS?
SNS pub/sub patterni ele alan uygulamadan uygulamaya (A2A) ya da uygulamadan kişiye (A2P) iletişim için yönetilen mesajlaşma hizmetidir.
* Application to Person

# Pub/Sub Pattern 
* Pub (publisher) (mesaj yayınlayan) ve Sub (Subscriber) (mesajı alan)
* publisher mesajı göndermeden sorumlu, subsciber mesajı almadan sorumlu
* pub ile sub arasında 1 to many ilişkisi var. pub'ın yayınladığı bir mesaj birden çok sub'a ulaşır.
* pub/sub model bir nevi fanout modele benziyor. (**fanout**: mesajı her yere ilet)

![Image of PubSub](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_1.png)

# The orher properties of SNS?
* Sns "event producer" bir mesaj atar ve birden çok "event reciever" (subscriptions) SNS topic dinlemeye başlar.
* Her bir subscriber SNS topic'ten gelen mesajı alırlar eğer bir **filtre** yok ise 
* Her bir **Topic** maksimum 10 milyon subscriber olabilir. (very high scale sağlıyor)
* 10.000 **Topic** limiti oluşturulabiliyor.
* Gönderilen mesaj birden fazla **consumer(tüketici)** tarafına iletilebilir. 
* Subscribers:
  * SQS (Simple Queue Service)
  * HTTP/HTTPS Endpoints
  * Lambda (serverless)
  * SMS messages
  * Mobile Notifications
* SNS amazon tarafından full yönetilen ve otomatik scale edilen bir hizmettir. Yani infrastructure, host, instance servisleri hakkında endişelenmenize gerek yok.
* SNS mesajları kaybolmamasını garanti altına alır. (Diğer tarafa ulaşmasını garanti altına almaz :) ) (deadLetterQueue)


# Topic and Subscription
* Topic - Erişim noktası gibi düşünebiliriz. 
  * Örneğin bir sipariş oluşumunda olabilecek **Topic** - Create Order.
  * Birden fazla endpoint gruplamamıza olanak sağlar. Örneğin Create Order 
* Subscriptions -Erişim noktalarını dinleyen kanal gibi düşünebiliriz.

![Image of Topic&Subsc](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_2.png)



