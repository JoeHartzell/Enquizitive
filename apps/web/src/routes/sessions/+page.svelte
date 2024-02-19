<script lang="ts">
  import { page } from "$app/stores";
  import { trpc } from "$lib/trpc/client";
  import { breadcrumbs } from "$lib/breadcrumbs";
  import { Heading, P, Button, Span, Badge, Indicator, Tabs, TabItem } from "flowbite-svelte";
  import { EditSolid, UsersGroupSolid } from "flowbite-svelte-icons";
  import { onMount } from "svelte";

  breadcrumbs.set([
    { label: "Home", path: "/" },
    { label: "Sessions", path: "/sessions" }
  ]);

  onMount(async () => {
    const greeting = await trpc($page).greeting.query();
    console.log(greeting);
  });
</script>

<div class="pb-4">
  <Heading tag="h1">Sessions</Heading>
  <span class="text-sm dark:text-gray-400">Your Gateway to Interactive Assessments, Connecting Learners and Educators in Real-Time</span>
</div>

<div class="mb-2 flex flex-row gap-2 rounded-lg bg-slate-100 p-4">
  <Button><EditSolid /> Create a quiz</Button>
  <Button><UsersGroupSolid /> Join a quiz</Button>
</div>

<Heading tag="h2" class="mt-4">
  <div class="flex flex-row items-end gap-2">
    <Span>Active Sessions</Span>
    <Badge rounded color="green">20</Badge>
  </div>
</Heading>
