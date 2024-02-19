<script>
  import { Button, P, Heading, Img, Span } from "flowbite-svelte";
  import { ArrowRightOutline } from "flowbite-svelte-icons";
</script>

<div class="m-auto flex max-w-[786px] flex-col items-center justify-center">
  <Heading class="mb-4 text-center md:mb-8" customSize="text-4xl font-extrabold md:text-5xl lg:text-7xl md:text-[70px]">
    En<Span gradient>quiz</Span>itive
  </Heading>
  <P class="text-17px mb-6 text-center md:text-xl dark:text-gray-400"
    >Experience learning with speed and simplicity. Engage in rapid quizzes that make education exciting and accessible</P
  >
  <Button href="/sessions">
    Start a session
    <ArrowRightOutline class="ms-2 h-3.5 w-3.5" />
  </Button>
</div>
