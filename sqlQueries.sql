create table Users (
    [Id] integer primary key AUTOINCREMENT not null,
    [Name] varchar(20) unique not null,
    [Password] varchar(20) not null,
    [LastTimeLoginId] integer nut null,
    foreign key (LastTimeLoginId) references LoginTimeLog(Id)
);

create table LoginTimeLog (
    [Id] integer primary key AUTOINCREMENT not null,
    [UserId] integer not null,
    [LastTimeLogin] datetime not null,
    foreign key (UserId) references Users(Id)
);

create table Queries (
    [Id] integer primary key AUTOINCREMENT not null,
    [UserId] integer not null,
    [Time] datetime not null,
    foreign key (UserId) references Users(Id)
);

create table DetectedWords(
    [Id] integer primary key AUTOINCREMENT not null,
    [UserId] integer not null,
    [QueryId] integer not null,
    [Word] varchar(20) not null,
    [Language] varchar(20),
    [Confidence] float,
    foreign key (UserId) references Users(Id),
    foreign key (QueryId) references Queries(QueryId)
);

create table TopRequests(
    [Id] integer primary key AUTOINCREMENT not null,
    [UserId] integer not null unique,
    [QueriesCount] integer,
    [LastTimeLoginId] integer,
    [AverageTimeBetweenQueries] datetime,
    foreign key (UserId) references Users(Id),
    foreign key (LastTimeLoginId) references LoginTimeLog(Id)
);

create trigger update_last_login_time after insert on LoginTimeLog
begin
    update Users set LastTimeLoginId = new.Id where new.UserId = Id;
    update TopRequests set LastTimeLoginId = new.Id where new.UserId = UserId;
end;

create trigger insert_topRequest_on_user_inserting after insert on Users
begin
    insert into TopRequests(UserId) values (new.Id);
end;

create trigger update_topRequests_on_inserting_query after insert on Queries
begin
    update TopRequests set QueriesCount = (select count(*) from Queries where Queries.UserId = new.UserId) where TopRequests.UserId = new.UserId;
    update TopRequests set AverageTimeBetweenQueries = (select (MAX(strftime('%M', Time)) - MIN('%M', strftime(Time))) / count(*) from Queries where Queries.UserId = new.UserId) where TopRequests.UserId = new.UserId;
end;

drop table Queries;
drop table DetectedWords;
drop table Users;
drop table TopRequests;
drop table LoginTimeLog;