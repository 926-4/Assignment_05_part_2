use Team42DB

go
create or alter function getAllUsers()
returns table as
	return (select * from Users)

go
create or alter function getUser(@id bigint)
returns table as
	return (select * from Users U where U.id=@id)
go

go
create or alter procedure removeUser @id bigint as
	delete from Users where id=@id

go 
create or alter procedure updateUser @id bigint, @name varchar(255) as
	update Users set [name]=@name where id=@id -- used [] because 'name' is a keyword in sql

go
create or alter procedure addUser @name varchar(255) as
	insert into Users([name]) values (@name)


-- for testing:
go
select * from dbo.getAllUsers()
select * from dbo.getUser(1)
exec updateUser 1, 'Mike'
select * from dbo.getUser(1)
exec addUser 'New'
select * from dbo.getUser(4)
exec removeUser 4
select * from dbo.getUser(4)
