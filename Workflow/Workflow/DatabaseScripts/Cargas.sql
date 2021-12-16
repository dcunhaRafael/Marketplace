--AppService

--ComissionStatementStatus
insert into ComissionStatementStatus(Name, Description, LegacyCode, BackgroundColor, TextColor, ImportantWarningText, Status, InclusionUserId, InclusionDate) values ('Pendente', '', '131', '#fcff33', '#000000', '', 1, 1, getdate());
insert into ComissionStatementStatus(Name, Description, LegacyCode, BackgroundColor, TextColor, ImportantWarningText, Status, InclusionUserId, InclusionDate) values ('Confirmado', '', '132', '#0000FF', '#FFFFFF', 'Pagamento efetuado no valor de @{VALOR_PAGAMENTO} atrav�s do cr�dito no banco @{VALOR_PAGAMENTO} - Ag. @{AGENCIA_PAGAMENTO} - C/C @{CONTA_PAGAMENTO}', 1, 1, getdate());
insert into ComissionStatementStatus(Name, Description, LegacyCode, BackgroundColor, TextColor, ImportantWarningText, Status, InclusionUserId, InclusionDate) values ('Cancelado', '', '133', '#FF0000', '#FFFFFF', '', 1, 1, getdate());
insert into ComissionStatementStatus(Name, Description, LegacyCode, BackgroundColor, TextColor, ImportantWarningText, Status, InclusionUserId, InclusionDate) values ('Liberado', '', '134', '#00FF00', '#FFFFFF', '', 1, 1, getdate());
insert into ComissionStatementStatus(Name, Description, LegacyCode, BackgroundColor, TextColor, ImportantWarningText, Status, InclusionUserId, InclusionDate) values ('Transferido', '', '136', '#1ED5C2', '#FFFFFF', '', 1, 1, getdate());
insert into ComissionStatementStatus(Name, Description, LegacyCode, BackgroundColor, TextColor, ImportantWarningText, Status, InclusionUserId, InclusionDate) values ('Rejeitado', '', '135', '#FF0000', '#FFFFFF', '', 1, 1, getdate());
insert into ComissionStatementStatus(Name, Description, LegacyCode, BackgroundColor, TextColor, ImportantWarningText, Status, InclusionUserId, InclusionDate) values ('Em an�lise', '', '130', '#000000', '#FFFFFF', '', 1, 1, getdate());
insert into ComissionStatementStatus(Name, Description, LegacyCode, BackgroundColor, TextColor, ImportantWarningText, Status, InclusionUserId, InclusionDate) values ('Reagendado', '', '137', '#c7ca16', '#000000', '', 1, 1, getdate());

--DocumentType
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Contrato Social', '6', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ultima altera��o contratual', '9', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('CCG', '55', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ficha Cadastral do Tomador', '59', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ficha Cadastral dos S�cios', '60', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ata de elei��o da atual diretoria, registrada', '75', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ata de assembleia geral', '76', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Confort Letter', '82', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Cart�o CNPJ', '84', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Termo de Compromisso Fian�a', '88', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Carta de nomea��o', '91', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ficha cadastral dos acionistas', '92', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Boletim de Subscri��o', '94', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Relat�rio interno da ocorr�ncia', '303', 1, 1, getdate())

--OccurrenceType

--OccurrenceTypeDocument

--OccurrenceTypeLiberationUser

--RefusalReason

--FixedDomain 
INSERT INTO FixedDomain (GroupName, Name, StringValue, DisplayOrder, Status, InclusionUserId, InclusionDate) VALUES ('LatePaymentSlipAgings', 'De 0 a 30 dias', '0|30|#FFF000', 1, 1, 1, getdate());
INSERT INTO FixedDomain (GroupName, Name, StringValue, DisplayOrder, Status, InclusionUserId, InclusionDate) VALUES ('LatePaymentSlipAgings', 'De 31 a 60 dias', '31|60|#FFC900', 2, 1, 1, getdate());
INSERT INTO FixedDomain (GroupName, Name, StringValue, DisplayOrder, Status, InclusionUserId, InclusionDate) VALUES ('LatePaymentSlipAgings', 'De 61 a 120 dias', '61|120|#FF9B00', 3, 1, 1, getdate());
INSERT INTO FixedDomain (GroupName, Name, StringValue, DisplayOrder, Status, InclusionUserId, InclusionDate) VALUES ('LatePaymentSlipAgings', 'De 121 a 180 dias', '121|180|#FF8000', 4, 1, 1, getdate());
INSERT INTO FixedDomain (GroupName, Name, StringValue, DisplayOrder, Status, InclusionUserId, InclusionDate) VALUES ('LatePaymentSlipAgings', 'De 181 a 365 dias', '181|365|#FF5D00', 5, 1, 1, getdate());
INSERT INTO FixedDomain (GroupName, Name, StringValue, DisplayOrder, Status, InclusionUserId, InclusionDate) VALUES ('LatePaymentSlipAgings', 'Acima de 365 dias', '365|999999|#FF0000', 6, 1, 1, getdate());