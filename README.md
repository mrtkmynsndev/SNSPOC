# What is SNS?
SNS pub/sub patterni ele alan uygulamadan uygulamaya (A2A) ya da uygulamadan kişiye (A2P) iletişim için yönetilen mesajlaşma hizmetidir.

### Application to Person
A2P push notification örneğini verebiliriz. Örneğin e-commerce projemizde kullanıcılara ya da özel kullanıcılara promosyon mesajları atıp kullanıcıların mobil, websitesi veya diğer kaynaklara erişimini artırabiliriz.

![Image of A2P](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_3.png)

### Application to Application
A2A için müşterinin ürün satın aldığı süreci örnek olarak verebiliriz. Müşteri ürünü satın aldıktan sonra ürünle ilgili bir takım analizler ya da farklı işlemler yapmak için kullanabiliriz. 

![Image of A2P](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_a2a.png)

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


# Topic
Topic - Erişim noktası gibi düşünebiliriz. 
  * Örneğin bir sipariş oluşumunda olabilecek **Topic** - Create Order.
  * Birden fazla endpoint gruplamamıza olanak sağlar. Örneğin Create Order 

# Subscription
Topic (erişim noktalarını) dinleyen/abone olan servisler.

![Image of Topic&Subsc](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_2.png)

### Type of endpoint to subscribe
* Amazon Knesis Data Firehouse
* Amazon SQS
* AWS Lambda
* HTTP/HTTPS
* SMS
* Email

### Subscription filter policy
Subscription aldığı mesajları filtreleyebilirler. Varsayılan olarak, topic tarafından publish edilen her message subscription tarafından alınır.

![Image of Filter Policy](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_filter_policy.png)

# Encryption
* publish message in-transit modunda default olarak encrypte ediliyor.
* Eğer verilerimiz hassas bilgiler içiriyorsa ve encrypte etmek istiyorsak (müşterilerin tckn'si gibi...) encryption modunu kullanbiliriz. (server side encryption)
* SNS verilerin korunması için CMK oluşturmuyoruz. CMK (Customer Master Key) sayesinde SNS verileri korunuyor. 

# Access Policy
* Access policy sayesinde topic'lere kimlerin erişebileceği ve topiclerin nasıl kullanabileceğimizi yönetebiliyoruz.
* Varsayılan ayar default geliyor.
* Basic ve Advanced modu var. Basic modunda topic'e kimlerin publish yapabileceğini ya da kimlerin topic'e subscribe olabileceğini yönetebiliyoruz.
* Policy sayesinde ne yapabiliriz.
  * Belirli aws hesaplarının topic'e erişimesine izin verebiliriz.
  * Subsription'lara limit koyabiliriz. Örneğin HTTPS protocol, 
  * AWS SQS queue mesaj atabiliriz.
  * AWS kaynaklarının topic'e yayın yapmasına izin verebiliriz.
```
{
  "Version": "2008-10-17",
  "Id": "__default_policy_ID",
  "Statement": [
    {
      "Sid": "__default_statement_ID",
      "Effect": "Allow",
      "Principal": {
        "AWS": "*"
      },
      "Action": [
        "SNS:Publish",
        "SNS:RemovePermission",
        "SNS:SetTopicAttributes",
        "SNS:DeleteTopic",
        "SNS:ListSubscriptionsByTopic",
        "SNS:GetTopicAttributes",
        "SNS:Receive",
        "SNS:AddPermission",
        "SNS:Subscribe"
      ],
      "Resource": "arn:aws:sns:eu-west-2:493815254296:CustomerOrderFanout",
      "Condition": {
        "StringEquals": {
          "AWS:SourceOwner": "493815254296"
        }
      }
    }
  ]
}
 ```

# Delivery Retry
* AWS SNS'in mesajı iletme konusunda başarısız olduğu durumları nasıl ele alacağını tanımlandığı yer. (Ör: başarısız olduğu durumda kaç kere deneyeceğinin tanımları vs...)
* Varsayılan ayar default geliyor.
* Policy'deki bazı tanımlar
  * **Number of retry:** Gecikmenin kaç kere yapılacağının tanımı. (Ör: 3 kere herhangi bir endpoint 200 OK mesajı gelmediğinde deneyeceği deneme sayısı)
  * **Retries without delay:** Gecikmeden yapılacak yeniden deneme sayısı (mümkün olan en kısa sürede). (best practice bir sonraki denemede delay koymak)
  * **Minumum delay:** Yeniden deneme için minumum gecikme tanımı.(Ör: topic'e gönderilen mesaj başarısız oldu ve hemen tekrar deneme 10sn sonra tekrar dene)
  * **Maximum delay:**  Yeniden deneme için maksimum gecikme tanımı.
  * **Minimum delay retries:**  Gecikme başlatmadan önce minDelayTarget aralıklarında yapılacak yeniden deneme sayısı.
  * **Maximum delay retries:**  Gecikme başlatmadan önce maxDelayTarget aralıklarında yapılacak yeniden deneme sayısı.
  * **Retry-backoff function:** Gecikme modeli: Doğrusal, Üstel veya Aritmetik.

  ```
   {
     "http": {
       "defaultHealthyRetryPolicy": {
         "numRetries": 3,
         "numNoDelayRetries": 0,
         "minDelayTarget": 20,
         "maxDelayTarget": 20,
         "numMinDelayRetries": 0,
         "numMaxDelayRetries": 0,
         "backoffFunction": "linear"
       },
       "disableSubscriptionOverrides": false
     }
   }
   ```

# Delivery Status Logging
AWS SNS farklı endpointlere sahip (HTTP, Lambda, SQS, Application)  topic'e gönderilen mesajların teslim durumunu kaydeder. (CloudWatch'a kaydeder)
* **Success Sample Rate:** teslim edilen başarılı mesajların yüzdesi. 100% yaptığımız topic'e gelen her bir mesajı CloudWatch'a kaydeder. (CloudWatch log maliyetlidir.)

# Tags
AWS SNS Topic lere atanan metadata etiketler. Bu etiketler sayesinde AWS SNS kaynaklarını izleyebiliriz.
* Örneğin birden fazla topic var ve belirli topiclerde fatura etiketi kullandık ve bu etiketi kullanarak fatura ile ilgili topicleri izleyebiliriz.
* Her bir etkiet key ve value alanlarından oluşur.
