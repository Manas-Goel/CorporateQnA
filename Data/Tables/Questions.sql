CREATE TABLE Questions(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Title NVARCHAR(MAX),
	Description NVARCHAR(MAX),
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	UserId NVARCHAR(255) FOREIGN KEY REFERENCES UserDetails(Id),
	Upvotes INT,
	Views INT,
	CreatedOn DATETIME2(7),
	IsResolved BIT,
	TotalAnswers INT
)