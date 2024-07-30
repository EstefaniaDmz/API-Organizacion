Create database Organizacion;

Use Organizacion;

Create table Puestos(
    idPuesto uniqueidentifier primary key default NEWID(),
    descripcion varchar(50) not null
);

Create table Empleados(
    idEmpleado uniqueidentifier primary key default NEWID(),
    nombre varchar(100) not null,
    apellidoPaterno varchar(100) not null,
    apellidoMaterno varchar(100),
    fechaNacimiento date not null,
    edad int,
    fotografia nvarchar(max) not null,
    salario float not null,
    puestoId uniqueidentifier not null,

    Foreign key (puestoId) References Puestos(idPuesto)
);

Create table Usuarios(
    idUsuario uniqueidentifier primary key default NEWID(),
    usuario varchar(50) not null,
    clave varchar(128) not null,
    empleadoId uniqueidentifier not null

    Foreign key (empleadoId) References Empleados(idEmpleado)
);

Create table Parentescos(
    idParentesco uniqueidentifier primary key default NEWID(),
    descripcion varchar(50) not null
);

Create table Beneficiarios(
    idBeneficiario uniqueidentifier primary key default NEWID(),
    nombre varchar(100) not null,
    apellidoPaterno varchar(100) not null,
    apellidoMaterno varchar(100),
    parentescoId uniqueidentifier not null,
    empleadoId uniqueidentifier not null,

    Foreign key (parentescoId) References Parentescos(idParentesco),
    Foreign key (empleadoId) References Empleados(idEmpleado)
);

-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------
--                                          I N S E R T                                  --
-------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------

Insert into Puestos(idPuesto, descripcion)
Values('e002a9ee-67ac-4a63-bd9b-2da24b422ab6', 'Desarrollador');

Insert into Empleados(idEmpleado, nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, edad, fotografia, salario, puestoId)
values('e5828215-8ca5-448c-9a22-7c92eff6ca39', 'Estefania', 'Rosales', 'Domínguez', '2000-10-29', 23, '', 18000, 'e002a9ee-67ac-4a63-bd9b-2da24b422ab6');

Insert into Usuarios(empleadoId, usuario, clave)
values('e5828215-8ca5-448c-9a22-7c92eff6ca39', 'stef', '31583cff658e71e493ba40d3161b492d1fb10bc70c4faa8c498c7f356c7418fbbbd630a0771a3a71f873c8b1b291bee93e32ddff6bb57c1ca77e2945b6fc0531');