CREATE TABLE Answers(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Description NVARCHAR(MAX),
	Likes INT,
	Dislikes INT,
	QuestionId INT,
	UserId NVARCHAR(255),
	IsBestSolution BIT,
	CreatedOn DATETIME2(7)
)