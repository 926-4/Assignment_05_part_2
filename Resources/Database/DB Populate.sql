use Team42DB
-- User insert
INSERT INTO [Users] ([name])
VALUES ('Alice'), ('Bob'), ('Charlie'), ('John'), ('Mary');

-- Categories insert
INSERT INTO [Categories] ([name])
VALUES ('Technology'), ('Science'), ('Art'), ('Sports'), ('VideoGames');

-- Moderates insert
INSERT INTO [Moderates] ([userId], [categoryId])
VALUES (1, 1), (2, 2), (1, 2), (1, 3), (4, 1);

-- Badges insert
INSERT INTO [Badges] ([name], [description], [image])
VALUES ('Bronze', 'Achieved after 10 posts', 0xc9b6d5), -- Insert image binary data
       ('Silver', 'Achieved after 50 posts', 0xc346d5),
       ('Gold', 'Achieved after 100 posts', 0xabb6d5);

-- Owns insert
INSERT INTO [Owns] ([userId], [badgeId])
VALUES (1, 1), (2, 2), (1, 2), (1, 3), (3, 1), (3, 2);

-- Posts insert
INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title], [categoryId])
VALUES (1, 'How does quantum computing work?', GETDATE(), 'question', 'Quantum Computing', 1);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title])
VALUES (2, 'Quantum computing leverages quantum bits (qubits) to perform complex calculations.', GETDATE(), 'answer', NULL);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (3, 'Interesting topic!', GETDATE(), 'comment');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title], [categoryId])
VALUES (1, 'How can I improve my coding skills? Any tips or resources?', GETDATE(), 'question', 'Improving Coding Skills', 1);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title])
VALUES (2, 'Improving your coding skills involves consistent practice, learning from others, and exploring new technologies.', GETDATE(), 'answer', NULL);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (3, 'Great advice! I''ve found that collaborating on GitHub projects also helps.', GETDATE(), 'comment');

-- Replies insert
INSERT INTO [Replies] ([idOfPostRepliedOn], [idOfReply])
VALUES (1, 2), (3, 2), (4, 5), (4, 6);

-- Votes insert
INSERT INTO [Votes] ([postId], [userId], [value])
VALUES (2, 1, 1), (1, 3, 1), (2, 2, -1), (3, 1, 1);

-- Tags insert
insert into [Tags] ([name])
values ('Trees'), ('C#'), ('Quantum'), ('Python'), ('DataScience'), ('FunctionalProgramming'), ('Lisp');

-- Has insert
insert into [Has] ([questionId], [tagId]) 
values (1, 3)

-- Notifications insert
INSERT INTO [Notifications] ([userId], [postId], [badgeId])
VALUES (1, 1, NULL), (1, 4, NULL), (3, 3, NULL), (1, 4, NULL), (1, NULL, 1), (1, NULL, 2), (1, NULL, 3), (2, NULL, 2), (3, NULL, 1), (3, NULL, 2);

