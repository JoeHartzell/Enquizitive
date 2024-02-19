<script>
  import { navigating, page } from "$app/stores";
  import { Breadcrumbs, breadcrumbs } from "$lib/breadcrumbs";
  import "../app.pcss";
  import { Navbar, NavBrand, NavLi, NavUl, NavHamburger, Span } from "flowbite-svelte";

  $: if ($navigating) {
    breadcrumbs.reset();
    console.log("something");
  }
</script>

<div class="flex min-h-screen flex-col items-center">
  <Navbar color="form" class="sticky top-0 z-20 w-full border-b py-2.5 ">
    <NavBrand href="/">
      <span class="self-center whitespace-nowrap text-xl font-semibold dark:text-white">En<Span gradient>quiz</Span>itive</span>
    </NavBrand>
    <NavHamburger />
    <NavUl>
      <NavLi href="/sessions">Sessions</NavLi>
    </NavUl>
  </Navbar>

  <div class="flex flex-1 flex-col px-2 pt-2 md:container md:px-0">
    <slot />
  </div>
</div>
