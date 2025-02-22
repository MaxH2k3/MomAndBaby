﻿-- Insert data into [role]
INSERT INTO [role] ([name])
VALUES 
('Admin'),
('Staff'),
('Customer'),
('Guest');

-- Insert data into [Status]
INSERT INTO [Status] ([name])
VALUES 
('Pending'),
('Processing'),
('Shipped'),
('Delivered'),
('Cancelled');

-- Insert data into [Category]
INSERT INTO [Category] ([name])
VALUES 
('Electronics'),
('Books'),
('Clothing'),
('Home & Kitchen'),
('Sports');



-- Insert data into [Product]
INSERT INTO [Product] ([id], [category_id], [name], [description], [unit_price], [purchase_price], [category], [stock], [image], [original], [company], [status])
VALUES 
(NEWID(), 1, 'Product1', 'Description1', 10.00, 8.00, 'Electronics', 100, 'image1.jpg', 'Brand1', 'Company1', 'Available'),
(NEWID(), 2, 'Product2', 'Description2', 20.00, 16.00, 'Books', 200, 'image2.jpg', 'Brand2', 'Company2', 'Out of Stock'),
(NEWID(), 3, 'Product3', 'Description3', 30.00, 24.00, 'Clothing', 300, 'image3.jpg', 'Brand3', 'Company3', 'Available'),
(NEWID(), 4, 'Product4', 'Description4', 40.00, 32.00, 'Home & Kitchen', 400, 'image4.jpg', 'Brand4', 'Company4', 'Available'),
(NEWID(), 5, 'Product5', 'Description5', 50.00, 40.00, 'Sports', 500, 'image5.jpg', 'Brand5', 'Company5', 'Discontinued');

-- Insert data into the Product table
INSERT INTO [Product] 
    ([id], [category_id], [name], [description], [unit_price], [purchase_price], [category], [stock], [image], [original], [company], [status])
VALUES
    (NEWID(), 1, 'Bluetooth Speaker', 'Portable Bluetooth speaker with high-quality sound', 59.99, 45.00, 'Electronics', 150, 'bluetooth_speaker.jpg', 'SoundWave', 'AudioTech', 'Available'),
    (NEWID(), 2, 'Cookbook: Healthy Recipes', 'A collection of healthy recipes for everyday meals', 25.00, 18.00, 'Books', 120, 'cookbook.jpg', 'HealthyLife', 'FoodBooks', 'Available'),
    (NEWID(), 3, 'Winter Jacket', 'Warm and stylish winter jacket', 120.00, 90.00, 'Clothing', 80, 'winter_jacket.jpg', 'FashionWear', 'ClothingCo', 'Available'),
    (NEWID(), 4, 'Blender', 'High-speed blender for smoothies and shakes', 80.00, 60.00, 'Home & Kitchen', 60, 'blender.jpg', 'KitchenMaster', 'HomeTech', 'Available'),
    (NEWID(), 5, 'Yoga Mat', 'Eco-friendly yoga mat with non-slip surface', 40.00, 30.00, 'Sports', 200, 'yoga_mat.jpg', 'FlexFit', 'SportsGear', 'Available'),
    (NEWID(), 6, 'Laptop Bag', 'Durable and stylish laptop bag', 70.00, 50.00, 'Accessories', 100, 'laptop_bag.jpg', 'BagPro', 'TechGear', 'Available'),
    (NEWID(), 7, 'Desk Lamp', 'LED desk lamp with adjustable brightness', 35.00, 25.00, 'Office Supplies', 75, 'desk_lamp.jpg', 'BrightLight', 'OfficeWorks', 'Available'),
    (NEWID(), 8, 'Travel Mug', 'Insulated travel mug to keep drinks hot or cold', 20.00, 15.00, 'Travel', 180, 'travel_mug.jpg', 'TravelEase', 'OutdoorsCo', 'Available');


INSERT INTO [Order] ([user_id], [order_date], [total_amount], [payment_method], [status_id], [shipping_address], [voucher_id])
VALUES
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-01 10:00:00', 100.00, 'Credit Card', 1, '123 Main St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-02 11:00:00', 150.00, 'PayPal', 2, '123 Main St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-03 12:00:00', 200.00, 'Credit Card', 3, '123 Main St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-04 13:00:00', 250.00, 'Credit Card', 4, '123 Main St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-05 14:00:00', 300.00, 'PayPal', 5, '123 Main St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-06 15:00:00', 350.00, 'Credit Card', 1, '456 Elm St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-07 16:00:00', 400.00, 'Credit Card', 2, '456 Elm St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-08 17:00:00', 450.00, 'PayPal', 3, '456 Elm St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-09 18:00:00', 500.00, 'Credit Card', 4, '456 Elm St, City, Country', NULL),
('2CA53866-BEB6-42BA-8323-F6482CCF1D6E', '2024-06-10 19:00:00', 550.00, 'PayPal', 5, '456 Elm St, City, Country', NULL);


INSERT INTO [Category] ([name]) VALUES ('Sữa cho bà bầu');
INSERT INTO [Category] ([name]) VALUES ('Sữa cho em bé từ 0-6 tháng');
INSERT INTO [Category] ([name]) VALUES ('Sữa cho em bé từ 6-12 tháng');
INSERT INTO [Category] ([name]) VALUES ('Sữa cho em bé từ 1-3 tuổi');
INSERT INTO [Category] ([name]) VALUES ('Sữa cho em bé từ 3-6 tuổi');
INSERT INTO [Category] ([name]) VALUES ('Sữa thực vật thay thế');
INSERT INTO [Category] ([name]) VALUES ('Bột ăn dặm');
INSERT INTO [Category] ([name]) VALUES ('Bánh ăn dặm');
