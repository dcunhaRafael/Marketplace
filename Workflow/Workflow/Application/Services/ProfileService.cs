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
    public class ProfileService : BaseLogger, IProfileService {
        private readonly IProfileRepository profileRepository;

        public ProfileService(ILogger<ProfileService> logger, IProfileRepository profileRepository) : base(logger) {
            this.profileRepository = profileRepository;
        }

        public async Task<IList<Profile>> ListAsync(RecordStatusEnum? status) {
            var methodParameters = new { status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await profileRepository.ListAsync(status);
                var payloads = from a in items select new Profile() { ProfileId = a.ProfileId, Name = a.Name };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos perfis: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
