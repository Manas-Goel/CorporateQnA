CREATE PROCEDURE spUpvoteNewQuestion
@questionId INT,
@userId NVARCHAR(MAX),
@upvote BIT
AS
BEGIN
	BEGIN TRANSACTION
		INSERT INTO QuestionUpvotes(QuestionId,UserId,Upvote)
        VALUES(@questionId,@userId,@upvote)

		UPDATE Questions
        SET UpVotes=UpVotes+1
        WHERE Id=@questionId
	COMMIT TRANSACTION
END