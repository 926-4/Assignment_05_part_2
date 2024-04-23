use Team42DB

-- Procedures (dont return a table, instead they change information about in a table)
-- add, delete, update
go
create or alter procedure removePostNotification @userId bigint, @postId bigint as
	delete from Notifications where (postId= @postId and userId = @userId)

go

create or alter procedure removeBadgeNotification @userId bigint, @badgeId bigint as
	delete from Notifications where (userId = @userId and badgeId = @badgeId)

go

create or alter procedure addNotification @userId bigint, @postId bigint,  @badgeId bigint as
	insert into Notifications values (@userId, @postId, @badgeId)

-- User-defined Functions (return a table)
-- getAll, getOne, maybe filters in the future
go
create or alter function getAllNotifications()
returns table as
	return (select * from Notifications)

go

create or alter function getAllNotificationsFromUser(@userId bigint)
returns table as
	return (select * from Notifications where userId = @userId)

go
