Photo Award Demo
================
#Introduction
Demo Anwendung für die Service Fabric. Gezeigt werden folgende Punkte:
1. Web API-Projekt mit Angular-Frontend-Anwendung
2. Stateless Service zur Thumbnail Generierung
3. Zwei Statefull-Services 
4. Zwei Actor-Services

Anwendungsfälle:
1. Benutzer kann sich registrieren
2. Thumbnails von Fotos werden angezeigt
3. Fotos können hochgeladen werden.
4. Aktuell nur Command-Line-Interface:
- Benutzer wählt ein Foto aus und bewertet es..
- Zusätzlich können auch Kommentare geschrieben werden.
- Passwort ändern
5. Für jedes Foto können Kommentare eingegeben werden
6. Alle Kommentare eines Fotos werden angezeigt. 

#Hinweis
Dieses Projekt demonstriert die verschiedenen Möglichkeiten der Service Fabric. Für einen produktiven Einsatz ist es nicht geeignet.

#Getting Started
1. Voraussetzungen
Vorraussetzung für das Projekt ist dass Sie das Service Fabric SDK auf dem Entwicklungsrechner installiert haben. Eine ausführliche Anleitung finden Sie unter https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started .
Damit aus der Anwendung der Cognitive Service zur Bildanalyse aufgerufen werden kann müssen Sie sich einen Zugriffstoken erzeugen lassen und diesen in der Klasse AnalyzeRepository im PhotoActor-Projekt eintragen.
Weitere Informationen zu den Cognitiv Services erhalten Sie unter https://azure.microsoft.com/de-de/services/cognitive-services/computer-vision/ .
2.	Build
Um die Angular-Anwendung kompilieren zu können müssen zuerst alle NPM-Pakete installiert sein. Bitte rufen Sie hierzu  in der Console im Verzeichnis PhotoAward.AngularClient npm install auf. Außerdem benötigen Sie noch die Windows Build Tools und  das AngularCLI-Tool: 
npm install -g windows-build-tools
npm install -g @angular/cli 
Nach erfolgter Installation können Sie über den Aufruf von "ng build" die Anwendung kompilieren.  
Anschließend öffnen Sie bitte Visual Studio und können die Anwendung kompilieren.
3.  Azure
Um das Projekt unter Azure zu testen muss Ihr Cluster mit einem SSL-Zertifikat gesichert sein. Bitte ändern Sie den in der Datei ApplicationManifest aufgeführten 
Thumbprint auf den Thumbprint ihres SSL-Zertifikats und entfernen Sie die Kommentare in ApplicationManifest.xml im Knoten.
<Policies>
      <!-- <EndpointBindingPolicy EndpointRef="ServiceEndpointssl" CertificateRef="TestCert1" />  -->
</Policies>
Außerdem muss in der ApplicationManifest der Fingerprint des Zertifikats angepasst werden:
<Certificates>
    <EndpointCertificate X509StoreName="MY" X509FindValue="2B5C7A6BDFCE84CC7559977375D384494CC3D2A5" Name="TestCert1" />
</Certificates>

#4.Build and Test
Hinweis: Die Angular4-Unittests sind NICHT lauffähig. Out of Scope für diese Demo 


