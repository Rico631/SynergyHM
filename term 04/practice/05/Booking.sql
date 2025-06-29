-- Подключения расширений
CREATE EXTENSION IF NOT EXISTS "pg_uuidv7";
CREATE EXTENSION IF NOT EXISTS "postgis";

-- Создание справочников
-- Типы недвижимости
CREATE TABLE PropertyTypes (
    property_type_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL
);
-- Типы комнат
CREATE TABLE RoomTypes (
    room_type_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL
);
-- Методы оплаты
CREATE TABLE PaymentMethods (
    method_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL
);
-- Категории удобств
CREATE TABLE AmenityTypes (
    type_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    name VARCHAR(50) UNIQUE NOT NULL
);

-- Наполнение справочников
INSERT INTO PropertyTypes (property_type_id, name) VALUES
(uuid_generate_v7(), 'apartment'),
(uuid_generate_v7(), 'house'),
(uuid_generate_v7(), 'villa'),
(uuid_generate_v7(), 'cottage'),
(uuid_generate_v7(), 'loft'),
(uuid_generate_v7(), 'cabin'),
(uuid_generate_v7(), 'townhouse'),
(uuid_generate_v7(), 'bungalow');

INSERT INTO RoomTypes (room_type_id, name) VALUES
(uuid_generate_v7(), 'entire'),
(uuid_generate_v7(), 'private'),
(uuid_generate_v7(), 'shared'),
(uuid_generate_v7(), 'hotel room'),
(uuid_generate_v7(), 'dormitory');

INSERT INTO PaymentMethods (method_id, name) VALUES
(uuid_generate_v7(), 'credit card'),
(uuid_generate_v7(), 'debit card'),
(uuid_generate_v7(), 'paypal'),
(uuid_generate_v7(), 'crypto'),
(uuid_generate_v7(), 'bank transfer'),
(uuid_generate_v7(), 'apple pay'),
(uuid_generate_v7(), 'google pay');

INSERT INTO AmenityTypes (type_id, name) VALUES
(uuid_generate_v7(), 'basic'),
(uuid_generate_v7(), 'safety'),
(uuid_generate_v7(), 'accessibility'),
(uuid_generate_v7(), 'luxury'),
(uuid_generate_v7(), 'family'),
(uuid_generate_v7(), 'outdoor'),
(uuid_generate_v7(), 'kitchen'),
(uuid_generate_v7(), 'parking');

-- Создание основных таблиц
-- Пользователи
CREATE TABLE Users (
    user_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    email VARCHAR(255) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    phone VARCHAR(20),
    registration_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_host BOOLEAN DEFAULT FALSE, -- признак арендодателя
    last_login TIMESTAMP,
    profile_image_url VARCHAR(255)
);
-- Объявления
CREATE TABLE Listings (
    listing_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    host_id UUID REFERENCES Users(user_id) ON DELETE CASCADE NOT NULL,
    title VARCHAR(100) NOT NULL,
    description TEXT,
    address VARCHAR(255) NOT NULL,
    city VARCHAR(50) NOT NULL,
    country VARCHAR(50) NOT NULL,
    location GEOGRAPHY(Point, 4326) NOT NULL,
    price_per_night NUMERIC(10, 2) NOT NULL CHECK (price_per_night > 0),
    property_type_id UUID REFERENCES PropertyTypes(property_type_id) ON DELETE SET NULL,
    room_type_id UUID REFERENCES RoomTypes(room_type_id) ON DELETE SET NULL,
    max_guests INTEGER NOT NULL CHECK (max_guests > 0),
    bathrooms INTEGER NOT NULL CHECK (bathrooms > 0),
    bedrooms INTEGER NOT NULL CHECK (bedrooms >= 0),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE
);
-- Бронирования
CREATE TABLE Bookings (
    booking_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    listing_id UUID REFERENCES Listings(listing_id) ON DELETE CASCADE NOT NULL,
    guest_id UUID REFERENCES Users(user_id) ON DELETE CASCADE NOT NULL,
    check_in_date DATE NOT NULL,
    check_out_date DATE NOT NULL,
    total_price NUMERIC(10, 2) NOT NULL CHECK (total_price > 0),
    status VARCHAR(20) DEFAULT 'pending' CHECK (status IN ('confirmed', 'cancelled', 'pending', 'completed')),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CHECK (check_out_date > check_in_date)
);
-- Обзоры
CREATE TABLE Reviews (
    review_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    booking_id UUID UNIQUE REFERENCES Bookings(booking_id) ON DELETE CASCADE NOT NULL,
    rating INTEGER NOT NULL CHECK (rating BETWEEN 1 AND 5),
    comment TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    response TEXT,
    response_date TIMESTAMP
);
-- Платежи
CREATE TABLE Payments (
    payment_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    booking_id UUID UNIQUE REFERENCES Bookings(booking_id) ON DELETE CASCADE NOT NULL,
    amount NUMERIC(10, 2) NOT NULL CHECK (amount > 0),
    method_id UUID REFERENCES PaymentMethods(method_id) ON DELETE SET NULL,
    transaction_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status VARCHAR(20) CHECK (status IN ('success', 'failed', 'pending', 'refunded')),
    transaction_id VARCHAR(100)
);
-- Удобства
CREATE TABLE Amenities (
    amenity_id UUID DEFAULT uuid_generate_v7() PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    type_id UUID REFERENCES AmenityTypes(type_id) ON DELETE SET NULL,
    icon VARCHAR(50)
);
-- Связь объявлений и удобств
CREATE TABLE Listing_Amenities (
    listing_id UUID REFERENCES Listings(listing_id) ON DELETE CASCADE NOT NULL,
    amenity_id UUID REFERENCES Amenities(amenity_id) ON DELETE CASCADE NOT NULL,
    PRIMARY KEY (listing_id, amenity_id)
);

