DROP DATABASE IF EXISTS LaptopM;
CREATE DATABASE	IF NOT EXISTS LaptopM;
USE LaptopM;

create user if not exists 'vtca'@'localhost' identified by 'vtcacademy';
grant all on LaptopM to 'vtca'@'localhost';
ALTER USER 'vtca'@'localhost' IDENTIFIED BY 'vtcacademy';

CREATE TABLE STAFFS
(
	StaffID int not null AUTO_INCREMENT,
    UserName VARCHAR(30),
    Password VARCHAR(100),
    Name VARCHAR(50),
    Phone VARCHAR(50),
    Email VARCHAR(50),
    Role INT NOT NULL DEFAULT 1,
    PRIMARY KEY(STAFFID)
);

CREATE TABLE Customers
(
	Customerid INT NOT NULL AUTO_INCREMENT,
    Name VARCHAR(50),
    Phone VARCHAR(10),
    Address VARCHAR(100),
    PRIMARY KEY(Customerid)
);

CREATE TABLE Invoices
(
	InvoiceNo INT NOT NULL AUTO_INCREMENT,
    SaleId INT,
    AccountantId INT DEFAULT 0,
    CustomerId INT,
    Datetime DATETIME,
    Status INT,
    PRIMARY KEY(InvoiceNo)
);

CREATE TABLE Laptops
(
	LaptopId INT NOT NULL AUTO_INCREMENT,
    BrandId int,
    Name VARCHAR(500),
    Price INT,
    CPU VARCHAR(500),
    RAM VARCHAR(500),
    HardDisk VARCHAR(500),
    Monitor VARCHAR(500),
    GraphicsCard VARCHAR(500),
    Jack VARCHAR(500),
    Os VARCHAR(200),
    Battery VARCHAR(200),
    Weight VARCHAR(30),
    WarrantyPeriod VARCHAR(30),
    Stock INT,
    PRIMARY KEY(LaptopId)
);

CREATE TABLE Brands
(
	BrandId INT NOT NULL AUTO_INCREMENT,
    BrandName VARCHAR(50),
    PRIMARY KEY (BrandId)
);

CREATE TABLE InvoiceDetails
(
	InvoiceNo INT NOT NULL,
    LaptopId INT NOT NULL,
    Quanity INT NOT NULL DEFAULT 1,
    Price INT,
    PRIMARY KEY(InvoiceNo, LaptopID)
);

-- FK for Invoice Details
ALTER TABLE InvoiceDetails ADD CONSTRAINT fk_01 FOREIGN KEY (InvoiceNo) REFERENCES invoices(InvoiceNo);
ALTER TABLE InvoiceDetails ADD CONSTRAINT fk_02 FOREIGN KEY (LaptopId) REFERENCES Laptops(LaptopId);

-- FK for Laptops
ALTER TABLE LAPTOPS ADD CONSTRAINT l_fk_01 FOREIGN KEY (brandId) REFERENCES Brands(BrandId);

-- Insert data to customer
INSERT INTO Customers (customerId, Name, Phone, Address) VALUES (1, "Lê Huy Giang", "0981111111", "Thanh Hoa");
INSERT INTO Customers (Name, Phone, Address) VALUES ("Lê Huy Hoàng", "098222222", "Thanh Hoá");
INSERT INTO Customers (Name, Phone, Address) VALUES ("Lê Huy Khang", "098333333", "Thanh Hoá"),
("Đặng Xuân Tiến", "0987777777", "Cà Mau");

-- Insert data to Staffs
INSERT INTO Staffs (StaffId, UserName, Password, Name, Phone, Email, Role)
	VALUES (1, "Giang1", "pGiang123", "Lê Huy Giang", "0981111111", "lhGiang@gmail.com", 1);

INSERT INTO Staffs (UserName, Password, Name, Phone, Email, Role)
	VALUES  ("Tiendx1", "pTien321", "Đặng Xuân Tiến", "0981222222", "dxTien@gmail.com", 2),
			("minhAnh1", "pMAnh445", "Phạm Minh Anh", "0987666543", "mAnh@gmail.com", 1),
            ("lkHuyen1", "pkHuyen12341", "Lữ Khánh Huyền", "0987554512", "lkHuyen@gmail.com", 2);

