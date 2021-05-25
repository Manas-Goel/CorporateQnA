CREATE PROCEDURE spGetAllAnswersByQuestionId
@QuestionId INT
AS
BEGIN
	SELECT Answers.*,UserDetails.Name,UserDetails.ProfileImageUrl FROM Answers
           INNER JOIN UserDetails ON Answers.UserId=UserDetails.Id
           WHERE QuestionId = @QuestionId
END