﻿ALTER DATABASE [$(DatabaseName)]
    ADD FILE (NAME = [CarrotwareCMS], FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL10.SQL2008EXPRESS\MSSQL\DATA\CarrotwareCMS.MDF', SIZE = 20544 KB, FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];
