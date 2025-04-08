INSERT INTO Authors  (Name)
VALUES
    ( 'Stephen King'),
    ('J.R.R. Tolkien'),
    ('Agatha Christie'),
    ('Dan Brown'),
    ('Jane Austen'),
    ('Charles Dickens'),
    ( 'Mark Twain'),
    ( 'Ernest Hemingway'),
    ( 'Leo Tolstoy'),
    ('H.G. Wells'),
    ( 'F. Scott Fitzgerald'),
    ( 'John Grisham'),
    ( 'Haruki Murakami'),
    ( 'Isaac Asimov'),
    ( 'Kurt Vonnegut'),
    ('Ray Bradbury'),
    ( 'Margaret Atwood'),
    ( 'Philip K. Dick');

-- Insert Books (20 random books)
INSERT INTO Books  (AuthorId, Description, ISBN, PublishedYear, Title)
VALUES
    ( 1, 'A young wizard embarks on a journey to discover his true destiny.', '9780747532743', 1997, 'Harry Potter and the Philosopher''s Stone'),
    ( 2, 'A dark and epic fantasy tale of power and betrayal.', '9780553103540', 1996, 'A Game of Thrones'),
    ( 3, 'A supernatural horror story about a haunted hotel.', '9780451169518', 1977, 'The Shining'),
    ( 4, 'An epic fantasy story of hobbits, elves, and a dark lord.', '9780618640157', 1954, 'The Fellowship of the Ring'),
	 (5, 'A detective novel featuring Hercule Poirot solving a murder mystery.', '9780062073501', 1926, 'The Murder of Roger Ackroyd'),
     (6, 'A thrilling adventure about a cryptic secret society.', '9780307474278', 2003, 'The Da Vinci Code'),
    ( 7, 'A story about a young woman navigating love and society.', '9780141439518', 1813, 'Pride and Prejudice'),
    (8, 'A social commentary on Victorian England with rich characters.', '9780140430547', 1859, 'A Tale of Two Cities'),
    (9, 'A humorous story about a riverboat captain and his adventures.', '9780143107477', 1884, 'Adventures of Huckleberry Finn'),
    ( 10, 'A gripping tale of a man’s fight for survival in the wilderness.', '9780684801223', 1952, 'The Old Man and the Sea'),
    ( 11, 'A powerful novel about the French revolution and its aftermath.', '9780140447934', 1869, 'War and Peace'),
    ( 12, 'A time travel story set in the future, with commentary on human society.', '9780451524935', 1895, 'The Time Machine'),
    ( 13, 'A classic American novel about the roaring twenties and the pursuit of the American Dream.', '9780743273565', 1925, 'The Great Gatsby'),
    ( 14, 'A legal thriller about a lawyer solving a complex case.', '9780385504201', 1991, 'The Firm'),
    ( 15, 'A surreal and dreamlike story about love, loss, and human nature.', '9780307476708', 2007, 'Kafka on the Shore'),
    ( 16, 'A science fiction classic about robots and the future of humanity.', '9780553382563', 1950, 'I, Robot'),
    ( 17, 'A satirical novel about the absurdity of war.', '9780385333487', 1969, 'Slaughterhouse-Five'),
    ( 18, 'A dystopian novel exploring the themes of censorship and conformity.', '9781451673319', 1953, 'Fahrenheit 451'),
    ( 19, 'A post-apocalyptic novel exploring survival and societal breakdown.', '9780385491433', 1985, 'The Handmaid''s Tale'),
    ( 20, 'A science fiction classic exploring the nature of reality and humanity.', '9780345457739', 1968, 'Do Androids Dream of Electric Sheep?');