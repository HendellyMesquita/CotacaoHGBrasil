using Newtonsoft.Json;

namespace CotacaoHGBrasil
{
    public class Market
    {
        [JsonProperty(PropertyName = "currencies")]
        public Currency Currency { get; set; }

        public Market()
        {
            this.Currency = new Currency();
        }
    }
}
