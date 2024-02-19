import { pgTable, pgEnum, text, uuid } from "drizzle-orm/pg-core";

export const typeEnum = pgEnum("session_type", ["educator", "learner"]);

export const session = pgTable("sessions", {
  id: uuid("id").primaryKey().defaultRandom(),
  name: text("name").notNull(),
  description: text("description"),
  type: typeEnum("type").notNull()
});

export type Session = typeof session.$inferSelect;