-- Insert data to Brands
INSERT INTO Brands (BrandId, BrandName) VALUES (1, "ASUS");
INSERT INTO Brands (BrandName) VALUES ("ACER"), ("DELL"), ("MSI"), ("LENOVO"), ("HP"), ("LG"), ("MacBook");

-- Insert data to Laptops
INSERT INTO Laptops (LaptopId, BrandId, Name, price, cpu, ram, harddisk, monitor, graphicscard, jack, os, battery, weight, warrantyperiod, stock)
	VALUES (1, 1, "Laptop Asus TUF Gaming FX506LH (HN002T)", 
    21490000, "i510300H, 2.5GHz", 
    "8 GBDDR4 2 khe (1 khe 8GB + 1 khe rời), 2933 MHz", 
    "SSD 512 GB NVMe PCIe, Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng",
    "15.6\"Full HD (1920 x 1080), 144Hz",
    "Card rời, GTX 1650 4GB",
    "Jack tai nghe 3.5 mm2 x USB 3.2, HDMI, LAN (RJ45), USB 2.0, USB Type-C",
    "Windows 10 Home SL",
    "3-cell Li-ion, 48 Wh",
    "Nặng 2.3 kg",
    "12 tháng",
    10
    );

INSERT INTO Laptops (BrandId, Name, price, cpu, ram, harddisk, monitor, graphicscard, jack, os, battery, weight, warrantyperiod, stock)
	VALUES (3, "Laptop Dell Gaming G3 15 (P89F002BWH) ", 
    31990000, "i7 10750H, 2.6GHz", 
    "16 GB, DDR4 2 khe (1 khe 8GB + 1 khe 8GB), 2933 MHz", 
    "SSD 512 GB NVMe PCIe, Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng",
    "15.6\"Full HD (1920 x 1080), 120Hz",
    "Card rời, GTX 1660Ti 6GB",
    "Jack tai nghe 3.5 mm, 1 x USB 3.2HDMILAN (RJ45), Thunderbolt 32 x USB 2.0, Mini DisplayPort",
    "Windows 10 Home SL",
    "4-cell Li-ion, 68 Wh",
    "Nặng 2.58 kg",
    "12 tháng",
    15
    ),
    (2, "Laptop Acer Nitro 5 Gaming AN515 57 74NU (NH.QD9SV.001.)",
    29990000, "i7, 11800H, 2.30 GHz",
    "8 GB, DDR4 2 khe (1 khe 8GB + 1 khe rời), 3200 MHz", 
    "SSD 512 GB NVMe PCIe (Có thể tháo ra, lắp thanh khác tối đa 1TB), Hỗ trợ khe cắm HDD SATA (nâng cấp tối đa 2TB), Hỗ trợ thêm 1 khe cắm SSD M.2 PCIe mở rộng (nâng cấp tối đa 1TB)",
    "15.6\"Full HD (1920 x 1080), 144Hz",
    "Card rời, RTX 3050Ti 4GB",
    "Jack tai nghe 3.5 mm, 3 x USB 3.2, HDMILAN (RJ45), USB Type-C",
    "Windows 10 Home SL",
    "4-cell Li-ion, 57 Wh",
    "Nặng 2.2 kg",
    "12 tháng",
    12
    );

-- Insert data to Invoices
INSERT INTO Invoices (invoiceNo, saleId, customerid, datetime, status)
	VALUES (1, 2, 1, Now(), 1);

-- Insert data to Invoice Details
INSERT INTO InvoiceDetails (invoiceno, laptopid, quanity, price)
	VALUES (1, 2, 1, 31990000);
INSERT INTO InvoiceDetails (invoiceno, laptopid, quanity, price)
	VALUES (1, 3, 1, 29990000);

-- Show Data
-- SELECT * FROM brands;
-- SELECT * FROM Laptops;
-- SELECT * FROM Customers;
-- SELECT * FROM staffs;
-- SELECT * FROM Invoices;
-- SELECT * FROM Invoicedetails;

UPDATE staffs SET Password = "a393e08eefc18c2b27ff83dd5beb88f4" WHERE staffid = 1; -- pass: pGiang123
UPDATE staffs SET Password = "1bd655f2516bbd426600247fd1f08107" WHERE staffid = 2; -- pass: pTien123
UPDATE staffs SET Password = "fd84e9e03a26f06dc2ae5a03d118cbf6" WHERE staffid = 3; -- pass: pMAnh445



