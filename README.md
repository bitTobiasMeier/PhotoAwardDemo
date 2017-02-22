Photo Award Demo
================
#Introduction
Demo Anwendung für die Service Fabric. Gezeigt werden folgende Punkte:
1. Web API-Projekt mit Angular2-Frontend-Anwendung
2. Stateless Service zur Thumbnail Generierung
3. Zwei Statefull-Services 
4. Zwei Actor-Services

#Hinweis
Dieses Projekt demonstriert die verschiedenen Möglichkeiten der Service Fabric. Für einen produktiven Einsatz ist es nicht geeignet.

#Getting Started
1. Voraussetzungen
Vorraussetzung für das Projekt ist dass Sie das Service Fabric auf dem Entwicklungsrechner installiert ist. Eine ausführliche Anleitung finden Sie unter https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started
2.	Build
Um die Anwendung kompilieren zu können müssen zuerst alle NPM-Pakete installiert sein. Normalerweise geschieht dies direkt beim Öffnen von Visual Studio. 
Damit das WebAPI-Projekt kompiliert werden kann muss die Angular-Anwendung gebaut sein. Die Demo ist so eingerichtet dass der NPM-Build-Task "Build" von Visual Studio
vor jedem Compile-Vorgang aufgerufen wird. Allerdings kann es vorkommen dass dies beim ersten Öffnen des Projekts nicht direkt geschieht. Rufen Sie in diesem Fall bitte 
manuell den Task "build" im Task Runner Explorer auf.
Um das Projekt unter Azure zu testen muss ihr Cluster mit einem SSL-Zertifikat gesichert sein. Bitte ändern Sie den in der Datei ApplicationManifest aufgeführten 
Thumbprint auf den Thumbprint ihres SSL-Zertifikats



#Build and Test
Hinweis: Die Angular2-Unittests sind NICHT lauffähig. Out of Scope für diese Demo 

