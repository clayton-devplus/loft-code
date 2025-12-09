using Loft.Code.Application.DTO;

namespace Loft.Code.Application.Services;

public class CalculadoraAluguelService : ICalculadoraAluguelService
{
    public List<CalculaPercentualImovelResponseDto> CalcularAluguel(List<CalculaPercentualImovelRequestDto> calculaPercentualImovelRequestDto)
    {
        var listResponse = new List<CalculaPercentualImovelResponseDto>();

        foreach (var item in calculaPercentualImovelRequestDto)
        {
            var itemResponse = new CalculaPercentualImovelResponseDto();
            itemResponse.Id = item.Id;

            if (item.TipoContrato == "reajustado")
            {
                itemResponse.ValorFinal = item.ValorBase + (item.ValorBase * item.PercentualReajuste.Value);
            }
            else if (item.TipoContrato == "fixo")
            {
                itemResponse.ValorFinal = item.ValorBase;
            }

            if (item.TipoImovel == "apartamento")
                itemResponse.ValorFinal += 250; // taxa fixa de apartamento

            listResponse.Add(itemResponse);

        }
        return listResponse;
    }
}