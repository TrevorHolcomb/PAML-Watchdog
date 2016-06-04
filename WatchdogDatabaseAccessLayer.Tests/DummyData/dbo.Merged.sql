SET IDENTITY_INSERT dbo.SupportCategories ON;
INSERT INTO SupportCategories (Id, Name, [Description]) VALUES (1, 'senior project team', 'generated by dbo.Merged.sql');
SET IDENTITY_INSERT dbo.SupportCategories OFF;

SET IDENTITY_INSERT dbo.AlertTypes ON;
INSERT INTO dbo.AlertTypes (Id, Name, [Description]) VALUES (1, 'test alertType', 'generated by dbo.Merged.sql');
SET IDENTITY_INSERT dbo.AlertTypes OFF;

INSERT INTO dbo.Engines(Name) VALUES ('WatchdogMessageGenerator');

INSERT INTO dbo.MessageTypes (Name, [Description]) VALUES ('Test Message Type', 'a test message type');

INSERT INTO dbo.MessageTypeParameterTypes (MessageTypeName, Name, [Type], [Required]) VALUES ('Test Message Type', 'QueueSize', 'Decimal', 1);

SET IDENTITY_INSERT dbo.EscalationChainLinks ON;
INSERT INTO dbo.EscalationChainLinks (Id) VALUES (1);
SET IDENTITY_INSERT dbo.EscalationChainLinks OFF;

SET IDENTITY_INSERT dbo.EscalationChains ON;
INSERT INTO dbo.EscalationChains (Id, Name, [Description], EscalationChainRootLink_Id) VALUES (1, 'testEscalationChain1', 'generated by dbo.Merged.sql', 1);
SET IDENTITY_INSERT dbo.EscalationChains OFF;

SET IDENTITY_INSERT dbo.RuleCategories ON;
INSERT INTO dbo.RuleCategories (Id, Name, [Description]) VALUES (1, 'ruleCategory1', 'generated by dbo.Merged.sql');
SET IDENTITY_INSERT dbo.RuleCategories OFF;

/*Change the value for MessageType_Id as convenient
 *If you're using the message generator and want to match against this rule, then make sure the message Ids match
 */
SET IDENTITY_INSERT dbo.Rules ON;
INSERT INTO dbo.Rules 
(MessageTypeName, Id, Name, [Description], Expression, AlertTypeId, SupportCategoryId, RuleCategoryId, RuleCreator, DefaultSeverity)
VALUES
('Test Message Type', 1, 'rule1', 'generated by dbo.Rules.data.sql', 'QueueSize > 50000', 1, 1, 1, 'a SQL script', 1);
SET IDENTITY_INSERT dbo.Rules OFF;