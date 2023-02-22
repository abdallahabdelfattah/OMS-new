USE [TMS_18_12_2019]
GO
--SET IDENTITY_INSERT [common].[AttachmentType] ON 

--GO
--INSERT [common].[AttachmentType] ([Id], [Code], [NameAr], [NameEn], [AllowedFilesExtension], [IsImage], [ImageMaxHeight], [ImageMaxWidth], [MaxSizeInMegabytes], [IsMandatory], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, N'GeneralFileAttachment', N'مرفق ملف', N'General Attachment', N'pdf,doc,docx,jpg,jpeg,bmp,png', 0, NULL, NULL, 2, 0, N'unada', CAST(N'2017-11-11T00:00:00.0000000' AS DateTime2), NULL, NULL)
--GO
--INSERT [common].[AttachmentType] ([Id], [Code], [NameAr], [NameEn], [AllowedFilesExtension], [IsImage], [ImageMaxHeight], [ImageMaxWidth], [MaxSizeInMegabytes], [IsMandatory], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, N'GeneralImageAttachment', N'مرفق صورة', N'General Image Attachment', N'jpg,jpeg,bmp,png', 1, 350, 350, 2, 0, N'unada', CAST(N'2017-11-11T00:00:00.0000000' AS DateTime2), NULL, NULL)
--GO
--INSERT [common].[AttachmentType] ([Id], [Code], [NameAr], [NameEn], [AllowedFilesExtension], [IsImage], [ImageMaxHeight], [ImageMaxWidth], [MaxSizeInMegabytes], [IsMandatory], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, N'RegulationAttachment', N'مرفق النظام أو اللائحة', N'Regulation / Law Attachment', N'pdf,doc,docx', 0, NULL, NULL, 2, 1, N'unada', CAST(N'2017-11-11T00:00:00.0000000' AS DateTime2), NULL, NULL)
--GO
--INSERT [common].[AttachmentType] ([Id], [Code], [NameAr], [NameEn], [AllowedFilesExtension], [IsImage], [ImageMaxHeight], [ImageMaxWidth], [MaxSizeInMegabytes], [IsMandatory], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, N'ArticleAttachment', N'مرفق المادة', N'Article Attachment', N'pdf,doc,docx', 0, NULL, NULL, 2, 0, N'unada', CAST(N'2017-11-11T00:00:00.0000000' AS DateTime2), NULL, NULL)
--GO
--INSERT [common].[AttachmentType] ([Id], [Code], [NameAr], [NameEn], [AllowedFilesExtension], [IsImage], [ImageMaxHeight], [ImageMaxWidth], [MaxSizeInMegabytes], [IsMandatory], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, N'RoyalDecreeAttachment', N'مرفق المرسوم الملكي', N'Royal Decree Attachment', N'pdf,doc,docx', 0, NULL, NULL, 2, 1, N'unada', CAST(N'2017-11-11T00:00:00.0000000' AS DateTime2), NULL, NULL)
--GO
--SET IDENTITY_INSERT [common].[AttachmentType] OFF
GO
INSERT [common].[NotificationSendStatus] ([Id], [NameAr], [NameEn]) VALUES (1, N'لم تعالج', N'Not Processed')
GO
INSERT [common].[NotificationSendStatus] ([Id], [NameAr], [NameEn]) VALUES (2, N'قيد المعالجة', N'Under Processing')
GO
INSERT [common].[NotificationSendStatus] ([Id], [NameAr], [NameEn]) VALUES (3, N'تم الإرسال', N'Sent')
GO
INSERT [common].[NotificationSendStatus] ([Id], [NameAr], [NameEn]) VALUES (4, N'فشل الإرسال', N'Failed')
GO
INSERT [common].[NotificationSendStatus] ([Id], [NameAr], [NameEn]) VALUES (5, N'تجاوز عدد مرات محاولات الإرسال', N'RetryThresholdExceeded')
GO
INSERT [common].[NotificationType] ([Id], [NameAr], [NameEn]) VALUES (1, N'بريد الكتروني', N'Email')
GO
INSERT [common].[NotificationType] ([Id], [NameAr], [NameEn]) VALUES (2, N'رسالة قصيرة', N'SMS')
GO
INSERT [common].[NotificationType] ([Id], [NameAr], [NameEn]) VALUES (3, N'ويب', N'Web')
GO
SET IDENTITY_INSERT [common].[NotificationTemplate] ON 

GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, NULL, N'CommonEmailStructureAr', N'<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" dir="rtl">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        body {
            padding: 0px;
        }

        .email_footer {
            padding: 10px 0;
            background-color: #269a85;
            color: #fff;
            font: 11px tahoma;
            text-align: center;
        }

            .email_footer a:link, .email_footer a:visited {
                color: #fff;
                font: 11px tahoma;
            }

        .email_conts {
            padding: 20px;
            font: 12px tahoma;
            line-height: 20px;
            color: #404040;
            text-align: right;
            
            border-top: none;
        }

        .email_header {
            border-bottom:5px solid #269a85;
			padding:20px;
			text-align:center;
        }
		.mail_table{
		width:100%;
		border: 1px solid #eee;
		}
    </style>
