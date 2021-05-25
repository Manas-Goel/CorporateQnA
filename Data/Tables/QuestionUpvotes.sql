CREATE TABLE QuestionUpvotes(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	UserId NVARCHAR(255),
	QuestionId INT,
	Upvote BIT
)