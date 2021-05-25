CREATE PROCEDURE spInsertAndUpdateTotalAnswers
@Description NVARCHAR(MAX),
@QuestionId INT,
@UserId NVARCHAR(MAX),
@IsBestSolution BIT,
@CreatedOn DATETIME2(7)

AS
BEGIN
	BEGIN TRANSACTION
		INSERT INTO Answers(Description,Likes,Dislikes,QuestionId,UserId,IsBestSolution,CreatedOn)
        VALUES(@Description,0,0,@QuestionId,@UserId,@IsBestSolution,@CreatedOn)
		
		UPDATE Questions
        SET TotalAnswers=TotalAnswers+1
        WHERE Id=@QuestionId
	COMMIT TRANSACTION
END