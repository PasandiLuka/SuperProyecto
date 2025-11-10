# :tickets: **Documentación del Proyecto Boletería Digital**

Este documento contiene toda la información técnica y funcional del proyecto. :ticket:

---

## :bookmark_tabs: **Accesos rápidos**

- [:building_construction: Arquitectura en capas](#arquitectura-en-capas)
- [:floppy_disk: Persistencia de datos con Dapper](#persistencia-de-datos-con-dapper)
- [:lock: Autenticación y autorización con JWT](#autenticación-y-autorización-con-jwt)
- [:globe_with_meridians: Endpoints principales de la API REST](#endpoints-principales-de-la-api-rest)
- [:test_tube: Pruebas unitarias y Moq](#pruebas-unitarias-y-moq)
- [:books: Documentación con Swagger](#documentación-con-swagger)
- [:gear: Implementación del patron Result en servicios](#implementación-del-patron-result-en-servicios)
- [:white_square_button: Generación y validación de códigos QR](#generación-y-validación-de-códigos-qr)
- [:link: Creación de URL](#creación-de-url)
- [:busts_in_silhouette: Manejo de usuarios MySQL](#manejo-de-usuarios-mysql)
- [:file_cabinet: Creación de la base de datos](#creación-de-la-base-de-datos)


### :triangular_ruler: **Diagramas**
- [Documentación diagramas](DIAGRAMAS.md)

---
<a id="arquitectura-en-capas"></a>
## :building_construction: **Arquitectura en capas**

El proyecto sigue una arquitectura en capas que **separa responsabilidades** y facilita la mantenibilidad del código:

- **Capa Core (Dominio)** → contiene entidades, interfaces y validadores de reglas de negocio, sin referencias a infraestructura.  
- **Capa Dapper (Infraestructura)** → implementa repositorios mediante ADO.NET y Dapper (micro-ORM).  
- **Capa Services** → gestiona la lógica de negocio, coordinando servicios y repositorios.  
- **Capa API** → aloja controladores ASP.NET Core y configuración del pipeline (inyección de dependencias, JWT, Swagger, etc.).  
- **Capa Tests** → contiene las pruebas unitarias.  

:bulb: Gracias a esta separación, se aplica el **Principio de Inversión de Dependencias (SOLID)**.

---
<a id="persistencia-de-datos-con-dapper"></a>
## :floppy_disk: **Persistencia de datos con Dapper**

La persistencia se realiza mediante **Dapper**, un micro-ORM ligero y de alto rendimiento para .NET.  

- Permite ejecutar SQL directamente y mapear resultados a objetos C#.  
- Cada entidad principal tiene su propio repositorio (`RepoEvento`, `RepoOrden`, etc.) que implementa interfaces del dominio.  
- Se aplica el **patrón Repositorio**, separando la lógica de acceso a datos de la lógica de negocio.  
- Facilita la **inyección de dependencias** y las **pruebas unitarias**.  

---
<a id="autenticación-y-autorización-con-jwt"></a>
## :lock: **Autenticación y autorización con JWT**

Para la seguridad se implementa **JWT (JSON Web Token)**.  

- Al iniciar sesión, el servidor emite un **token firmado** que el cliente incluye en `Authorization: Bearer <token>`.  
- ASP.NET Core valida el token y extrae la identidad desde las *claims*.  
- Se gestionan roles: **Administrador**, **Organizador**, **Cliente** y **Default**.  
- Endpoints principales:  
    - `/auth/register`
    - `/auth/login`
    - `/auth/refresh`
    - `/auth/logout`
    - `/auth/me`
    - `/auth/roles`
    - `/usuarios/{id}/roles`

---

<a id="endpoints-principales-de-la-api-rest"></a>
## :globe_with_meridians: **Endpoints principales de la API REST**

Los endpoints siguen un enfoque **RESTful**, devolviendo **JSON** y códigos **HTTP estándar** (`200`, `201`, `204`, `400`, `401`, `403`, `404`).  

### Recursos disponibles:

- <details>
  <summary> <b>Locales: </summary>

    - `POST /locales`
    - `GET /locales`
    - `PUT /locales/{id}`
    - `DELETE /locales/{id}`  
</details>

- <details>
  <summary> <b>Sectores: </summary>

    - `POST /locales/{id}/sectores`
    - `GET /locales/{id}/sectores`
    - `PUT /sectores/{id}`
    - `DELETE /sectores/{id}` 
</details>

- <details>
  <summary> <b>Eventos: </summary>

    - `POST /eventos`
    - `GET /eventos`
    - `PUT /eventos/{id}`
    - `POST /eventos/{id}/publicar`
    - `POST /eventos/{id}/cancelar` 
</details>

- <details>
  <summary> <b>Funciones: </summary>

    - `POST /funciones`
    - `GET /funciones`
    - `PUT /funciones/{id}`
    - `POST /funciones/{id}/cancelar`
</details>

- <details>
  <summary> <b>Tarifas: </summary>

    - `POST /tarifas`
    - `GET /funciones/{id}/tarifas`
    - `PUT /tarifas/{id}`
    - `GET /tarifas/{id}`
</details>

- <details>
  <summary> <b>Clientes: </summary>

    - `POST /clientes`
    - `GET /clientes`
    - `PUT /clientes/{id}`
</details>

- <details>
  <summary> <b>Órdenes: </summary>

    - `POST /ordenes`
    - `GET /ordenes`
    - `POST /ordenes/{id}/pagar`
    - `POST /ordenes/{id}/cancelar`
</details>

- <details>
  <summary> <b>Entradas: </summary>

    - `GET /entradas`
    - `POST /entradas/{id}/anular`
    - `GET /entradas/{id}/qr` 
</details>

- <details>
  <summary> <b>Códigos QR: </summary>

    - `POST /qr/validar`
</details>
    
<br>
> Todos los endpoints pueden probarse fácilmente en **Swagger UI**.

---

<a id="pruebas-unitarias-y-moq"></a>
## :test_tube: **Pruebas unitarias y Moq**

El proyecto incluye un conjunto de pruebas **xUnit** con **Moq** para simular dependencias.  

- Se validan reglas de negocio y flujos de datos.  
- Moq permite crear *mocks* de repositorios y servicios sin usar una base real.  
- Los tests siguen el patrón `Arrange - Act - Assert` y buenas prácticas de nomenclatura.  

---

<a id="documentación-con-swagger"></a>
## :books: **Documentación con Swagger**

Se utiliza **Swagger/OpenAPI** (vía *Swashbuckle*) para generar documentación **interactiva**.  

- Permite probar cada endpoint con “Try it out!”.  
- Muestra parámetros, códigos de respuesta y modelos de datos.  
- Facilita la verificación del comportamiento de la API durante el desarrollo.  

---


<a id="implementación-del-patron-result-en-servicios"></a>
## :gear: **Implementación del patron Result en servicios**

Los servicios devuelven un objeto basado en **Result Pattern** :arrow_right: una estructura que simula respuestas HTTP (`Ok`, `Created`, `Unauthorized`, `BadRequest`).  

Esto se traduce a resultados HTTP reales mediante el método de extensión `ToMinimalResult()`.  

### Ejemplo sin método de extensión:
~~~csharp
var respuesta = service.GetClientes();
var resultExtencions = new ResultExtencions();
return resultExtencions.ToMinimalResult(respuesta);
~~~

### Ejemplo con método de extensión:
~~~csharp
var respuesta = service.GetClientes();
return respuesta.ToMinimalResult();
~~~

---

<a id="generación-y-validación-de-códigos-qr"></a>
## :white_square_button: **Generación y validación de códigos QR**

La generación y validación de QR se realiza mediante la librería QRCoder para .NET.

- Cada orden pagada genera un QR único (formato PNG/Base64).

- El endpoint POST /qr/validar comprueba la firma y el estado de la entrada.

- Posibles resultados: Ok, YaUsada, Expirada, FirmaInvalida, NoExiste.

---

<a id="creación-de-url"></a>
## :link: **Creación de URL**

Las URLs dinámicas se generan mediante LinkGenerator y IHttpContextAccessor.

- Estas herramientas permiten obtener automáticamente la dirección y puerto del servidor.

- Se combinan con la ruta del endpoint para construir enlaces dinámicos hacia los recursos necesarios.

---

<a id="manejo-de-usuarios-mysql"></a>
## :busts_in_silhouette: **Manejo de usuarios MySQL**

El usuario MySQL se determina a partir del token JWT :key:.

- En cada petición (GET, POST, PUT, DELETE), se leen las claims del token, especialmente el claim Role.

- Según el rol, se conecta con el usuario MySQL correspondiente.

- Antes del inicio de sesión, existe un usuario “por defecto” con permisos mínimos.

---

<a id="creación-de-la-base-de-datos"></a>
## :file_cabinet: Creación de la base de datos

La creación de la base de datos está automatizada mediante un HostedService.

- Este servicio se ejecuta una sola vez al iniciar la aplicación.

- Contiene la lógica necesaria para crear la BD y sus estructuras.

- Así se evita la creación manual de la conexión inicial.

Hacer monolingue todo.