import React from 'react';

// Component receives data, loading status, and handlers from App.jsx .
function ProviderList({ 
    providers, isLoading, error, fetchProviders, 
    pageNumber, setPageNumber, searchTerm, setSearchTerm, 
    
}) { 

    const handleNextPage = () => {
        setPageNumber(pageNumber + 1);
    };

    const handlePrevPage = () => {
        if (pageNumber > 1) {
            setPageNumber(pageNumber - 1);
        }
    };
    // Simple logic for searching (will update the hook's state)
    const handleSearchChange = (e) => {
        setSearchTerm(e.target.value);
        setPageNumber(1); 
    };

    if (isLoading) {
        return <p>Loading providers...</p>;
    }

    if (error) {
        return <p style={{ color: 'red' }}>Error: {error}</p>;
    }

    if (providers.length === 0) {
        return <p>No providers found.</p>;
    }

    return (
        <div>
            <h2>Provider List ({providers.length} shown)</h2>
            
            {/* Search Input  */}
            <input
                type="text"
                placeholder="Search by name..."
                value={searchTerm}
                onChange={handleSearchChange}
                style={{ marginBottom: '10px', padding: '5px' }}
            />

            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '10px' }}>
                {/* Manual Refresh Button (for debugging) */}
                <button onClick={fetchProviders} >Manual Refresh</button>

                {/* Pagination Controls  */}
                <div>
                    <button onClick={handlePrevPage} disabled={pageNumber === 1}>Previous</button>
                    <span style={{ margin: '0 10px' }}>Page: {pageNumber}</span>
                    <button onClick={handleNextPage}>Next</button> 
                </div>
            </div>

            <table border="1" cellPadding="5" cellSpacing="0" style={{ width: '100%', marginTop: '10px' }}>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>NIT</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Services Count</th>
                        <th>Custom Fields</th>
                    </tr>
                </thead>
                <tbody>
                    {providers.map(provider => (
                        <tr key={provider.id}>
                            <td>{provider.id}</td>
                            <td>{provider.nit}</td>
                            <td>{provider.name}</td>
                            <td>{provider.email}</td>
                            {/* Accessing nested data  */}
                            <td>{provider.services?.$values?.length || 0}</td> 
                            <td>{provider.customFields?.$values?.length || 0}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default ProviderList;