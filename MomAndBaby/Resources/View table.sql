CREATE VIEW ProductStatistics AS
SELECT 
    p.id AS ProductId,
    p.name AS ProductName,
    
    -- Calculate total purchase quantity
    ISNULL(SUM(od.quantity), 0) AS TotalPurchase,
    
    -- Calculate total number of reviews
    ISNULL(COUNT(r.id), 0) AS TotalReview,
    
    -- Calculate average star rating with decimal precision
    ISNULL(CAST(AVG(CAST(r.rating AS DECIMAL(3, 2))) AS FLOAT), 0) AS AverageStar
    
FROM 
    Product p
    LEFT JOIN Order_Detail od ON p.id = od.product_id
    LEFT JOIN [Order] o ON od.order_id = o.id
    LEFT JOIN Review r ON p.id = r.product_id
    
GROUP BY 
    p.id, p.name;