</head>
<body dir="rtl">
    <!--<img src="{RootUrl}/Content/images/email_header.jpg" width="598" height="123" class="email_header" />-->
	
    <table class="mail_table" border="0" cellspacing="0" cellpadding="0">
	<tr>
	<td class="email_header">
	<img src="{RootUrl}/Content/images/coc_logo.png"/>
	</td>
	</tr>
        <tr>
            <td class="email_conts">
               {Body}
            </td>
        </tr>
        <tr>
           
			<td class="email_footer">
                لمقترحاتكم وملاحظاتكم راسلونا على البريد الإلكتروني: <a href="mailto:{ContactUsEmail}">{ContactUsEmail}</a>
            </td>
			
        </tr>
    </table>
</body>
</html>', 1, 1, N'unada', CAST(N'2016-12-12T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-12-12T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, NULL, N'CommonEmailStructureEn', N'<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" dir="ltr">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        body {
        padding: 0px;
	direction:ltr;
		
        }

        .email_footer {
            padding: 10px 0;
            background-color: #269a85;
            color: #fff;
            font: 11px tahoma;
            text-align: center;
        }

            .email_footer a:link, .email_footer a:visited {
                color: #fff;
                font: 11px tahoma;
            }

        .email_conts {
            padding: 20px;
            font: 12px tahoma;
            line-height: 20px;
            color: #404040;
            text-align: left;
            
            border-top: none;
        }

        .email_header {
            border-bottom:5px solid #269a85;
			padding:20px;
			text-align:center;
        }
		.mail_table{
		width:100%;
		border: 1px solid #eee;
		}
    </style>
</head>
<body>
    <!--<img src="{RootUrl}/Content/images/email_header.jpg" width="598" height="123" class="email_header" />-->
	
    <table class="mail_table" border="0" cellspacing="0" cellpadding="0">
	<tr>
	<td class="email_header">
	<img src="{RootUrl}/Content/images/coc_logo.png"/>
	</td>
	</tr>
        <tr>
            <td class="email_conts">
               {Body}
            </td>
        </tr>
        <tr>
           
			<td class="email_footer">
                For Inquiries and suggestions contact us on email: <a href="mailto:{ContactUsEmail}">{ContactUsEmail}</a>
            </td>
			
        </tr>
    </table>
