import { t, publicProcedure } from "$lib/trpc/t";
import { db } from "$lib/drizzle/client";
import { quiz } from "./model";
import { schemaCreateQuiz } from "$lib/sessions/validation/create.schema";

export const route = t.router({
  create: publicProcedure.input(schemaCreateQuiz).mutation(async ({ input }) => {
    console.log(input);
    return await db
      .insert(quiz)
      .values({
        ...input
      })
      .execute();
  }),

  getAll: publicProcedure.query(async () => {
    return await db.select().from(quiz).execute();
  })
});
