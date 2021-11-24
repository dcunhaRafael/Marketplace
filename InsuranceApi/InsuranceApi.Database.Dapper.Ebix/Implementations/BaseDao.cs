using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace InsuranceApi.Database.Dapper.Ebix.Implementations {
    public class BaseDao {
        private readonly IAppAuditorshipDao appAuditorshipDao;

        public BaseDao(IAppAuditorshipDao appAuditorshipDao) {
            this.appAuditorshipDao = appAuditorshipDao;
        }

        /// <summary>
        /// Faz a gravação no histórico de atualização da entidade
        /// </summary>
        /// <param name="method"></param>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <param name="userId"></param>
        public void WriteHistory(MethodBase method, object entity, long entityId, int? userId) {
            appAuditorshipDao.Add(new AppAuditorshipEntity {
                EntityClass = entity.GetType().Name,
                EntityId = entityId,
                ActionName = string.Format("{0}.{1}", method.ReflectedType.Name, method.Name),
                RecordData = JsonConvert.SerializeObject(entity),
                UserId = userId,
                DateUtc = DateTime.UtcNow
            });
        }

    }
}
