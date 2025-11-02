import { useState, useEffect, useCallback } from 'react';
import { getProviders } from '../Services/ProviderService';


export function useProviders() {
    const [providers, setProviders] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    // State management for Paging/Sorting/Searching
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [searchTerm, setSearchTerm] = useState('');
    const [sortBy, setSortBy] = useState('name');


    const fetchProviders = useCallback(async () => {
        setIsLoading(true);
        setError(null);
        try {
            // Call the service layer, passing all query parameters
            const params = {
                pageNumber,
                pageSize,
                sortBy,
                name: searchTerm 
            };
            const data = await getProviders(params);
            setProviders(data); 
        } catch (error) {
            console.error("Fetch error:", error);
            
            setError('Failed to fetch providers. Check network and backend status.');
            setProviders([]);
        } finally {
            setIsLoading(false);
        }
    }, [pageNumber, pageSize, searchTerm, sortBy]); 

    useEffect(() => {
        fetchProviders();
    }, [fetchProviders]);

    return {
        providers,
        isLoading,
        error,
        fetchProviders, // Expose fetchProviders for refreshing the list after creation
        pageNumber, setPageNumber,
        pageSize, setPageSize,
        searchTerm, setSearchTerm,
        sortBy, setSortBy
    };
}