using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace TransactionService.Contract.Models
{
    public class Error
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Enum Type { get; set; }

        public string? Title { get; set; }

        public string? Message { get; set; }

        public Error(Enum type, string? message = null)
        {
            this.Type = type;
            this.Message = message;
        }
    }
}