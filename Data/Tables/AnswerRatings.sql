CREATE TABLE AnswerRatings(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	AnswerId INT,
	UserId NVARCHAR(255),
	Liked BIT,
	Disliked BIT
)