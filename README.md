# CyberChallenger

## Описание

Платформа для проведения соревнований по CS:GO.

## Подготовка

### Development

1. Склонировать репозиторий.

2. Для проекта _App_ проделать следующие операции:
   1. Скопировать из папки _ConfigTemplates_ файл _appsettings.Development.json_ в корень проекта (на один уровень с _.csproj_ файлом);
   2. Изменить параметры в _appsettings.Development.json_ под свою конфигурацию (адреса сервисов, БД и т.д.).

3. Сгенерировать RSA-ключи для подписывания и валидации JWT-токенов в сервисе _IdentityProviderService_. Это можно сделать при помощи OpenSSL:
   ```
   openssl genrsa -out private.pem 2048
   openssl rsa -in private.pem -outform PEM -pubout -out public.pem
   ```
   Вместо _private.pem_ и _public.pem_ использовать названия файлов в соответствии с _appsettings.Development.json_.

4. По аналогии с пунктом 3 сгенерировать RSA-ключи для шифрования JWT-токенов. 

5. Обе пары ключей перетащить в папку _/src/App/RsaKeys_.

### Production

TODO - продумать, когда это будет нужно и составить инструкцию.  
Нужно будет взаимодействие с _ConfigTemplates/appsettings.Production.json_.