use Team42DB

-- Procedures (dont return a table, instead they change information about in a table)
-- add, delete, update
go
create or alter procedure removeTag @id bigint as
	delete from Tags where id=@id

go

create or alter procedure updateTag @id bigint, @name varchar(255) as
	update Tags set [name]=@name where id=@id

go

create or alter procedure addTag @name varchar(255) as
	insert into Tags values (@name)

go
-- User-defined Functions (return a table)
-- getAll, getOne, maybe filters in the future
create or alter function getAllTags()
returns table as
	return (select * from Tags)

go

create or alter function getTagById(@id bigint)
returns table as
	return (select * from Tags T where T.id=@id)
go