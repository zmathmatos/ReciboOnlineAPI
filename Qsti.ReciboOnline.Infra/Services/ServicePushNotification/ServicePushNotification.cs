using Newtonsoft.Json;
using Qsti.ReciboOnline.Domain.Interfaces.Services;
using RestSharp;
using System;
using System.Net.Http;
using System.Text;

namespace Qsti.ReciboOnline.Infra.Services.ServicePushNotification
{
    public class ServicePushNotification : IServicePushNotification
    {
        public ServicePushNotification()
        {

        }

        public bool Enviar(string token, string title, string body, string click_action)
        {
            try
            {
                var data = new { 
                    to = token,
                    notification = new { title = title, body = body, click_action = click_action } 
                };

                var client = new RestClient("https://fcm.googleapis.com/fcm/send");
                var request = new RestRequest(Method.POST);
                request.AddHeader("postman-token", "86405081-d3a6-aa1d-6a34-176d4ee35757");
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "key=AAAA7SJ5Ulw:APA91bFFi0eF5tZEJEfH5wOCLvLWaaRkW3_36yfGd1_HLPusISdqHUDF5bW_DalLVKJwnVdtDCFNyR4QTXgW9bvDZWOQwZwFI1I2XB1KO8VZ_x0pL7hnsydaW7viOcnE2P9lZfIX3ZY6");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(data), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
