import { writable } from "svelte/store";

export type Breadcrumb = {
  label: string;
  path: string;
};

const createBreadcrumbs = () => {
  const { subscribe, set, update } = writable<Breadcrumb[]>([]);

  return {
    subscribe,
    set: (breadcrumbs: Breadcrumb[]) => set(breadcrumbs),
    reset: () => set([])
  };
};

export const breadcrumbs = createBreadcrumbs();