-- Создание индексов
CREATE INDEX idx_listings_location ON Listings USING GIST(location);
CREATE INDEX idx_listings_city ON Listings(city);
CREATE INDEX idx_listings_price ON Listings(price_per_night);
CREATE INDEX idx_bookings_dates ON Bookings(check_in_date, check_out_date);
CREATE INDEX idx_bookings_guest ON Bookings(guest_id);
CREATE INDEX idx_reviews_rating ON Reviews(rating);
CREATE INDEX idx_payments_status ON Payments(status);
CREATE INDEX idx_users_email ON Users(email);

-- Создание представлений
CREATE VIEW Active_Listings AS
SELECT 
    l.listing_id,
    l.title,
    l.address,
    l.city,
    l.country,
    ST_Y(l.location::geometry) AS latitude,
    ST_X(l.location::geometry) AS longitude,
    l.price_per_night,
    pt.name AS property_type,
    rt.name AS room_type,
    l.max_guests,
    u.first_name || ' ' || u.last_name AS host_name
FROM Listings l
JOIN PropertyTypes pt ON l.property_type_id = pt.property_type_id
JOIN RoomTypes rt ON l.room_type_id = rt.room_type_id
JOIN Users u ON l.host_id = u.user_id
WHERE l.is_active = TRUE;

CREATE VIEW Booking_Details AS
SELECT 
    b.booking_id,
    u.first_name || ' ' || u.last_name AS guest_name,
    l.title AS listing_title,
    b.check_in_date,
    b.check_out_date,
    b.total_price,
    b.status AS booking_status,
    p.status AS payment_status,
    pm.name AS payment_method,
    r.rating,
    r.comment
FROM Bookings b
JOIN Users u ON b.guest_id = u.user_id
JOIN Listings l ON b.listing_id = l.listing_id
LEFT JOIN Payments p ON b.booking_id = p.booking_id
LEFT JOIN PaymentMethods pm ON p.method_id = pm.method_id
LEFT JOIN Reviews r ON b.booking_id = r.booking_id;

-- Триггеры
-- Автоматический расчет total_price при вставке/обновлении бронирования
CREATE OR REPLACE FUNCTION calculate_total_price()
RETURNS TRIGGER AS $$
BEGIN
    NEW.total_price := (
        SELECT price_per_night * (NEW.check_out_date - NEW.check_in_date)
        FROM Listings 
        WHERE listing_id = NEW.listing_id
    );
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_calculate_price
BEFORE INSERT OR UPDATE ON Bookings
FOR EACH ROW EXECUTE FUNCTION calculate_total_price();

