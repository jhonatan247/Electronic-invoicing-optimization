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
	('Saved'), 
	('Save error'),
	('Signed'), 
	('Sign error'), 
	('Validated by xsd'), 
	('Error when validating with xsd'), 
	('Sended'), 
	('Send error'), 
	('Connection error (step 4)'),
	('Connection error (step 5)'),
	('Not accepted'),
	('Received'); 
	go

	create table request(
		id bigint identity primary key,
		status int references request_status on delete no action not null,
		type varchar(50) not null,
		validator int not null,
		prefix varchar(500),
		consecutive varchar(500),
		UBL varchar(500),
		pathPdfSource varchar(500),
		sourceXmlText varchar(500),
		downloadLink varchar(500),
		xmlText varchar(500),
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
		declare @validator1 int, @validator2 int, @validator3 int, @validator int;
		set @validator1 = (select count(*) from request where validator = 1 and status = 1);
		set @validator2 = (select count(*) from request where validator = 2 and status = 1);
		set @validator3 = (select count(*) from request where validator = 3 and status = 1);

		if(@validator1 <= @validator2 and @validator1<=@validator3)
			set @validator = 1;
		else if (@validator2 <= @validator1 and @validator1<=@validator3)
			set @validator = 2;
		else 
			set @validator = 3;

		if (@type = 'PDF')
			insert  request (status, type, pathPdfSource, validator, createdTime) values (1, @type, @value, @validator, getdate());
		else 
			insert  request (status, type, sourceXmlText, validator, createdTime) values (1, @type, @value, @validator, getdate());
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
		sourceXmltext,
		xmltext,
		createdTime
	from request r
	inner join  request_status s
	on s.id = r.status;
	go
	create view v_request_interface
	as
	select 
		r.id as Id, type as Type, s.status as Status
		from request r
		inner join request_status s
		on s.id = r.status;
	go
	create proc update_request_state(
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
	create proc select_request_interface
	as
	begin
		select * from v_request_interface;
	end
	go
	create proc select_request
	as
	begin
		select * from request;
	end
	go

	create proc select_request_validator(
		@validator int
	)
	as
	begin
		select * from request where validator = @validator and status = 1;
	end
	go
	create proc select_request_status(
		@status int
	)
	as
	begin
		select * from request where status = @status;
	end
	go
	create proc select_request_two_status(
		@status1 int,
		@status2 int
	)
	as
	begin
		select * from request where status = @status1 or status = @status2;
	end
	go
	create proc update_request_xml(
		@id bigint
	)
	as
	begin
		update request set
		prefix = 'L',
		consecutive = cast(id as varchar(500)),
		UBL = pathPdfSource,
		xmlText = '<invoice><prefix>L</prefix><consecutive>'+cast(id as varchar(500))+'</consecutive><ubl>'+pathPdfSource+'</ubl><invoice>'
		where id = @id;
	end
	go
	
	create proc update_request_pdf(
		@id bigint
	)
	as
	begin
		update request set
		prefix = 'P',
		consecutive = cast(id as varchar(500)),
		UBL = sourceXmlText,
		xmlText = '<invoice><prefix>L</prefix><consecutive>'+cast(id as varchar(500))+'</consecutive><ubl>'+sourceXmlText+'</ubl><invoice>'
		where id = @id;
	end
	go
	create proc clear_data
	as
	begin
		truncate table request;
	end
	go
	select * from request;
