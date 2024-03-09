import { pgTable, uuid, text } from "drizzle-orm/pg-core";

export const quiz = pgTable("quizzes", {
  id: uuid("id").primaryKey().defaultRandom(),
  name: text("name").notNull(),
  description: text("description")
});

export type Quiz = typeof quiz.$inferSelect;