-- Автоматическое обновление updated_at
CREATE OR REPLACE FUNCTION update_timestamp()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_update_listings
BEFORE UPDATE ON Listings
FOR EACH ROW EXECUTE FUNCTION update_timestamp();

CREATE TRIGGER trg_update_bookings
BEFORE UPDATE ON Bookings
FOR EACH ROW EXECUTE FUNCTION update_timestamp();

-- Наполнение тестовыми данными
-- Пользователи
INSERT INTO Users (user_id, email, password_hash, first_name, last_name, is_host) VALUES
(uuid_generate_v7(), 'alex@example.com', 'hash1', 'Alex', 'Ivanov', true),
(uuid_generate_v7(), 'maria@example.com', 'hash2', 'Maria', 'Petrova', true),
(uuid_generate_v7(), 'ivan@example.com', 'hash3', 'Ivan', 'Sidorov', false),
(uuid_generate_v7(), 'olga@example.com', 'hash4', 'Olga', 'Smirnova', false);

-- Удобства
INSERT INTO Amenities (amenity_id, name, type_id, icon) 
SELECT 
    uuid_generate_v7(),
    amenity.name,
    (SELECT type_id FROM AmenityTypes WHERE name = amenity.type),
    amenity.icon
FROM (VALUES
    ('Wi-Fi', 'basic', 'wifi'),
    ('Kitchen', 'kitchen', 'kitchen'),
    ('Pool', 'luxury', 'pool'),
    ('Parking', 'parking', 'parking'),
    ('Air Conditioning', 'basic', 'ac'),
    ('Fire Extinguisher', 'safety', 'fire'),
    ('Wheelchair Access', 'accessibility', 'wheelchair'),
    ('Pet Friendly', 'family', 'pets'),
    ('Garden', 'outdoor', 'garden')
) AS amenity(name, type, icon);

-- Объявления
INSERT INTO Listings (
    listing_id, host_id, title, address, city, country, location, 
    price_per_night, property_type_id, room_type_id, max_guests, bedrooms, bathrooms
) 
SELECT 
    uuid_generate_v7(),
    (SELECT user_id FROM Users WHERE email = 'alex@example.com'),
    'Modern Apartment in Center',
    'Tverskaya St, 15',
    'Moscow',
    'Russia',
    ST_GeogFromText('POINT(55.760735 37.605671)'),
    4500.00,
    (SELECT property_type_id FROM PropertyTypes WHERE name = 'apartment'),
    (SELECT room_type_id FROM RoomTypes WHERE name = 'entire'),
    4,
    2,
    1;

INSERT INTO Listings (
    listing_id, host_id, title, address, city, country, location, 
    price_per_night, property_type_id, room_type_id, max_guests, bedrooms, bathrooms
) 
SELECT 
    uuid_generate_v7(),
    (SELECT user_id FROM Users WHERE email = 'maria@example.com'),
    'Cozy Cottage near Lake',
    'Forest Rd, 7',
    'Sergiev Posad',
    'Russia',
    ST_GeogFromText('POINT(56.316669 38.133331)'),
    7800.00,
    (SELECT property_type_id FROM PropertyTypes WHERE name = 'cottage'),
    (SELECT room_type_id FROM RoomTypes WHERE name = 'entire'),
    6,
    3,
    2;

-- Связи объявлений и удобств
INSERT INTO Listing_Amenities (listing_id, amenity_id)
SELECT 
    l.listing_id,
    a.amenity_id
FROM Listings l
CROSS JOIN Amenities a
WHERE l.title = 'Modern Apartment in Center'
AND a.name IN ('Wi-Fi', 'Kitchen', 'Air Conditioning', 'Parking');

INSERT INTO Listing_Amenities (listing_id, amenity_id)
SELECT 
    l.listing_id,
    a.amenity_id
FROM Listings l
CROSS JOIN Amenities a
WHERE l.title = 'Cozy Cottage near Lake'
AND a.name IN ('Wi-Fi', 'Kitchen', 'Garden', 'Pet Friendly', 'Parking');

