import { createRouter, createWebHistory } from 'vue-router';

export const navRoutes = [
  { label: 'Books', path: '/' },
  { label: 'Search', path: '/search' },
  { label: 'Authors', path: '/authors' },
  { label: 'Members', path: '/members' },
  { label: 'Borrowings', path: '/borrowings' },
] as const;

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      component: () => import('../layouts/MainLayout.vue'),
      children: [
        {
          path: '',
          name: 'Home',
          component: () => import('../pages/Home.vue'),
        },
        {
          path: 'book/:id',
          name: 'BookDetail',
          component: () => import('../pages/BookDetail.vue'),
        },
        {
          path: 'search',
          name: 'Search',
          component: () => import('../pages/Search.vue'),
        },
        {
          path: 'authors',
          name: 'Authors',
          component: () => import('../pages/Authors.vue'),
        },
        {
          path: 'members',
          name: 'Members',
          component: () => import('../pages/Members.vue'),
        },
        {
          path: 'borrowings',
          name: 'Borrowings',
          component: () => import('../pages/Borrowings.vue'),
        },
      ],
    },
  ],
});

export default router;

