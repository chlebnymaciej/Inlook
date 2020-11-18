Witam w moim proadniku przedstawię wam dzisiaj jak uzywać zenętrzengo API do projektu maila.

1. Utworzenie serwisu na serwerze, który będzie się komunikował z external API do notyfikacji, z podstawowymi metodami (create, ...).
2. Mail Controller będzie za pomocą utworzonego serwisu wysyłał do external API, POST message za każdsym razem jak ktoś wyśle maila (dla każdego z odbiorców osobno).
3. Aplikacja kliencka będzie "nasłuchiwać", na tym serisie, nowych wiadomości i kiedy otrzyma nowe powidomienie, wyświetli je użytkownikowi (jeśli aplikacja jest włącznona), lub wyśle notifikacje do systemu operacyjnego (jeśli jest nieaktywna, zależne od systemu).

Thanks for watching,
dont forget to subscribe, leave a like and comment.
Also turn on the notification button to never miss our new videos.
Bye