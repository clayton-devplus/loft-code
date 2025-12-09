using System.Text.Json.Serialization;

namespace Loft.Code.Application.DTO
{
    public class CalculaPercentualImovelResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("valor_final")]
        public decimal ValorFinal { get; set; }
    }
}