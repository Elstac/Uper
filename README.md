# Project Title

Uper

# Used libraries/ nuget packages

|    Package    |Version       |
|:--------------|:------------:|
**Microsoft.EntityFrameworkCore.Sqlite**| 2.2.4|
**Xunit** |2.4.1|
**Moq** |4.10.1|
**Microsoft.AspNetCore.Identity**| 2.2.0|
**X.PagedList.Mvc.Core**| 7.6.0|
**MimeKit**| 2.1.5.1|
**MailKit**| 2.1.5.1|
**Microsoft.AspNet.SignalR**| 2.4.1|
**Microsoft.AspNet.SignalR.Client**| 2.4.1|
**Microsoft.AspNet.SignalR.JS**| 2.4.1|
**LinqKit.Microsoft.EntityFrameworkCore**| 1.1.16|
**Syncfusion.Pdf.Net.Core** |17.1.0.48|
**Microsoft.VisualStudio.Web.CodeGeneration.Design** | 2.2.3|

# Installation

Clone project from this repository.

# Configuration

## Database

### Predefined database

To use database filled with predefined data change filed **BuildType** to "testconnection". All date will be saved
in testDB.db file. You can check database using sqlite browser ex. https://sqlitebrowser.org/

### Own database

|BuildType|Description|
|:--------|:---------:|
**SqlServer**|Use TestSqlServerConnection connection string|
**Sqlite**| TestSqliteConnection|

## Smtp Client

To configure Smtp client simply add **passwd.json** file to WebApp directory and write your gmail smtp client credentials
```
{
  "Username": "gmail username",
  "Password": "gmail password"
}
```