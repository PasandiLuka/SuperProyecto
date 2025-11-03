# Documentación del Proyecto Boletería Digital

Este documento contiene toda la información técnica y funcional del proyecto.

## Contenido

### Composición del proyecto

- [Arquitectura en capas](#arquitectura-en-capas)
- [Persistencia de datos con Dapper](#persistencia-de-datos-con-dapper)
- [Autenticación y autorización con JWT](#autenticación-y-autorización-con-jwt)
- [Endpoints principales de la API REST](#endpoints-principales-de-la-api-rest)
- [Pruebas unitarias y Moq](#pruebas-unitarias-y-moq)
- [Documentación con Swagger](#documentación-con-swagger)
- [Implementacion IResult en servicios](#implementacion-iResult-en-servicios)
- [Generación y validación de códigos QR](#generación-y-validación-de-códigos-qr)
- [Creacion de Qr](#creación-de-url)
- [Manejo de Usuarios MySQL](#manejo-de-usuarios-mysql)
- [Creación de la BD](#creación-de-la-base-de-datos)

### Diagramas

- [Documentación diagramas](DIAGRAMAS.md)

---

## Arquitectura en capas

### El proyecto sigue una arquitectura en capas que separa responsabilidades
- **La capa Core (dominio)** contiene las clases de entidades, interfaces de repositorios y servicios, así como validadores de reglas de negocio, sin referencias a infraestructura.
- **La capa Dapper (infraestructura)** implementa esos repositorios usando ADO.NET y Dapper (micro-ORM). 
- **La capa Services** implementa la lógica de negocio coordinando las operaciones entre servicios y repositorios. 
- **La capa API** contiene los controladores ASP.NET Core y la configuración del pipeline (inyección de dependencias, autenticación JWT, Swagger, etc.). 
- **La capa Tests** incluye las pruebas unitarias. Gracias a esta separación, las capas internas definen interfaces y las implementaciones concretas se inyectan en tiempo de ejecución (Principio de Inversión de Dependencias).

---

## Persistencia de datos con Dapper

### La persistencia se realiza con Dapper, un micro-ORM ligero y de alto rendimiento para .NET
- Dapper permite ejecutar consultas SQL directamente y mapear resultados a objetos C#. 
- Cada entidad principal tiene un repositorio específico (por ejemplo `RepoEvento`, `RepoOrden`) que implementa interfaces definidas en Core. 
- El uso del patrón Repositorio separa la lógica de acceso a datos de la lógica de negocio, facilitando la inyección de dependencias y las pruebas unitarias.

---

## Autenticación y autorización con JWT

### Para seguridad se implementa JWT (JSON Web Token) en la API
- Al iniciar sesión, el servidor emite un token firmado que el cliente incluye en el encabezado `Authorization: Bearer <token>` en cada petición. 
- ASP.NET Core valida este token en cada endpoint, extrayendo la identidad del usuario a partir de las claims. 
- Se gestionan roles (Administrador, Organizador, Cliente, Default) para autorizar permisos. 
- Los endpoints de autenticación son:  
  `/auth/register`, `/auth/login`, `/auth/refresh`, `/auth/logout`, `/auth/me` y otros para gestión de roles de usuarios.

---

## Endpoints principales de la API REST

### Se exponen recursos RESTful con respuestas JSON y códigos HTTP estándar (200, 201, 204, 400, 401, 403, 404). 

Los endpoints cubren CRUD (Create, Read, Update, Delete) para cada entidad clave y operaciones específicas:

- **Locales:** POST /locales, GET /locales, GET /locales/{id}, PUT /locales/{id}, DELETE /locales/{id}.
- **Sectores:** POST /locales/{id}/sectores, GET /locales/{id}/sectores, PUT /sectores/{id}, DELETE /sectores/{id}.
- **Eventos:** POST /eventos, GET /eventos, GET /eventos/{id}, PUT /eventos/{id}, POST /eventos/{id}/publicar, POST /eventos/{id}/cancelar.
- **Funciones:** POST /funciones, GET /funciones, GET /funciones/{id}, PUT /funciones/{id}, POST /funciones/{id}/cancelar.
- **Tarifas:** POST /tarifas, GET /funciones/{id}/tarifas, PUT /tarifas/{id}, GET /tarifas/{id}.
- **Clientes:** POST /clientes, GET /clientes, GET /clientes/{id}, PUT /clientes/{id}.
- **Órdenes de Compra:** POST /ordenes, GET /ordenes, GET /ordenes/{id}, POST /ordenes/{id}/pagar, POST /ordenes/{id}/cancelar.
- **Entradas (Tickets):** GET /entradas, GET /entradas/{id}, POST /entradas/{id}/anular.
- **Usuarios y autenticación:** POST /auth/register, POST /auth/login, POST /auth/refresh, POST /auth/logout, GET /auth/me, GET /auth/roles, POST /usuarios/{id}/roles.
- **Códigos QR:** GET /entradas/{id}/qr (genera código QR para la entrada); POST /qr/validar (valida un QR escaneado).

> Estos endpoints siguen convenciones REST(Representational State Transfer - Transferencia de Estado Representacional) y pueden probarse fácilmente usando Swagger UI.

---

## Pruebas unitarias y Moq

### Hay un proyecto de pruebas (xUnit) que verifica la lógica de negocio y de acceso a datos. 

- Se definen casos de prueba claros siguiendo buenas prácticas de nomenclatura. 
- Para simular dependencias externas (como llamadas a la base de datos), se usa Moq, un framework de simulación de objetos open-source muy popular en .NET.
- Moq permite crear mocks de repositorios y servicios, verificando que las capas superiores actúen según lo esperado sin tocar recursos reales.

---

## Documentación con Swagger

### Se integra Swagger/OpenAPI (mediante Swashbuckle) para generar documentación interactiva de la API.

- Cada acción pública en los controladores es accesible y ejecutable desde esa interfaz, lo que permite agregar parámetros y probar el método directamente con “Try it out!”.
- Esto agiliza el desarrollo y la validación. 
- En `Program.cs` también se configura la lectura de parámetros (appsettings, cadenas de conexión) y se registra la inyección de dependencias necesaria.

---

## Implementacion IResult en servicios

### Como estoy utilizando servicios, yo tengo que saber el status de una operación mediante una respuesta HTTPS(Ok, Created, Unauthorized, BadRequest).

- Para lograr eso hago uso de un enum el cual "simula" las respuestas HTTPS y guarda la información del resultado obtenido por el metodo.
- Una vez hecho eso, dentro de mi endpoint, llamo al servicio, obtengo su respuestas y la "traduzco" a la respuesta HTTPS correcta mediante el metodo de extención ToMinimalResult().
<br><br>
- Implementacion del metodo ToMinimalResult() como un metodo normal:
~~~C#
var respuesta = service.GetClientes();
var resultExtencions = new ResultExtencions();
return resultExtencions.ToMinimalResult(respuesta);
~~~

- En cambio si implementamos el metodo de extencion, el codigo nos quedaria asi:
~~~C#
var respuesta = service.GetClientes();
return respuesta.ToMinimalResult();
~~~

---

## Generación y validación de códigos QR

### La generación de códigos QR se maneja con la librería QRCoder para .NET

- Al procesar una orden de compra pagada, se emite un código QR único por cada entrada (en formato PNG/Base64) que incluye los datos firmados. 
- El endpoint de validación (`POST /qr/validar`) verifica la firma y el estado de la entrada, devolviendo resultados como `Ok`, `YaUsada`, `Expirada`, `FirmaInvalida` o `NoExiste` según las reglas de negocio definidas en la consigna.

---

## Creación de Url

### Los links se crean mediante una libreria de AspNetCore la cual me permite crear estas url de manera dinamica.

- Al combinar la libreria LinkGenerator y IHttpContextAccessor logro obtener la direccion y puertos donde se levanta el servidor y automaticamente mappear la ruta del endpoint al que quiero acceder, indicandole los valores necesarios para poder hacer la consulta deseada.

## Manejo de usuarios MySQL

### Los usuarios se manejan mediante el token de autenticación

- Cada vez que yo ejecuto un endpoint, se crea un nuevo parametro scoped de los servicios instanciados, y yo juego con eso.
- Cada vez que hago un GET, POST, PUT o DELETE reviso los claims del token de autentificación, especificamente me fijo en el claim Roles el cual me permite identificar bajo que usuario MySQL me debo conectar, por defecto (cuando aun no ingrese el token) tengo un usuario que tiene los permisos justos como para logearse, y después con cada usuario otros permisos.

## Creación de la base de datos

### La base de datos se crea utilizando un tipo de servicio especifico

- Para no tener que andar creando la conexión a la BD manualmente mejor se lo delegamos a un servicio, este es de tipo "HostedService" el cual simplemente se ejecuta una vez cada vez que inicio la aplicación, y dentro de ese sericio se encuentra la lógica para crear la BD. 