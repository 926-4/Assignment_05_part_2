use [Team42DB]

go
create or alter function getCategoriesModeratedByUser(@userId bigint)
returns table as
	return (select C.id id, C.[name] [name] from Moderates M inner join Categories C on M.categoryId=C.id where M.userId=@userId)
go

select * from getCategoriesModeratedByUser(2)