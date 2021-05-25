CREATE PROCEDURE spAddQuestion
@Title NVARCHAR(MAX),
@Description NVARCHAR(MAX),
@CategoryId INT,
@UserId NVARCHAR(MAX),
@CreatedOn DATETIME2(7)
AS
BEGIN
	Begin Transaction
		insert into Questions(Title,Description,CategoryId,UserId,CreatedOn,Upvotes,Views)
        values(@Title,@Description,@CategoryId,@UserId,@CreatedOn,0,0)

		Update Categories
        Set QuestionsTagged=QuestionsTagged+1
        Where Id=@CategoryId

		Update UserDetails
        Set QuestionsAsked = QuestionsAsked+1
        Where Id=@UserId
	Commit Transaction
END