use CarRental;
go

exec sp_configure 'clr strict security', 0
go
exec sp_configure 'clr enabled', 1;
go
reconfigure;
go

exec RentalDate '2021-02-19','2021-02-25'

declare @phone PhoneNumber
set @phone = '+375336796510'
print @phone.ToFormattedString()
go

drop assembly DB

drop procedure RentalDate


alter table Client ALTER COLUMN phoneNumber PhoneNumber


drop table Number

create table Number
(
	phone PhoneNumber
)
select * from Number

insert into Number values ('+375337696510')

select * from Client
