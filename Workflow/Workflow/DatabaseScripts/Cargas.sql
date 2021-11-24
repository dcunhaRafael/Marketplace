--AppService

--ComissionStatementStatus
insert into ComissionStatementStatus(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Pendente', '131', 1, 1, getdate());
insert into ComissionStatementStatus(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Confirmado', '132', 1, 1, getdate());
insert into ComissionStatementStatus(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Cancelado', '133', 1, 1, getdate());
insert into ComissionStatementStatus(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Liberado', '134', 1, 1, getdate());
insert into ComissionStatementStatus(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Transferido', '136', 1, 1, getdate());
insert into ComissionStatementStatus(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Rejeitado', '135', 1, 1, getdate());
insert into ComissionStatementStatus(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Em análise', '130', 1, 1, getdate());
insert into ComissionStatementStatus(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Reagendado', '137', 1, 1, getdate());

--DocumentType
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Contrato Social', '6', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ultima alteração contratual', '9', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('CCG', '55', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ficha Cadastral do Tomador', '59', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ficha Cadastral dos Sócios', '60', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ata de eleição da atual diretoria, registrada', '75', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ata de assembleia geral', '76', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Confort Letter', '82', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Cartão CNPJ', '84', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Termo de Compromisso Fiança', '88', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Carta de nomeação', '91', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Ficha cadastral dos acionistas', '92', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Boletim de Subscrição', '94', 1, 1, getdate())
insert into DocumentType(Name, LegacyCode, Status, InclusionUserId, InclusionDate) values ('Relatório interno da ocorrência', '303', 1, 1, getdate())

--OccurrenceType

--OccurrenceTypeDocument

--OccurrenceTypeLiberationUser

--RefusalReason