-- Posts insert (additional questions, answers, and comments)
-- Question 1
INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title], [categoryId])
VALUES (1, 'What are the benefits of using Python programming language?', GETDATE(), 'question', 'Python Benefits', 1);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (2, 'Python is known for its simplicity, readability, and vast ecosystem of libraries.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (4, 'Another benefit of Python is its versatility; it can be used for web development, data analysis, artificial intelligence, and more.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (3, 'I agree, Python''s readability and versatility make it a popular choice among developers.', GETDATE(), 'comment');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (5, 'Yes, Python is also beginner-friendly, which makes it a great language for those new to programming.', GETDATE(), 'comment');

INSERT INTO [Replies] ([idOfPostRepliedOn], [idOfReply])
VALUES (7, 8), (7, 9), (7, 10), (7, 11);


-- Question 2
INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title], [categoryId])
VALUES (2, 'What are some best practices for writing clean code?', GETDATE(), 'question', 'Clean Code Best Practices', 1);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (4, 'One best practice is to use meaningful variable and function names that describe their purpose.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (3, 'Another important practice is to break down complex tasks into smaller, modular functions.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (5, 'I find that writing descriptive comments alongside code helps improve readability and maintainability.', GETDATE(), 'comment');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (1, 'Agreed! Code should be self-explanatory, but comments can provide additional context.', GETDATE(), 'comment');

INSERT INTO [Replies] ([idOfPostRepliedOn], [idOfReply])
VALUES (12,13), (12, 14), (12, 15), (12, 16);

-- Question 3
INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title], [categoryId])
VALUES (3, 'What are the key differences between object-oriented programming and functional programming?', GETDATE(), 'question', 'OOP vs Functional Programming', 1);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (1, 'Object-oriented programming focuses on representing data as objects with behavior and attributes.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (2, 'Functional programming emphasizes writing functions that are pure and avoid mutable state.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (4, 'In OOP, inheritance and polymorphism are commonly used to achieve code reuse and abstraction.', GETDATE(), 'comment');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (5, 'Functional programming relies heavily on higher-order functions and immutable data structures.', GETDATE(), 'comment');

INSERT INTO [Replies] ([idOfPostRepliedOn], [idOfReply])
VALUES (17, 18), (17, 19), (17, 20), (17, 21);

-- Question 4
INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title], [categoryId])
VALUES (4, 'What are some common design patterns used in software development?', GETDATE(), 'question', 'Software Design Patterns', 1);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (3, 'Some common design patterns include the Singleton pattern, Factory pattern, and Observer pattern.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (1, 'Design patterns provide reusable solutions to common problems in software design.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (2, 'The Observer pattern is particularly useful for implementing event handling systems.', GETDATE(), 'comment');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (5, 'Ive also found the Strategy pattern to be helpful for implementing different algorithms.', GETDATE(), 'comment');

INSERT INTO [Replies] ([idOfPostRepliedOn], [idOfReply])
VALUES (22, 23), (22, 24), (22, 25), (22, 26);


-- Question 5
INSERT INTO [Posts] ([userId], [content], [datePosted], [type], [title], [categoryId])
VALUES (5, 'What are the advantages of using version control systems like Git?', GETDATE(), 'question', 'Advantages of Git', 1);

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (1, 'One advantage of Git is the ability to track changes and collaborate with others on projects.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (2, 'Git also allows for branching and merging, which facilitates concurrent development.', GETDATE(), 'answer');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (3, 'Branching in Git enables developers to work on new features without affecting the main codebase.', GETDATE(), 'comment');

INSERT INTO [Posts] ([userId], [content], [datePosted], [type])
VALUES (4, 'Merging allows changes from different branches to be combined into a single branch.', GETDATE(), 'comment');


INSERT INTO [Replies] ([idOfPostRepliedOn], [idOfReply])
VALUES (27, 28), (27, 29), (27, 30), (27, 31);




-- Votes insert (adding votes for each question)
-- Question 1
INSERT INTO [Votes] ([postId], [userId], [value])
VALUES (7, 1, 1), (7, 2, 1), (7, 3, -1), (7, 4, 1);

-- Question 2
INSERT INTO [Votes] ([postId], [userId], [value])
VALUES (12, 1, 1), (12, 2, 1), (12, 3, 1), (12, 4, 1);

-- Question 3
INSERT INTO [Votes] ([postId], [userId], [value])
VALUES (17, 1, 1), (17, 2, -1), (17, 3, 1), (17, 4, 1);

-- Question 4
INSERT INTO [Votes] ([postId], [userId], [value])
VALUES (22, 1, 1), (22, 2, 1), (22, 3, -1), (22, 4, 1);

-- Question 5
INSERT INTO [Votes] ([postId], [userId], [value])
VALUES (27, 1, 1), (27, 2, -1), (27, 3, 1), (27, 4, 1);

select * from [Users]
select * from [Posts]
select * from [Badges]
select * from [Has]
select * from [Owns]
select * from [Tags]
select * from [Categories]
select * from [Moderates]
select * from [Notifications]
select * from [Replies]
select * from [Votes]