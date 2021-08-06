# VirtualMind
Test of VirtualMind

**ESPECIFICACIONES TÉCNICAS PARA EL DESARROLLO**

1. Base Datos
   1. Sql Server Management Studio v17.4
   1. Microsoft SQL Server 2016 (RTM) - 13.0.1601.5 (X64)   Apr 29 2016 23:23:58   Copyright (c) Microsoft Corporation  Enterprise Edition (64-bit) on Windows Server 2016 Standard 6.3 <X64> (Build 14393: ) (Hypervisor) 


1. Rest API 
   1. Visual Studio Community 2019
   1. Framework: .NET 5.0
   1. EntityFrameworkCore.SqlServer 5.0.8
   1. EntityFrameworkCore.Tools 5.0.8
   1. JWT 6.11
   1. JwtBearer 5.0.8
   1. BCrypt 4.0.2
   1. RestSharp 10.12.0




1. Aplicación de interfaz de usuario (Cliente)
   1. axios: ^0.21.1,
   1. bootstrap: ^5.1.0,
   1. md5: ^2.3.0,
   1. react: ^17.0.2,
   1. react-bootstrap: ^1.6.1,
   1. react-dom: ^17.0.2,
   1. react-router-dom: ^5.2.0,
   1. react-scripts: 4.0.3,
   1. universal-cookie: ^4.0.4,





**PASOS PARA INICIAR EL PROYECTO**

1. **Base Datos**
   1. Ejecutar los siguiente Scripts
      1. Script para crear Base Datos, Tablas, e Insertar Data de Usuario: DataBaseScript\Crear Base Datos\_Tablas\_Data.sql
      1. **“Usuario”**: mjavier |  “**Password”**: 123456
      1. **“Nota”**: La password esta encriptada con MD5
1. **Rest API** 
   1. Insertar Credenciales de conexión a base de datos (“**Server, User, Password”**) en el API, ubicado en archivo “**appsettings.json”** en el objeto “**ConnectionStrings”** . 
   1. Ejecutar servicio Rest API ubicado en la carpeta: **…\Currency**
   1. Lista de EndPoint.-Puede probarse por la aplicación de POSTMAN
      1. **GET**  tasa del dolar del dia: [**https://localhost:44303/api/Currency/Dolar**](https://localhost:44303/api/Currency/Dolar)
      1. **GET**  tasa del real del dia: [**https://localhost:44303/api/Currency/real**](https://localhost:44303/api/Currency/real)
      1. **POST** Realizar atenticacion para obtener el **TOKEN** : <https://localhost:44303/User/authenticate>
         1. **Body** à  **raw:** 

{

`    `"username": "mjavier",

`    `"password": " e10adc3949ba59abbe56e057f20f883e"

}

1. **POST** Realizar cambio a dolar: [**https://localhost:44303/api/Currency/change**](https://localhost:44303/api/Currency/change)
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

1. **Subir Interfaz de Usuario React (Cliente)**
   1. **Instalar:** Node.js si no lo tiene instalado**.**
   1. **Abrir la siguiente carpeta en Visual Code :** Client\currencyclient
   1. **Abrir la línea de comando in Visual Code: window (Ctrl + `) o Mac shortcut (Command + Shift + P)**
   1. **Ejcutar el siguiente código para descargar paquetes de node\_modules :** npm install
   1. **Una vez descargardos, ejecutar el siguiente código para Ejecutar Cliente Reat.js:** npm start


