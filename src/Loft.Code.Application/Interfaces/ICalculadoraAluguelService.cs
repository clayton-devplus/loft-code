using Loft.Code.Application.DTO;

public interface ICalculadoraAluguelService
{
    List<CalculaPercentualImovelResponseDto> CalcularAluguel(List<CalculaPercentualImovelRequestDto> calculaPercentualImovelRequestDto);
}