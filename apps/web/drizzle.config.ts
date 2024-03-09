import { defineConfig } from "drizzle-kit";

export default defineConfig({
  schema: "./src/lib/drizzle/schema.ts",
  driver: "pg",
  dbCredentials: {
    connectionString: process.env.DATABASE_URL ?? "postgres://postgres@localhost:5432/postgres"
  },
  verbose: true,
  strict: true,
  out: "./src/lib/drizzle/migraions"
});
