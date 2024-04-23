using Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Utilities.Constants;

/*
 * Created by   : Jahangir
 * Date created : 28.03.2024
 * Modified by  : 
 * Last modified: 
 * Reviewed by  :
 * Date reviewed:
 */
namespace MVC.HttpServices
{
    public class ProductsHttpService
    {
        private readonly HttpClient httpClient;
        public ProductsHttpService(HttpClient httpClient)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(GeneralConstants.JsonFileName).Build();

            var baseUri = config.GetValue<string>(GeneralConstants.ShopBaseUri);
            var publickey = config.GetValue<string>(GeneralConstants.PublicKey);

            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(baseUri);
            this.httpClient.DefaultRequestHeaders.Clear();

            this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {publickey}");
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #region Create
        public async Task<ResponseOutcome<Product>> CreateProduct(Product product)
        {
            ResponseOutcome<Product> outcome = new ResponseOutcome<Product>();

            try
            {
                using (HttpResponseMessage response = await httpClient.PostAsJsonAsync(RouteConstants.CreateProduct, product))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = await response.Content.ReadAsAsync<Product>();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }
            return outcome;
        }
        #endregion

        #region Read
        public async Task<ResponseOutcome<Product>> ReadProducts()
        {
            ResponseOutcome<Product> outcome = new ResponseOutcome<Product>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadProducts.ToString()))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.EntityList = JsonConvert.DeserializeObject<List<Product>>(result) ?? new List<Product>();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        public async Task<ResponseOutcome<Product>> ReadProductById(int id)
        {
            ResponseOutcome<Product> outcome = new ResponseOutcome<Product>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadProductByKey.Replace("{key}", id.ToString())))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = JsonConvert.DeserializeObject<Product>(result) ?? new();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        public async Task<ResponseOutcome<Product>> ReadProductByCategory(int categoryId)
        {
            ResponseOutcome<Product> outcome = new ResponseOutcome<Product>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadProductsByCategoryId.Replace("{categoryId}", categoryId.ToString())))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.EntityList = JsonConvert.DeserializeObject<List<Product>>(result) ?? new List<Product>();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        #endregion
        #region Update
        public async Task<ResponseOutcome<Product>> UpdateProduct(Product product)
        {
            ResponseOutcome<Product> outcome = new ResponseOutcome<Product>();

            try
            {
                using (HttpResponseMessage response = await httpClient.PutAsJsonAsync(RouteConstants.UpdateProduct.Replace("{key}", product.ProductId.ToString()), product))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = product;
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                outcome.Message = ex.Message;
            }

            return outcome;
        }
        #endregion
    }
}
