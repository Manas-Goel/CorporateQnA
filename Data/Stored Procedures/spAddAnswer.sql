CREATE PROCEDURE spAddAnswer
@questionId INT,
@userId NVARCHAR(MAX),
@Description NVARCHAR(MAX),
@IsBestSolution BIT,
@CreatedOn DATETIME2(7)
AS
BEGIN
	BEGIN TRANSACTION
		IF
		(SELECT COUNT(*) FROM Answers WHERE QuestionId=@questionId AND UserId=@userId) = 0
		BEGIN
			UPDATE UserDetails
            SET QuestionsAnswered=QuestionsAnswered+1
            WHERE Id=@UserId
		END

		INSERT INTO Answers(Description,Likes,Dislikes,QuestionId,UserId,IsBestSolution,CreatedOn)
        VALUES(@Description,0,0,@questionId,@UserId,@IsBestSolution,@CreatedOn)
		
		UPDATE Questions
        SET TotalAnswers=TotalAnswers+1
        WHERE Id=@questionId
	COMMIT TRANSACTION
END