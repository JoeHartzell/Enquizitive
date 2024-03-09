<script lang="ts">
  import { page } from "$app/stores";
  import { trpc } from "$lib/trpc/client";
  import { db } from "$lib/localstorage/db";
  import { breadcrumbs } from "$lib/breadcrumbs";
  import SuperDebug, { superForm } from "sveltekit-superforms";
  import { Heading, P, Button, Span, Badge, Modal, Input, Label, Helper } from "flowbite-svelte";
  import { EditSolid, UsersGroupSolid } from "flowbite-svelte-icons";
  import { onMount } from "svelte";
  import type { PageData } from "./$types";

  export let data: PageData;

  let openCreateQuizModal = false;

  breadcrumbs.set([{ label: "Sessions", path: "/sessions" }]);

  const { form, enhance, errors } = superForm(data.createQuizForm, {
    onUpdated({ form }) {
      if (form.valid) {
        db.sessions.add({
          name: form.data.name,
          description: form.data.description
          // globalId: form.data.id,
        });
      }
    }
  });

  onMount(async () => {
    const blah = await trpc($page).quizzes.getAll.query();
    console.log(blah);
  });
</script>

<Modal bind:open={openCreateQuizModal}>
  <form method="POST" action="?/createQuiz" class="flex flex-col space-y-6" use:enhance>
    <Heading tag="h3">Create a Quiz</Heading>
    <Label class="space-y-2">
      <Span>Name</Span>
      <Input name="name" color={$errors.name ? "red" : undefined} type="text" bind:value={$form.name} />
    </Label>
    <Label class="space-y-2">
      <Span>Description</Span>
      <Input name="description" type="text" color={$errors.description ? "red" : undefined} bind:value={$form.description} />
    </Label>
    <Button type="submit">Create</Button>
    <SuperDebug data={$form} />
  </form>
</Modal>

<div class="pb-4">
  <Heading tag="h1">Sessions</Heading>
  <span class="text-sm dark:text-gray-400">Your Gateway to Interactive Assessments, Connecting Learners and Educators in Real-Time</span>
</div>

<div class="mb-2 flex flex-row gap-2 rounded-lg bg-slate-100 p-4">
  <Button on:click={() => (openCreateQuizModal = true)}><EditSolid /> Create a quiz</Button>
  <Button><UsersGroupSolid /> Join a quiz</Button>
</div>

<Heading tag="h2" class="mt-4">
  <div class="flex flex-row items-end gap-2">
    <Span>Active Sessions</Span>
    <Badge rounded color="green">20</Badge>
  </div>
</Heading>
