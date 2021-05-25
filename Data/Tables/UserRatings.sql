CREATE TABLE UserRatings(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	UserBeingRatedId NVARCHAR(MAX),
	UserGivingRatingId NVARCHAR(MAX),
	Liked BIT,
	DisLiked BIT
)