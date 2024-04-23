use [Team42DB]

go
create or alter function getVotesOfPost(@postId bigint)
returns table as
	return (select * from Votes V where V.postId=@postId)
go

select * from getVotesOfPost(2)