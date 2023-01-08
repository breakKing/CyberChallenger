# CyberChallenger

## Описание

Платформа для проведения соревнований по CS:GO.

## Используемые порты

_GatewayApi_ - 5000 (http), 7000 (https).  
_IdentityProviderService_ - 5001 (http), 7001 (https).  
_UserProfileService_ - 5002 (http), 7002 (https).  
_TeamService_ - 5003 (http), 7003 (https).  
_GameService_ - 5004 (http), 7004 (https).  
_TournamentService_ - 5005 (http), 7005 (https).  

## Подготовка

### Development

1. Склонировать репозиторий.

2. Для каждого из C#-проектов проделать следующие операции:
   1. Скопировать из папки _ConfigTemplates_ файл _appsettings.Development.json_ в корень проекта (на один уровень с _.csproj_ файлом);
   2. Изменить параметры в _appsettings.Development.json_ под свою конфигурацию (адреса сервисов, БД и т.д.).

### Production

TODO - продумать, когда это будет нужно и составить инструкцию.  
Нужно будет взаимодействие с _ConfigTemplates/appsettings.Production.json_.