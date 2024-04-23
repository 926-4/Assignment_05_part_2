use Team42DB
go

-- Procedure to add a reply
CREATE OR ALTER PROCEDURE AddReply
    @idOfPostRepliedOn BIGINT,
    @idOfReply BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @idOfPostRepliedOn)
    BEGIN
        IF EXISTS (SELECT 1 FROM Posts WHERE id = @idOfReply)
        BEGIN
            IF EXISTS (SELECT 1 FROM Posts WHERE id = @idOfPostRepliedOn AND type = 'question' AND (SELECT type FROM Posts WHERE id = @idOfReply) = 'answer')
            BEGIN
                INSERT INTO Replies (idOfPostRepliedOn, idOfReply)
                VALUES (@idOfPostRepliedOn, @idOfReply);
            END
        END
    END
END;
GO

-- Procedure to delete a reply
CREATE OR ALTER PROCEDURE DeleteReply
    @idOfPostRepliedOn BIGINT,
    @idOfReply BIGINT
AS
BEGIN
    DELETE FROM Replies
    WHERE idOfPostRepliedOn = @idOfPostRepliedOn AND idOfReply = @idOfReply;
END;
GO


-- Function to get all replies of a post
CREATE OR ALTER FUNCTION GetAllRepliesOfPost(@postId BIGINT)
RETURNS TABLE
AS
RETURN
    (SELECT P.id id, P.userId userId, P.content content, P.datePosted datePosted, 
	P.dateOfLastEdit dateOfLastEdit, P.[type] [type], P.title title, P.categoryId categoryId
	FROM Replies R inner join Posts P on P.id=R.idOfReply  WHERE R.idOfPostRepliedOn = @postId);
GO

select * from dbo.GetAllRepliesOfPost(1)
