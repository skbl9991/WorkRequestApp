using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkRequestManagment.Models;

namespace WorkRequestManagment.Infrastructure
{
    //Declare Access to Requests
    public interface IWorkRequestDataRepository
    {
        WorkRequest GetWorkRequest(long id);
        IEnumerable<WorkRequest> GetAllWorkRequests(bool includeUsers = false);
        IEnumerable<WorkRequest> GetFilteredWorkRequests(int[] statuses = null);
        void CreateWorkRequest(WorkRequest workRequest);
        void UpdateWorkRequest(WorkRequest cahngeWorkRequest, WorkRequest originalWorkRequest);
        void Delete(long id);
    }
}
