import ProviderList from './components/ProviderList'; 
import ProviderForm from './components/ProviderForm'; 
import { useProviders } from './hooks/useProviders'; 
import './App.css'; 

function App() {
    // App.jsx is the ONLY component using the hook 
    const { 
        providers, isLoading, error, fetchProviders, 
        pageNumber, setPageNumber, pageSize, setPageSize, 
        searchTerm, setSearchTerm, sortBy, 
    } = useProviders();

    // Function passed to the Form to trigger a data refresh
    const handleProviderCreated = () => {
        fetchProviders();
    };

    return (
        <div className="App">
            <h1>TEKUS Provider Management (Fullstack MVP)</h1>
            <hr />
            
            {/*  Pass the refresh function to the Form */}
            <ProviderForm onProviderCreated={handleProviderCreated} /> 

            {/* Pass the data and state to the List */}
            <ProviderList 
                providers={providers}
                isLoading={isLoading}
                error={error}
                fetchProviders={fetchProviders}
                pageNumber={pageNumber} setPageNumber={setPageNumber}
                pageSize={pageSize} setPageSize={setPageSize}
                searchTerm={searchTerm} setSearchTerm={setSearchTerm}
                sortBy={sortBy} 
            /> 
        </div>
    );
}

export default App;