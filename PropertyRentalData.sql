-- Insert dummy data into propertyOwners table
INSERT INTO propertyOwners (first_name, last_name, email, password)
VALUES
('Alice', 'Johnson', 'alice.johnson@example.com', 'password123'),
('Bob', 'Smith', 'bob.smith@example.com', 'password456'),
('Charlie', 'Brown', 'charlie.brown@example.com', 'password789'),
('David', 'White', 'david.white@example.com', 'password101'),
('Eve', 'Green', 'eve.green@example.com', 'password202');

-- Insert dummy data into propertyManagers table
INSERT INTO propertyManagers (first_name, last_name, email, phone, password, po_id)
VALUES
('John', 'Doe', 'john.doe@example.com', '123-456-0001', 'password123', 13),
('Jane', 'Smith', 'jane.smith@example.com', '123-456-0002', 'password456', 14),
('Jim', 'Brown', 'jim.brown@example.com', '123-456-0003', 'password789', 15),
('Jake', 'White', 'jake.white@example.com', '123-456-0004', 'password101', 16),
('Jill', 'Green', 'jill.green@example.com', '123-456-0005', 'password202', 17);

-- Insert dummy data into buildings table
INSERT INTO buildings (name, address, pm_id)
VALUES
('Building A', '123 Main St, City, Country', 22),
('Building B', '456 Oak Ave, City, Country', 23),
('Building C', '789 Pine Rd, City, Country', 24),
('Building D', '101 Maple Ln, City, Country', 25),
('Building E', '202 Birch Blvd, City, Country', 26);

-- Insert dummy data into apartments table
INSERT INTO apartments (building_id, unit_number, floor, bedrooms, bathrooms, rent)
VALUES
(18, 'Apt 101', 1, 2, 1, 1200.00),
(18, 'Apt 102', 1, 3, 2, 1500.00),
(18, 'Apt 103', 2, 2, 1, 1300.00),
(18, 'Apt 104', 2, 1, 1, 1000.00),
(18, 'Apt 105', 3, 3, 2, 1700.00),

(19, 'Apt 201', 1, 2, 1, 1100.00),
(19, 'Apt 202', 1, 1, 1, 950.00),
(19, 'Apt 203', 2, 3, 2, 1600.00),
(19, 'Apt 204', 2, 2, 2, 1400.00),
(19, 'Apt 205', 3, 2, 1, 1350.00),

(20, 'Apt 301', 1, 1, 1, 800.00),
(20, 'Apt 302', 1, 2, 1, 1200.00),
(20, 'Apt 303', 2, 3, 2, 1600.00),
(20, 'Apt 304', 2, 2, 1, 1300.00),
(20, 'Apt 305', 3, 1, 1, 950.00),

(21, 'Apt 401', 1, 2, 2, 1400.00),
(21, 'Apt 402', 1, 3, 1, 1500.00),
(21, 'Apt 403', 2, 2, 2, 1350.00),
(21, 'Apt 404', 2, 1, 1, 1100.00),
(21, 'Apt 405', 3, 3, 2, 1750.00),

(22, 'Apt 501', 1, 2, 1, 1200.00),
(22, 'Apt 502', 1, 1, 1, 950.00),
(22, 'Apt 503', 2, 3, 2, 1550.00),
(22, 'Apt 504', 2, 2, 1, 1300.00),
(22, 'Apt 505', 3, 3, 2, 1650.00);

-- Insert dummy data into tenants table
INSERT INTO tenants (first_name, last_name, email, phone, password, apartment_id, move_in_date, move_out_date)
VALUES
('Tom', 'Hanks', 'tom.hanks@example.com', '123-456-0010', 'password123', NULL, NULL, NULL),
('Sarah', 'Connor', 'sarah.connor@example.com', '123-456-0011', 'password456', NULL, NULL, NULL),
('Luke', 'Skywalker', 'luke.skywalker@example.com', '123-456-0012', 'password789', NULL, NULL, NULL),
('Leia', 'Organa', 'leia.organa@example.com', '123-456-0013', 'password101', NULL, NULL, NULL),
('Han', 'Solo', 'han.solo@example.com', '123-456-0014', 'password202', NULL, NULL, NULL);

-- Insert dummy data into appointments table
INSERT INTO appointments (tenant_id, pm_id, appointment_date, notes)
VALUES
(1, 1, '2024-02-01 14:00', 'Routine maintenance check'),
(2, 2, '2024-03-01 10:00', 'Request for heating repair'),
(3, 3, '2024-04-01 16:00', 'General inquiry about lease extension'),
(4, 4, '2024-05-01 09:00', 'Scheduled inspection'),
(5, 5, '2024-06-01 11:00', 'Noise complaint follow-up');

-- Insert dummy data into messages table
INSERT INTO messages (sender_type, sender_id, receiver_type, receiver_id, content)
VALUES
('tenant', 1, 'propertyManager', 1, 'Hello, I need some maintenance in my apartment.'),
('tenant', 2, 'propertyManager', 2, 'Can you please check the heating system?'),
('tenant', 3, 'propertyManager', 3, 'I have a question about renewing my lease.'),
('tenant', 4, 'propertyManager', 4, 'I want to schedule a time for the inspection.'),
('tenant', 5, 'propertyManager', 5, 'There is a lot of noise coming from my neighbor.');

