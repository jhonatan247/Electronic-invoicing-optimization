use master;
	go
	begin
		set nocount on
		declare @dbname varchar(50)
		declare @spidstr varchar(8000)
		declare @connkilled smallint
		set @connkilled=0
		set @spidstr = ''
		set @dbname = 'siigo'

		if db_id(@dbname) < 4
		begin
			print 'connections to system databases cannot be killed'
			return
		end

		select @spidstr=coalesce(@spidstr,',' )+'kill '+convert(varchar, spid)+ '; '
			from master..sysprocesses where dbid=db_id(@dbname)
 
		if len(@spidstr) > 0
		begin
			exec(@spidstr)
			select @connkilled = count(1)
			from master..sysprocesses where dbid=db_id(@dbname)
		end
	end
	begin
		if exists(select * from sys.databases where name = 'siigo')
			drop database siigo;
	end
		go			
				
	create database siigo;
		go

use siigo;
	go
	create table request_status(
		id int identity primary key,
		status varchar(50) unique not null
	);
	go
	create index i1 on request_status (status);
	go
	insert request_status values 
	('Added'), 
	('Validated'), 
	('Validation error'),
	('Signed'), 
	('Sign error'), 
	('Confirmed'), 
	('Confirm error'), 
	('Sended'), 
	('Send error'), 
	('Received'); 
	go

	create table request(
		id bigint identity primary key,
		status int references request_status on delete no action not null,
		type varchar(50) not null,
		prefix text,
		consecutive text,
		UBL text,
		pathPdfSource text,
		downloadLink text,
		sourceXmlText text,
		xmlText text,
		createdTime datetime default getdate() not null,
		check(type = 'PDF' or type = 'XML')
	);
	go
	create index i2 on request (type);
	go
	create proc insert_request(
		@type varchar(50),
		@value text
	)
	as
	begin
		if (@type = 'PDF')
			insert  insert_request (status, type, pathPdfSource, createdTime) values (1, @type, @value, getdate());
		else 
			insert  insert_request (status, type, sourceXmlText, createdTime) values (1, @type, @value, getdate());
	end
	go

	create view v_request
	as
		select 
		r.id,
		s.status,
		type,
		prefix,
		consecutive,
		UBL,
		pathPdfSource,
		DownloadLink,
		sourceXmlText,
		xmlText,
		createdTime
	from request r
	inner join  request_status s
	on s.id = r.status;
	go
	create proc change_state(
		@id bigint,
		@status int
	)
	as
	begin
		update request set
		status = @status
		where id = @id;
	end
	go



