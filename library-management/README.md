# Library Management System - Frontend

A modern, responsive library management system built with Vue 3, TypeScript, and Vite.

## Quick Start

### Prerequisites
- Node.js (v18 or higher)
- npm or yarn
- Backend API running (see [Backend README](../LibraryManagement.Api/README.md))

### Setup and Run (3 minutes)

1. **Navigate to the frontend directory:**
   ```bash
   cd library-management
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Ensure the backend API is running:**
   - The API should be running on `http://localhost:5150`
   - See [Backend README](../LibraryManagement.Api/README.md) for setup instructions

4. **Start the development server:**
   ```bash
   npm run dev
   ```

5. **Open your browser:**
   - The app will be available at `http://localhost:5173`
   - Vite will automatically open it, or you can navigate manually

The frontend is now running! 🎉

**Note**: If you see connection errors, make sure:
- The backend API is running on port 5150
- SQL Server is running and database is set up
- Check browser console for specific error messages

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

**Using npm:**
```bash
npm install
```

**Or using yarn:**
```bash
yarn install
```

**Verify installation:**
After installation, you should see a `node_modules` directory created. If you encounter errors:
- Check Node.js version: `node --version` (should be v18+)
- Clear npm cache: `npm cache clean --force`
- Delete `node_modules` and `package-lock.json`, then run `npm install` again

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

**Start the development server:**

```bash
npm run dev
```

Or using yarn:
```bash
yarn dev
```

**The application will be available at:**
- `http://localhost:5173` (default Vite port)
- Vite will automatically open your browser, or you can navigate manually

**Features:**
- Hot Module Replacement (HMR) - changes reflect immediately
- Fast refresh - components update without losing state
- TypeScript type checking
- Error overlay in browser

**Verify it's working:**
1. Open `http://localhost:5173` in your browser
2. You should see the Library Management System home page
3. Navigate to different sections (Books, Authors, Members, Borrowings)
4. If you see loading spinners or errors, check that the backend API is running

**Note**: Make sure the backend API is running before starting the frontend, otherwise you'll see connection errors.

### Stop the Development Server

Press `Ctrl+C` in the terminal where the dev server is running.

### Restart the Development Server

```bash
# Stop with Ctrl+C, then:
npm run dev
```

### Build for Production

**Build the application:**

```bash
npm run build
```

Or using yarn:
```bash
yarn build
```

**Output:**
- Production build will be created in the `dist/` directory
- Files are optimized and minified
- Ready for deployment to any static hosting service

**Build options:**
```bash
# Build with detailed output
npm run build -- --mode production

# Build for staging
npm run build -- --mode staging
```

### Preview Production Build

**Test the production build locally:**

```bash
npm run preview
```

Or using yarn:
```bash
yarn preview
```

**This will:**
- Serve the production build from the `dist/` directory
- Available at `http://localhost:4173` (default preview port)
- Useful for testing the production build before deployment

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

**Symptoms**: "Network error", "Cannot connect to API", blank pages, loading forever

**Solutions:**

1. **Verify backend API is running:**
   ```bash
   # Check if API responds
   curl http://localhost:5150/health
   # Or open in browser: http://localhost:5150/swagger
   ```

2. **Start the backend API if it's not running:**
   ```bash
   cd ../LibraryManagement.Api
   dotnet run
   # Should see: "Now listening on: http://localhost:5150"
   ```

3. **Check API URL configuration:**
   - Default: `http://localhost:5150/api`
   - Check `src/utils/constants.ts` or `.env` file
   - Ensure it matches your backend port

4. **Verify CORS is configured:**
   - Backend should allow `http://localhost:5173`
   - Check `LibraryManagement.Api/Program.cs` for CORS configuration

5. **Check browser console:**
   - Open DevTools (F12)
   - Look for error messages in Console tab
   - Check Network tab for failed requests

6. **Restart both frontend and backend:**
   ```bash
   # Terminal 1 - Backend
   cd LibraryManagement.Api
   dotnet run
   
   # Terminal 2 - Frontend
   cd library-management
   npm run dev
   ```

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
