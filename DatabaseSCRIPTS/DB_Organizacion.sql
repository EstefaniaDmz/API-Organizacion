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