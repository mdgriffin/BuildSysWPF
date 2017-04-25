/**
 * Material Analysis 
 **/

-- All Material
SELECT * FROM Materials;

-- Number of Materials
SELECT COUNT(material_id) FROM Materials;

-- Average Material Cost
SELECT ROUND(AVG(price_per_unit), 2) FROM Materials WHERE status = 'A';

-- Average Material Cost Growth??
--SELECT * FROM Quote_Materials;

-- Materials Created By Month
SELECT AVG(COUNT(M.MATERIAL_ID)) AS Num_Materials_Per_Month FROM Quote_Materials M, Quotes Q WHERE M.Quote_id = Q.Quote_id GROUP BY EXTRACT(MONTH FROM DATE_ISSUED);

-- Average Number of Materials Per Quote
SELECT AVG(COUNT(MATERIAL_ID)) AS Average_Materials_Per_Quote FROM Quote_Materials GROUP BY Quote_Id;

-- Number of Non-Service Materials
SELECT count(material_id) FROM Materials Where Is_Service = 0;

-- Number of Service
SELECT count(material_id) FROM Materials Where Is_Service = 1;

-- Most expensive material
SELECT * FROM Materials WHERE Price_Per_Unit = (SELECT MAX(Price_Per_Unit) FROM Materials);

-- Average Material Cost
SELECT ROUND(AVG(Price_Per_Unit), 2) FROM Materials;

-- Divide by Price
select m.price_range as Material_Price_Range, count(*) as number_of_occurences
from (
  select case  
    when price_per_unit between  0 and 10 then  '0 - 10'
    when price_per_unit between 10 and 20 then '10 - 20'
    when price_per_unit between 20 and 30 then '20 - 30'
    when price_per_unit between 30 and 40 then '30 - 40'
    when price_per_unit between 40 and 50 then '40 - 50'
    when price_per_unit between 50 and 60 then '50 - 60'
    when price_per_unit between 60 and 70 then '60 - 70'
    when price_per_unit between 70 and 80 then '70 - 80'
    when price_per_unit between 80 and 80 then '80 - 90'
    when price_per_unit between 90 and 100 then '90 - 100'
    else '100+' end as price_range
  from Materials) m
group by m.price_range;

/**
 * Quote Analysis 
 **/

-- All Quotes
SELECT * FROM Quotes;

-- Number of Quotes
SELECT Count(Quote_Id) FROM Quotes;

-- Average Quote Value
SELECT AVG(Subtotal + Vat) FROM Quotes;

-- Total Quotes Issued
SELECT SUM(Subtotal + Vat) FROM Quotes;

-- Largest Quote Issued
SELECT SUM(Subtotal + Vat) FROM Quotes;

-- Get number of Quotes issued by month
SELECT  EXTRACT(MONTH FROM DATE_ISSUED), COUNT(quote_id) AS Quotes_Per_Month  FROM Quotes Q GROUP BY EXTRACT(MONTH FROM DATE_ISSUED) ORDER BY  EXTRACT(MONTH FROM DATE_ISSUED);

-- Number of Quotes issued by year, Limit to last 5 years
SELECT  EXTRACT(YEAR FROM DATE_ISSUED), COUNT(quote_id) AS Quotes_Per_Year  FROM Quotes Q WHERE ROWNUM < 6 GROUP BY EXTRACT(YEAR FROM DATE_ISSUED) ORDER BY  EXTRACT(YEAR FROM DATE_ISSUED);

/**
 * Customer Analysis 
 **/

-- All Customers
SELECT * FROM Customers;

-- Number of Customers
SELECT COUNT(customer_id) FROM Customers;

-- Number of customers registed by month
SELECT EXTRACT(month FROM registered_on) AS month_code, EXTRACT(year from registered_on) AS year_code, COUNT(customer_id) AS num_customers_registered  FROM Customers
  WHERE registered_on > add_months(sysdate, -12)
  GROUP BY EXTRACT(year FROM registered_on), EXTRACT(month from registered_on) 
  ORDER BY EXTRACT(year FROM registered_on) ASC, EXTRACT(month from registered_on) ASC;

-- Best Customer
SELECT * FROM Customers WHERE customer_id = (
SELECT customer_id FROM (
  SELECT customer_id, SUM(subtotal + vat) as TOTAL_QUOTE_VALUE FROM Quotes GROUP BY customer_id ORDER BY total_quote_value DESC
) WHERE rownum = 1);