CREATE PROCEDURE spRateOldAnswer
@Liked BIT,
@Disliked BIT,
@PreviousLiked BIT,
@PreviousDisliked BIT,
@answerId INT,
@UserId NVARCHAR(MAX)
AS
BEGIN
	BEGIN TRANSACTION
		IF @Liked=1 AND @Disliked=0
		BEGIN
			IF @PreviousDisliked=1
				UPDATE Answers
                SET Likes = Likes+1,Dislikes = Dislikes-1
                WHERE Id=@answerId
			ELSE
				UPDATE Answers
                SET Likes = Likes+1
                WHERE Id=@answerId
		END
		ELSE IF @Liked=0 AND @Disliked=1
		BEGIN
			IF @PreviousLiked=1
				UPDATE Answers
                SET Likes = Likes-1,Dislikes = Dislikes+1
                WHERE Id=@answerId
			ELSE
				UPDATE Answers
                SET Dislikes = Dislikes+1
                WHERE Id=@answerId
		END
		ELSE
		BEGIN
			IF @PreviousLiked=1
				UPDATE Answers
                SET Likes = Likes-1
                WHERE Id=@answerId
			ELSE
				UPDATE Answers
                SET Dislikes = Dislikes-1
                WHERE Id=@answerId
		END

		UPDATE AnswerRatings
        SET Liked=@Liked, Disliked=@Disliked
        WHERE AnswerId=@AnswerId AND UserId=@UserId
	COMMIT TRANSACTION
END