CREATE PROCEDURE spRateNewAnswer
@Liked BIT,
@Disliked BIT,
@answerId INT,
@userId NVARCHAR(MAX)
AS
BEGIN
	BEGIN TRANSACTION
		IF @Liked=1 AND @Disliked=0
		BEGIN
			UPDATE Answers
            SET Likes = Likes+1
            WHERE Id=@answerId
		END
		ELSE IF @Liked=0 AND @Disliked=1
		BEGIN
			UPDATE Answers
            SET Dislikes = Dislikes+1
            WHERE Id=@answerId
		END

		INSERT INTO AnswerRatings(AnswerId,UserId,Liked,Disliked)
        VALUES(@answerId,@userId,@Liked,@Disliked)

	COMMIT TRANSACTION
END