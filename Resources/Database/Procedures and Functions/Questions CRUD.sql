use Team42DB;

go
-- Function to get all the questions 
CREATE OR ALTER FUNCTION getAllQuestions()
RETURNS TABLE
AS
RETURN
    (SELECT * FROM Posts WHERE type = 'question');

go
-- Function to get a question by id
CREATE OR ALTER FUNCTION getQuestionByID(@id bigint)
RETURNS TABLE
AS
RETURN
    (SELECT * FROM Posts WHERE type = 'question' and id=@id);
go

-- Procedure to add a question
CREATE OR ALTER PROCEDURE addQuestion
    @userId BIGINT,
    @content VARCHAR(8000),
    @title VARCHAR(255),
    @categoryId BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Categories WHERE id = @categoryId) AND @title IS NOT NULL
    BEGIN
        INSERT INTO Posts (userId, content, datePosted, type, title, categoryId)
        VALUES (@userId, @content, GETDATE(), 'question', @title, @categoryId);
    END
END;
GO

-- Procedure to update a question
CREATE OR ALTER PROCEDURE updateQuestion
    @questionId BIGINT,
    @content VARCHAR(8000),
    @title VARCHAR(255),
    @categoryId BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @questionId AND type = 'question')
    BEGIN
        IF EXISTS (SELECT 1 FROM Categories WHERE id = @categoryId) OR @categoryId IS NULL
        BEGIN
            UPDATE Posts
            SET content = @content,
                dateOfLastEdit = GETDATE(),
                title = @title,
                categoryId = @categoryId
            WHERE id = @questionId;
        END
    END
END;
GO

-- Procedure to delete a question
CREATE OR ALTER PROCEDURE deleteQuestion
    @questionId BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Posts WHERE id = @questionId AND type = 'question')
    BEGIN
        DELETE FROM Posts WHERE id = @questionId;
    END
END;
GO
