CREATE TABLE Categories(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(MAX),
	Description NVARCHAR(MAX),
	QuestionsTagged INT
)