-- Бронирования
INSERT INTO Bookings (
    booking_id, listing_id, guest_id, check_in_date, check_out_date
)
SELECT 
    uuid_generate_v7(),
    (SELECT listing_id FROM Listings WHERE title = 'Modern Apartment in Center'),
    (SELECT user_id FROM Users WHERE email = 'ivan@example.com'),
    '2023-07-15',
    '2023-07-20';

INSERT INTO Bookings (
    booking_id, listing_id, guest_id, check_in_date, check_out_date, status
)
SELECT 
    uuid_generate_v7(),
    (SELECT listing_id FROM Listings WHERE title = 'Cozy Cottage near Lake'),
    (SELECT user_id FROM Users WHERE email = 'olga@example.com'),
    '2023-08-01',
    '2023-08-10',
    'confirmed';

-- Платежи
INSERT INTO Payments (
    payment_id, booking_id, amount, method_id, status
)
SELECT 
    uuid_generate_v7(),
    b.booking_id,
    b.total_price,
    (SELECT method_id FROM PaymentMethods WHERE name = 'credit card'),
    'success'
FROM Bookings b
JOIN Users u ON b.guest_id = u.user_id
WHERE u.email = 'ivan@example.com';

INSERT INTO Payments (
    payment_id, booking_id, amount, method_id, status
)
SELECT 
    uuid_generate_v7(),
    b.booking_id,
    b.total_price,
    (SELECT method_id FROM PaymentMethods WHERE name = 'paypal'),
    'pending'
FROM Bookings b
JOIN Users u ON b.guest_id = u.user_id
WHERE u.email = 'olga@example.com';

-- Отзывы
INSERT INTO Reviews (
    review_id, booking_id, rating, comment
)
SELECT 
    uuid_generate_v7(),
    b.booking_id,
    5,
    'Perfect apartment in the center!'
FROM Bookings b
JOIN Users u ON b.guest_id = u.user_id
WHERE u.email = 'ivan@example.com';

-- Запросы для проверки
-- Все активные объявления в Москве
SELECT * FROM Active_Listings WHERE city = 'Moscow';
/*
"listing_id"	"title"	"address"	"city"	"country"	"latitude"	"longitude"	"price_per_night"	"property_type"	"room_type"	"max_guests"	"host_name"
"0197bcd1-12d6-70ef-bf62-9502aca0a542"	"Modern Apartment in Center"	"Tverskaya St, 15"	"Moscow"	"Russia"	37.605671	55.760735	4500.00	"apartment"	"entire"	4	"Alex Ivanov"
*/
-- Детали бронирований
SELECT * FROM Booking_Details;
/*
"booking_id"	"guest_name"	"listing_title"	"check_in_date"	"check_out_date"	"total_price"	"booking_status"	"payment_status"	"payment_method"	"rating"	"comment"
"0197bcd1-12d9-7b8c-bfcb-3e11740322ab"	"Ivan Sidorov"	"Modern Apartment in Center"	"2023-07-15"	"2023-07-20"	22500.00	"pending"	"success"	"credit card"	5	"Perfect apartment in the center!"
"0197bcd1-12db-7569-9670-73d40998cc2c"	"Olga Smirnova"	"Cozy Cottage near Lake"	"2023-08-01"	"2023-08-10"	70200.00	"confirmed"	"pending"	"paypal"		
*/

-- Поиск жилья с Wi-Fi в радиусе 10 км от центра Москвы
SELECT 
    l.title,
    l.address,
    l.price_per_night,
    ST_Distance(
        l.location, 
        ST_GeogFromText('POINT(55.755826 37.617494)')
    ) AS distance_meters
FROM Listings l
JOIN Listing_Amenities la ON l.listing_id = la.listing_id
JOIN Amenities a ON la.amenity_id = a.amenity_id
WHERE a.name = 'Wi-Fi'
AND ST_DWithin(
    l.location, 
    ST_GeogFromText('POINT(55.755826 37.617494)'), 
    10000
)
ORDER BY l.price_per_night;

/*
"title"	"address"	"price_per_night"	"distance_meters"
"Modern Apartment in Center"	"Tverskaya St, 15"	4500.00	1381.95448783
*/