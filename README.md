SF - HOY NO CIRCULA - DIAZ

Requirimientos

1. Base de datos SQL SERVER 2019
2. VS  2019
3. ASP.NET CORE 6 CON ENTITYFRAMEWORK

Instrucciones
- En la base de datos se crea una base de datos hnc-diaz una tabla con los siguientes parámetros

USE hnc_db;

CREATE TABLE info_hnc (
    id_info INT PRIMARY KEY NOT NULL,
    matricula_info NVARCHAR(50) ,
	fecha_info NVARCHAR(50) ,
	hora_info NVARCHAR(50) ,
	estado_info NVARCHAR(50) 
);

- se realiza la cadena de conexion con el entity framework de manera nativa 

  // cadena de conexion a la base de datos
  "ConnectionStrings": {
    "conexion": "Server=localhost;Database=hnc_db;Trusted_Connection=True; TrustServerCertificate=yes"
  }
}

- Diseño MVC
