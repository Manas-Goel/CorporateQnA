CREATE PROCEDURE spRateNewUser
@Liked BIT,
@Disliked BIT,
@UserId NVARCHAR(MAX),
@UserId2 NVARCHAR(MAX)
As
BEGIN
	BEGIN TRANSACTION
		IF @Liked=1 and @Disliked=0
			UPDATE UserDetails
			SET Likes = Likes+1
			WHERE Id=@UserId
		ELSE IF @Liked=0 and @Disliked=1
			UPDATE UserDetails
            SET Dislikes = Dislikes+1
            WHERE Id=@UserId

		INSERT INTO UserRatings(UserBeingRatedId,UserGivingRatingId,Liked,Disliked)
        VALUES(@UserId,@UserId2,@Liked,@Disliked)
	COMMIT TRANSACTION
END