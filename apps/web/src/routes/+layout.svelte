<script>
	import '../app.pcss';
	import { Navbar, NavBrand, NavLi, NavUl, NavHamburger } from 'flowbite-svelte';
</script>

<Navbar  >
  <NavBrand href="/">
    <span class="self-center whitespace-nowrap text-xl font-semibold dark:text-white">Enquizitive</span>
  </NavBrand>
  <NavHamburger  />
  <NavUl >
  </NavUl>
</Navbar>

<slot />
