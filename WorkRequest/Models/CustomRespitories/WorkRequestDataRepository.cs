using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Infrastructure;
using WorkRequestManagment.Models.EFContexts;

namespace WorkRequestManagment.Models.CustomRespitories
{
    public class WorkRequestDataRepository : IWorkRequestDataRepository
    {
        private EFWorkRequestContext context;
        public WorkRequestDataRepository(EFWorkRequestContext ctx) => context = ctx;

        public void CreateWorkRequest(WorkRequest workRequest)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkRequest> GetAllWorkRequests(bool includeUsers = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkRequest> GetFilteredWorkRequests(int[] statuses = null)
        {
            throw new NotImplementedException();
        }

        public WorkRequest GetWorkRequest(long id)
        {
            throw new NotImplementedException();
        }

        public void UpdateWorkRequest(WorkRequest cahngeWorkRequest, WorkRequest originalWorkRequest)
        {
            throw new NotImplementedException();
        }
    }
}
