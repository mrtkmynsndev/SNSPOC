# SNSPOC

# What is SNS?
SNS pub/sub patterni ele alan uygulamadan uygulamaya (A2A) ya da uygulamadan kişiye (A2P) iletişim için yönetilen mesajlaşma hizmetidir.

## Pub/Sub Pattern 
* Pub publisher (mesaj yayınlayan) ve Sub Subscriber (mesajı alan)
Format: ![Image of PubSub](https://github.com/mrtkmynsndev/SNSPOC/blob/main/images/sns_1.png)
* Sns "event producer" bir mesaj atar ve birden çok "event reciever" (subscriptions) SNS topic dinlemeye başlar.
* Her bir subscriber SNS topic'ten gelen mesajı alırlar eğer bi **filtre** yok ise 

