import { t, publicProcedure } from "$lib/trpc/t";
import { z } from "zod";
import { db } from "$lib/drizzle/client";
import { session } from "./model";
import { schemaCreateQuiz } from "$lib/sessions/validation/create.schema";

export const route = t.router({
  createQuiz: publicProcedure
    .input(schemaCreateQuiz)
    .mutation(async ({ input }) => {
      console.log(input);
      return await db
        .insert(session)
        .values({
          ...input,
          type: "educator"
        })
        .execute();
    }),

  getSessions: publicProcedure.query(async () => {
    return await db.select().from(session).execute();
  })
});
