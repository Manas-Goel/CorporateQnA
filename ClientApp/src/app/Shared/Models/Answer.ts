export class Answer {
  id: number;
  description: string;
  likes: number;
  dislikes: number;
  questionId: number;
  userId: string;
  isBestSolution: boolean;
  createdOn: Date;
}
