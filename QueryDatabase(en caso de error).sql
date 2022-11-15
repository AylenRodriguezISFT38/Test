--create database
create database Test;

--create tables
create table TipoProducto(
	id int identity(1,1) primary key not null,
	descripcion nvarchar(200) not null
);

create table Producto(
	id int identity(1,1) primary key not null,
	idTipoProducto int not null,
	nombre nvarchar(50),
	precio float not null,
	CONSTRAINT fk_Producto_TipoProducto FOREIGN KEY (idTipoProducto) REFERENCES TipoProducto (Id)
);

create table Stock(
	id int identity(1,1) primary key not null,
	idProducto int not null,
	cantidad int not null,
	CONSTRAINT fk_Stock_Producto FOREIGN KEY (idProducto) REFERENCES Producto(Id)
);

--create view
create view vw_StockProducto 
as
select p.nombre, p.precio, s.cantidad from Producto as P inner join Stock as S on S.idProducto = P.id;

select * from vw_StockProducto;

--sp insertarProducto
create proc sp_InsertarProducto
@tipoProducto as int,
@nombre as nvarchar(50),
@precio as float
as
insert into Producto(idTipoProducto, nombre, precio) values(@tipoProducto, @nombre, @precio);

exec sp_InsertarProducto 1, 'Gigabyte Nvidia RTX 3080 Ti', 280.000;

--sp editarProducto
create proc sp_ModificarProducto
@tipoProducto as int,
@nombre as nvarchar(50),
@precio as float,
@busqueda as nvarchar(50)
as
UPDATE Producto SET idTipoProducto = @tipoProducto, nombre = @nombre, precio = @precio
  WHERE nombre = @busqueda;

exec sp_ModificarProducto 2, 'Samsung Curvo 24¨', 43.499, 'Gigabyte Nvidia RTX 3080 Ti';

--sp eliminarProducto
create proc sp_EliminarProducto
@id as int
as 
delete from Producto where id = @id;

exec sp_EliminarProducto 1;