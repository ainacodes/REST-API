# REST-API
REST API using **ASP.NET Core Web API** framework. This API shows how to access our database with [GET], [POST], [PUT] and [DELETE] in my case using MySQL workbench. 

### Requirements
- Visual Studio
- MySQL Workbench

### 1. Clone repo
```
git clone https://github.com/ainacodes/REST-API.git
```

### 2. Create table in database
Make sure you have `MemberID`, `Name`, `Email`, `Password` and `PhoneNumeber` column and enter a few dummy data in it. 
You can enter the data through API later on.


### 3. Connect to the database using your own credentials
In `DBConnectionService` file at `line 14` fill it with your database credentials.


### 4. Run the API
In `Visual Studio` build/rebuild the solution. If there is no error, select `IISEXPRESS` to run the code. It will automatically open the browser.

![rest-api](https://github.com/3nacodes/REST-API/blob/main/img/rest-api.PNG)
