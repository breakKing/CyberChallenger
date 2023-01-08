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

2. Для каждого из проектов проделать следующие операции:
   1. скопировать из папки _ConfigTemplates_ файл _appsettings.Development.json_ в корень проекта (на один уровень с _.csproj_ файлом);
   2. скопировать из папки _ConfigTemplates_ файл _launchSettings.Development.json_ в папку _Properties_ соответствующего проекта, переименовав файл в _launchSettings.json_; 
   3. изменить параметры в _appsettings.Development.json_ под свою конфигурацию (адреса сервисов, БД и т.д.).

### Production