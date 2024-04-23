use [Team42DB]

go
create or alter function getNotificationsOfUser(@userId bigint)
returns table as
	return (select * from Notifications N where N.userId=@userId)
go

select * from getNotificationsOfUser(1)