use Team42DB

-- Procedures (dont return a table, instead they change information about in a table)
-- add, delete, update
go
create or alter procedure removeVote @postId bigint, @userId bigint as
	delete from Votes where (postId= @postId and userId = @userId)

go

create or alter procedure updateVoteValue @postId bigint, @userId bigint, @value int as
	update Votes set value=@value where (postId= @postId and userId = @userId)

go

create or alter procedure addVote @postId bigint, @userId bigint, @value int as
	insert into Votes values (@postId, @userId, @value)

-- User-defined Functions (return a table)
-- getAll, getOne, maybe filters in the future
go
create or alter function getAllVotes()
returns table as
	return (select * from Votes)

go

create or alter function getAllVotesFromUserId(@id bigint)
returns table as
	return (select * from Votes V where V.userId=@id)
go

create or alter function getAllVotesOnPostId(@id bigint)
returns table as
	return (select * from Votes V where V.postId=@id)
go