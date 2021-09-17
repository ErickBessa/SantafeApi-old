using System.Collections.Generic;

namespace SantafeApi.Services
{
    public class QualificationModel
    {
        public int CodItem { get; set; }
        public string ItemName { get; set; }
        public int Conforme { get; set; }
        public int NaoConforme { get; set; }
    }

    public class ComparadorQualificacao : IEqualityComparer<QualificationModel>
    {
        public bool Equals(QualificationModel x, QualificationModel y)
        {
            return x.CodItem == y.CodItem;
        }

        public int GetHashCode(QualificationModel obj)
        {
            return obj.CodItem;
        }
    }
}