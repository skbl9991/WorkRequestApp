using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkRequestManagment.Models
{
    #region ColorsFor Statuses
    //Created: text-info #17a2b8
    //InProgress: text-success #007bff
    //Done: text-secondary  #28a745
    //Canceled: text-danger #6c757d
    //Deleted/Removed: text-danger #dc3545
    #endregion
    public enum Statuses
    {
        Created, InProgress, Done, Canceled
    }

    public static class StatusHelper
    {
        public static string GetPartOfClassByStatusName(Statuses status)
        {
            switch (status)
            {
                case Statuses.Created:
                    return "info";
                case Statuses.InProgress:
                    return "success";
                case Statuses.Done:
                    return "secondary";
                case Statuses.Canceled:
                    return "danger";
                default:
                    return "";
            }
        }
    }
}
