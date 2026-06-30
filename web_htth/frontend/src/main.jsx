import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import { AuthProvider } from './context/AuthContext'
import MainLayout from './components/MainLayout'
import './styles/index.css'
import App from './App.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <MainLayout>
          <App />
        </MainLayout>
      </AuthProvider>
    </BrowserRouter>
  </StrictMode>,
)

