import axios from 'axios';

// Load API key from environment variables.

const API_KEY = import.meta.env.VITE_API_KEY;

// Base URL of the API.
const API_BASE_URL = 'http://localhost:5262/api/Providers';

const config = {
    headers: {
        'X-API-Key': API_KEY 
    }
};

 // Executes the GET request to retrieve providers with filtering/paging.

export const getProviders = async (params) => {
    try {
        const response = await axios.get(API_BASE_URL, { params, ...config });
        return response.data.$values || []; 
    } catch (error) {
        console.error("API Call Error in getProviders:", error);
        throw error;
    }
};


 // Executes the POST request to create a new provider.
 
export const createProvider = async (providerData) => {
    try {
        const response = await axios.post(API_BASE_URL, providerData, config);
        return response.data; 
    } catch (error) {
        console.error("API Call Error in createProvider:", error);
        throw error;
    }
};