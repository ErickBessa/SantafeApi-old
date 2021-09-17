using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SantafeApi.Services.Interfaces
{
    public interface IQualificationService
    {
        IEnumerable<QualificationModel> GetQualificationReport(int codCliente, DateTime start, DateTime end);
    }
}