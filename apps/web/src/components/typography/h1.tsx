import { Slot, component$ } from "@builder.io/qwik";

export default component$(() => {
  return (
    <h1 class="text-5xl font-extrabold dark:text-white">
      <Slot />
    </h1>
  );
});
