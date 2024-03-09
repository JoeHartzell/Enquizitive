import { t } from "$lib/trpc/t";
// import { route as sessions } from "$lib/sessions/route";
import { route as quizzes } from "$lib/quizzes/route";

export const router = t.router({
  quizzes,
  greeting: t.procedure.query(async () => {
    return `Hello tRPC v10 @ ${new Date().toLocaleTimeString()}`;
  })
});

export type Router = typeof router;
