use Team42DB
go
-- Procedure to add a comment
CREATE OR ALTER PROCEDURE AddComment
    @userId BIGINT,
    @content VARCHAR(8000),
    @postId BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @postId)
    BEGIN
        INSERT INTO Posts (userId, content, datePosted, type, title, categoryId)
        VALUES (@userId, @content, GETDATE(), 'comment', NULL, NULL);
    END
END;
GO

-- Procedure to update a comment
CREATE OR ALTER PROCEDURE UpdateComment
    @commentId BIGINT,
    @content VARCHAR(8000)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @commentId AND type = 'comment')
    BEGIN
        UPDATE Posts
        SET content = @content,
            dateOfLastEdit = GETDATE()
        WHERE id = @commentId;
    END
END;
GO

-- Procedure to delete a comment
CREATE OR ALTER PROCEDURE DeleteComment
    @commentId BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @commentId AND type = 'comment')
    BEGIN
        DELETE FROM Posts WHERE id = @commentId;
    END
END;
GO

-- Function to get all comments
CREATE OR ALTER FUNCTION GetAllComments()
RETURNS TABLE
AS
RETURN
    (SELECT * FROM Posts WHERE type = 'comment');
GO

-- Function to get a comment by ID
CREATE OR ALTER FUNCTION GetCommentById(@id BIGINT)
RETURNS TABLE
AS
RETURN
    (SELECT * FROM Posts WHERE id = @id AND type = 'comment');
GO
