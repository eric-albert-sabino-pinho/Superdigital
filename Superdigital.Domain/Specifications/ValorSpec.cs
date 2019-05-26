using Superdigital.Domain.Entities;
using Superdigital.Domain.Interface;
using Superdigital.Domain.Menssagens;

namespace Superdigital.Domain.Specifications
{
    public class ValorSpec : ISpecification<TransacaoEntity>
    {
        public bool IsSatisfiedBy(TransacaoEntity entity)
        {
            if (entity == null)
                return false;

            if (entity.ContaDestinoValorTransacao > 0)
                return true;
            else
                return false;
        }

        public string MensagemDeRetorno
        {
            get
            {
                return Dicionario.CriticasChaves.Processo_Valor_Obrigatorio;
            }
        }
    }

}
