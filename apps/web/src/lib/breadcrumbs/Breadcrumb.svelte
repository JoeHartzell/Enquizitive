<script lang="ts">
  import { page } from "$app/stores";
  import { Breadcrumb, BreadcrumbItem } from "flowbite-svelte";
  import { breadcrumbs } from "./breadcrumb.store";
</script>

<Breadcrumb>
  {#each $breadcrumbs as breadcrumb}
    <BreadcrumbItem href={breadcrumb.path}>{breadcrumb.label}</BreadcrumbItem>
  {/each}
</Breadcrumb>
