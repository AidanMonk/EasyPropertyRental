CREATE TABLE propertyOwners (
    po_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    password NVARCHAR(50) NOT NULL,
    created_at DATETIME DEFAULT GETDATE()
);

CREATE TABLE propertyManagers (
    pm_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    phone NVARCHAR(20),
    password NVARCHAR(50) NOT NULL,
    po_id INT,
    FOREIGN KEY (po_id) REFERENCES propertyOwners(po_id) ON DELETE SET NULL
);

CREATE TABLE buildings (
    building_id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL,
    address NVARCHAR(255) NOT NULL,
    pm_id INT,
    FOREIGN KEY (pm_id) REFERENCES propertyManagers(pm_id) ON DELETE SET NULL
);

CREATE TABLE apartments (
    apartment_id INT IDENTITY(1,1) PRIMARY KEY,
    building_id INT,
    unit_number NVARCHAR(10) NOT NULL,
    floor INT,
    bedrooms INT,
    bathrooms INT,
    rent DECIMAL(10, 2),
    is_available BIT DEFAULT 1,
    FOREIGN KEY (building_id) REFERENCES buildings(building_id) ON DELETE CASCADE
);

CREATE TABLE tenants (
    tenant_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    phone NVARCHAR(20),
	password NVARCHAR(50) NOT NULL,
    apartment_id INT,
    move_in_date DATE,
    move_out_date DATE,
    FOREIGN KEY (apartment_id) REFERENCES apartments(apartment_id) ON DELETE SET NULL
);

CREATE TABLE appointments (
    appointment_id INT IDENTITY(1,1) PRIMARY KEY,
    tenant_id INT,
    pm_id INT,
    appointment_date DATETIME NOT NULL,
    notes NVARCHAR(MAX),
    created_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (tenant_id) REFERENCES tenants(tenant_id) ON DELETE CASCADE,
    FOREIGN KEY (pm_id) REFERENCES propertyManagers(pm_id) ON DELETE SET NULL
);

CREATE TABLE messages (
    message_id INT IDENTITY(1,1) PRIMARY KEY,
    sender_type NVARCHAR(20) CHECK (sender_type IN ('tenant', 'propertyManager', 'administrator')),
    sender_id INT NOT NULL,
    receiver_type NVARCHAR(20) CHECK (receiver_type IN ('tenant', 'propertyManager', 'administrator')),
    receiver_id INT NOT NULL,
    content NVARCHAR(MAX) NOT NULL,
    sent_at DATETIME DEFAULT GETDATE(),

    -- Foreign key constraints to ensure valid sender and receiver IDs
    FOREIGN KEY (sender_id) REFERENCES tenants(tenant_id),
    FOREIGN KEY (sender_id) REFERENCES propertyManagers(pm_id),
    FOREIGN KEY (sender_id) REFERENCES propertyOwners(po_id),

    FOREIGN KEY (receiver_id) REFERENCES tenants(tenant_id),
    FOREIGN KEY (receiver_id) REFERENCES propertyManagers(pm_id),
    FOREIGN KEY (receiver_id) REFERENCES propertyOwners(po_id)
);