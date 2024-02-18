<script lang="ts">
  import { Breadcrumbs } from "$lib/breadcrumbs";
  import { Span, Button, Dropdown, DropdownItem, DropdownDivider, Helper } from "flowbite-svelte";
  import { UserCircleSolid, UserSettingsSolid } from "flowbite-svelte-icons";
</script>

<div class="flex flex-row justify-between pb-2">
  <Breadcrumbs />
  <div class="flex flex-row items-center gap-2">
    <span class="hidden font-semibold sm:inline-block">Anonymous Yellow Rhino</span>
    <Button pill={true} class="!p-2" size="lg">
      <UserSettingsSolid />
    </Button>
    <Dropdown>
      <DropdownItem>Edit name</DropdownItem>
      <DropdownItem>Regenerate name</DropdownItem>
      <DropdownItem>Copy user ID</DropdownItem>
      <!-- <DropdownDivider />
      <DropdownItem>Login</DropdownItem> -->
    </Dropdown>
  </div>
</div>
<slot />
