/* Ensure the correct database context is selected before execution.
    USE TekusDB;
    GO
*/

--- 1. POPULATE Providers ---
INSERT INTO Providers (Nit, Name, Email)
VALUES
    ('900.123.456-1', 'Tekus Colombia', 'contacto@tekus.co'),
    ('800.123.456-2', 'Dev Solutions', 'solutions@devs.com'),
    ('700.123.456-3', 'Cloud Services Inc.', 'info@cloudservices.com'),
    ('600.123.456-4', 'Infraestructura Segura', 'seguridad@infra.net'),
    ('500.123.456-5', 'Innovatech SAS', 'innovacion@innovatech.co'),
    ('400.123.456-6', 'Software a Medida', 'proyectos@softwaremedida.com'),
    ('300.123.456-7', 'Data Analytics Co', 'data@analytics.com'),
    ('200.123.456-8', 'Mobile Apps Corp', 'apps@mobilecorp.io'),
    ('100.123.456-9', 'Web Masters Latam', 'soporte@webmasters.lat'),
    ('999.123.456-0', 'Consultores TI', 'consultoria@ti.com');
GO

-- --- 2. POPULATE ProviderCustomFields ---
-- Sample custom data for existing providers.
INSERT INTO ProviderCustomFields (ProviderId, [Key], Value)
VALUES
(1, 'Mars Contact Number', '123-MARS-456'),
(1, 'Payroll Pets Count', '3'),
(2, 'CEO', 'Ana Torres'),
(3, 'Data Center Location', 'USA-East-1');
GO

-- --- 3. POPULATE Services ---
-- Services linked to various providers via ProviderId.
INSERT INTO Services (Name, HourlyRateUSD, ProviderId)
VALUES
('Spatial Content Delivery', 150.00, 1),
('Forced Byte Disappearance', 200.50, 1),
('REST API Development', 90.00, 2),
('AWS Consulting', 120.00, 3),
('Security Audit', 110.00, 4),
('Mobile App Development (iOS)', 85.00, 8),
('Mobile App Development (Android)', 85.00, 8),
('UX/UI Design', 70.00, 5),
('Database Maintenance', 100.00, 7),
('Corporate Web Design', 60.00, 9);
GO

--- 4. POPULATE ServiceCountries ---
-- Country availability mapping for services.
INSERT INTO ServiceCountries (ServiceId, CountryName)
VALUES
(1, 'Colombia'), -- Service 1 available in Colombia
(1, 'Peru'),     -- Service 1 available in Peru
(1, 'Mexico'),
(2, 'Colombia'), 
(3, 'Argentina'),
(4, 'USA'),
(4, 'Canada');
GO