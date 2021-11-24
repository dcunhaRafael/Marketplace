using Application.Interfaces.Services;
using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Log;
using Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class DocumentTypeService : BaseLogger, IDocumentTypeService {
        private readonly IDocumentTypeRepository documentTypeRepository;

        public DocumentTypeService(ILogger<DocumentTypeService> logger, IDocumentTypeRepository documentTypeRepository) : base(logger) {
            this.documentTypeRepository = documentTypeRepository;
        }

        public async Task<IList<DocumentType>> ListAsync(RecordStatusEnum? status) {
            var methodParameters = new { status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await documentTypeRepository.ListAsync(status);
                var payloads = from a in items select new DocumentType() { DocumentTypeId = a.DocumentTypeId, Name = a.Name };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos tipos de documentos: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
