using Newtonsoft.Json;

namespace Vueling.Domain.Entities
{
    public class Transaction
    {
        /// <summary>
        /// SKU of the transaction (identifier).
        /// </summary>
        [JsonProperty("sku")]
        public string SKU { get; set; }

        /// <summary>
        /// Transaction currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Transaction amount of money.
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
