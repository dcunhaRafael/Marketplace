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

--OccurrenceType

--OccurrenceTypeDocument

--OccurrenceTypeLiberationUser

--RefusalReason