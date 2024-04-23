/*
 * Created by    : Jahangir
 * Date created  : 27-03-2024
 * Modified by   : 
 * Last modified : 
 * Reviewed by   : 
 * Date Reviewed : 
 */
using Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Utilities.Constants;

namespace MVC.HttpServices
{
    public class CategorysHttpService
    {
        private readonly HttpClient httpClient;
        public CategorysHttpService(HttpClient httpClient)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(GeneralConstants.JsonFileName).Build();

            var baseUri = config.GetValue<string>(GeneralConstants.ShopBaseUri);
            var publicKey = config.GetValue<string>(GeneralConstants.PublicKey);

            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri(baseUri);
            this.httpClient.DefaultRequestHeaders.Clear();

            this.httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {publicKey}");
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #region Create
        public async Task<ResponseOutcome<Category>> CreateCategory(Category category)
        {
            ResponseOutcome<Category> outcome = new ResponseOutcome<Category>();

            try
            {
                using (HttpResponseMessage response = await httpClient.PostAsJsonAsync(RouteConstants.CreateCategory, category))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = await response.Content.ReadAsAsync<Category>();
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
        //ReadCategorysByFacilities(int id).Replace("{key}",id)
        public async Task<ResponseOutcome<Category>> ReadCategorys()
        {
            ResponseOutcome<Category> outcome = new ResponseOutcome<Category>();

            try
            {
                using (HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadCategorys.ToString()))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.EntityList = JsonConvert.DeserializeObject<List<Category>>(result) ?? new List<Category>();
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
        public async Task<ResponseOutcome<Category>> ReadCategoryById(int id)
        {
            ResponseOutcome<Category> outcome = new ResponseOutcome<Category>();
                
            try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(RouteConstants.ReadCategoryByKey.Replace("{key}", id.ToString()));
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = JsonConvert.DeserializeObject<Category>(result)?? new ();
                    }
                    else
                    {
                        outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
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
        public async Task<ResponseOutcome<Category>> UpdateCategory(Category category)
        {
            ResponseOutcome<Category> outcome = new ResponseOutcome<Category>();

            try
            {
                using (HttpResponseMessage response = await httpClient.PutAsJsonAsync(RouteConstants.UpdateCategory.Replace("{key}", category.CategoryId.ToString()), category))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        outcome.ResponseStatus = ResponseStatus.Success;
                        outcome.Entity = category;
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
