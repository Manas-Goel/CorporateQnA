CREATE VIEW viewUserQuestions AS
SELECT Questions.*,UserDetails.Name,UserDetails.ProfileImageUrl FROM Questions
INNER JOIN UserDetails ON Questions.UserId=UserDetails.Id