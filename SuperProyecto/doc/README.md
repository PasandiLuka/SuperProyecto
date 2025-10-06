# ***Documentacion Proyecto Plan de Estudio***

## **Diagrama de Clases:**


```mermaid
classDiagram

    class Local {
        +int idLocal
        +string nombre
        +string direccion
    }

    class Evento {
        +int idEvento
        +string nombre
        +string descripcion
        +DateTime fechaPublicacion
        +bool publicado
        +bool cancelado
    }

    class Usuario {
        +int idUsuario
        +string email
        +string passwordHash
        +string rol
    }

    class Sector {
        +int idSector
        +int idLocal
        +string nombre
        +int capacidad
    }

    class Cliente {
        +int DNI
        +int idUsuario
        +string nombre
        +string apellido
        +int telefono
    }

    class RefreshTokens {
        +int idRefreshToken
        +int idUsuario
        +string token
        +DateTime expiracion
        +bool revocado
    }

    class Tarifa {
        +int idTarifa
        +int idSector
        +decimal precio
    }

    class Funcion {
        +int idFuncion
        +int idTarifa
        +int idEvento
        +DateTime fechaHora
        +int stock
        +bool cancelada
    }

    class Entrada {
        +int idEntrada
        +int idFuncion
        +int idOrden
        +bool usada
    }

    class Qr {
        +int idQr
        +int idEntrada
        +string url
    }

    class Orden {
        +int idOrden
        +int DNI
        +DateTime fecha
        +bool pagada
    }

    %% Relaciones
    Local "1" --> "many" Sector : contiene
    Sector "1" --> "many" Tarifa : define
    Tarifa "1" --> "many" Funcion : aplica
    Evento "1" --> "many" Funcion : organiza
    Funcion "1" --> "many" Entrada : genera
    Entrada "1" --> "1" Qr : tiene
    Cliente "1" --> "many" Orden : realiza
    Entrada "1" --> "1" Orden : incluye
    Usuario "1" --> "1" Cliente : pertenece
    Usuario "1" --> "many" RefreshTokens : genera
```

## **DER**

```mermaid
erDiagram

    Local {
        INT idLocal PK
        VARCHAR(45) nombre
        VARCHAR(45) direccion
    }

    Evento {
        INT idEvento PK
        VARCHAR(45) nombre
        VARCHAR(45) descripcion
        DATETIME fechaPublicacion
        BOOLEAN publicado
        BOOLEAN cancelado
    }

    Usuario {
        INT idUsuario PK
        VARCHAR(45) email
        VARCHAR(255) passwordHash
        VARCHAR(45) rol
    }

    Sector {
        INT idSector PK
        INT idLocal FK
        VARCHAR(45) nombre
        INT capacidad
    }

    Cliente {
        INT DNI PK
        INT idUsuario FK
        VARCHAR(45) nombre
        VARCHAR(45) apellido
        INT telefono
    }

    RefreshTokens {
        INT idRefreshToken PK
        INT idUsuario FK
        VARCHAR(200) token
        DATETIME expiracion
        BIT revocado
    }

    Tarifa {
        INT idTarifa PK
        INT idSector FK
        DECIMAL(10-2) precio
    }

    Funcion {
        INT idFuncion PK
        INT idTarifa FK
        INT idEvento FK
        DATETIME fechaHora
        INT stock
        BOOLEAN cancelada
    }

    Entrada {
        INT idEntrada PK
        INT idFuncion FK
        INT idOrden FK
        BOOLEAN usada
    }

    Qr {
        INT idQr PK
        INT idEntrada FK
        VARCHAR(100) url
    }

    Orden {
        INT idOrden PK
        INT DNI FK
        DATETIME fecha
        BOOLEAN pagada
    }

    %% Relaciones
    Local ||--o{ Sector : "posee"
    Sector ||--o{ Tarifa : "define"
    Tarifa ||--o{ Funcion : "aplica a"
    Evento ||--o{ Funcion : "contiene"
    Funcion ||--o{ Entrada : "genera"
    Entrada ||--|| Qr : "tiene"
    Cliente ||--o{ Orden : "realiza"
    Entrada ||--|| Orden : "incluye"
    Usuario ||--o{ Cliente : "pertenece a"
    Usuario ||--o{ RefreshTokens : "posee"
```


## **Tareas**

### **Lista de tareas**

| **Orden** | **Tarea**                         | **Duracion (hs)** | **Dependencias** |
|-------|---------------------------------------|:-------------:|:------------:|
|a  |Configurar el entorno de desarrollo        | 1             | -            |
|b	|Crear y preparar la base de datos	        | 2	            | a            |
|c	|Integrar validadores en la API	            | 2	            | a            |
|d	|Ejecutar las pruebas unitarias existentes	| 3	            | b, c         |
|e	|Probar autenticación y autorización JWT	| 2	            | d            |
|f	|Documentar la API y el proyecto	        | 2	            | d            |
|g	|Preparar el despliegue en producción	    | 3	            | e, f         |
|h	|Desplegar la aplicación	                | 2	            | g            |
|i	|Realizar pruebas de aceptación	            | 2	            | h            |
|j	|Cierre del proyecto	                    | 1	            | i            |

### **Gantt**

```mermaid
gantt
    title Proyecto: Boletería Digital
    dateFormat  HH
    axisFormat  %Hh
    section Preparación
    a : a, 00, 1h
    b : b, after a, 2h
    c : c, after a, 2h

    section Desarrollo y Pruebas
    d : d, after b c, 3h
    e : e, after d, 2h
    f : f, after d, 2h

    section Despliegue
    g : g, after e f, 3h
    h : h, after g, 2h

    section Finalización
    i : i, after h, 2h
    j : j, after i, 1h
```