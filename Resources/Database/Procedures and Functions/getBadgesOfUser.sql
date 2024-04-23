use [Team42DB]

go
create or alter function getBadgesOfUser(@userId bigint)
returns table as
	return (select B.id id, B.[name] [name], B.[description] [description], B.[image] [image]
		from Owns O inner join Badges B on O.badgeId=B.id where O.userId=@userId)
go

select * from getBadgesOfUser(2)