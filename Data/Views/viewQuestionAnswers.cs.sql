CREATE VIEW viewQuestionAnswers AS
SELECT Answers.*,UserDetails.Name,UserDetails.ProfileImageUrl FROM Answers
INNER JOIN UserDetails ON Answers.UserId=UserDetails.Id