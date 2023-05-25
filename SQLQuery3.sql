alter table [Message]
add ReceiverId int
foreign key (ReceiverId) references [User](UserId)

insert into [Message] (GroupId, SenderId, MessageContent, DateAndTimeSent, IsDeleted )values (null, 1, 'text', GETDATE(), 0)
insert into [Message] (GroupId, SenderId, MessageContent, DateAndTimeSent, IsDeleted, ReceiveAsNotification )values (null, 1, 'text', GETDATE(), 0, 1)
insert into [Message] values (null, 1, 'text12313', GETDATE(), 0, 2)

select * from [Message]
select * from [User]

update [User]
set ReceiveNotifications = 1 where UserId = 1

update [Message]
set ReceiverId = 1

alter table [User]
add ReceiveNotifications bit

update [Message]
set MessageContent = 'Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit' where MessageId = 2


SELECT [Message].* 
FROM [Message]
INNER JOIN [User] ON [Message].receiverId = [User].userId
WHERE [Message].receiverId = 1 AND [User].ReceiveNotifications = 1

alter table [Message]
add NotificationRead bit

update [Message]
set NotificationRead = 1 where MessageId = 1