</body>
</html>', 1, 1, N'unada', CAST(N'2016-03-02T18:41:14.0200000' AS DateTime2), N'unada', CAST(N'2016-03-02T18:41:14.0200000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, NULL, N'NewUserWelcomeEmailAr', N'عزيزي {FullName} 
<br />
لقد تم تسجيلك ضمن البوابة بنجاح, معلومات الحساب هي :
<br />
اسم المستخدم : {Username}
<br />
كما يمكنك تسجيل الدخول من خلال الرابط التالي: {RootUrl}/Account/Login
<br />
شكرا', 1, 1, N'unada', CAST(N'2016-03-02T18:47:53.2700000' AS DateTime2), N'unada', CAST(N'2016-03-02T18:47:53.2700000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, NULL, N'NewUserWelcomeEmailEn', N'Dear {FullName} 
<br />
You have been registered successfully in our portal :
<br />
Your User Name : {Username}
<br />
Login page Url : {RootUrl}/Account/Login
<br />
Thanks', 1, 1, N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, NULL, N'ConfirmUserEmailAr', N'عزيزي {FullName}
<br/>
تم تسجيلكم في البوابة. برجاء استخدام الرابط التالي من اجل تفعيل حسابكم
<br/>
<a href="{RootUrl}/Account/ConfirmEmail/?userId={UserId}&code={Code}&mode=activate" title="رابط تفعيل حساب مستخدم جديد">{RootUrl}/Account/ConfirmEmail/?userId={UserId}&code={Code}&mode=activate</a>
<br/>
سوف تنتهي صلاحية الرابط المرسل اليكم بعد 24 ساعة. يمكنك ارسال رابط تفعيل جديد في حالة انتهاء صلاحية الرابط الحالي
<br/>
شكرا', 1, 1, N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (6, NULL, N'ConfirmUserEmailEn', N'Dear {FullName}
<br/>
You have registered in our system. 
<br/>
please click below link to activate your account
<a href="{RootUrl}/Account/ConfirmEmail/?userId={UserId}&code={Code}&mode=activate" title="Activate User Account Link">{RootUrl}/Account/ConfirmEmail/?userId={UserId}&code={Code}&mode=activate</a>
<br/>
This link expires after 24 hours. In case of expiration you can resend it to activate your account
<br/>
Thanks', 1, 1, N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (7, NULL, N'ForgotPasswordEmailAr', N'عزيزي {FullName}
<br/>
نأمل استخدام الرابط التالي من أجل تغيير كلمة مرور الحساب الخاص بكم على البوابة
<br/>
<a href="{RootUrl}/Account/ResetPassword/?userId={UserId}&code={Code}&mode=reset" title="رابط تغيير كلمة المرور">{RootUrl}/Account/ResetPassword/?userId={UserId}&code={Code}&mode=reset</a>
<br/>
سوف تنتهي صلاحية الرابط المرسل اليكم بعد 24 ساعة. يمكنك ارسال رابط جديد لتغيير كلمة المرور في حالة انتهاء صلاحية الرابط الحالي
<br/>
شكرا', 1, 1, N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (8, NULL, N'ForgotPasswordEmailEn', N'Dear {FullName} 
<br/>
Kindly use below link to change your password
<br/>
<a href="{RootUrl}/Account/ResetPassword/?userId={UserId}&code={Code}&mode=reset" title="Reset Password Link">{RootUrl}/Account/ResetPassword/?userId={UserId}&code={Code}&mode=reset</a>
<br/>
This Link expires after 24 hours. In case of expiration you can send another new link to reset your password.
<br/>
Thanks', 1, 1, N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (9, NULL, N'RequestSubmittedAr', N'عزيزي {FullName},
<br />
نود اشعاركم أنه تم إرسال طلبكم الخاص بـ {ServiceName}
<br />
رقم الطلب: <div style="direction:ltr;">{RequestNumber}</div>
<br />
يمكنكم استعراض ومتابعة تفاصيل طلباتكم من خلال صفحة "طلباتي" في ملفكم الشخصي على البوابة
<br />
شكرا', 1, 1, N'unada', CAST(N'2016-11-11T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-11-02T14:49:14.1870000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10, NULL, N'RequestSubmittedEn', N'Dear {FullName},
<br />
Your Request for "{ServiceName}" Has Been Received Successfully 
<br />
Request Number: <div style="direction:ltr">{RequestNumber}</div> 
<br />
You can Display your Request and follow up on its status from My Requests Page in your account
<br />
Thanks', 1, 1, N'unada', CAST(N'2016-11-11T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-11-02T14:50:21.8670000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (11, NULL, N'RequestStatusUpdatedAr', N'عزيزي {FullName},
<br />
نود إشعاركم  بـخصوص طلبكم رقم <div style="direction:ltr;">{RequestNumber}</div> الخاص بـ {ServiceName}
بأنه تم تعديل حالته إلى {RequestStatus}
<br />
يمكنكم استعراض ومتابعة تفاصيل طلباتكم من خلال صفحة "طلباتي" في ملفكم الشخصي على البوابة
<br />
شكرا', 1, 1, N'unada', CAST(N'2016-11-11T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-11-02T14:59:07.1970000' AS DateTime2))
GO
INSERT [common].[NotificationTemplate] ([Id], [ApplicationId], [Key], [Value], [NotificationTypeId], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (12, NULL, N'RequestStatusUpdatedEn', N'Dear {FullName},
<br />
Your Request <div style="direction:ltr;">{RequestNumber}</div> for "{ServiceName}" Status Has Been Updated to {RequestStatus}
<br />
You can Display your Request and follow up on its status from My Requests Page in your account
<br />
Thanks', 1, 1, N'unada', CAST(N'2016-11-11T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-11-02T14:59:29.0070000' AS DateTime2))
GO
SET IDENTITY_INSERT [common].[NotificationTemplate] OFF
GO
SET IDENTITY_INSERT [common].[Navigation] ON 

GO
INSERT [common].[Navigation] ([Id], [ApplicationId], [NameAr], [NameEn], [LinkUrl], [ParentId], [Roles], [ItemOrder], [IsActive], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (1, NULL, N'عن الهيئة', N'About Commission', NULL, NULL, NULL, NULL, 1, CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', NULL, NULL)
GO
INSERT [common].[Navigation] ([Id], [ApplicationId], [NameAr], [NameEn], [LinkUrl], [ParentId], [Roles], [ItemOrder], [IsActive], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (2, NULL, N'كلمة رئيس الهيئة', N'President Speech', NULL, 1, NULL, NULL, 1, CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', NULL, NULL)
GO
INSERT [common].[Navigation] ([Id], [ApplicationId], [NameAr], [NameEn], [LinkUrl], [ParentId], [Roles], [ItemOrder], [IsActive], [CreatedOn], [CreatedBy], [UpdatedOn], [UpdatedBy]) VALUES (3, NULL, N'رؤيتنا', N'Our Vision', NULL, 1, NULL, NULL, 1, CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', NULL, NULL)

SET IDENTITY_INSERT [common].[Navigation] OFF
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (1, NULL, 0, N'AttachmentsMaxSize', N'int', N'1', N'Attachments', 1, 1, N'unada', CAST(N'2012-10-10T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2012-10-10T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (2, NULL, 0, N'AttachmentsAllowedTypes', N'string', N'jpg,jpeg,png,pdf,doc,docx', N'Attachments', 1, 1, N'unada', CAST(N'2012-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2012-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (3, NULL, 0, N'AttachmentsAllowedHeight', N'int', N'200', N'Attachments', 1, 1, N'unada', CAST(N'2016-01-03T00:00:00.0000000' AS DateTime2), N'malbayoush', CAST(N'2016-01-03T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (4, NULL, 0, N'AttachmentsAllowedWidth', N'string', N'200', N'Attachments', 1, 1, N'unada', CAST(N'2016-01-03T00:00:00.0000000' AS DateTime2), N'malbayoush', CAST(N'2016-01-03T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (5, NULL, 0, N'AttachmentsPath', N'string', N'\\SURE-SPS13\MsbarDevelopmentAttachments', N'Attachments', 1, 1, N'unada', CAST(N'2016-01-03T00:00:00.0000000' AS DateTime2), N'malbayoush', CAST(N'2016-01-03T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (6, NULL, 0, N'SaveFilesToDatabase', N'string', N'True', N'Attachments', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (7, NULL, 0, N'DisableSMSNotifications', N'bool', N'False', N'Notifications', 1, 1, N'admin', CAST(N'2016-12-12T00:00:00.0000000' AS DateTime2), N'admin', CAST(N'2016-12-12T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (8, NULL, 0, N'DisableEmailNotifications', N'bool', N'False', N'Notifications', 1, 1, N'admin', CAST(N'2016-12-12T00:00:00.0000000' AS DateTime2), N'admin', CAST(N'2016-12-12T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (9, NULL, 0, N'EmailSubject', N'string', N'هيئة المقاييس والمواصفات السعودية', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (10, NULL, 0, N'SmtpServer', N'string', N'smtp.gmail.com', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (11, NULL, 0, N'SmtpUsername', N'string', N'skeltaadmin@sure.com.sa', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (12, NULL, 0, N'SmtpPassword', N'string', N'skeltaadmin@sure.co', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (13, NULL, 0, N'IsSmtpAuthenticated', N'bool', N'True', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (14, NULL, 0, N'SmtpPort', N'int', N'25', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (15, NULL, 0, N'SmtpEnableSSL', N'bool', N'True', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (16, NULL, 0, N'EmailFromAddress', N'string', N'skeltaadmin@sure.com.sa', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (17, NULL, 0, N'EmailFromName', N'string', N'هيئة المقاييس والمواصفات السعودية', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (18, NULL, 0, N'ContactUsEmail', N'string', N'contactus@saso.gov.sa', N'Notifications', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (19, NULL, 0, N'MaxLoginAttemptsBeforeCaptcha', N'int', N'3', N'UsersMgmt', 1, 1, N'unada', CAST(N'2016-01-03T00:00:00.0000000' AS DateTime2), N'malbayoush', CAST(N'2016-01-03T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (20, NULL, 0, N'VerificationCodeValidHours', N'int', N'24', N'UsersMgmt', 1, 1, N'unada', CAST(N'2016-06-06T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-06-06T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (21, NULL, 0, N'DateFormat', N'string', N'dd/MM/yyyy', N'General', 1, 1, N'unada', CAST(N'2014-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2014-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (22, NULL, 0, N'TimeFormat', N'string', N'hh:mm:ss tt', N'General', 1, 1, N'unada', CAST(N'2015-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2015-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (23, NULL, 0, N'DefaultPagerPageSize', N'int', N'20', N'General', 1, 1, N'unada', CAST(N'2015-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2015-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (24, NULL, 0, N'PhoneInputInitialCountry', N'string', N'sa', N'General', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (25, NULL, 0, N'PhoneInputPreferredCountries', N'string', N'sa,qa,ae,kw,om,bh', N'General', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (26, NULL, 0, N'GoogleMapKey', N'string', N'AIzaSyAjuBJgrT5MhAA9mPp1x75aOADfSKHVy1g', N'General', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
INSERT [common].[SystemSettings] ([Id], [ApplicationId], [Secure], [Key], [ValueType], [Value], [GroupName], [IsSticky], [IsActive], [CreatedBy], [CreatedOn], [UpdatedBy], [UpdatedOn]) VALUES (27, NULL, 0, N'ApplicationUrl', N'string', N'http://localhost:5001/', N'General', 1, 1, N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2), N'unada', CAST(N'2016-01-01T00:00:00.0000000' AS DateTime2))
GO
