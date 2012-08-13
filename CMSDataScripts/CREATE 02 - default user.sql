declare @AppID uniqueidentifier
declare @NewUserID uniqueidentifier
declare @DefUsername nvarchar(200)
declare @DefEmail nvarchar(200)


set @DefUsername = N'carrotadmin'  -- change to the username you want to use to start
set @DefEmail = N'user@example.com' -- change to the email address you want to link to the username

-- default password for the user is carrot123
--=========================================================

set @AppID = NEWID()
set @NewUserID = NEWID()

set @DefUsername = LOWER(@DefUsername)
set @DefEmail = LOWER(@DefEmail)


if (select count([RoleId]) from [dbo].[aspnet_Roles] where [RoleName] = N'CarrotCMS Administrators') < 1 begin

	INSERT [dbo].[aspnet_Applications] ([ApplicationName], [LoweredApplicationName], [ApplicationId], [Description]) 
		VALUES (N'/', N'/', @AppID, NULL)

	INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description])
		 VALUES (@AppID, N'bab36011-9341-458a-a1c7-be34042d619d', N'CarrotCMS Administrators', N'carrotcms administrators', NULL)
	INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description])
		 VALUES (@AppID, N'05a9f80f-afc8-4001-91ba-c19e08df89f2', N'CarrotCMS Editors', N'carrotcms editors', NULL)
	INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description])
		 VALUES (@AppID, N'52018be4-39ef-44ee-aeab-2c64af04cc35', N'CarrotCMS Users', N'carrotcms users', NULL)

	SELECT 'new app "' + CAST( @AppID as varchar(60)) + '" created' as msg	

end else begin

	set @AppID = (select top 1 [ApplicationId] from [aspnet_Applications])
	SELECT 'app "' + CAST( @AppID as varchar(60)) + '" located' as msg
	
end


if (select count([UserId]) from [dbo].[aspnet_Users] where [UserName] = @DefUsername) < 1 begin

	INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) 
		VALUES (@AppID, @NewUserID, @DefUsername, @DefUsername, NULL, 0, GetDate())

	SELECT 'user "' + @DefUsername + '" created' as msg	

	INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (@NewUserID, N'52018be4-39ef-44ee-aeab-2c64af04cc35')
	INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (@NewUserID, N'bab36011-9341-458a-a1c7-be34042d619d')
	INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (@NewUserID, N'05a9f80f-afc8-4001-91ba-c19e08df89f2')

	INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) 
		VALUES (@AppID, @NewUserID, N'3zWCYeLdZe183VNsgMyVhoLKIvw=', 1, N'Xh6pEVmNItc0+hqRjtNJSA==', NULL, @DefEmail, @DefEmail, NULL, NULL, 1, 0, GetDate(), GetDate(), GetDate(), N'1754-01-01', 0, N'1754-01-01', 0, N'1754-01-01', NULL)
	
	SELECT 'user "' + @DefUsername + '" added to groups' as msg	
	
end else begin

	SELECT 'user "' + @DefUsername + '" not added, already exists' as msg

end