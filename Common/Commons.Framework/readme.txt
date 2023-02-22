How to start new MVC Project

 1 - put Commons.Framework, and Commons.NotificationsService below commons solution folder
 2 - put UsersMgmt below UsersMgmt solution folder
 3 - 
	Create your dal and add edmx and set propery model namespace to your Dal project namespace
	delete t4 template used in generating entities
	edit t4 template of context and add new using line for your model
	Note: Select Edmx and press F4 then Change Namespace Property to The Project Namespace

 4 - Create Model library and add Entities.t4 template. Edit template to point to your new edmx
 5 - 
	create Bll Class Library and add folder repositories and unitofwork t4 template
	edit repositories t4 template to point to edmx
6 - add folder UserManagent by
 in Web
7 - Class Library Project Resources

Add DateModelBinder to global ascx file  to enable hijri and gregorian dates binding 

 reference file commons.js in your layouts files. This file contains very useful and nessessary code to our web app 
  - language change
  - easy way to enable autopostback in mvc
  - auto adding max-length to all inputs
  - fix max length for textarea in IE, add text counter to all text areas
  - cookie setting and reading utils
  - localization support in javascript

Helpers add folder widgets to root of your web app folder.

in web.congig add 

	connection string named CommonsDbEntities. 
	this connection string.
	Used for "System Settings", "Notification Queue", "Logging", and "Notifications Templates" 
	USED BY asp.net IDENTITY (Same as next conn string. This is Code First used by identity and second one is database first used by our code)
	<add name="CommonsDbEntities" connectionString="data source=10.2.2.52;initial catalog=MciRegulations_Staging;user id=mciRegulationsUser;password=P$ssw0rd;integrated security=False;MultipleActiveResultSets=True;application name=EntityFramework" providerName="System.Data.SqlClient" />

add this to web.config  <modules> section
	<add name="GlobalizationModule" type="Commons.Framework.Globalization.GlobalizationModule, Commons.Framework, Version=1.0.0.0, Culture=neutral" />
	<add name="RenderRegisteredScriptsModule" type="Commons.Framework.Web.Mvc.RenderRegisteredScriptsModule, Commons.Framework" />

 t4 templates
 localizations
 common localizations
 alert messages
 
 lookup caching and expiring
 common settings for caching period, .....

 data annotations and metadata classes
 automapper can be used but limited to custom viewmodels

 .....

 lookupsdata
 lookupshelper
 logging
 exceptions
 base controller
 mvc helpers
 culturehelper
 css styling
 
 tables description

 -------------------------- todo ----------------------------
 user management / commonsframework with dbfirst transactions with 
 take attachment attachment content legal rep, ... out of user management
 add missing tables in code first of commons.framework like notificationsendstatus

 ---------------------

If changes is made to tabled of asp.net users run below commands

Code First Tables for Asp.Net Identity in UsersMgmt Project
Enable-Migrations -EnableAutomaticMigrations -ProjectName Commons.UsersMgmt
Update-Database –Verbose -ProjectName Commons.UsersMgmt

Code First Tables for Commons
Enable-Migrations -EnableAutomaticMigrations -ProjectName Commons.Framework -Force
Add-Migration -Name InitialMigration -ConfigurationTypeName Commons.Framework.Migrations.Configuration
Update-Database –Verbose -ProjectName Commons.Framework  -ConfigurationTypeName Commons.Framework.Migrations.Configuration


----------------------
Log4Net: After Creating Table Common.Log you must put the default value (newsequentialid()) for the Id Coulmn,
		Because Log4Net throw an Exception Cannot insert the value NULL into column 'Id'
-----------------------

javascript for validating Muqeem and Citizen Identity Number from Yaser to common.js