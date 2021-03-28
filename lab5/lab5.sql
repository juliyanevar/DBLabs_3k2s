use CarRental;

create table Employee
(
	hid hierarchyid not null,
	id int not null,
	[name] nvarchar(120) not null,
	constraint pk_empl primary key clustered
	(
		hid asc
	)
)

--GetRoot() Ч возвращает id корн€ иерархии.

insert into Employee values(hierarchyid::GetRoot(), 1, 'Name1');

select * from Employee;

--hid.GetAncestor(1) = hierarchyid::GetRoot() Ч выбирает все записи, предком которых €вл€етс€ корень;
--hierarchyid::GetRoot().GetDescendant(@id, null) Ч выбирает первый свободный hierarchyid потомков корн€ дерева


declare @Id hierarchyid  
select @Id = MAX(hid) from Employee where hid.GetAncestor(1) = hierarchyid::GetRoot() ; 
insert into Employee values(hierarchyid::GetRoot().GetDescendant(@id, null), 2, 'Name2');


declare @Id hierarchyid
select @Id = MAX(hid) from Employee where hid.GetAncestor(1) = hierarchyid::GetRoot() ;
insert into Employee values(hierarchyid::GetRoot().GetDescendant(@id, null), 3, 'Name3');


declare @phId hierarchyid
select @phId = (SELECT hid FROM Employee WHERE id = 2);
declare @Id hierarchyid
select @Id = MAX(hid) from Employee where hid.GetAncestor(1) = @phId;
insert into Employee values( @phId.GetDescendant(@id, null), 7, 'Name4');


declare @phId hierarchyid
select @phId = (SELECT hid FROM Employee WHERE id = 6);
declare @Id hierarchyid
select @Id = MAX(hid) from Employee where hid.GetAncestor(1) = @phId;
insert into Employee values( @phId.GetDescendant(@id, null), 5, 'Name5');


declare @Id hierarchyid
select @Id = MAX(hid) from Employee where hid.GetAncestor(1) = hierarchyid::GetRoot() ;
insert into Employee values(hierarchyid::GetRoot().GetDescendant(@id, null), 6, 'Name6');


select hid.ToString(), hid.GetLevel(), * from Employee; 



--GetLevel Ч выдает уровень hierarchyid;

create procedure GetRoot(@level int)
as
begin
	select hid.ToString(), * from Employee where hid.GetLevel()=@level;
end;

exec GetRoot 1;


create procedure AddChildRoot(@idEmp int, @name nvarchar(120))
as
begin
	declare @id hierarchyid, @phId hierarchyid;
	select @phId = (select hid from Employee where id = @idEmp);
	select @id = max(hid) from Employee where hid.GetAncestor(1) = @phId;
	insert into Employee values(@phId.GetDescendant(@id, null), @idEmp, @name);
end;

exec AddChildRoot 6, 'Name7';

select * from Employee;

create procedure MoveRoot(@old int, @new int)
as
begin
	declare @nold hierarchyid, @nnew hierarchyid;
	select @nold = hid from Employee where id = @old;
	
	set transaction isolation level serializable
	begin transaction
		select @nnew = hid from Employee where id = @new;
		select #nnew = @nnew.GetDescendant(max(hid), NULL) FROM Employee WHERE hid.GetAncestor(1)=@nnew;
		update Employee set hid = hid.GetReparentedValue(@nold, @nnew) where hid.IsDescendantOf(@nold) = 1;
	commit;
end;

exec MoveRoot 1,2;

select hid.ToString(), hid.GetLevel(), * from Employee;