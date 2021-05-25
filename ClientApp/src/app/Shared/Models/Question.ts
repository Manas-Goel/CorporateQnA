export class Question {
  id: number;
  title: string;
  description: string;
  categoryId: number;
  userId: string;
  upVotes: number;
  views: number;
  createdOn: Date;
  totalAnswers: number;
  isResolved: boolean;
}
