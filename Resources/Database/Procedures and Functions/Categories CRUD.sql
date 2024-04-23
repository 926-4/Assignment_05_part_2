use Team42DB

-- Procedures (dont return a table, instead they change information about in a table)
-- add, delete, update
go
create or alter procedure removeCategory @id bigint as
	delete from Categories where id=@id

go

create or alter procedure updateCategories @id bigint, @name varchar(255) as
	update Categories set [name]=@name where id=@id

go

create or alter procedure addCategory @name varchar(255) as
	insert into Categories values (@name)

-- User-defined Functions (return a table)
-- getAll, getOne, maybe filters in the future
go
create or alter function getAllCategories()
returns table as
	return (select * from Categories)

go

create or alter function getCategoryById(@id bigint)
returns table as
	return (select * from Categories C where C.id=@id)
go