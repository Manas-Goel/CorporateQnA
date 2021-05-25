CREATE PROCEDURE spRateOldUser
@Liked BIT,
@Disliked BIT,
@UserId NVARCHAR(MAX),
@UserId2 NVARCHAR(MAX),
@PrevLiked BIT,
@PrevDisliked BIT
AS
BEGIN
	BEGIN TRANSACTION
		IF @Liked=1 AND @Disliked=0
		BEGIN
			IF @PrevDisliked=1
				UPDATE UserDetails
                SET Likes = Likes+1,Dislikes = Dislikes-1
                WHERE Id=@UserId
			ELSE
				UPDATE UserDetails
                SET Likes = Likes+1
                WHERE Id=@UserId
		END
		ELSE IF @Liked=0 AND @Disliked=1
		BEGIN
			IF @PrevLiked=1
				UPDATE UserDetails
                SET Likes = Likes-1,Dislikes = Dislikes+1
                WHERE Id=@UserId
			ELSE
				UPDATE UserDetails
                SET Dislikes = Dislikes+1
                WHERE Id=@UserId
		END
		ELSE
		BEGIN
			IF @PrevLiked=1
				UPDATE UserDetails
				SET Likes = Likes-1
				WHERE Id=@UserId
			ELSE
				UPDATE UserDetails
				sET Dislikes = Dislikes-1
				WHERE Id=@UserId
		END

		UPDATE UserRatings
        SET Liked=@Liked, Disliked=@Disliked
        WHERE UserBeingRatedId=@UserId and UserGivingRatingId=@UserId2
	COMMIT TRANSACTION
END