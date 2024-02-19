import { pgTable, text, uuid } from "drizzle-orm/pg-core";

export const session = pgTable("sessions", {
  id: uuid("id").primaryKey(),
  name: text("name").notNull(),
  description: text("description")
});
