/* Database creation and usage. 
    Execute manually in a SQL Server management tool.
    
    CREATE DATABASE TekusDB;
    GO
    USE TekusDB;
    GO
*/

 --- 1. Providers Table ---
-- Stores primary information for each provider entity.
CREATE TABLE Providers (
                           Id INT PRIMARY KEY IDENTITY(1,1),
                           Nit NVARCHAR(20) NOT NULL UNIQUE,       
                           Name NVARCHAR(100) NOT NULL,
                           Email NVARCHAR(100) NOT NULL
);
GO

--- 2. Services Table ---
-- Stores services offered by providers.
CREATE TABLE Services (
                          Id INT PRIMARY KEY IDENTITY(1,1),
                          Name NVARCHAR(100) NOT NULL,
                          HourlyRateUSD DECIMAL(18, 2) NOT NULL, 
                          ProviderId INT NOT NULL,
                          CONSTRAINT FK_Service_Provider FOREIGN KEY (ProviderId) REFERENCES Providers(Id)
);
GO

--- 3. ServiceCountries Table ---
-- Junction table for the many-to-many relationship between Services and Countries.

CREATE TABLE ServiceCountries (
                                  Id INT PRIMARY KEY IDENTITY(1,1),
                                  CountryName NVARCHAR(100) NOT NULL, 
                                  ServiceId INT NOT NULL,
                                  CONSTRAINT FK_ServiceCountry_Service FOREIGN KEY (ServiceId) REFERENCES Services(Id)
);
GO

--- 4. ProviderCustomFields Table ---
-- Implements the "custom fields" requirement using a Key/Value store.

CREATE TABLE ProviderCustomFields (
                                      Id INT PRIMARY KEY IDENTITY(1,1),
    [Key] NVARCHAR(100) NOT NULL, 
    Value NVARCHAR(500) NOT NULL,
    ProviderId INT NOT NULL,
    CONSTRAINT FK_CustomField_Provider FOREIGN KEY (ProviderId) REFERENCES Providers(Id)
    );
GO