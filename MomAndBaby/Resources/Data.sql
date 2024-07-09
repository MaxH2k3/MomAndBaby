-- Insert data into [role]
INSERT INTO [role] ([name])
VALUES 
('Admin'),
('Customer'),
('Seller'),
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

