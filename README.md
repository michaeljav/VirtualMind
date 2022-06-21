# VirtualMind
Test of VirtualMind

**TECHNICAL SPECIFICATIONS FOR DEVELOPMENT**

1. **Database**
   1. Sql Server Management Studio v17.4
   1. Microsoft SQL Server 2016 (RTM) - 13.0.1601.5 (X64)   Apr 29 2016 23:23:58   Copyright (c) Microsoft Corporation  Enterprise Edition (64-bit) on Windows Server 2016 Standard 6.3 <X64> (Build 14393: ) (Hypervisor) 


1. **Rest API Backend** 
   1. Visual Studio Community 2019
   1. Framework: .NET 5.0
   1. EntityFrameworkCore.SqlServer 5.0.8
   1. EntityFrameworkCore.Tools 5.0.8
   1. JWT 6.11
   1. JwtBearer 5.0.8
   1. BCrypt 4.0.2
   1. RestSharp 10.12.0




1. **User interface application (Client)**
   1. axios: ^0.21.1,
   1. bootstrap: ^5.1.0,
   1. md5: ^2.3.0,
   1. react: ^17.0.2,
   1. react-bootstrap: ^1.6.1,
   1. react-dom: ^17.0.2,
   1. react-router-dom: ^5.2.0,
   1. react-scripts: 4.0.3,
   1. universal-cookie: ^4.0.4,





**STEPS TO START THE PROJECT**

1. **Data base**
   1. Run the following Scripts
      1. Script to create Database, Tables, and Insert User Data: DataBaseScript\Create Database\_Tables\_Data.sql
      1. **“User”**: mjavier |  “**Password”**: 123456
      1. **“Note”**: The password is encrypted with MD5
1. **Rest API** 
   1. Insert database connection credentials (“**Server, User, Password”**) in the API, located in the “**appsettings.json”** file in the “**ConnectionStrings”** object.
   1. Run Rest API service located in folder: **…\Currency**
   1. EndPoint list.-Can be tested by POSTMAN application
      1. **GET**  dollar rate of the day: [**https://localhost:44303/api/Currency/Dolar**](https://localhost:44303/api/Currency/Dolar)
      1. **GET**  real rate of the day: [**https://localhost:44303/api/Currency/real**](https://localhost:44303/api/Currency/real)
      1. **POST** Perform authentication to obtain the **TOKEN**: <https://localhost:44303/User/authenticate>
         1. **Body** à  **raw:** 

{

`    `"username": "mjavier",

`    `"password": " e10adc3949ba59abbe56e057f20f883e"

}

1. **POST** Make change to dollar: [**https://localhost:44303/api/Currency/change**](https://localhost:44303/api/Currency/change)
   1. **Body** à  **raw:** 

{

`    `"CbuyCurrencyOrigenAmount": "12",

`    `"CbuyCurrencyToBuyType": "dolar",

`     `"toSave":false

}

1. Headers
   1. “**KEY**”: Authorization     “**VALUE**”:<TOKEN GENERADO EJ: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VJZCI6IjMiLCJuYmYiOjE2MjgyNjA4NTIsImV4cCI6MTYyOTk4ODg1MiwiaWF0IjoxNjI4MjYwODUyfQ.NYwVx56O9fcOb159pYHN6\_h5Xr6OrE183H4CyJ1vtIQ>

1. **POST** Realizar cambio a real: [**https://localhost:44303/api/Currency/change**](https://localhost:44303/api/Currency/change)
   1. **Body** à  **raw:** 

{

`    `"CbuyCurrencyOrigenAmount": "12",

`    `"CbuyCurrencyToBuyType": " real ",

`     `"toSave":false

}

1. Headers
   1. “**KEY**”: Authorization     “**VALUE**”:<TOKEN GENERADO EJ: eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VJZCI6IjMiLCJuYmYiOjE2MjgyNjA4NTIsImV4cCI6MTYyOTk4ODg1MiwiaWF0IjoxNjI4MjYwODUyfQ.NYwVx56O9fcOb159pYHN6\_h5Xr6OrE183H4CyJ1vtIQ>

1. **Launch React UI (Client)**
   1. **Install:** Node.js if you don't have it installed**.**
   1. **Open the following folder in Visual Code :** Client\currentcyclient
   1. **Open command line in Visual Code: window (Ctrl + `) or Mac shortcut (Command + Shift + P)**
   1. **Run the following code to download packages from node\_modules :** npm install
   1. **Once downloaded, run the following code to Run Reat.js Client:** npm start
   
   
##### Images Backend API


![](Swager.png)

##### Images Client

**Login**

![](Login.png)

**currency exchange**

![](CurrencyExchange.png)

**Successful currency exchange**
![](Successful.png)

**Records saved successfully**

![](Records%20Saved.png)

