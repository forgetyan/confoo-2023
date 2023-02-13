# Confoo-2023

Ce repository contiens tous les exemples de code et documents liés à la présentation du confoo 2023 ".Net Nanoframework ou C# pour les Microcontrôleurs" présenté par Yannick Forget.

Je vous recommande la lecture du site officiel du .Net NanoFramework pour plus d'informations:

<https://nanoframework.net/>

## Micro-contrôleur

Le contrôleur utilisé pour la présentation est un ESP-32, monté sur une platine de développement appelé LOLIN-32. Ce "board" peut être trouvé à faible coût sur le marketplace Aliexpress: (Attention, puisqu'il s'agit d'un marketplace chinois, vous devrez travailler votre patience car la réception des produits prend généralement plusieurs seamines. Sinon, vous pouvez utiliser Amazon mais atendez-vous à payer plus cher pour le privilège d'obtenir vos pièces rapidement. Environ 15$ plutôt que 3$ par board)

<https://fr.aliexpress.com/w/wholesale-lolin32.html?SearchText=lolin32>

## Préparatifs

La première chose à faire pour utiliser .Net nanoFramework est d'installer l'outil "Nanoff" (“Nano Firmware Flasher”). On peut l'installer facilement en tappant cette commande:

```terminal
dotnet tool install -g nanoff
```

Ensuite, quand on a le firmware flasher installé, il faut s’en servir pour installer le nanoCLR (Pour Common Language Runtime) sur le board. On va y arriver avec cette commande:

```terminal
nanoff --platform esp32 --serialport COM3 --update
```

Évidemment il faudra choisir votre type de microcontrôleur avec le paramètre “platform” et on devra chosir le bon port série pour communiquer avec le board. Vous pourriez le trouver dans le gestionnaire des périphériques de Windows mais il existe un truc plus simple.

Vous pouvez utiliser nanoff pour lister les port en utilisant une commande “listports”

```terminal
nanoff --listports
```

Source:

<https://docs.nanoframework.net/content/getting-started-guides/index.html>

## Exemples de code

1. [Hello World](/1-HelloWorld/README.md)
2. [Capteur de Température](/2-CapteurTemperature/README.md)
3. [Site Web](/3-Web/README.md)

N'hésitez pas à visiter les exemples de code officiels fournis par nanoFramework
<https://github.com/nanoframework/Samples>
