CREATE PROCEDURE spMarkBestAnswer
@questionId INT,
@answerId INT,
@isBest BIT
AS
BEGIN
	DECLARE @userId AS NVARCHAR(255)
	DECLARE @count AS INT
	SELECT @userId=UserId FROM Answers WHERE QuestionId=@questionId AND Id=@answerId
	SELECT @count = COUNT(*) FROM Answers WHERE QuestionId=@questionId AND IsBestSolution=1 AND UserId=@userId

	BEGIN TRANSACTION
		IF @isBest=1
		BEGIN
			IF
			@count = 0
				UPDATE UserDetails
				SET QuestionsSolved=QuestionsSolved+1
				WHERE Id=@userId
		END
		ELSE
		BEGIN
			IF @count = 1
				UPDATE UserDetails
				SET QuestionsSolved=QuestionsSolved-1
				WHERE Id=@userId
		END

		UPDATE Answers
        SET IsBestSolution=@isBest
        WHERE Id=@answerId AND QuestionId=@questionId

		IF
		(SELECT COUNT(*) FROM Answers WHERE QuestionId=@questionId AND IsBestSolution=1) > 0
			UPDATE Questions
			SET IsResolved=1
			WHERE Id=@QuestionId
		ELSE
			UPDATE Questions
			SET IsResolved=0
			WHERE Id=@QuestionId

	COMMIT TRANSACTION
END