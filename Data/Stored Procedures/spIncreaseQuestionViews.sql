CREATE PROCEDURE spIncreaseQuestionViews
@questionId INT,
@userId NVARCHAR(MAX)
AS
BEGIN
	BEGIN TRANSACTION
		IF
		(SELECT COUNT(*) FROM QuestionViews WHERE QuestionId=@questionId AND UserId=@userId)=0
		BEGIN
			UPDATE Questions
            SET Views=Views+1
            WHERE Id=@questionId

			INSERT INTO QuestionViews(QuestionId,UserId)
            VALUES(@questionId,@userId)
		END
	COMMIT TRANSACTION
END