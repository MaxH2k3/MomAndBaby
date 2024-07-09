-- Insert data into [role]
INSERT INTO [role] ([name])
VALUES 
('Admin'),
('Customer'),
('Seller'),
('Guest'),
('Manager');

-- Insert data into [Status]
INSERT INTO [Status] ([name])
VALUES 
('Pending'),
('Processing'),
('Shipped'),
('Delivered'),
('Cancelled');



-- Insert data into [Product]
INSERT INTO [Product] ([id], [name], [description], [price], [category], [stock], [image], [status])
VALUES 
(NEWID(), 'Product1', 'Description1', 10.00, 'Category1', 100, 'image1.jpg', 'Available'),
(NEWID(), 'Product2', 'Description2', 20.00, 'Category2', 200, 'image2.jpg', 'Out of Stock'),
(NEWID(), 'Product3', 'Description3', 30.00, 'Category3', 300, 'image3.jpg', 'Available'),
(NEWID(), 'Product4', 'Description4', 40.00, 'Category4', 400, 'image4.jpg', 'Available'),
(NEWID(), 'Product5', 'Description5', 50.00, 'Category5', 500, 'image5.jpg', 'Discontinued');

-- Insert data into [Order]
INSERT INTO [Order] ([user_id], [total_amount], [payment_method], [status_id], [shipping_address], [voucher_id])
VALUES 
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user1'), 100.00, 'Credit Card', 1, 'Shipping Address 1', NULL),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user2'), 200.00, 'PayPal', 2, 'Shipping Address 2', NULL),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user3'), 300.00, 'Credit Card', 3, 'Shipping Address 3', NULL),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user4'), 400.00, 'Credit Card', 4, 'Shipping Address 4', NULL),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user5'), 500.00, 'PayPal', 5, 'Shipping Address 5', NULL);

-- Insert data into [Order_Detail]
INSERT INTO [Order_Detail] ([order_id], [product_id], [quantity], [price])
VALUES 
(1, (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product1'), 1, 10.00),
(2, (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product2'), 2, 20.00),
(3, (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product3'), 3, 30.00),
(4, (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product4'), 4, 40.00),
(5, (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product5'), 5, 50.00);

-- Insert data into [Review]
INSERT INTO [Review] ([user_id], [product_id], [rating], [comment], [Status])
VALUES 
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user1'), (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product1'), 5, 'Great product!', 1),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user2'), (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product2'), 4, 'Good quality.', 1),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user3'), (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product3'), 3, 'Average.', 1),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user4'), (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product4'), 2, 'Not satisfied.', 1),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user5'), (SELECT TOP 1 [id] FROM [Product] WHERE [name] = 'Product5'), 1, 'Terrible experience.', 1);

-- Insert data into [Article]
INSERT INTO [Article] ([title], [content], [author_id])
VALUES 
('Article 1', 'Content 1', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user1')),
('Article 2', 'Content 2', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user2')),
('Article 3', 'Content 3', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user3')),
('Article 4', 'Content 4', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user4')),
('Article 5', 'Content 5', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user5'));

-- Insert data into [Voucher]
INSERT INTO [Voucher] ([code], [discount], [start_date], [end_date], [created_by], [amount])
VALUES 
('VOUCHER1', 10.00, '2024-01-01', '2024-12-31', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user1'), 100),
('VOUCHER2', 15.00, '2024-01-01', '2024-12-31', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user2'), 50),
('VOUCHER3', 20.00, '2024-01-01', '2024-12-31', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user3'), 25),
('VOUCHER4', 25.00, '2024-01-01', '2024-12-31', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user4'), 10),
('VOUCHER5', 30.00, '2024-01-01', '2024-12-31', (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user5'), 5);

-- Insert data into [Message]
INSERT INTO [Message] ([sender_id], [receiver_id], [is_system], [content], [created_at])
VALUES 
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user1'), (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user2'), 0, 'Hello User 2', GETDATE()),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user2'), (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user3'), 0, 'Hello User 3', GETDATE()),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user3'), (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user4'), 0, 'Hello User 4', GETDATE()),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user4'), (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user5'), 0, 'Hello User 5', GETDATE()),
((SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user5'), (SELECT TOP 1 [id] FROM [User] WHERE [username] = 'user1'), 0, 'Hello User 1', GETDATE());

-- Insert data into [Order_Tracking]
INSERT INTO [Order_Tracking] ([order_id], [order_confirmation], [package], [delivery], [complete])
VALUES 
(1, GETDATE(), GETDATE(), GETDATE(), GETDATE()),
(2, GETDATE(), GETDATE(), GETDATE(), GETDATE()),
(3, GETDATE(), GETDATE(), GETDATE(), GETDATE()),
(4, GETDATE(), GETDATE(), GETDATE(), GETDATE()),
(5, GETDATE(), GETDATE(), GETDATE(), GETDATE());

-- Insert data into [Gift]
INSERT INTO [Gift] ([name], [stock], [exchange_point])
VALUES 
('Gift1', 10, 100),
('Gift2', 20, 200),
('Gift3', 30, 300),
('Gift4', 40, 400),
('Gift5', 50, 500);
