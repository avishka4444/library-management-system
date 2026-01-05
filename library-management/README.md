# Library Management System - Frontend

A modern, responsive library management system built with Vue 3, TypeScript, and Vite.

## Features

- **Books Management**: Browse, search, create, update, and delete books
- **Authors Management**: Manage author information and biographies
- **Members Management**: Handle library member registrations and profiles
- **Borrowings Management**: Track book borrowings, returns, and due dates
- **Search Functionality**: Search across books, authors, and members
- **Responsive Design**: Modern UI built with Tailwind CSS

## Prerequisites

Before you begin, ensure you have the following installed:

- **Node.js** (v18 or higher) - [Download Node.js](https://nodejs.org/)
- **npm** (comes with Node.js) or **yarn**
- **Backend API** - The Library Management API should be running (see [Backend README](../LibraryManagement.Api/README.md))

## Installation

### 1. Navigate to the project directory

```bash
cd library-management
```

### 2. Install dependencies

Using npm:
```bash
npm install
```

Or using yarn:
```bash
yarn install
```

## Configuration

### API Base URL

The frontend connects to the backend API. By default, it's configured to connect to:
- `http://localhost:5150/api`

To change the API URL, create a `.env` file in the `library-management` directory:

```env
VITE_API_BASE_URL=http://localhost:5150/api
```

**Note**: If your backend runs on a different port, update the `.env` file accordingly.

## Running the Application

### Development Mode

Start the development server:

```bash
npm run dev
```

Or using yarn:
```bash
yarn dev
```

The application will be available at:
- `http://localhost:5173` (default Vite port)

The dev server supports hot module replacement (HMR), so changes will be reflected immediately.

### Build for Production

To build the application for production:

```bash
npm run build
```

Or using yarn:
```bash
yarn build
```

The production build will be created in the `dist/` directory.

### Preview Production Build

To preview the production build locally:

```bash
npm run preview
```

Or using yarn:
```bash
yarn preview
```

## Project Structure

```
library-management/
├── src/
│   ├── components/          # Reusable Vue components
│   │   ├── AppBar.vue      # Navigation bar
│   │   ├── BookCard.vue    # Book display card
│   │   ├── BookForm.vue    # Book create/edit form
│   │   ├── AuthorForm.vue  # Author create/edit form
│   │   ├── MemberForm.vue  # Member create/edit form
│   │   ├── BorrowingForm.vue # Borrowing form
│   │   ├── Modal.vue       # Modal dialog component
│   │   └── LoadingSpinner.vue # Loading indicator
│   ├── composables/        # Vue composables
│   │   ├── useApi.ts       # API integration
│   │   └── useFormValidation.ts # Form validation logic
│   ├── layouts/            # Layout components
│   │   └── MainLayout.vue  # Main application layout
│   ├── pages/              # Page components
│   │   ├── Home.vue        # Home page
│   │   ├── BookDetail.vue  # Book details page
│   │   ├── Authors.vue     # Authors management page
│   │   ├── Members.vue      # Members management page
│   │   ├── Borrowings.vue  # Borrowings management page
│   │   └── Search.vue      # Search page
│   ├── router/             # Vue Router configuration
│   │   └── index.ts        # Route definitions
│   ├── services/           # API service layer
│   │   └── api.ts          # API client functions
│   ├── utils/              # Utility functions
│   │   ├── constants.ts    # Application constants
│   │   └── errorHandler.ts # Error handling utilities
│   ├── App.vue             # Root component
│   ├── main.ts             # Application entry point
│   └── style.css           # Global styles
├── public/                 # Static assets
├── index.html              # HTML template
├── package.json            # Dependencies and scripts
├── tsconfig.json           # TypeScript configuration
├── vite.config.ts          # Vite configuration
└── tailwind.config.js      # Tailwind CSS configuration
```

## Technology Stack

- **Vue 3** - Progressive JavaScript framework
- **TypeScript** - Type-safe JavaScript
- **Vite** - Next-generation frontend build tool
- **Vue Router** - Official router for Vue.js
- **Tailwind CSS** - Utility-first CSS framework

## Available Scripts

- `npm run dev` - Start development server
- `npm run build` - Build for production
- `npm run preview` - Preview production build

## Troubleshooting

### Cannot connect to API

If you see connection errors:

1. **Check if the backend is running**: Ensure the Library Management API is running on the configured port (default: `http://localhost:5150`)
2. **Verify API URL**: Check your `.env` file or `src/utils/constants.ts` for the correct API base URL
3. **Check CORS settings**: Ensure the backend CORS policy allows requests from your frontend URL (default: `http://localhost:5173`)

### Port already in use

If port 5173 is already in use, Vite will automatically try the next available port. You can also specify a port:

```bash
npm run dev -- --port 3000
```

### Build errors

If you encounter TypeScript errors during build:

1. Check that all dependencies are installed: `npm install`
2. Verify TypeScript version compatibility
3. Check for any type errors in the console

## Development Tips

- Use Vue DevTools browser extension for debugging
- Check the browser console for API errors and warnings
- The application uses Vue 3 Composition API with `<script setup>` syntax
- All API calls are centralized in `src/services/api.ts`

## Contributing

1. Make sure the backend API is running
2. Install dependencies: `npm install`
3. Start the dev server: `npm run dev`
4. Make your changes
5. Test thoroughly before committing

## License

This project is part of the Library Management System.
