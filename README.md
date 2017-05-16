Photo Award Demo
================
#Introduction
Demo Anwendung für die Service Fabric. Gezeigt werden folgende Punkte:
1. Web API-Projekt mit Angular2-Frontend-Anwendung
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
5. Angedacht: 
- Wenn ein Foto im Detail aufgerufen wird, werden alle Kommentare angezeigt.
- Fotos werden im Original in einer DocumentDb abgelegt werden


#Hinweis
Dieses Projekt demonstriert die verschiedenen Möglichkeiten der Service Fabric. Für einen produktiven Einsatz ist es nicht geeignet.

#Getting Started
1. Voraussetzungen
Vorraussetzung für das Projekt ist dass Sie das Service Fabric auf dem Entwicklungsrechner installiert ist. Eine ausführliche Anleitung finden Sie unter https://docs.microsoft.com/en-us/azure/service-fabric/service-fabric-get-started
2.	Build
Um die Anwendung kompilieren zu können müssen zuerst alle NPM-Pakete installiert sein. Normalerweise geschieht dies direkt beim Öffnen von Visual Studio. 
Damit das WebAPI-Projekt kompiliert werden kann muss die Angular-Anwendung gebaut sein. 
Bitte ruft vor dem Build-Vorgang ng build auf. Außerdem muss Visual Studio auch die NuGet-Pakete per Restore einspielen.
3.  Azure
Um das Projekt unter Azure zu testen muss Ihr Cluster mit einem SSL-Zertifikat gesichert sein. Bitte ändern Sie den in der Datei ApplicationManifest aufgeführten 
Thumbprint auf den Thumbprint ihres SSL-Zertifikats und entfernt die Kommentare in
ApplicationManifest.xml im Knoten , in dem Sie die Anfürungszeihcen entfernen.
<Policies>
      <!-- <EndpointBindingPolicy EndpointRef="ServiceEndpointssl" CertificateRef="TestCert1" />  -->
</Policies>
Außerdem muss in der ApplicationManifest der Fingerprint des Zertifikats angepasst werden:
<Certificates>
    <EndpointCertificate X509StoreName="MY" X509FindValue="2B5C7A6BDFCE84CC7559977375D384494CC3D2A5" Name="TestCert1" />
</Certificates>

#4.Build and Test
Hinweis: Die Angular4-Unittests sind NICHT lauffähig. Out of Scope für diese Demo 


