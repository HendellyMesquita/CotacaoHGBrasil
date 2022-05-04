using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Windows.Forms;

namespace CotacaoHGBrasil
{
    public partial class FrmCotacaoHGBrasil : Form
    {
        public FrmCotacaoHGBrasil()
        {
            InitializeComponent();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string strURL = "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=15247546";

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(strURL).Result;

                try
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        LblBuy.Text = "--,--R$";
                        LblSell.Text = "--,--R$";
                        LblVar.Text = "--,--%";

                        MessageBox.Show("Erro de execução. Tente novamente mais tarde: ");


                        return;
                    }
                    var result = response.Content.ReadAsStringAsync().Result;
                    Market market = JsonConvert.DeserializeObject<Market>(result);

                    LblBuy.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Buy);
                    LblSell.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Sell);
                    LblVar.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:P}", market.Currency.Variation / 100);
                }
                catch (Exception ex)
                {
                    LblBuy.Text = "--,--R$";
                    LblSell.Text = "--,--R$";
                    LblVar.Text = "--,--%";

                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
