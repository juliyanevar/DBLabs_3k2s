use master;

create database CarRental;

use CarRental;

create table Client
(
	id int primary key identity(1,1),
	[name] nvarchar(320) unique not null,
	[address] nvarchar(320),
	phoneNumber nvarchar(15)
)

create table CarType
(
	id int primary key identity(1,1),
	carType nvarchar(20)
)

create table Brand
(
	id int primary key identity(1,1),
	brand nvarchar(25)
)

create table Car
(
	id int primary key identity(1,1),
	governmentPlate nvarchar(15),
	brand int foreign key references Brand(id),
	yearOfRelease int,
	typeCar int foreign key references CarType(id),
	costOf1Day money
)

create table Rental
(
	id int primary key identity(1,1),
	client int foreign key references Client(id),
	car int foreign key references Car(id),
	dateOfIssue date,
	countOfDays int,
	amount money
)


create procedure AddRental @id_client int, @id_car int, @date date, @count_days int
as
begin
	if(not exists(select * from rental where car=@id_car and dateOfIssue=@date))
	begin
		declare @amount money, @costOf1Day money;
		set @costOf1Day = (select costOf1Day from Car where id=@id_car);
		set @amount = @costOf1Day * @count_days;
		insert into Rental(client, car, dateOfIssue, countOfDays, amount) values (@id_client, @id_car, @date, @count_days, @amount);
	end;
end;

exec AddRental 1, 2, '2021-01-10', 3;


insert into Client([name], [address], phoneNumber) values ('name1', 'address1', '111111111');

insert into Car(governmentPlate, brand,	yearOfRelease, typeCar,	costOf1Day) values ('gt124578', 1, 2001, 1, 50);

insert into CarType(carType) values ('sedan');

insert into CarType(carType) values ('2-door'),
									('truck'),
									('hatchback'),
									('pickup'),
									('wagon');

insert into Brand(brand) values ('volvo');

select * from Rental;

select * from Client;


insert into Rental(client, car,	dateOfIssue, countOfDays, amount) values (1, 1, '2021-02-02', 1, 50);

create view v_rental
as select r.id, cl.name, c.governmentPlate, b.brand, ct.carType, r.dateOfIssue, r.countOfDays, r.amount
from Rental r join Client cl on r.client=cl.id
join Car c on r.car=c.id
join CarType ct on c.typeCar=ct.id
join Brand b on c.brand=b.id;

select * from v_rental;
select * from car;


create procedure GetBrands 
as
begin
	select * from Brand;
end;


create procedure AddBrand @brand nvarchar(25)
as
begin
	if(not exists(select * from Brand where brand=@brand))
	begin
		insert into Brand(brand) values (@brand);
	end;
end;

create procedure GetBrandById @id int
as
begin
	select * from Brand where id=@id;
end;


create procedure UpdateBrand @id int, @brand nvarchar(25)
as
begin
	update Brand set brand=@brand where id=@id;
end;

create procedure DeleteBrand @id int
as
begin 
	declare @id_car int;
	if(exists(select * from Car where brand=@id))
	begin
		set @id_car=(select id from Car where brand=@id);
		if(exists(select * from Rental where car=@id_car))
		begin
			delete Rental where car=@id_car;
		end;
		delete Car where brand=@id;
	end;
	delete Brand where id=@id;
end;



create procedure GetCarTypes 
as
begin
	select * from CarType;
end;


create procedure AddCarType @carType nvarchar(20)
as
begin
	if(not exists(select * from CarType where carType=@carType))
	begin
		insert into CarType(carType) values (@carType);
	end;
end;


exec AddCarType 'cartype'

create procedure GetCarTypeById @id int
as
begin
	if(exists(select * from CarType where id=@id))
	begin 
		select * from CarType where id=@id;
	end;
end;


create procedure UpdateCarType @id int, @carType nvarchar(20)
as
begin
	update CarType set carType=@carType where id=@id;
end;


create procedure DeleteCarType @id int
as
begin 
	declare @id_car int;
	if(exists(select * from Car where typeCar=@id))
	begin
		set @id_car=(select id from Car where typeCar=@id);
		if(exists(select * from Rental where car=@id_car))
		begin
			delete Rental where car=@id_car;
		end;
		delete Car where typeCar=@id;
	end;
	delete CarType where id=@id;
end;


create procedure GetClients
as
begin
	select * from Client;
end;

create procedure AddClient @name nvarchar(320), @address nvarchar(320), @phoneNumber nvarchar(15)
as
begin
	insert into Client([name], [address], phoneNumber) values (@name, @address, @phoneNumber);
end;

create procedure GetClientById @id int
as
begin
	select * from Client where id=@id;
end;

create procedure UpdateClient @id int, @name nvarchar(320), @address nvarchar(320), @phoneNumber nvarchar(15)
as
begin
	update Client set [name]=@name, [address]=@address, phoneNumber=@phoneNumber where id=@id;
end;

create procedure DeleteClient @id int
as
begin
	if(exists(select * from Rental where car=@id))
	begin
		delete Rental where car=@id;
	end;
	delete Client where id=@id;
end;


create procedure GetCars
as
begin
	select * from Car;
end;

create procedure AddCar @governmentPlate nvarchar(15), @brand int, @yearOfRelease int, @typeCar int, @costOf1Day money
as
begin
	insert into Car(governmentPlate, brand,	yearOfRelease, typeCar, costOf1Day) values (@governmentPlate, @brand, @yearOfRelease, @typeCar, @costOf1Day)
end;

create procedure GetCarById @id int
as
begin
	select * from Car where id=@id;
end;

create procedure UpdateCar @id int, @governmentPlate nvarchar(15), @brand int, @yearOfRelease int, @typeCar int, @costOf1Day money
as
begin
	declare @countDays int, @id_rental int;
	update Car set governmentPlate=@governmentPlate, brand=@brand, yearOfRelease=@yearOfRelease, typeCar=@typeCar, costOf1Day=@costOf1Day where id=@id;
	if(exists(select * from Rental where car=@id))
	begin
		declare cursorRental cursor local
			for select id, countOfDays from Rental where car=@id;
		open cursorRental
		fetch cursorRental into @id_rental, @countDays;
		while @@FETCH_STATUS=0
		begin
			update Rental set amount=@countDays*@costOf1Day where id=@id_rental;
			fetch cursorRental into @id_rental, @countDays;
		end;
		close cursorRental;
	end;
end;

create procedure DeleteCar @id int
as
begin
	if(exists(select * from Rental where car=@id))
	begin
		delete Rental where  car=@id;
	end;
	delete Car where id=@id;
end;

use CarRental
select * from brand

select * from CarType


create procedure GetRentals
as
begin
	select * from Rental;
end;

create procedure GetRentalById @id int
as
begin
	select * from Rental where id=@id;
end;

create procedure UpdateRental @id int, @id_client int, @id_car int, @date date, @count_days int
as
begin
	declare @amount money, @costOf1Day money;
	set @costOf1Day = (select costOf1Day from Car where id=@id_car);
	set @amount = @costOf1Day * @count_days;
	update Rental set client=@id_client, car=@id_car, dateOfIssue=@date, countOfDays=@count_days, amount=@amount where id=@id;
end;


create procedure DeleteRental @id int
as
begin
	delete Rental where id=@id;
end;

create procedure GetRentalsOverPeriod @firstDay date, @lastDay date
as
begin
	select * from Rental where dateOfIssue>@firstDay and dateOfIssue<@lastDay order by dateOfIssue;
end;


