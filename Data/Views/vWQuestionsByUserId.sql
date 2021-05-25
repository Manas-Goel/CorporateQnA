CREATE VIEW vWQuestionsByUserId AS
SELECT Questions.*,UserDetails.Name,UserDetails.ProfileImageUrl FROM Questions
INNER JOIN UserDetails ON Questions.UserId=UserDetails.Id