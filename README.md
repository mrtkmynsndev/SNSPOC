# SNSPOC

# What is SNS?
SNS pub/sub patterni ele alan uygulamadan uygulamaya (A2A) ya da uygulamadan kişiye (A2P) iletişim için yönetilen mesajlaşma hizmetidir.

# Topic and Subscription
* Topic - Erişim noktası gibi düşünebiliriz. 
  * Örneğin bir sipariş oluşumunda olabilecek **Topic** - Create Order.
  * Birden fazla endpoint gruplamamıza olanak sağlar. Örneğin Create Order 
* Subscriptions - Set up each defferent customers
Format: ![Image of Topic&Subsc](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_2.png)

## Pub/Sub Pattern 
* Pub publisher (mesaj yayınlayan) ve Sub Subscriber (mesajı alan)
Format: ![Image of PubSub](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_1.png)
* Sns "event producer" bir mesaj atar ve birden çok "event reciever" (subscriptions) SNS topic dinlemeye başlar.
* Her bir subscriber SNS topic'ten gelen mesajı alırlar eğer bi **filtre** yok ise 
* Her bir **Topic** maksimum 10 milyon subscriber olabilir. (very high scale sağlıyor)
* 10.000 **Topic** limiti oluşturulabiliyor.
* Subscribers:
  * SQS (Simple Queue Service)
  * HTTP/HTTPS Endpoints
  * Lambda (serverless)
  * SMS messages
  * Mobile Notifications
* SNS amazon tarafından full yönetilen ve otomatik scale edilen bir hizmettir. Yani infrastructure, host, instance servisleri hakkında endişelenmenize gerek yok.
* SNS mesajları kaybolmamasını garanti altına alır. (Diğer tarafa ulaşmasını garanti altına almaz :) )
