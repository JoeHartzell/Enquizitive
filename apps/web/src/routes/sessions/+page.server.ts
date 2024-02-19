import { schemaCreateQuiz } from "$lib/sessions/validation/create.schema";
import { createContext } from "$lib/trpc/context.js";
import { router } from "$lib/trpc/router.js";
import { t } from "$lib/trpc/t";
import { fail } from "@sveltejs/kit";
import { message, superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";

export const load = async () => {
  const createQuizForm = await superValidate(zod(schemaCreateQuiz));
  return { createQuizForm };
};

export const actions = {
  createQuiz: async (event) => {
    const { request } = event;
    const createQuizForm = await superValidate(request, zod(schemaCreateQuiz));

    if (!createQuizForm.valid) return fail(400, { createQuizForm });

    t.createCallerFactory(router)(
      await createContext(event)
    ).sessions.createQuiz(createQuizForm.data);

    return message(createQuizForm, "Quiz form submitted");
  }
};
