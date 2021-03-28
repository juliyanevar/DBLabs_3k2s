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
set @phone = '+375336596510'
go

drop assembly DB

drop procedure RentalDate


