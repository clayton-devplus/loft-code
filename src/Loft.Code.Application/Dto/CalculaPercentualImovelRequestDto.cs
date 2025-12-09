using System.Text.Json.Serialization;

namespace Loft.Code.Application.DTO
{
    public class CalculaPercentualImovelRequestDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tipo_imovel")]
        public string TipoImovel { get; set; }

        [JsonPropertyName("tipo_contrato")]
        public string TipoContrato { get; set; }

        [JsonPropertyName("valor_base")]
        public decimal ValorBase { get; set; }

        [JsonPropertyName("percentual_reajuste")]
        public decimal? PercentualReajuste { get; set; }
    }
}