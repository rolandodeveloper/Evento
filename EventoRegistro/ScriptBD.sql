#Creación e la BD
CREATE DATABASE EventoClientes;
USE EventoClientes;

#Creación de tabla
CREATE TABLE tblCliente 
(
    codigo 				INT 			AUTO_INCREMENT PRIMARY KEY,
    nombres 			VARCHAR(50)		NOT NULL,
    apellidoPaterno 	VARCHAR(40)		NOT NULL,
    apellidoMaterno 	VARCHAR(40)		NOT NULL,
    sexo 				CHAR(1)			NOT NULL,
    estadoCivil 		VARCHAR(3)		NOT NULL,
    tipo 				CHAR(1) 		NOT NULL	DEFAULT 'S',
    correo 				VARCHAR(70) 	NULL,
    fechaNacimiento 	DATE,
    puntaje 			SMALLINT 		NOT NULL	DEFAULT 0,
    cantidadHijos 		TINYINT			NOT NULL	DEFAULT 0,
    esRecomendado 		BIT				NOT NULL	DEFAULT false
)
auto_increment=100;

#Creación del procedimiento almacenado
DELIMITER //
CREATE PROCEDURE usp_Cliente_Insertar 
(	
IN parNombres			varchar(50),    
IN parApellidoPaterno	varchar(40),    
IN parApellidoMaterno	varchar(40),    
IN parSexo				char(1)	,    
IN parEstadoCivil		varchar(3),    
IN parTipo				char(1) ,    
IN parCorreo			varchar(70),    
IN parFechaNacimiento	date,    
IN parPuntaje			smallint,    
IN parCantidadHijos		tinyint,    
IN parEsRecomendado		bit				 
)
    BEGIN
		INSERT INTO tblCliente(nombres,apellidoPaterno,apellidoMaterno,
        sexo,estadoCivil,tipo,correo,fechaNacimiento,puntaje,
        cantidadHijos,esRecomendado) 
        VALUES(parNombres,parApellidoPaterno,parApellidoMaterno,
        parSexo,parEstadoCivil,parTipo,parCorreo,parFechaNacimiento,
        parPuntaje,parCantidadHijos,parEsRecomendado);
    END;
//

/*
USE EventoClientes;
SELECT * FROM tblcliente;
DELETE FROM tblcliente WHERE codigo>0;
*/

