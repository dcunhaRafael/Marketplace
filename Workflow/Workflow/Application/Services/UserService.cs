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
    public class UserService : BaseLogger, IUserService {
        private readonly IUserRepository userRepository;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository) : base(logger) {
            this.userRepository = userRepository;
        }

        public async Task<IList<User>> ListAsync(int profileId, RecordStatusEnum? status) {
            var methodParameters = new { profileId, status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await userRepository.ListAsync(profileId, status);
                var payloads = from a in items select new User() { UserId = a.UserId.Value, Name = a.Name, Email = a.Email };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos usuários: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
