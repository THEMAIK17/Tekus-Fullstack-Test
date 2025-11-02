import React, { useState } from 'react';
import { createProvider } from '../Services/ProviderService';

// Simple email validation regex for professional touch
const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

function ProviderForm({ onProviderCreated }) {
    const [formData, setFormData] = useState({
        nit: '',
        name: '',
        email: ''
    });
    const [status, setStatus] = useState(null); 

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault(); 
        setStatus('Creating...');

        if (!emailRegex.test(formData.email)) {
            setStatus('Error: Invalid email format.');
            return;
        }
        
        try {
            await createProvider(formData); 
            setStatus(`Success! Provider created.`);
            setFormData({ nit: '', name: '', email: '' }); 
            
            if (onProviderCreated) {
                onProviderCreated(); // Call the refresh function passed from App.jsx
            }

        } catch (error) {
            let errorMessage = 'Creation failed.';
            if (error.response && error.response.data) {
                 errorMessage = error.response.data.Message || JSON.stringify(error.response.data);
            }
            setStatus(`Error: ${errorMessage}`);
        }
    };

    return (
        <div style={{ padding: '20px', border: '1px solid #ccc', margin: '20px 0' }}>
            <h3>Create New Provider (POST)</h3>
            <form onSubmit={handleSubmit}>
                <div><label htmlFor="nit">NIT:</label><input type="text" id="nit" name="nit" value={formData.nit} onChange={handleChange} required /></div>
                <div><label htmlFor="name">Name:</label><input type="text" id="name" name="name" value={formData.name} onChange={handleChange} required /></div>
                <div><label htmlFor="email">Email:</label><input type="email" id="email" name="email" value={formData.email} onChange={handleChange} required /></div>
                <button type="submit" style={{ marginTop: '10px' }}>Create Provider</button>
            </form>
            {status && <p style={{ color: status.startsWith('Error') ? 'red' : 'green' }}>{status}</p>}
        </div>
    );
}

export default ProviderForm;