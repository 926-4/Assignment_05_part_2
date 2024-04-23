use Team42DB

-- Procedures (dont return a table, instead they change information about in a table)
-- add, delete, update
go
create or alter procedure removeBadge @id bigint as
	delete from Badges where id=@id

go

create or alter procedure updateBadge @id bigint, @name varchar(255), @description varchar(255), @image image as
	update Badges set [name]=@name, description=@description, image=@image where id=@id

go

create or alter procedure addBadge @name varchar(255), @description varchar(255), @image image as
	insert into Badges values (@name, @description, @image)

-- User-defined Functions (return a table)
-- getAll, getOne, maybe filters in the future
go
create or alter function getAllBadges()
returns table as
	return (select * from Badges)

go

create or alter function getBadgeById(@id bigint)
returns table as
	return (select * from Badges B where B.id=@id)
go