--drop table [Has]
--drop table [Owns]
--drop table [Tags]
--drop table [Notifications]
--drop table [Moderates]
--drop table [Replies]
--drop table [Badges]
--drop table [Votes]
--drop table [Posts]
--drop table [Users]
--drop table [Categories]

create table [Users](
[id] bigint identity primary key,
[name] varchar(255) not null
)
create table [Categories](
[id] bigint identity primary key,
[name] varchar(255) not null
)

create table [Moderates](
[userId] bigint not null foreign key references [Users]([id]),
[categoryId] bigint not null foreign key references [Categories]([id]), 
primary key ([userId], [categoryId])
)

create table [Badges](
[id] bigint identity primary key,
[name] varchar(255) not null,
[description] varchar(500) not null,
[image] image not null
)

create table [Owns](
[userId] bigint not null foreign key references [Users]([id]),
[badgeId] bigint not null foreign key references [Badges]([id]),
primary key ([userId], [badgeId])
)

create table [Posts]( -- here we store questions AND answers AND comments. This is called single table inheritance. We will need proper validation when writing procedures for this
[id] bigint identity primary key,
[userId] bigint not null,
[content] varchar(8000) not null, -- cutting some slack for the image links and markdown
[datePosted] datetime not null,
[dateOfLastEdit] datetime default null,
[type] varchar(20) not null,
constraint [typeConstraint] check ([type] in ('question', 'answer', 'comment')),
[title] varchar(255) default null, -- allowed only if this is a question
[categoryId] bigint foreign key references [Categories]([id]) default null -- allowed only if this is a question
)

create table [Replies](
-- extensive validation when inserting here
-- the ids must be different
-- answers can only reply to questions
-- questions cannot reply to anything
-- commnents can reply to everything
[idOfPostRepliedOn] bigint not null foreign key references [Posts]([id]),
[idOfReply] bigint not null foreign key references [Posts]([id]),
primary key([idOfPostRepliedOn], [idOfReply])
)

create table [Votes](
[postId] bigint not null foreign key references [Posts]([id]),
[userId] bigint not null foreign key references [Users]([id]),
[value] int not null, -- 1 for upvotes and -1 for downvotes
constraint [valueConstraint] check ([value] in (-1, 1)),
primary key ([postId], [userId])
)

create table [Tags](
[id] bigint identity primary key,
[name] varchar(255)
)

create table [Has]( -- only questions can have tags, careful here
[questionId] bigint not null foreign key references [Posts]([id]),
[tagId] bigint not null foreign key references [Tags]([id]),
primary key ([questionId], [tagId])
)

create table [Notifications]( 
-- users get notifications when someone replies for one of their posts or when they get a new badge
-- there is a 'text' field in the class diagram. The text will be generated when an object of Notification type will be instantiated, there is no need to store it here
-- optional: clicking on a reply notification should take the user to that reply 
[id] bigint identity not null primary key,
[userId] bigint not null foreign key references [Users]([id]),
[postId] bigint foreign key references [Posts]([id]),
[badgeId] bigint foreign key references [Badges]([id]),
constraint [typeOfNotificationConstraint] check ( (([postId] is null)and([badgeId] is not null)) or  (([postId] is not null)and([badgeId] is null)) )
)