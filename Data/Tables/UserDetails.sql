CREATE TABLE UserDetails(
	Id NVARCHAR(255) PRIMARY KEY,
	Email NVARCHAR(255) UNIQUE,
	Password NVARCHAR(MAX),
	Name NVARCHAR(MAX),
	JobProfile NVARCHAR(MAX),
	Department NVARCHAR(MAX),
	Location NVARCHAR(MAX),
	ProfileImageUrl NVARCHAR(MAX),
	QuestionsAsked INT,
	QuestionsAnswered INT,
	QuestionsSolved INT,
	Likes INT,
	Dislikes INT
)