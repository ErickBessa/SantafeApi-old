using System;
using System.Threading.Tasks;
using System.Linq;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Services.Interfaces;
using System.Globalization;
using System.Collections.Generic;
using SantafeApi.Utils.Consts;

namespace SantafeApi.Services
{
    public class QualificationService : IQualificationService
    {
        private readonly SantafeApiContext _dbContext;
        public QualificationService(SantafeApiContext dbContext)

        {
            _dbContext = dbContext;
        }
        public IEnumerable<QualificationModel> GetQualificationReport(int codCliente, DateTime start, DateTime end)
        {
            var codControleOs = GetAllCodControleOs(codCliente, start, end);
            Console.WriteLine($"codControleOs: {codControleOs.Length}");
            var vistorias = _dbContext.Vistorias.Where(vistoria => codControleOs.Contains(vistoria.CodControle)).ToList();

            var listQualificationModel = vistorias.Select(vistoria =>
            {
                var itensConformes = vistorias.Where(vistoriaConforme => vistoriaConforme.Conformidade == Conformidade.CONFORME && vistoriaConforme.CodItem == vistoria.CodItem).Count();
                var itensNaoConformes = vistorias.Where(vistoriaConforme => vistoriaConforme.Conformidade == Conformidade.NAO_CONFORME && vistoriaConforme.CodItem == vistoria.CodItem).Count();
                return new QualificationModel
                {
                    CodItem = vistoria.CodItem,
                    ItemName = vistoria.NomeItem,
                    Conforme = itensConformes,
                    NaoConforme = itensNaoConformes
                };
            }).Distinct(new ComparadorQualificacao()).ToList();


            return listQualificationModel;
        }

        private int[] GetAllCodControleOs(int codCliente, DateTime minDate, DateTime maxdate)
        {
            var clientOs = _dbContext.ControleOs.Where(x => x.CodCliente == codCliente).ToList();
            clientOs.ForEach(c => Console.WriteLine(c.DataVistoria));
            var allCodOs = clientOs
            .Where(cos => Convert.ToDateTime(cos.DataVistoria) >= minDate && Convert.ToDateTime(cos.DataVistoria) <= maxdate)
            .Select(controle => controle.Cod).ToArray();

            return allCodOs;
        }

    }
}