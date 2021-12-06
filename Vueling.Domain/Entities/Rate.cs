using Newtonsoft.Json;

namespace BusinessMan.Domain.Entities
{
    public class Rate
    {
        /// <summary>
        /// Source currency.
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// Destination currency.
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// Rate value.
        /// </summary>
        [JsonProperty("rate")]
        public decimal Value { get; set; }
    }
}
