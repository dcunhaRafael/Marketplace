
SELECT bp.Name as BrokerName, bp.CpfCnpjNumber as BrokerCpfCnpjNumber
     , ( SELECT TOP 1 Value 
	     FROM PersonContact 
		 WHERE PersonId = bp.Id 
		 AND ContactTypeId = (SELECT Value FROM ApplicationParameter WHERE Id = 400) 
		 AND Status = 1
		 AND Value IS NOT NULL) as BrokerEmail
     , pl.Id as ProposalLinkId, pl.Name as ProposalLinkName
     , p.Id as ProposalId, p.ProposalNumber, p.ProposalDate, p.StartOfTerm, p.EndOfTerm
     , pw.InsuredAmount, pw.TariffPremium
	  , prd.Id as ProductId, prd.Name as ProductName
	  , cov.Id as CoverageId, cov.Name as CoverageName
	  , tp.Name as TakerName, tp.CpfCnpjNumber as TakerCpfCnpjNumber
	  , ip.Name as InsuredName, ip.CpfCnpjNumber as InsuredCpfCnpjNumber
	  , pr.Id as ProposalRestrictionId, pr.ProductCoverageRestrictionId, pr.RestrictionStatus, pr.InclusionDate as RestrictionDate
	  , pcr.Name as RestrictionName, pcr.IsAutomaticRefusal, pcr.AutomaticRefusalReasonId, pcr.InformationTimeout, pcr.WarningTimeout, pcr.DangerTimeout
     , ( SELECT COUNT(1)
         FROM ProposalRestriction a
         INNER JOIN ProductCoverageRestriction b ON b.Id = a.ProductCoverageRestrictionId
         INNER JOIN ProductCoverageRestrictionDocumentType c ON c.ProductCoverageRestrictionId = b.Id AND c.Status = 1 AND c.IsRequired = 1
         LEFT JOIN (
               SELECT a.Id, a.ProposalRestrictionId, a.DocumentAttachedId, a.Status, a.InclusionUserId, a.InclusionDate, a.LastChangeUserId, a.LastChangeDate, b.DocumentTypeId
         	    FROM ProposalRestrictionDocumentAttached a
           	INNER JOIN DocumentAttached b ON b.Id = a.DocumentAttachedId
           	WHERE a.ProposalRestrictionId = pr.Id
               AND a.Status = 1--@Active
         ) d ON d.DocumentTypeId = c.DocumentTypeId
         WHERE a.Id = pr.Id
         AND d.DocumentAttachedId IS NULL) as PendingDocumentCount
FROM ProposalRestriction pr
INNER JOIN ProductCoverageRestriction pcr ON pcr.Id = pr.ProductCoverageRestrictionId
INNER JOIN Proposal p ON p.Id = pr.ProposalId
INNER JOIN ProposalLink pl ON pl.Id = p.ProposalLinkId
INNER JOIN Product prd ON prd.Id = pl.ProductId
INNER JOIN Coverage cov ON cov.Id = pl.CoverageId
INNER JOIN ProposalWarranty pw ON pw.Id = p.Id
INNER JOIN Broker b ON b.Id = p.BrokerId
INNER JOIN Person bp ON bp.Id = b.PersonId
INNER JOIN Taker t ON t.Id = p.TakerId
INNER JOIN Person tp ON tp.Id = t.PersonId
INNER JOIN Insured i ON i.Id = p.InsuredId
INNER JOIN Person ip ON ip.Id = i.PersonId
--WHERE (@ProposalRestrictionId IS NULL OR pr.Id = @ProposalRestrictionId)
--AND (@ProposalNumber IS NULL OR p.ProposalNumber = @ProposalNumber)
--AND (@FromDate IS NULL OR pr.InclusionDate >= @FromDate)
--AND (@ToDate IS NULL OR pr.InclusionDate <= @ToDate)
--AND (@RestrictionStatus IS NULL OR pr.RestrictionStatus = @RestrictionStatus)
--AND (@ProposalLinkId IS NULL OR p.ProposalLinkId = @ProposalLinkId)
--AND (@BrokerId IS NULL OR p.BrokerId = @BrokerId)
--AND (@TakerId IS NULL OR p.TakerId = @TakerId)
--AND (@InsuredId IS NULL OR p.InsuredId = @InsuredId)
--AND (@LoggedUserId IS NULL OR (SELECT COUNT(1) FROM ProductCoverageRestrictionUser WHERE ProductCoverageRestrictionId = pcr.Id AND UserId = @LoggedUserId) > 0)
ORDER BY pr.InclusionDate

select * from personcontact


SELECT COUNT(1) AS ProposalRestrictionCount FROM ProposalRestriction prc
INNER JOIN ProductCoverageRestrictionUser pcru ON pcru.ProductCoverageRestrictionId = prc.ProductCoverageRestrictionId AND pcru.Status = 1
WHERE prc.RestrictionStatus = 1
AND pcru.UserId = 1
