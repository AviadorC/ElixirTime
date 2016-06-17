using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElixirService
{
    public class ElixirService
    {
        public ElixirService()
        {
        }

        public async Task<IEnumerable<BankModel>> GetBanks()
        {
            string uri = "http://elixirtime.azurewebsites.net/api/GetBanks";
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await http.GetAsync(uri);

                string contentString = await response.Content.ReadAsStringAsync();


                var bankList = JsonConvert.DeserializeObject<IEnumerable<BankModel>>(contentString);

                return bankList;
            }
        }

        public async Task<TransferResultModel> GetElixirTime(TransferModel model)
        {
            string uri = "http://elixirtime.azurewebsites.net/api/GetElixirTime";
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("Accept", "application/json");

                var serializedObject = JsonConvert.SerializeObject(model);
                var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await http.PostAsync(uri, content);

                string contentString = await response.Content.ReadAsStringAsync();


                var bankList = JsonConvert.DeserializeObject<TransferResultModel>(contentString);

                return bankList;

            }
        }
    }
}
