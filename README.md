Photo Award Demo
================
#Einführung
Demo Anwendung für die Service Fabric. Gezeigt werden folgende Punkte:
1. Web API-Projekt mit Angular-Frontend-Anwendung
2. Stateless Service zur Thumbnail Generierung
3. Zwei Statefull-Services 
4. Zwei Actor-Services
5. Web Api-Projekt für die Administration. Diese kann nur lokal aufgerufen werden
* Backup erzeugen: http://localhost:8208/api/administration/backup/NameOfTheBackup
* Backup einspielen: http://localhost:8208/api/administration/restore/NameOfTheBackup

Anwendungsfälle:
1. Benutzer kann sich registrieren
2. Thumbnails von Fotos werden angezeigt
3. Fotos können hochgeladen werden.
4. Für jedes Foto können Kommentare eingegeben werden
5. Alle Kommentare eines Fotos werden angezeigt. 
6. Aktuell nur Command-Line-Interface:
* - Passwort ändern


#Hinweis
Dieses Projekt demonstriert die verschiedenen Möglichkeiten der Service Fabric. Für einen produktiven Einsatz ist es nicht geeignet.

#Einführung
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
Um alle Funktionen testen zu können sind in Cloud.xml, Local.1Node.xml oder Local.5Node.xml noch einige Anpassungen vorgenommen werden:
* Parameter PhotoDBConfig_authKey: Authentifizierungsschlüssel für den Zugriff auf die DocumentDb
* Parameter PhotoDBConfig_endpoint: Adresse unter der die DocumentDB erreicht werden kann, z.B.: https://localhost:8081/
* Parameter PhotoDBConfig_database: Name der DocumentDB-Datenbank, z.B. "PhotoAwardDemo" 
* Parameter PhotoDBConfig_collection: Name der DocumentDb collection, z.B. "Photos"
* Parameter PhotoActorConfig_CognitiveServiceUri: Adresse des Cognitive Services zur Bildanalyse
* Parameter PhotoActorConfig_OcpApimSubscriptionKey": Zugriffschlüssel zum Zugriff auf die Cognitive Services
* Parameter PhotoAwardBackupRestoreFileStoreType: Der Wert "local" gibt an dass die Bakcupdateien in das im Parameter "PhotoAwardBackupDirectory" angegebenes Verzeichnis geschrieben werden. Der Wert "azureblobstorage" schreibt die Backupdateien in ein Azure Blob Storage. Vergl. den Parameter PhotoAwardAzureStorageConnectionString .
* Parameter PhotoAwardBackupDirectory: In welches Verzeichnis soll das Backup geschrieben werden. Steht nur bei eine PhotoAwardBackupRestoreFileStoreType "local" zur Verfügung.
    <Parameter Name="PhotoAwardAzureStorageConnectionString" Value="" />
* Parameter PhotoAwardAzureStorageConnectionString": Connection-String zum AzureBlobStorage

#4.Build and Test
Hinweis: Die Angular4-Unittests sind NICHT lauffähig. Out of Scope für diese Demo 


