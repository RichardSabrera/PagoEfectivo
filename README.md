# PagoEfectivo

Backend Technical Test - PagoEfectivo (Resolución)

Inicializar el proyecto.

Base de Datos

1. En la carpeta DB se encontrará un script que generará la Base de datos a usar.
2. Abrir y ejecutar el cual generará las tablas y procedimientos.

Configuración WsApiEfectivo

1. Abrir los archivos "appsettings.json" y "appsettings.Development.json".
2. Ubicar la variable "DevConnection" y modificar a la cadena del servidor de base de datos a usar.


Configuración AppPagoEfectivo
1. Abrir los archivos "appsettings.json" y "appsettings.Development.json"
2. Ubicar la variable "URL_API" y modificar la url del servicio.


Configuración WsApiEfectivoTests
1. abrir todos los archivos .cs  
2. Modificar la variable "sqlConn" que contiene la conexión a la base de datos

Iniciar los proyectos WsApiEfectivo y AppPagoEfectivo al mismo tiempo.
1. Ir a Propiedades/Propiedades ccomunes
2. Seleccionar Proyecto de inicio
3. Marcar la opción de proyectos de inicio múltiples
4. Seleccionar Iniciar en WsApiEfectivo y AppPagoEfectivo


Nota: Por el momento no se estan considerando los CORS

