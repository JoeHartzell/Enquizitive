import { t } from "$lib/trpc/t";
import { route as sessions } from "$lib/sessions/route";

export const router = t.router({
  sessions,
  greeting: t.procedure.query(async () => {
    return `Hello tRPC v10 @ ${new Date().toLocaleTimeString()}`;
  })
});

export type Router = typeof router;
