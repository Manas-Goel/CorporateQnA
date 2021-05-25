CREATE PROCEDURE spUpvoteOldQuestion
@questionId INT,
@userId NVARCHAR(MAX),
@upvote BIT
AS
BEGIN
	BEGIN TRANSACTION
		UPDATE QuestionUpvotes
        SET Upvote=@upvote
        WHERE QuestionId=@questionId AND UserId=@userId

		IF @upvote = 1
			UPDATE Questions
			SET UpVotes=UpVotes+1
			WHERE Id=@questionId
		ELSE
			UPDATE Questions
			SET UpVotes=UpVotes-1
			WHERE Id=@questionId
	COMMIT TRANSACTION
END