![Build Status](https://teamcity.johnrowley.co/app/rest/builds/buildType:(id:PamlAlerter_Build)/statusIcon)

#PAML Watchdog

##Introduction
The Watchdog is a fairly simple application. It creates alerts based on events and rules. The events, rules, and alerts are all stored inside of a mssql database. 

A different application should be used to notify based on the alerts.

##What does what, and a high level overview of things
###WatchdogDatabaseAccessLayer
Contains the classes required for interfacing with the database. It is used by any project that needs to interface with the database.

###AdministrationPortal
The AdministrationPortal is an ASP.NET MVC 5 application that allows for the creation and management of rules, message-types, alert-types, groups, escalationchains, etc etc.

###UserPortal
Allows for users to acknowledge alerts which prevents alert escalation from occuring, write notes based on an alert, and for them to do this all from a mobile friendly site.

###WatchdogMessageGenerator
Creates messages and places them in the database. Used for testing.

###WatchdogDaemon
Polls the database and creates alerts based on the rules.
