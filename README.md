# Proyecto Boletería Digital

Bienvenido al repositorio del proyecto **Boletería Digital**.  

### La aplicación emplea programación orientada a objetos (POO) y principios SOLID para lograr un diseño modular y mantenible. Se modelan las entidades (Cliente, Evento, Entrada, etc.) usando clases con encapsulamiento y herencia. Los Principios SOLID son pautas clave que mejoran la calidad, extensibilidad y mantenibilidad del código

---

## Documentación completa

Toda la documentación técnica y funcional del proyecto se encuentra en un archivo separado:

📄 [Ver Documentación del Proyecto](doc\README.md)

---

## **Contenido destacado de la documentación**

- **Principios SOLID y POO**: diseño modular, mantenible y extensible.
- **Arquitectura en capas**: Core, Dapper, Services, API, Tests.
- **Persistencia de datos**: micro-ORM Dapper y patrón Repositorio.
- **Autenticación y autorización**: JWT y gestión de roles.
- **Endpoints REST**: CRUD y operaciones específicas para cada entidad.
- **Pruebas unitarias**: xUnit y Moq.
- **Documentación interactiva**: Swagger/OpenAPI.
- **Generación y validación de códigos QR**: con librería QRCoder.
- **Tareas y planificación**: lista de tareas y diagrama Gantt.

---

## **Pasos para la instalacion:**

### **Requerimiento:**

- Se requiere tener instalado el SDK 8 de .NET

---

## **1 - Clonar repositorio:**

#### Ejecute el siguiente comando en su VSCode:
~~~bash
    git clone https://github.com/PasandiLuka/SuperProyecto.git
~~~

---

## **2 - Modificar contraseñas usuarios BD (opcional):**

#### Para este paso deberás modificar las contraseñas de cada usuario que se registra en el siguiente archivo:

[01 USERS.sql](scripts\bd\MySQL\01 USERS.sql)

---

### **3 - Configuracion del appsettings.json:**

### En el caso de que hayas modificado las contraseñas de los usuarios en tu archivo 01 USERS.sql, tambien deberas hacerlo acá.

### Bajo la key llamada "Users" deberas cambiar las contraseñas de cada usuario:

~~~json
"Users": {
    "Administrador":"Server=localhost;Uid=administrador;Pwd=contraseñaNueva;Database=bd_boleteria;",
    "Cliente":"Server=localhost;Uid=cliente;Pwd=contraseñaNueva;Database=bd_boleteria;",
    "Organizador":"Server=localhost;Uid=organizador;Pwd=contraseñaNueva;Database=bd_boleteria;",
    "Default":"Server=localhost;Uid=default;Pwd=contraseñaNueva;Database=bd_boleteria;"
  }
~~~

---

## **4 - Configuracion de tu Super Usuario:**

- ### Este super usuario te permitirá crear la base de datos, y eso, nada más.

### Tenemos que acceder nuevamente a nuestro appsettings.json y bajo la key "Root" debemos agregar un usuario ya creado que poseea permisos de creación de bases de datos (No es igual a los usuarios en "Users", ya que estos son para cuando la app ya esta operativa) de la siguiente manera:

~~~json
"Root":{
    "UserRoot1":"Server=localhost;Uid=root;Pwd=contraseña;Database=bd_boleteria;",
    "UserRoot2":"Server=localhost;Uid=5to_agbd;Pwd=contraseña;Database=bd_boleteria;"
  }
~~~

## Y con todo esto nuestro proyecto ya debería estar operativo.