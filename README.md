# :tickets: **Proyecto Boletería Digital**

¡Bienvenido al repositorio del proyecto **Boletería Digital**! :bulb:  

### :computer: La aplicación emplea **programación orientada a objetos (POO)** y principios **SOLID** para lograr un diseño modular y mantenible.  
Se modelan las entidades (:walking: Cliente, :ticket: Evento, :stadium: Entrada, etc.) usando clases con **encapsulamiento y herencia**.  
Los principios SOLID son pautas clave que mejoran la **calidad**, **extensibilidad** y **mantenibilidad del código**.  

---

## :book: **Documentación completa**

Toda la documentación técnica y funcional del proyecto se encuentra en un archivo separado:  

:page_facing_up: [**Ver Documentación del Proyecto**](doc/README.md)

---

## :star2: **Contenido destacado de la documentación**

- :jigsaw: **Principios SOLID y POO** → diseño modular, mantenible y extensible.  
- :building_construction: **Arquitectura en capas** → Core, Dapper, Services, API, Tests.  
- :floppy_disk: **Persistencia de datos** → micro-ORM Dapper y patrón Repositorio.  
- :lock: **Autenticación y autorización** → JWT y gestión de roles.  
- :globe_with_meridians: **Endpoints REST y Result pattern** → CRUD y operaciones específicas para cada entidad.  
- :test_tube: **Pruebas unitarias** → xUnit y Moq.  
- :books: **Documentación interactiva** → Swagger / OpenAPI.  
- :white_square_button: **Generación y validación de códigos QR** → librería QRCoder.  
- :calendar: **Tareas y planificación** → lista de tareas y diagrama Gantt.

---

## :gear: **Pasos para la instalación**

### :scroll: **Requerimiento**

- Se requiere tener instalado el **SDK 8 de .NET** :brick:

---

## :compass: **1 - Clonar repositorio**

Ejecutá el siguiente comando en tu terminal de VSCode:  

~~~bash
git clone https://github.com/PasandiLuka/SuperProyecto.git
~~~

---

## :key: **2 - Modificar contraseñas de usuarios BD (opcional)**

Debés modificar las contraseñas de cada usuario en el siguiente archivo:  

:file_folder: [**02-USERS.sql**](scripts/bd/MySQL/02-USERS.sql)

---

## :hammer_and_wrench: **3 - Configuración del appsettings.json (opcional)**

Si cambiaste las contraseñas en tu archivo `02-USERS.sql`, también debés hacerlo aquí :point_down:  

Dentro de la key `"Users"` actualizá las contraseñas correspondientes:

~~~json
"Users": {
  "Administrador":"Server=localhost;Uid=administrador;Pwd=contraseniaNueva;Database=bd_boleteria;",
  "Cliente":"Server=localhost;Uid=cliente;Pwd=contraseniaNueva;Database=bd_boleteria;",
  "Organizador":"Server=localhost;Uid=organizador;Pwd=contraseniaNueva;Database=bd_boleteria;",
  "Default":"Server=localhost;Uid=default;Pwd=contraseniaNueva;Database=bd_boleteria;"
}
~~~

---

## :crown: **4 - Configuración de tu Super Usuario**

Este **super usuario** te permitirá crear la base de datos :toolbox:  

Ingresá en tu archivo `appsettings.json` y bajo la key `"Root"`, agregá un usuario con permisos de **creación de bases de datos** (distinto a los usuarios en `"Users"`):

~~~json
"Root": {
  "UserRoot1":"Server=localhost;Uid=root;Pwd=contrasenia;Database=bd_boleteria;",
  "UserRoot2":"Server=localhost;Uid=5to_agbd;Pwd=contrasenia;Database=bd_boleteria;"
}
~~~

---

## :rotating_light: **Error común: puerto en uso**

Si aparece un error indicando que el **puerto IP ya está en uso**, dirigite a la carpeta  
`src/CSharp/SuperProyecto.Api/Properties` y abrí el archivo `launchSettings.json`.  
Luego modificá el puerto de conexión, por ejemplo:

~~~bash
Ip      Puerto
0.0.0.0:5000
~~~

---

## :white_check_mark: **¡Y listo!**

Con todos estos pasos completados, tu proyecto **Boletería Digital** :tickets: ya debería estar **totalmente operativo** :rocket:  