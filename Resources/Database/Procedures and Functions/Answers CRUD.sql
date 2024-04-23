use Team42DB
go
-- Procedure to add an answer
CREATE OR ALTER PROCEDURE AddAnswer
    @userId BIGINT,
    @content VARCHAR(8000),
    @questionId BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @questionId AND type = 'question')
    BEGIN
        INSERT INTO Posts (userId, content, datePosted, type, title, categoryId)
        VALUES (@userId, @content, GETDATE(), 'answer', NULL, NULL);
    END
END;
GO

-- Procedure to update an answer
CREATE OR ALTER PROCEDURE UpdateAnswer
    @answerId BIGINT,
    @content VARCHAR(8000)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @answerId AND type = 'answer')
    BEGIN
        UPDATE Posts
        SET content = @content,
            dateOfLastEdit = GETDATE()
        WHERE id = @answerId;
    END
END;
GO

-- Procedure to delete an answer
CREATE OR ALTER PROCEDURE DeleteAnswer
    @answerId BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @answerId AND type = 'answer')
    BEGIN
        DELETE FROM Posts WHERE id = @answerId;
    END
END;
GO

-- Function to get all answers
CREATE OR ALTER FUNCTION GetAllAnswers()
RETURNS TABLE
AS
RETURN
    (SELECT * FROM Posts WHERE type = 'answer');
GO

-- Function to get an answer by ID
CREATE OR ALTER FUNCTION GetAnswerById(@id BIGINT)
RETURNS TABLE
AS
RETURN
    (SELECT * FROM Posts WHERE id = @id AND type = 'answer');
GO

go
create or alter function getPostsByUserId(@userId bigint)
returns table as
	return (select * from Posts P where p.userId=@userId)
go

-- Function to get all answers of a question 
CREATE OR ALTER FUNCTION GetAllAnswersOnQuestion(@questionId BIGINT)
RETURNS TABLE
AS
RETURN
    (SELECT P.*
     FROM Posts AS P
     JOIN Replies AS R ON P.id = R.idOfReply
     WHERE P.type = 'answer' 
     AND R.idOfPostRepliedOn = @questionId);
GO